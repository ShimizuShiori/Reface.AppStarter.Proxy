using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Proxy.Tests.Models;
using Reface.AppStarter.Proxy.Tests.Services;
using Reface.AppStarter.UnitTests;
using System;
using System.Diagnostics;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class UserServiceTests : TestClassBase<TestAppModule>
    {
        [TestMethod]
        public void RegisterUserAndIdIsNotEmpty()
        {
            IUserService userService = this.ComponentContainer.CreateComponent<IUserService>();
            Debug.WriteLine($"userService.GetType() = {userService.GetType()}");
            User user = userService.Register("Felix");
            Assert.AreEqual("Felix", user.Name);
        }
    }
}
