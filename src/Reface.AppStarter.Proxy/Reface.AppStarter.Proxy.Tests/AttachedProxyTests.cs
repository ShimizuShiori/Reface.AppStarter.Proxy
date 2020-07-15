using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Proxy.Tests.Services;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class AttachedProxyTests
    {
        [TestMethod]
        public void FileServiceIsNotDefaultFileService()
        {
            var app = AppSetup.Start<CustomProxyAppModule>();
            IFileService service = app.CreateComponent<IFileService>();
            Assert.IsNotInstanceOfType(service, typeof(DefaultFileService));
        }

        [TestMethod]
        public void CacheServiceHasProxy()
        {
            var app = AppSetup.Start<CustomProxyAppModule>();
            var service = app.CreateComponent<ICachableService>();
            Assert.IsNotInstanceOfType(service, typeof(CachableService));
        }
    }
}
