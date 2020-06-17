using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.Attributes;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 动态实现模块。
    /// 依赖该模块，会动态实现那些标记了 <see cref="ImplementorAttribute"/> 的接口
    /// </summary>
    public class DynamicImplementationAppModule : AppModule, INamespaceFilter
    {
        public string[] IncludeNamespaces { get; set; }

        public string[] ExcludeNamespaces { get; set; }

        public override void OnUsing(AppModuleUsingArguments args)
        {
            var setup = args.AppSetup;
            ProxyAppContainerBuilder builder = setup.GetAppContainerBuilder<ProxyAppContainerBuilder>();
            args.ScannedAttributeAndTypeInfos.ForEach(x =>
            {
                if (!x.Type.IsInterface) return;
                if (x.Attribute is ImplementorAttribute)
                    builder.RegisterImplementor(x);
            });
        }
    }
}
