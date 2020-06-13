using Reface.AppStarter.Attributes;

namespace Reface.AppStarter.Proxy.Tests.Services
{
    [Component]
    public class ServiceNoAttributeAndPlugsTwoInt : IServiceNoAttributeAndPlugsTwoInt
    {
        public int Plus(int i, int j)
        {
            return i + j;
        }
    }
}
