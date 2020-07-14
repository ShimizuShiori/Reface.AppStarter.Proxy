using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 类型上的代理信息
    /// </summary>
    [DebuggerDisplay("Type = {Type}")]
    public class ProxyOnTypeInfo
    {
        private readonly ICollection<MethodInfo> allMethods;


        public Type Type { get; private set; }
        public ICollection<Type> ProxyTypesOnClass { get; set; }
        public IDictionary<string, ICollection<Type>> ProxyTypsOnMethodMap { get; set; }

        public IEnumerable<MethodInfo> AllMethods { get { return allMethods; } }
        private ProxyOnTypeInfo(Type type)
        {
            Type = type;
            ProxyTypesOnClass = new List<Type>();
            ProxyTypsOnMethodMap = new Dictionary<string, ICollection<Type>>();
            this.allMethods = new List<MethodInfo>();
        }

        public IEnumerable<Type> GetProxiesOnMethod(MethodInfo method)
        {
            string key = method.ToString();
            ICollection<Type> result;
            if (this.ProxyTypsOnMethodMap.TryGetValue(key, out result))
                return result;

            return Enumerable.Empty<Type>();
        }

        public void AddProxyOnClass(Type proxyType)
        {
            this.ProxyTypesOnClass.Add(proxyType);
        }

        public void AddProxyOnMethod(Type proxyType, MethodInfo methodInfo)
        {
            string key = methodInfo.ToString();
            ICollection<Type> proxies;
            if (!this.ProxyTypsOnMethodMap.TryGetValue(key, out proxies))
            {
                proxies = new List<Type>();
                this.ProxyTypsOnMethodMap[key] = proxies;
            }
            proxies.Add(proxyType);
        }


        /// <summary>
        /// 基于普通类型创建
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ProxyOnTypeInfo CreateByNormalType(Type type)
        {
            var result = new ProxyOnTypeInfo(type);
            type.GetProxies().ForEach(proxy => result.AddProxyOnClass(proxy.GetType()));
            var proxiesOnMethod = new Dictionary<string, IEnumerable<IProxy>>();
            type.GetMethods()
                .ForEach(method =>
                {
                    result.allMethods.Add(method);
                    method.GetProxies().ForEach(proxy =>
                    {
                        result.AddProxyOnMethod(proxy.GetType(), method);
                    });

                });
            return result;
        }

        /// <summary>
        /// 基于接口类型创建
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public static ProxyOnTypeInfo CreateByInterface(Type interfaceType)
        {
            ProxyOnTypeInfo result = new ProxyOnTypeInfo(interfaceType);
            Type[] baseInterfaces = interfaceType.GetInterfaces();
            List<Type> allTypes = new List<Type>(baseInterfaces);
            allTypes.Add(interfaceType);

            Dictionary<string, IEnumerable<IProxy>> proxiesOnMethods = new Dictionary<string, IEnumerable<IProxy>>();

            allTypes.ForEach(type =>
            {
                type.GetProxies().ForEach(proxy => result.AddProxyOnClass(proxy.GetType()));
                type.GetMethods().ForEach(method =>
                {
                    result.allMethods.Add(method);
                    method.GetProxies().ForEach(proxy => 
                    {
                        result.AddProxyOnMethod(proxy.GetType(), method);
                    });
                });
            });
            return result;
        }
    }
}
