using Reface.AppStarter.AppModules;
using Reface.AppStarter.Proxy.Tests.Modules.A;
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
    [AAppModule]
    [BAppModule]
    public class ABAppModule : AppModule
    {
    }
}
