using Castle.Core.Interceptor;
using Castle.DynamicProxy;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.AppContainers
{
    public class ProxyAppContainer : IProxyAppContainer
    {
        public class TypeHasProxyInfo
        {
            public Type Type { get; private set; }
            public Boolean HasProxy { get; private set; }

            public TypeHasProxyInfo(Type type, bool hasProxy)
            {
                Type = type;
                HasProxy = hasProxy;
            }
        }

        private readonly ConcurrentDictionary<Type, TypeHasProxyInfo> cacheForTypeHasProxy = new ConcurrentDictionary<Type, TypeHasProxyInfo>();
        private readonly ProxyGenerator proxyGenerator = new ProxyGenerator();
        private App thisApp;

        public void Dispose()
        {
        }

        public void OnAppPrepair(App app)
        {
            this.thisApp = app;
            IComponentContainer componentContainer = app.GetAppContainer<IComponentContainer>();
            componentContainer.ComponentCreating += ComponentContainer_ComponentCreating;
        }

        private void ComponentContainer_ComponentCreating(object sender, AutofacExt.ComponentCreatingEventArgs e)
        {
            Debug.WriteLine($"ComponentContainer_ComponentCreating {e.CreatedObject.GetType().FullName} ");
            TypeHasProxyInfo info = cacheForTypeHasProxy.GetOrAdd(e.CreatedObject.GetType(), type =>
            {
                if (e.CreatedObject is IProxyTargetAccessor)
                {
                    IProxyTargetAccessor accessor = (IProxyTargetAccessor)e.CreatedObject;
                    type = ((ImplementorAttributeExecuteInterceptor)accessor.GetInterceptors()[0]).InterfaceType;
                }
                bool hasProxy = type.GetCustomAttributes<ProxyAttribute>().Any();
                if (!hasProxy) hasProxy = type.GetMethods().Where(m => m.GetCustomAttributes<ProxyAttribute>().Any()).Any();
                return new TypeHasProxyInfo(type, hasProxy);
            });
            if (!info.HasProxy) return;

            ClassProxyOnTypeInfo proxyOnTypeInfo = new ClassProxyOnTypeInfo(info.Type);
            foreach (var attr in proxyOnTypeInfo.ProxiesOnClass)
                e.ComponentManager.InjectPropeties(attr);
            foreach (var attr in proxyOnTypeInfo.ProxiesOnMethod.SelectMany(x => x.Value))
                e.ComponentManager.InjectPropeties(attr);

            Debug.WriteLine($"开始创建 {e.CreatedObject.GetType().FullName} 的代理类");

            ProxyAttributeExecuteInterceptor proxyAttributeExecuteInterceptor = new ProxyAttributeExecuteInterceptor(proxyOnTypeInfo);
            var newInstance = proxyGenerator.CreateInterfaceProxyWithTarget(
                e.RequiredType,
                e.CreatedObject,
                proxyAttributeExecuteInterceptor
            );
            e.Replace(newInstance);

        }

        public void OnAppStarted(App app)
        {
        }
    }
}
