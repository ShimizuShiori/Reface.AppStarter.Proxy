using Reface.AppStarter.Proxy;
using System.Collections.Generic;

namespace Reface.AppStarter.AppContainers
{
    public class ProxyAppContainerOptions
    {
        public IEnumerable<AttachedInfo> AttachedProxyInfo { get; set; }
        public IEnumerable<AttachedInfo> AttachedImplementorInfo { get; set; }
    }
}
