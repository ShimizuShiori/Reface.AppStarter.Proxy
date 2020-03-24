using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Proxy.Tests.Models;
using Reface.AppStarter.Proxy.Tests.Services;
using Reface.AppStarter.UnitTests;
using System;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class UserServiceTests : TestClassBase<TestAppModule>
    {
        [TestMethod]
        public void RegisterUserAndIdIsNotEmpty()
        {
            IUserService userService = this.ComponentContainer.CreateComponent<IUserService>();
            User user = userService.Register("Felix");
            Assert.AreNotEqual(Guid.Empty, user.Id);
            Assert.AreEqual("Felix", user.Name);
        }
    }
}
