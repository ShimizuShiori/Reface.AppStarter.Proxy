using Castle.DynamicProxy;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy;
using System.Collections.Generic;

namespace Reface.AppStarter.AppContainerBuilders
{
    /// <summary>
    /// 代理容器构建器
    /// </summary>
    public class ProxyAppContainerBuilder : BaseAppContainerBuilder
    {
        private readonly List<AttributeAndTypeInfo> attributeAndTypeInfos = new List<AttributeAndTypeInfo>();

        public void RegisterProxyOfInterface(AttributeAndTypeInfo info)
        {
            if (!info.Type.IsInterface) return;
            if (info.Attribute is ImplementorAttribute)
                this.attributeAndTypeInfos.Add(info);
        }

        public override void Prepare(AppSetup setup)
        {
            AutofacContainerBuilder autofacContainerBuilder = setup.GetAppContainerBuilder<AutofacContainerBuilder>();
            autofacContainerBuilder.Building += AutofacContainerBuilder_Building;
        }

        private void AutofacContainerBuilder_Building(object sender, AppContainerBuilderBuildEventArgs e)
        {
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            AutofacContainerBuilder autofacContainerBuilder
                = (AutofacContainerBuilder)sender;
            foreach (var info in attributeAndTypeInfos)
            {
                if (!info.Type.IsInterface) continue;
                autofacContainerBuilder.RegisterByCreator(c =>
                {
                    c.InjectPropeties(info.Attribute);

                    ImplementorAttributeExecuteInterceptor interceptor = new ImplementorAttributeExecuteInterceptor(info.Type, (ImplementorAttribute)info.Attribute);
                    var rlt = proxyGenerator.CreateInterfaceProxyWithoutTarget(
                        info.Type,
                        interceptor
                    );
                    return rlt;
                }, info.Type);
            }
        }

        public override IAppContainer Build(AppSetup setup)
        {
            return new ProxyAppContainer();
        }
    }
}
