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
    public class JustBTests : TestClassBase<JustBAppModule>
    {
        [AutoCreate]
        public IBService Service { get; set; }

        [TestMethod]
        public void DoB()
        {
            this.Service.DoB();
            StringBuilder sb = this.App.Context["MB"] as StringBuilder;
            Assert.AreEqual("B", sb.ToString());
        }

        [TestMethod]
        public void GetAServiceAndNotExists()
        {
            Assert.ThrowsException<ComponentNotRegistedException>(() =>
            {
                var service = this.ComponentContainer.CreateComponent<IAService>();
            });
        }
    }
}
