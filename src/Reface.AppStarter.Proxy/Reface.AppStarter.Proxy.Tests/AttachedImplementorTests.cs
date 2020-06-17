using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Attributes;
using Reface.AppStarter.UnitTests;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class AttachedImplementorTests : TestClassBase<TestAppModule>
    {

        [TestMethod]
        public void InvokeGo()
        {
            var service = this.ComponentContainer.CreateComponent<IAIService>();
            service.Go();
            Assert.AreEqual("Go", this.App.GetTestText());
        }
    }

    [ShowMethodName]
    public interface IAIService
    {
        void Go();
    }
}
