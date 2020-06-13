using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Attributes;

namespace Reface.AppStarter.Proxy.Tests.Services
{
    [Component(RegistionMode.Both)]
    [OnClass]
    public class SomeTypeThatHasOnClassAttribute : ISomeTypeThatHasOnClassAttribute
    {
        public virtual void Do()
        {
        }
    }
}
