using System;

namespace Reface.AppStarter.Proxy
{
    public class AttachedMethodProxyRuntimeInfo
    {
        public IMethodAttachment MethodAttachment { get; set; }
        public Type AttachedType { get; set; }
    }
}
