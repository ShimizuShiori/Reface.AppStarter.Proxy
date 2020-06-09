using Reface.AppStarter.Attributes;
using System;

namespace Reface.AppStarter.Proxy
{
    public class AppendedProxyInfo
    {
        public Func<Type, bool> TypeFilter { get; set; }
        public ProxyAttribute Proxy { get; set; }
    }
}
