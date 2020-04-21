using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Modules.A.Attributes;

namespace Reface.AppStarter.Proxy.Tests.Modules.A.Services
{
    public interface IAService
    {
        void DoA();
    }

    [Component]
    public class AService : IAService
    {
        [AProxy]
        public void DoA()
        {
            
        }
    }
}
