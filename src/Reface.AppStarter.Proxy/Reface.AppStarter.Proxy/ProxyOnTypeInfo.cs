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
    class ProxyOnTypeInfo
    {
        public Type Type { get; private set; }
        public IEnumerable<ProxyAttribute> ProxiesOnClass { get; private set; }
        public Dictionary<string, IEnumerable<ProxyAttribute>> ProxiesOnMethod { get; private set; }

        private ProxyOnTypeInfo(Type type, IEnumerable<ProxyAttribute> proxiesOnClass, Dictionary<string, IEnumerable<ProxyAttribute>> proxiesOnMethod)
        {
            Type = type;
            ProxiesOnClass = proxiesOnClass;
            ProxiesOnMethod = proxiesOnMethod;
        }

        /// <summary>
        /// 基于普通类型创建
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ProxyOnTypeInfo CreateByNormalType(Type type)
        {
            var proxiesOnClass = type.GetCustomAttributes<ProxyAttribute>();
            var proxiesOnMethod = new Dictionary<string, IEnumerable<ProxyAttribute>>();
            type.GetMethods()
                .Select(x => new
                {
                    Method = x,
                    Attributes = x.GetCustomAttributes<ProxyAttribute>()
                })
                .ForEach(x => proxiesOnMethod[x.Method.ToString()] = x.Attributes);
            return new ProxyOnTypeInfo(type, proxiesOnClass, proxiesOnMethod);
        }

        /// <summary>
        /// 基于接口类型创建
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public static ProxyOnTypeInfo CreateByInterface(Type interfaceType)
        {
            Type[] baseInterfaces = interfaceType.GetInterfaces();
            List<Type> allTypes = new List<Type>(baseInterfaces);
            allTypes.Add(interfaceType);

            List<ProxyAttribute> proxiesOnType = new List<ProxyAttribute>();
            Dictionary<string, IEnumerable<ProxyAttribute>> proxiesOnMethods = new Dictionary<string, IEnumerable<ProxyAttribute>>();

            allTypes.ForEach(type =>
            {
                proxiesOnType.AddRange(type.GetProxies());
                type.GetMethods().ForEach(method =>
                {
                    IEnumerable<ProxyAttribute> proxies = method.GetProxies();
                    if (!proxies.Any())
                        proxies = Enumerable.Empty<ProxyAttribute>();

                    proxiesOnMethods[method.ToString()] = proxies;
                });
            });

            return new ProxyOnTypeInfo(interfaceType, proxiesOnType, proxiesOnMethods);
        }
    }
}
