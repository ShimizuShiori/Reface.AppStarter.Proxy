using Reface.AppStarter.Proxy.Tests.Attributes;
using Reface.AppStarter.Proxy.Tests.Models;
using System;

namespace Reface.AppStarter.Proxy.Tests.Dao
{
    [AutoDao]
    [Logger]
    public interface IUserDao
    {
        bool Delete(User user);

        string GetUserName(Guid id);

        [Logger]
        [GenerateId]
        User GetById(Guid id);

        void Test();
    }
}
