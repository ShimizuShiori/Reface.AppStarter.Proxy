using Reface.AppStarter.Proxy;
using System.Collections.Generic;

namespace Reface.AppStarter.AppContainers
{
    public class ProxyAppContainerOptions
    {
        public IEnumerable<AttachedProxyInfo> AttachedProxyInfo { get; set; }
    }
}
