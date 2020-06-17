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
        private readonly List<AttachedInfo> attachedProxyInfo = new List<AttachedInfo>();
        private readonly List<AttachedInfo> attachedImplementorInfo = new List<AttachedInfo>();

        /// <summary>
        /// 注册一个动态实现类的信息。
        /// </summary>
        /// <param name="info"></param>
        public void RegisterImplementor(AttributeAndTypeInfo info)
        {

            this.attributeAndTypeInfos.Add(info);
        }

        /// <summary>
        /// 附加一个代理
        /// </summary>
        /// <param name="info"></param>
        public void AttachProxy(AttachedInfo info)
        {
            this.attachedProxyInfo.Add(info);
        }

        /// <summary>
        /// 附加一个实现器
        /// </summary>
        /// <param name="info"></param>
        public void AttachImplementor(AttachedInfo info)
        {
            this.attachedImplementorInfo.Add(info);
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
                autofacContainerBuilder.RegisterByCreator(c =>
                {
                    c.InjectProperties(info.Attribute);

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
            ProxyAppContainerOptions options = new ProxyAppContainerOptions();
            options.AttachedProxyInfo = this.attachedProxyInfo;
            options.AttachedImplementorInfo = this.attachedImplementorInfo;
            return new ProxyAppContainer(options);
        }
    }
}
