using Reface.AppStarter.AppModules;

namespace Reface.AppStarter.Proxy.Tests.Modules.B
{
    [DynamicImplementationAppModule
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
