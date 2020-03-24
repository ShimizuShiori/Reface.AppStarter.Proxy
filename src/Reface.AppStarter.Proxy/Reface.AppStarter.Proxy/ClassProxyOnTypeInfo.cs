using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 类型上的代理信息
    /// </summary>
    public class ClassProxyOnTypeInfo
    {
        public Type Type { get; private set; }
        public IEnumerable<ProxyAttribute> ProxiesOnClass { get; private set; }
        public Dictionary<MethodInfo, IEnumerable<ProxyAttribute>> ProxiesOnMethod { get; private set; }

        public ClassProxyOnTypeInfo(Type type)
        {
            Type = type;
            this.ProxiesOnClass = type.GetCustomAttributes<ProxyAttribute>();
            this.ProxiesOnMethod = new Dictionary<MethodInfo, IEnumerable<ProxyAttribute>>();
            type.GetMethods()
                .Select(x => new
                {
                    Method = x,
                    Attributes = x.GetCustomAttributes<ProxyAttribute>()
                })
                .ForEach(x => ProxiesOnMethod[x.Method] = x.Attributes);
        }
    }
}
