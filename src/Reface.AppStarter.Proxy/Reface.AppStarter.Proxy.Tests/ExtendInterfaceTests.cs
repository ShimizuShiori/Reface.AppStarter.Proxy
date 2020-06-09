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

    [Print("IA")]
    public interface IInterfaceA
    {
        [Print("MA")]
        int MethodA();
    }

    [ImplA]
    [Print("IB")]
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

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = true)]
    public class Print : ProxyAttribute
    {
        private readonly string content;

        public Print(string content)
        {
            this.content = content;
        }

        public override void OnExecuted(ExecutedInfo executedInfo)
        {
        }

        public override void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {
        }

        public override void OnExecuting(ExecutingInfo executingInfo)
        {
            Console.WriteLine(this.content);
        }
    }
}
