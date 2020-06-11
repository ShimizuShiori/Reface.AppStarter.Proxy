using Reface.AppStarter.AppModules;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Proxies;
using System;

namespace Reface.AppStarter.Proxy.Tests
{
    [ComponentScanAppModule]
    [ProxyAppModule]
    public class CustomProxyAppModule : AppModule
    {
    }
}
