using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.Attributes;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 代理模块。<br />
    /// 使用此会将创建 <see cref="ProxyAppContainerBuilder"/> 使得 <see cref="App"/> 中具有相应的容器。
    /// 这些容器会帮助你启动基于 <see cref="ProxyAttribute"/> 的代理。
    /// 但是无法使用附加代理，附加实现，动态实现等功能。
    /// </summary>
    public class ProxyAppModule : AppModule
    {

        public override void OnUsing(AppModuleUsingArguments arguments)
        {
            var setup = arguments.AppSetup;
            ProxyAppContainerBuilder builder = setup.GetAppContainerBuilder<ProxyAppContainerBuilder>();

        }
    }
}
