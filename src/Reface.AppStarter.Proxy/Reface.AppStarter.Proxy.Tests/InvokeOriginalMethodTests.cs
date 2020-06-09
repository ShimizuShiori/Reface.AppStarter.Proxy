using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Services;
using Reface.AppStarter.UnitTests;
using System;
using System.Threading;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class InvokeOriginalMethodTests : TestClassBase<TestAppModule>
    {
        [AutoCreate]
        public IFileService FileService { get; set; }

        [TestMethod]
        public void SubThreadWithNoReturnValue()
        {
            Console.WriteLine("[{0}]\t{1}", Thread.CurrentThread.ManagedThreadId, "SubThreadWithNoReturnValue START");
            this.FileService.Upload("1.txt");
            Console.WriteLine("[{0}]\t{1}", Thread.CurrentThread.ManagedThreadId, "SubThreadWithNoReturnValue END");
        }

        [TestMethod]
        public void SubThreadWithTaskOfString()
        {
            Console.WriteLine("[{0}]\t{1}", Thread.CurrentThread.ManagedThreadId, "SubThreadWithTaskOfString START");
            string fileName = this.FileService.GetFileName(Guid.NewGuid());
            Console.WriteLine("[{0}]\t{1} : {2}", Thread.CurrentThread.ManagedThreadId, "SubThreadWithTaskOfString END",
                fileName);
        }
    }
}
