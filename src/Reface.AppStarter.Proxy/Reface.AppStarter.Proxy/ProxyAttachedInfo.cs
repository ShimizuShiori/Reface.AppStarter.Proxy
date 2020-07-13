using System.Collections.Generic;

namespace Reface.AppStarter.Proxy
{
    public class ProxyAttachedInfo : AttachedInfo
    {
        public IEnumerable<IMethodAttachment> MethodAttachments { get; set; }
    }
}
