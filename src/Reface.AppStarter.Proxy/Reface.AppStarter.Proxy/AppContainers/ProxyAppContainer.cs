using Castle.DynamicProxy;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.AppContainers
{
    public class ProxyAppContainer : IProxyAppContainer
    {
        private readonly ConcurrentDictionary<Type, bool> cacheForTypeHasProxy = new ConcurrentDictionary<Type, bool>();
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
            bool hasProxy = cacheForTypeHasProxy.GetOrAdd(e.CreatedObject.GetType(), type =>
            {
                bool result = type.GetCustomAttributes<ProxyAttribute>().Any();
                if (!result) result = type.GetMethods().Where(m => m.GetCustomAttributes<ProxyAttribute>().Any()).Any();
                return result;
            });
            if (!hasProxy) return;

            ClassProxyOnTypeInfo proxyOnTypeInfo = new ClassProxyOnTypeInfo(e.CreatedObject.GetType());
            foreach (var attr in proxyOnTypeInfo.ProxiesOnClass)
                e.ComponentManager.InjectPropeties(attr);
            foreach (var attr in proxyOnTypeInfo.ProxiesOnMethod.SelectMany(x => x.Value))
                e.ComponentManager.InjectPropeties(attr);

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
