using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Attributes;
using Reface.AppStarter.Proxy.Tests.Models;
using System;

namespace Reface.AppStarter.Proxy.Tests.Services
{
    [Component]
    public class DefaultUserService : IUserService
    {
        [GenerateId]
        public User Register(string name)
        {
            User user = new User() { Name = name };
            return user;
        }
    }
}
