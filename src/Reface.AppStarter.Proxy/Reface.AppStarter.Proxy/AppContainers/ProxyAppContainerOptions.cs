using Reface.AppStarter.Proxy;
using System.Collections.Generic;

namespace Reface.AppStarter.AppContainers
{
    public class ProxyAppContainerOptions
    {
        public IEnumerable<ProxyAttachedInfo> AttachedProxyInfo { get; set; }
        public IEnumerable<AttachedInfo> AttachedImplementorInfo { get; set; }
    }
}
