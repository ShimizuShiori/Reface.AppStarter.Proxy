using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Attributes;
using System;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class ExtendInterfaceTests
    {
        [TestMethod]
        public void Impl()
        {
            var app = new AppSetup().Start(new TestAppModule());
            using (var work = app.BeginWork("t1"))
            {
                var b = work.CreateComponent<IInterfaceB>();
                int i = b.MethodA();
                Assert.AreEqual(1, i);
            }
        }
    }

    public interface IInterfaceA
    {
        int MethodA();
    }

    [ImplA]
    public interface IInterfaceB : IInterfaceA
    { }

    [AttributeUsage(AttributeTargets.Interface)]
    public class ImplA : ImplementorAttribute
    {
        public override void Intercept(InterfaceInvocationInfo info)
        {
            info.ReturnValue = 1;
        }
    }
}
