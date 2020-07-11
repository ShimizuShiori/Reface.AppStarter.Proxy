using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Proxy.Tests.Models;
using Reface.AppStarter.Proxy.Tests.Repos;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class AttachProxyOnGenercClassTests
    {
        [TestMethod]
        public void GetTwoRepos()
        {
            var app = new AppSetup().Start(new TestAppModule());
            using (var work = app.BeginWork(""))
            {
                var repoOfInt = work.CreateComponent<IRepo<Order>>();
                var repoOfString = work.CreateComponent<IRepo<User>>();
            }
        }
    }
}
