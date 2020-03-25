using Castle.DynamicProxy;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter.AppContainerBuilders
{
    public class ProxyAppContainerBuilder : BaseAppContainerBuilder
    {
        private readonly List<AttributeAndTypeInfo> attributeAndTypeInfos = new List<AttributeAndTypeInfo>();

        public void RegisterProxyOfInterface(AttributeAndTypeInfo info)
        {
            if (!info.Type.IsInterface) return;
            if (info.Attribute is ImplementorAttribute)
                this.attributeAndTypeInfos.Add(info);
        }

        public override IAppContainer Build(AppSetup setup)
        {
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            AutofacContainerBuilder autofacContainerBuilder = setup.GetAppContainerBuilder<AutofacContainerBuilder>();
            foreach (var info in attributeAndTypeInfos)
            {
                if (!info.Type.IsInterface) continue;
                autofacContainerBuilder.RegisterByCreator(c =>
                {
                    c.InjectPropeties(info.Attribute);

                    //ClassProxyOnTypeInfo proxyOnTypeInfo = new ClassProxyOnTypeInfo(info.Type);
                    //foreach (var attr in proxyOnTypeInfo.ProxiesOnClass)
                    //    c.InjectPropeties(attr);
                    //foreach (var attr in proxyOnTypeInfo.ProxiesOnMethod.SelectMany(x => x.Value))
                    //    c.InjectPropeties(attr);

                    ImplementorAttributeExecuteInterceptor interceptor = new ImplementorAttributeExecuteInterceptor(info.Type, (ImplementorAttribute)info.Attribute);
                    var rlt = proxyGenerator.CreateInterfaceProxyWithoutTarget(
                        info.Type,
                        interceptor
                    );
                    return rlt;
                }, info.Type);
            }
            return new ProxyAppContainer();
        }
    }
}
