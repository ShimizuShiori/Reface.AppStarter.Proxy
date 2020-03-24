using Reface.AppStarter.Proxy.Tests.Attributes;
using Reface.AppStarter.Proxy.Tests.Models;

namespace Reface.AppStarter.Proxy.Tests.Dao
{
    [AutoDao]
    public interface IUserDao
    {
        bool Delete(User user);
    }
}
