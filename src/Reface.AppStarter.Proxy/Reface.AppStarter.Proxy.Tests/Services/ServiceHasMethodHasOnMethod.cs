using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Attributes;

namespace Reface.AppStarter.Proxy.Tests.Services
{
    [Component]
    public class ServiceHasMethodHasOnMethod : IServiceHasMethodHasOnMethod
    {
        [OnMethod]
        public void Do() { }
    }
}
