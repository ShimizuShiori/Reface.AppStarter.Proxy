using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Errors;
using Reface.AppStarter.Proxy.Tests.AppModules;
using Reface.AppStarter.Proxy.Tests.Modules.A.Services;
using Reface.AppStarter.Proxy.Tests.Modules.B.Services;
using Reface.AppStarter.UnitTests;
using System.Text;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class JustATests : TestClassBase<JustAAppModule>
    {
        [AutoCreate]
        public IAService Service { get; set; }

        [TestMethod]
        public void DoAService()
        {
            this.Service.DoA();
            StringBuilder sb = App.Context["MB"] as StringBuilder;
            string message = sb.ToString();
            Assert.AreEqual("A1A2", sb.ToString());
        }

        [TestMethod]
        public void GetBServiceIsNotExists()
        {
            Assert.ThrowsException<ComponentNotRegistedException>(() =>
            {
                var serviceB = this.ComponentContainer.CreateComponent<IBService>();
            });
        }
    }
}
