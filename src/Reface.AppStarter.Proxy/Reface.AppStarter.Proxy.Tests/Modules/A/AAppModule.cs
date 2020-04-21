using Reface.AppStarter.AppModules;

namespace Reface.AppStarter.Proxy.Tests.Modules.A
{
    [ComponentScanAppModule
        (
            IncludeNamespaces = new string[] 
            {
                "Reface.AppStarter.Proxy.Tests.Modules.A"
            }
        )]
    [ProxyAppModule
        (
            IncludeNamespaces = new string[]
            {
                "Reface.AppStarter.Proxy.Tests.Modules.A"
            }
        )]

    public class AAppModule : AppModule
    {
    }
}
