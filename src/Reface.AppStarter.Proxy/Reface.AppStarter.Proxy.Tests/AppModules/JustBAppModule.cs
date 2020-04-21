using Reface.AppStarter.AppModules;
using Reface.AppStarter.Proxy.Tests.Modules.B;

namespace Reface.AppStarter.Proxy.Tests.AppModules
{
    [ComponentScanAppModule
        (
            IncludeNamespaces = new string[]
            {
                "Reface.AppStarter.Proxy.Tests.Listeners"
            }
        )]
    [BAppModule]
    public class JustBAppModule : AppModule
    {
    }
}
