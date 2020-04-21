using Castle.Core.Interceptor;
using Castle.DynamicProxy;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.AutofacExt;
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

        private bool IsDynamicImplemented(object value)
        {
            if (!(value is IProxyTargetAccessor)) return false;
            IProxyTargetAccessor proxyTargetAccessor = value as IProxyTargetAccessor;
            IInterceptor[] interceptors = proxyTargetAccessor.GetInterceptors();
            if (interceptors == null) return false;
            IEnumerable<ImplementorAttributeExecuteInterceptor> myInterceptor
                 = interceptors.OfType<ImplementorAttributeExecuteInterceptor>();
            if (!myInterceptor.Any()) return false;
            return true;
        }

        private void ComponentContainer_ComponentCreating(object sender, AutofacExt.ComponentCreatingEventArgs e)
        {
            TypeHasProxyInfo info = GetTypeHadProxyInfo(e);
            if (!info.HasProxy) return;

            // 对特征的属性进行注入
            ClassProxyOnTypeInfo proxyOnTypeInfo = new ClassProxyOnTypeInfo(info.Type);
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

        /// <summary>
        /// 获取一个类型是否有切面特征
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private TypeHasProxyInfo GetTypeHadProxyInfo(ComponentCreatingEventArgs e)
        {
            TypeHasProxyInfo info = cacheForTypeHasProxy.GetOrAdd(e.CreatedObject.GetType(), type =>
            {
                /**
                 * 动态实现的接口，是无法将 AOP 特征挂载在 class 上的。
                 * 相反，手动实现的接口，是不会将 AOP 特征挂载在 interface 上的。
                 * 因此，在此处判断一个对象是否需要进行 AOP 操作时，就要分清上面两种情况，才能做出正确的反射。
                 * 
                 * 当创建的对象是动态实现的时候，将对 Interface 判断是否存在 AOP 特征；
                 * 当创建的对象不是动态实现的时候，将对该对象本身判断是否存在 AOP 特征。
                 */
                if (IsDynamicImplemented(e.CreatedObject))
                    type = e.RequiredType;

                bool hasProxy = type.GetCustomAttributes<ProxyAttribute>().Any();
                if (!hasProxy) hasProxy = type.GetMethods().Where(m => m.GetCustomAttributes<ProxyAttribute>().Any()).Any();
                return new TypeHasProxyInfo(type, hasProxy);
            });
            return info;
        }

        public void OnAppStarted(App app)
        {
        }
    }
}
