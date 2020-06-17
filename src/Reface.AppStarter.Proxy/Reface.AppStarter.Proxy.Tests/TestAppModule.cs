using LibA;
using Reface.AppStarter.AppModules;

namespace Reface.AppStarter.Proxy.Tests
{
    [ComponentScanAppModule]
    [DynamicImplementationAppModule]
    [ProxyScanAppModule]
    [ImplementorScanAppModule]
    [LibAAppModule]
    public class TestAppModule : AppModule
    {
    }
}
