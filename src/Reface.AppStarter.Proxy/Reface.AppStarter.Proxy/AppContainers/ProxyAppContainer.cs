using Castle.DynamicProxy;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Predicates;
using Reface.AppStarter.Proxy;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.AppContainers
{
    /// <summary>
    /// 代理容器
    /// </summary>
    public class ProxyAppContainer : IProxyAppContainer
    {
        private readonly ConcurrentDictionary<Type, ProxyInfo> cacheForTypeHasProxy = new ConcurrentDictionary<Type, ProxyInfo>();
        private readonly ProxyGenerator proxyGenerator = new ProxyGenerator();
        private readonly IEnumerable<CustomProxyRuntimeInfo> customProxyInfos;

        public ProxyAppContainer(ProxyAppContainerOptions options)
        {
            this.customProxyInfos = options.CustomProxyInfos
                .Select(info => new CustomProxyRuntimeInfo()
                {
                    AttachmentCondition = Activator.CreateInstance(info.AttachmentConditionType) as ICustomProxyAttachmentCondition,
                    ProxyType = info.ProxyType
                });
            Debug.WriteLine("CustomProxyInfo Count = {0}", this.customProxyInfos.Count());
        }

        public void Dispose()
        {
        }

        public void OnAppPrepair(App app)
        {
            IComponentContainer componentContainer = app.GetAppContainer<IComponentContainer>();
            componentContainer.ComponentCreating += ComponentContainer_ComponentCreating;
        }

        private void ComponentContainer_ComponentCreating(object sender, ComponentCreatingEventArgs e)
        {
            ProxyInfo info = GetProxyInfo(e);
            Debug.WriteLine("ProxyInfo = {0},{1}", info.Type, info.HasProxy);
            if (!info.HasProxy) return;

            // 对特征的属性进行注入
            ProxyOnTypeInfo proxyOnTypeInfo;

            if (info.IsDynamicImplemented)
                proxyOnTypeInfo = ProxyOnTypeInfo.CreateByInterface(info.Type);
            else
                proxyOnTypeInfo = ProxyOnTypeInfo.CreateByNormalType(info.Type);

            // 从所有的自定义代理中查找针对当前类型的代理
            var customProxies = this.customProxyInfos.Where(x => x.AttachmentCondition.CanAttach(info.Type))
                            .Select(x => Activator.CreateInstance(x.ProxyType))
                            .OfType<IProxy>();
            Debug.WriteLine("Attached Proxy Count = {0}", customProxies.Count());

            // 若存在自定义代理，则将已有的类型代理自定义代理合并
            if (customProxies.Any())
                proxyOnTypeInfo.ProxiesOnClass = proxyOnTypeInfo.ProxiesOnClass.Concat(customProxies);


            foreach (var attr in proxyOnTypeInfo.ProxiesOnClass)
                e.ComponentManager.InjectProperties(attr);
            foreach (var attr in proxyOnTypeInfo.ProxiesOnMethod.SelectMany(x => x.Value))
                e.ComponentManager.InjectProperties(attr);

            ProxyAttributeExecuteInterceptor proxyAttributeExecuteInterceptor = new ProxyAttributeExecuteInterceptor(proxyOnTypeInfo);
            var newInstance = proxyGenerator.CreateInterfaceProxyWithTarget(
                e.RequiredType,
                e.CreatedObject,
                proxyAttributeExecuteInterceptor
            );
            e.Replace(newInstance);
        }

        /// <summary>
        /// 获取一个类型是否有切面特征
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private ProxyInfo GetProxyInfo(ComponentCreatingEventArgs e)
        {
            ProxyInfo info = cacheForTypeHasProxy.GetOrAdd(e.CreatedObject.GetType(), type =>
            {

                // 动态实现的接口，是无法将 AOP 特征挂载在 class 上的。

                // 相反，手动实现的接口，是不会将 AOP 特征挂载在 interface 上的。

                // 因此，在此处判断一个对象是否需要进行 AOP 操作时，就要分清上两种情况，才能做出正确的反射。

                // 当创建的对象是动态实现的时候，将对 Interface 判断是否存在 AOP 特征；

                // 当创建的对象不是动态实现的时候，将对该对象本身判断是否存在 AOP 特征。

                // 当上面的判断均认定没有代理时，还需要从 customProxyInfos 判断是否有添加的自定义代理

                bool isDynamicImplemented = e.CreatedObject.IsDynamicImplemented();

                if (isDynamicImplemented)
                    type = e.RequiredType;

                bool hasProxy = GetPredicateOfHasProxy(type).IsTrue();

                return new ProxyInfo(type, hasProxy, isDynamicImplemented);
            });
            return info;
        }

        public void OnAppStarted(App app)
        {
        }

        private Predicate GetPredicateOfProxyOnType(Type type)
        {
            return Predicate.Create(() => type.GetCustomAttributes<ProxyAttribute>().Any());
        }

        private Predicate GetPredicateOfProxyInMethods(Type type)
        {
            return Predicate.Create(() => type.GetMethods().Where(m => m.GetCustomAttributes<ProxyAttribute>().Any()).Any());
        }

        private Predicate GetPredicateOfFilteredInCustomProxies(Type type)
        {
            return Predicate.Create(() =>
            {
                foreach (var item in this.customProxyInfos)
                {
                    if (!item.AttachmentCondition.CanAttach(type))
                        continue;
                    return true;
                }
                return false;
            });
        }

        private Predicate GetPredicateOfHasProxy(Type type)
        {
            Predicate proxyOnType = GetPredicateOfProxyOnType(type);
            Predicate proxyOnMethods = GetPredicateOfProxyInMethods(type);
            Predicate filteredInCustomProxy = GetPredicateOfFilteredInCustomProxies(type);

            return proxyOnType.Or(proxyOnMethods).Or(filteredInCustomProxy);
        }
    }
}
