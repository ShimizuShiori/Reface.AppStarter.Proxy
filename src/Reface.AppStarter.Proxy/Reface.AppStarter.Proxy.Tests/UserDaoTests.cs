using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Proxy.Tests.Dao;
using Reface.AppStarter.Proxy.Tests.Models;
using Reface.AppStarter.UnitTests;
using System;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class UserDaoTests : TestClassBase<TestAppModule>
    {
        [TestMethod]
        public void Delete()
        {
            IUserDao userDao = this.ComponentContainer.CreateComponent<IUserDao>();
            Assert.IsTrue(userDao.Delete(null));
        }
        [TestMethod]
        public void GetById()
        {
            IUserDao userDao = this.ComponentContainer.CreateComponent<IUserDao>();
            User user = userDao.GetById(Guid.Empty);
        }

        [TestMethod]
        public void GetUserName()
        {
            IUserDao userDao = this.ComponentContainer.CreateComponent<IUserDao>();
            string userName = userDao.GetUserName(Guid.Empty);
            Console.WriteLine("UserName = {0}", userName);
        }

        [TestMethod]
        public void Test()
        {
            IUserDao userDao = this.ComponentContainer.CreateComponent<IUserDao>();
            userDao.Test();
        }
    }
}
