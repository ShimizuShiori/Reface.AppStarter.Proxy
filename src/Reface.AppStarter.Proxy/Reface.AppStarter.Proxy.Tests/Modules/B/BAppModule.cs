using Reface.AppStarter.AppModules;

namespace Reface.AppStarter.Proxy.Tests.Modules.B
{
    [ProxyAppModule
        (
            IncludeNamespaces = new string[]
            {
                "Reface.AppStarter.Proxy.Tests.Modules.B"
            }
        )]
    public class BAppModule : AppModule
    {
    }
}
