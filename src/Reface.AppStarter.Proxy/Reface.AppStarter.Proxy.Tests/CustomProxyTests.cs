using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Services;
using Reface.AppStarter.UnitTests;
using System;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class CustomProxyTests// : TestClassBase<CustomProxyAppModule>
    {
        //[AutoCreate]
        //public IFileService FileService { get; set; }

        [TestMethod]
        public void InvokeAnyMethod()
        {
            var app = new AppSetup().Start(new CustomProxyAppModule());
            using (var work = app.BeginWork("Test"))
            {
                IFileService s = work.CreateComponent<IFileService>();
                string fileName = s.GetFileName(Guid.NewGuid());
            }
        }
    }
}
