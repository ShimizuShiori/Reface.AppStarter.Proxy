using LibA;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.UnitTests;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class LibAUserDaoTests : TestClassBase<TestAppModule>
    {
        [TestMethod]
        public void GetUserDaoFromLibA()
        {
            IUserDao userDao = this.ComponentContainer.CreateComponent<IUserDao>();
            Assert.IsNotNull(userDao);
        }
    }
}
