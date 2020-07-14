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
        private readonly ConcurrentDictionary<Type, ProxyOnTypeInfo> cacheForProxyInfo = new ConcurrentDictionary<Type, ProxyOnTypeInfo>();
        private readonly ProxyGenerator proxyGenerator = new ProxyGenerator();
        private readonly IEnumerable<AttachedRuntimeInfo> attahcedProxyInfo;
        private readonly IEnumerable<AttachedMethodProxyRuntimeInfo> attachedMethodProxyInfo;
        private readonly IEnumerable<AttachedRuntimeInfo> attachedImplementorInfo;

        public ProxyAppContainer(ProxyAppContainerOptions options)
        {
            this.attahcedProxyInfo = this.GetRuntimeInfo(options.AttachedProxyInfo);
            this.attachedImplementorInfo = this.GetRuntimeInfo(options.AttachedImplementorInfo);
            this.attachedMethodProxyInfo = this.GetAttahcedMethodRuntimeInfo(options.AttachedProxyInfo);
        }

        private IEnumerable<AttachedMethodProxyRuntimeInfo> GetAttahcedMethodRuntimeInfo(IEnumerable<ProxyAttachedInfo> infos)
        {
            var list = new List<AttachedMethodProxyRuntimeInfo>();
            infos.ForEach(info =>
            {
                if (!info.MethodAttachments.Any())
                    return;

                info.MethodAttachments.ForEach(attachment =>
                {
                    list.Add(new AttachedMethodProxyRuntimeInfo()
                    {
                        AttachedType = info.AttachedType,
                        MethodAttachment = attachment
                    });
                });
            });
            return list;
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

            var proxyOnTypeInfo = GetProxyInfo(e);

            if (proxyOnTypeInfo.ProxyTypesOnClass.Count == 0 && proxyOnTypeInfo.ProxyTypsOnMethodMap.Count == 0)
                return;

            var proxyOnTypeRuntimeInfo = new ProxyOnTypeRuntimeInfo(e.ComponentManager, proxyOnTypeInfo);
            ProxyAttributeExecuteInterceptor proxyAttributeExecuteInterceptor = new ProxyAttributeExecuteInterceptor(proxyOnTypeRuntimeInfo);
            var newInstance = proxyGenerator.CreateInterfaceProxyWithTarget(
                e.RequiredType,
                e.CreatedObject,
                proxyAttributeExecuteInterceptor
            );
            Debug.WriteLine("replace \n\t{0} \n\tto {1} \n\tas {2} ", e.CreatedObject.GetType(), newInstance, e.RequiredType);
            e.Replace(newInstance);
        }

        private ProxyOnTypeInfo GetProxyInfo(ComponentCreatingEventArgs e)
        {
            Type type = e.CreatedObject.GetType();
            bool isDynamicImplemented = e.CreatedObject.IsDynamicImplemented();
            if (isDynamicImplemented)
                type = e.RequiredType;

            Debug.WriteLine("GetProxyInfo({0})", type);
            return cacheForProxyInfo.GetOrAdd(type, key => CreateProxyInfo(type, isDynamicImplemented));

        }

        private ProxyOnTypeInfo CreateProxyInfo(Type type, bool isDynamicImplemented)
        {
            Debug.WriteLine("CreateProxyInfo({0},{1})", type, isDynamicImplemented);
            // 对特征的属性进行注入
            ProxyOnTypeInfo proxyOnTypeInfo;

            if (isDynamicImplemented)
                proxyOnTypeInfo = ProxyOnTypeInfo.CreateByInterface(type);
            else
                proxyOnTypeInfo = ProxyOnTypeInfo.CreateByNormalType(type);

            // 从所有的自定义代理中查找针对当前类型的代理
            this.attahcedProxyInfo.Where(x => x.Attachment.CanAttach(type))
                .Select(x => x.AttachedType)
                .ForEach(x => proxyOnTypeInfo.AddProxyOnClass(x));

            proxyOnTypeInfo.AllMethods
                .ForEach(method =>
                {
                    this.attachedMethodProxyInfo.ForEach(attachment =>
                    {
                        if (!attachment.MethodAttachment.CanAttachOnMethod(method))
                            return;

                        proxyOnTypeInfo.AddProxyOnMethod(attachment.AttachedType, method);
                    });
                });

#if DEBUG
            Debug.WriteLine("all proxy found : {1}\n\ton class : {0}", proxyOnTypeInfo.ProxyTypesOnClass.Count, proxyOnTypeInfo.Type);
            proxyOnTypeInfo.AllMethods.ForEach(method =>
            {
                Debug.WriteLine("\ton [{1}] : {0}", proxyOnTypeInfo.GetProxiesOnMethod(method).Count(), method.ToString());
            });
#endif
            return proxyOnTypeInfo;
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
