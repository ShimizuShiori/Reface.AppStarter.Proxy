using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.AppModules;
using Reface.AppStarter.Proxy.Tests.Modules.A.Services;
using Reface.AppStarter.Proxy.Tests.Modules.B.Services;
using Reface.AppStarter.UnitTests;
using System.Text;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class ABTests : TestClassBase<ABAppModule>
    {
        [AutoCreate]
        public IAService AService { get; set; }

        [AutoCreate]
        public IBService BService { get; set; }

        [TestMethod]
        public void DoAAndBAndCheckMessage()
        {
            this.AService.DoA();
            this.BService.DoB();
            StringBuilder sb = this.App.Context["MB"] as StringBuilder;
            Assert.AreEqual("A1A2B", sb.ToString());
        }
    }
}
