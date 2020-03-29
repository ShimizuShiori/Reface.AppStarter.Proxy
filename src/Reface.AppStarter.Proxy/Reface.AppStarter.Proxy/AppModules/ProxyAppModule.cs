using Reface.AppStarter.AppContainerBuilders;

namespace Reface.AppStarter.AppModules
{
    public class ProxyAppModule : AppModule
    {
        private readonly IAppModule targetModule = null;

        /// <summary>
        /// 通过构造函数指定一个 targetModule 可以更换获取代理类的 module
        /// </summary>
        /// <param name="targetModule"></param>
        public ProxyAppModule(IAppModule targetModule)
        {
            this.targetModule = targetModule;
        }
        public ProxyAppModule() : this(null)
        {

        }

        public override void OnUsing(AppSetup setup, IAppModule targetModule)
        {
            ProxyAppContainerBuilder builder = setup.GetAppContainerBuilder<ProxyAppContainerBuilder>();
            AppModuleScanResult infos;
            if (this.targetModule != null)
                infos = setup.GetScanResult(this.targetModule);
            else
                infos = setup.GetScanResult(targetModule);

            infos.ScannableAttributeAndTypeInfos.ForEach(x => builder.RegisterProxyOfInterface(x));
        }
    }
}
