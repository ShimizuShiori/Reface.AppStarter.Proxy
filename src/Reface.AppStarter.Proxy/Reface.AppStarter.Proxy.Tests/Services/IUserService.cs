using Reface.AppStarter.Proxy.Tests.Models;

namespace Reface.AppStarter.Proxy.Tests.Services
{
    public interface IUserService
    {
        User Register(string name);

        void Another();
    }
}
