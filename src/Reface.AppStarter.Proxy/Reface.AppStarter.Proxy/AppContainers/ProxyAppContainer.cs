using Castle.DynamicProxy;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Predicates;
using Reface.AppStarter.Proxy;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        private readonly IEnumerable<AttachedRuntimeInfo> attahcedProxyInfo;
        private readonly IEnumerable<AttachedRuntimeInfo> attachedImplementorInfo;

        public ProxyAppContainer(ProxyAppContainerOptions options)
        {
            this.attahcedProxyInfo = this.GetRuntimeInfo(options.AttachedProxyInfo);
            this.attachedImplementorInfo = this.GetRuntimeInfo(options.AttachedImplementorInfo);
        }

        private IEnumerable<AttachedRuntimeInfo> GetRuntimeInfo(IEnumerable<AttachedInfo> attachedInfos)
        {
            var list = new List<AttachedRuntimeInfo>();
            attachedInfos.ForEach(info =>
            {
                if (!info.Attachments.Any()) return;
                info.Attachments.ForEach(att =>
                {
                    list.Add(new AttachedRuntimeInfo()
                    {
                        Attachment = att,
                        AttachedType = info.AttachedType
                    });
                });
            });
            return list;
        }

        public void Dispose()
        {
        }

        public void OnAppPrepair(App app)
        {
            IComponentContainer componentContainer = app.GetAppContainer<IComponentContainer>();
            componentContainer.ComponentCreating += ComponentContainer_ComponentCreating;
        }

        private void ComponentContainer_NoComponentRegisted(object sender, NoComponentRegistedEventArgs e)
        {
            if (!e.ServiceType.IsInterface) return;
            IEnumerable<AttachedRuntimeInfo> matchedInfos = this.attachedImplementorInfo
                .Where(info => info.Attachment.CanAttach(e.ServiceType));
            int count = matchedInfos.Count();
            if (count == 0) return;
            if (count > 1)
                throw new ApplicationException();

            var implementorType = matchedInfos.First().AttachedType;

            e.ComponentProvider = cm =>
            {
                var implementor = (IImplementor)cm.CreateComponent(implementorType);
                ImplementorAttributeExecuteInterceptor interceptor = new ImplementorAttributeExecuteInterceptor(e.ServiceType, implementor);
                var rlt = proxyGenerator.CreateInterfaceProxyWithoutTarget(
                    e.ServiceType,
                    interceptor
                );
                return rlt;
            };
        }

        private void ComponentContainer_ComponentCreating(object sender, ComponentCreatingEventArgs e)
        {
            if (!e.RequiredType.IsInterface)
                return;

            ProxyInfo info = GetProxyInfo(e);
            if (!info.HasProxy) return;

            // 对特征的属性进行注入
            ProxyOnTypeInfo proxyOnTypeInfo;

            if (info.IsDynamicImplemented)
                proxyOnTypeInfo = ProxyOnTypeInfo.CreateByInterface(info.Type);
            else
                proxyOnTypeInfo = ProxyOnTypeInfo.CreateByNormalType(info.Type);

            // 从所有的自定义代理中查找针对当前类型的代理
            var customProxies = this.attahcedProxyInfo.Where(x => x.Attachment.CanAttach(info.Type))
                .Select(x => e.ComponentManager.CreateComponent(x.AttachedType))
                .OfType<IProxy>()
                .ToList();

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
            IComponentContainer componentContainer = app.GetAppContainer<IComponentContainer>();
            componentContainer.NoComponentRegisted += ComponentContainer_NoComponentRegisted;
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
                foreach (var item in this.attahcedProxyInfo)
                {
                    if (!item.Attachment.CanAttach(type))
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
