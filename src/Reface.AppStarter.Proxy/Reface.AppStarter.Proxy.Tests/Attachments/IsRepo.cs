using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Repos;
using System;

namespace Reface.AppStarter.Proxy.Tests.Attachments
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IsRepo : AttachmentAttribute
    {
        public override bool CanAttach(Type type)
        {
            return type.GetInterface(typeof(IRepo<>).FullName) != null;
        }
    }
}
