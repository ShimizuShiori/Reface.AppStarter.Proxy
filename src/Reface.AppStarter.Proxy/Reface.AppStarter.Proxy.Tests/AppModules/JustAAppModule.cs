using Reface.AppStarter.AppModules;
using Reface.AppStarter.Proxy.Tests.Modules.A;

namespace Reface.AppStarter.Proxy.Tests.AppModules
{
    [ComponentScanAppModule
        (
            IncludeNamespaces = new string[]
            {
                "Reface.AppStarter.Proxy.Tests.Listeners"
            }
        )]
    [AAppModule]
    public class JustAAppModule : AppModule
    {
    }
}
