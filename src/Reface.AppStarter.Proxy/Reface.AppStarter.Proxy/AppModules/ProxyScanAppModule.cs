﻿using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 扫描代理的增加型模块。
    /// 依赖该模块，会将扫描实现了 <see cref="IProxy"/> 且标记了 <see cref="AttachedProxyAttribute"/> 的类型
    /// </summary>
    public class ProxyScanAppModule : AppModule, INamespaceFilter
    {
        public string[] IncludeNamespaces { get; set; }

        public string[] ExcludeNamespaces { get; set; }

        public override void OnUsing(AppModuleUsingArguments args)
        {
            var setup = args.AppSetup;
            ProxyAppContainerBuilder builder = setup.GetAppContainerBuilder<ProxyAppContainerBuilder>();
            args.ScannedAttributeAndTypeInfos.ForEach(x =>
            {
                if (!(x.Attribute is AttachedProxyAttribute))
                    return;

                var attrs = x.Type.GetCustomAttributes();
                if (!attrs.Any())
                    return;

                var attachments = attrs.OfType<IAttachment>();
                var methodAttachments = attrs.OfType<IMethodAttachment>();

                if (!attachments.Any() && !methodAttachments.Any())
                    return;

                builder.AttachProxy(new ProxyAttachedInfo()
                {
                    AttachedType = x.Type,
                    Attachments = attachments,
                    MethodAttachments = methodAttachments
                });

            });
        }
    }
}
