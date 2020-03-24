using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Proxy.Tests.Dao;
using Reface.AppStarter.UnitTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
