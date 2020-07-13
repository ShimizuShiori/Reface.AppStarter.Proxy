using Reface.AppStarter.Attributes;
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
        public ICollection<IProxy> ProxiesOnClass { get; set; }
        public IDictionary<string, ICollection<IProxy>> ProxiesOnMethod { get; set; }

        public IEnumerable<MethodInfo> AllMethods { get { return allMethods; } }
        private ProxyOnTypeInfo(Type type)
        {
            Type = type;
            ProxiesOnClass = new List<IProxy>();
            ProxiesOnMethod = new Dictionary<string, ICollection<IProxy>>();
            this.allMethods = new List<MethodInfo>();
        }

        public IEnumerable<IProxy> GetProxiesOnMethod(MethodInfo method)
        {
            string key = method.ToString();
            ICollection<IProxy> result;
            if (this.ProxiesOnMethod.TryGetValue(key, out result))
                return result;

            return Enumerable.Empty<IProxy>();
        }

        public void AddProxyOnClass(IProxy proxy)
        {
            this.ProxiesOnClass.Add(proxy);
        }

        public void AddProxyOnMethod(IProxy proxy, MethodInfo methodInfo)
        {
            string key = methodInfo.ToString();
            ICollection<IProxy> proxies;
            if (!this.ProxiesOnMethod.TryGetValue(key, out proxies))
            {
                proxies = new List<IProxy>();
                this.ProxiesOnMethod[key] = proxies;
            }
            proxies.Add(proxy);
        }


        /// <summary>
        /// 基于普通类型创建
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ProxyOnTypeInfo CreateByNormalType(Type type)
        {
            var result = new ProxyOnTypeInfo(type);
            type.GetCustomAttributes<ProxyAttribute>().ForEach(proxy => result.AddProxyOnClass(proxy));
            var proxiesOnMethod = new Dictionary<string, IEnumerable<IProxy>>();
            type.GetMethods()
                .ForEach(method =>
                {
                    result.allMethods.Add(method);
                    method.GetProxies().ForEach(proxy =>
                    {
                        result.AddProxyOnMethod(proxy, method);
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
                type.GetProxies().ForEach(proxy => result.AddProxyOnClass(proxy));
                type.GetMethods().ForEach(method =>
                {
                    result.allMethods.Add(method);
                    method.GetProxies().ForEach(proxy => 
                    {
                        result.AddProxyOnMethod(proxy, method);
                    });
                });
            });
            return result;
        }
    }
}
