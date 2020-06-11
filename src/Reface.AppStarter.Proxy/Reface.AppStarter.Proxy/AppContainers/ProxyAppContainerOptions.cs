using Reface.AppStarter.Proxy;
using System.Collections.Generic;

namespace Reface.AppStarter.AppContainers
{
    public class ProxyAppContainerOptions
    {
        public IEnumerable<CustomProxyInfo> CustomProxyInfos { get; set; }
    }
}
