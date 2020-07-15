using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Attributes;

namespace Reface.AppStarter.Proxy.Tests.Services
{
    public interface ICachableService
    {
        void Clean();
    }

    [Component]
    public class CachableService : ICachableService
    {
        [CleanCache("a")]
        [CleanCache("b")]
        public void Clean()
        {
        }
    }
}
