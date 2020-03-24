
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.UnitTests;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class OthersTest : TestClassBase<TestAppModule>
    {
        [TestMethod]
        public void OnePlusOneIsTwo()
        {
            Assert.AreEqual(2, 1 + 1);
        }
    }
}
