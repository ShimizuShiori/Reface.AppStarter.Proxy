
using Castle.Core.Interceptor;
using Castle.DynamicProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Proxy.Tests.Services;
using Reface.AppStarter.UnitTests;
using System.Diagnostics;

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

        [TestMethod]
        public void IInterceptorOnClass()
        {
            ProxyGenerator pg = new ProxyGenerator();
            SomeTypeThatHasOnClassAttribute s = new SomeTypeThatHasOnClassAttribute();
            ProxyGenerationOptions opt = new ProxyGenerationOptions();
            opt.AddMixinInstance(s);
            var s2 = (SomeTypeThatHasOnClassAttribute)pg.CreateClassProxy(s.GetType(), opt, new MyIInterceptor());
            s2.Do();
        }
    }

    public class MyIInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}
