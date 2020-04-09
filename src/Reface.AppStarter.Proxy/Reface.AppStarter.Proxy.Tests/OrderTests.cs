using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Proxy.Tests.Services;
using Reface.AppStarter.UnitTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class OrderTests : TestClassBase<TestAppModule>
    {
        private IOrderService GetOrderService()
        {
            return this.ComponentContainer.CreateComponent<IOrderService>();
        }

        [TestMethod]
        public void Create()
        {
            var service = this.GetOrderService();
            service.Create();
        }
    }
}
