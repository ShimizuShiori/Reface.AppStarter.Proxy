using Reface.AppStarter.AppModules;

namespace Reface.AppStarter.Proxy.Tests
{
    [ComponentScanAppModule]
    [ProxyAppModule]
    [LibA.LibAAppModule]
    public class TestAppModule : AppModule
    {
    }
}
