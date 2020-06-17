using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy;
using Reface.AppStarter.Proxy.Attributes;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 扫描动态实现类的模块。
    /// 依赖此模块会扫描所有实现了 <see cref="IImplementor"/> 且标记了 <see cref="AttachedImplementorAttribute"/> 的实现类，并将它们使用在相应的接口中。
    /// </summary>
    public class ImplementorScanAppModule : AppModule, INamespaceFilter
    {
        public string[] IncludeNamespaces { get; set; }

        public string[] ExcludeNamespaces { get; set; }

        public override void OnUsing(AppModuleUsingArguments args)
        {
            var setup = args.AppSetup;
            ProxyAppContainerBuilder builder = setup.GetAppContainerBuilder<ProxyAppContainerBuilder>();
            args.ScannedAttributeAndTypeInfos.ForEach(x =>
            {
                if (!(x.Attribute is AttachedImplementorAttribute))
                    return;

                var attachments = x.Type.GetCustomAttributes<AttachmentAttribute>();
                if (!attachments.Any())
                    return;

                builder.AttachImplementor(new AttachedInfo()
                {
                    AttachedType = x.Type,
                    Attachments = attachments
                });

            });
        }
    }
}
