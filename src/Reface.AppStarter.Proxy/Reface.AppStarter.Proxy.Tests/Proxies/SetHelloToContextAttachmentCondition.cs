using Reface.AppStarter.Proxy.Tests.Services;
using System;

namespace Reface.AppStarter.Proxy.Tests.Proxies
{
    public class SetHelloToContextAttachmentCondition : ICustomProxyAttachmentCondition
    {
        public bool CanAttach(Type type)
        {
            return typeof(IFileService).IsAssignableFrom(type);
        }
    }
}
