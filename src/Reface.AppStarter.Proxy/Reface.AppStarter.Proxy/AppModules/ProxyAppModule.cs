using Reface.AppStarter.AppContainerBuilders;

namespace Reface.AppStarter.AppModules
{
    public class ProxyAppModule : AppModule
    {
        public override void OnUsing(AppSetup setup, IAppModule targetModule)
        {
            var infos = setup.GetScanResult(targetModule);
            ProxyAppContainerBuilder builder = setup.GetAppContainerBuilder<ProxyAppContainerBuilder>();
            infos.ScannableAttributeAndTypeInfos.ForEach(x => builder.RegisterProxyOfInterface(x));
        }
    }
}
