using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.Proxy
{
    public class ProxyOnTypeRuntimeInfo
    {
        public Type Type { get; private set; }
        public ICollection<IProxy> ProxyOnClass { get; private set; }
        public IDictionary<string, ICollection<IProxy>> ProxyOnMethodMap { get; private set; }

        public ProxyOnTypeRuntimeInfo(IComponentManager componentManager, ProxyOnTypeInfo info)
        {
            this.Type = info.Type;
            this.ProxyOnClass = info.ProxyTypesOnClass
                .Select(x => componentManager.CreateComponent(x))
                .OfType<IProxy>()
                .ToList();

            this.ProxyOnMethodMap = new Dictionary<string, ICollection<IProxy>>();
            info.ProxyTypsOnMethodMap.ForEach(item =>
            {
                this.ProxyOnMethodMap[item.Key] = item.Value
                    .Select(x => componentManager.CreateComponent(x))
                    .OfType<IProxy>()
                    .ToList();
            });

        }

        public IEnumerable<IProxy> GetProxiesOnMethod(MethodInfo method)
        {
            string key = method.ToString();
            ICollection<IProxy> proxies;
            if (this.ProxyOnMethodMap.TryGetValue(key, out proxies))
                return proxies;

            return Enumerable.Empty<IProxy>();
        }

    }
}
