using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Proxies;
using Reface.AppStarter.Proxy.Tests.Services;
using Reface.AppStarter.UnitTests;
using System;
using System.Diagnostics;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class AttachedProxyTests : TestClassBase<CustomProxyAppModule>
    {
        [AutoCreate]
        public IFileService FileService { get; set; }

        [AutoCreate]
        public SomeTypeThatHasOnClassAttribute T1OnCLass { get; set; }

        [AutoCreate]
        public ISomeTypeThatHasOnClassAttribute T1OnInterface { get; set; }

        [AutoCreate]
        public IServiceHasMethodHasOnMethod T2 { get; set; }

        [AutoCreate]
        public IServiceNoAttributeAndPlugsTwoInt T3 { get; set; }

        [TestMethod]
        public void AttachmentCanCastAs()
        {
            this.FileService.GetFileName(Guid.NewGuid());
            Assert.AreEqual(typeof(SetHelloToContext).FullName, this.App.GetTestText());
        }

        [TestMethod]
        public void Attachment_AttributeOnClass_UseClass()
        {
            this.T1OnCLass.Do();
            //Assert.AreEqual(typeof(ProxyForOnClass).FullName, this.App.GetTestText());
            Assert.IsFalse(App.Context.ContainsKey(Constant.APP_CONTEXT_TEST_TEXT));
        }

        [TestMethod]
        public void Attachment_AttributeOnClass_UseInterface()
        {
            this.T1OnInterface.Do();
            Assert.AreEqual(typeof(ProxyForOnClass).FullName, this.App.GetTestText());
        }

        [TestMethod]
        public void Attachment_OnMethod()
        {
            this.T2.Do();
            Assert.AreEqual(typeof(ProxyForOnMethod).FullName, this.App.GetTestText());
        }
        [TestMethod]
        public void Attachment_OnMethod2()
        {
            this.T2.DoWithoutAttriute();
            Assert.AreEqual("", this.App.GetTestText());
        }

        [TestMethod]
        public void Attachment_HasMethod()
        {
            this.T3.Plus(1, 1);
            Assert.AreEqual(typeof(ProxyMatchMethod).FullName, this.App.GetTestText());
        }

        [TestMethod]
        public void TestProxyOnServiceAttribute()
        {
            var service1 = this.ComponentContainer.CreateComponent<IService1>();
            var service2 = this.ComponentContainer.CreateComponent<IService2>();
            Assert.AreEqual(service1.GetType(), service2.GetService1Type());
        }
    }
}
