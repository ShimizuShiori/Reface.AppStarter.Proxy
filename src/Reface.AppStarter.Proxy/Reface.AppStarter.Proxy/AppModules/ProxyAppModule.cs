using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 代理模块。
    /// 依赖该模块，实现得到以下功能 : 
    /// 1. 为具有 <see cref="ProxyAttribute"/> 特征成员的类型创建代理类，调以 AOP 的方式调用这些 <see cref="ProxyAttribute"/>
    /// 2. 为具有 <see cref="ImplementorAttribute"/> 特征的接口创建代理类，以动态实现这些接口中的方法
    /// </summary>
    public class ProxyAppModule : AppModule, INamespaceFilter
    {
        public string[] IncludeNamespaces { get; set; }

        public string[] ExcludeNamespaces { get; set; }

        public override void OnUsing(AppModuleUsingArguments arguments)
        {
            var setup = arguments.AppSetup;
            ProxyAppContainerBuilder builder = setup.GetAppContainerBuilder<ProxyAppContainerBuilder>();

            arguments.ScannedAttributeAndTypeInfos.ForEach(x =>
            {
                builder.RegisterImplementor(x);
                if (!(x.Attribute is AttachedProxyAttribute))
                    return;

                var attachments = x.Type.GetCustomAttributes<AttachmentAttribute>();
                if (!attachments.Any())
                    return;

                builder.AppendProxy(new AttachedProxyInfo()
                {
                    ProxyType = x.Type,
                    Attachments = attachments
                });

            });
        }
    }
}
