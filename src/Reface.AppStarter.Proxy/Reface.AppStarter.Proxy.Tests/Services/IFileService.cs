using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Attributes;
using System;
using System.Threading;

namespace Reface.AppStarter.Proxy.Tests.Services
{
    public interface IFileService
    {
        void Upload(string fileName);

        string GetFileName(Guid fileId);
    }

    [Component]
    public class DefaultFileService : IFileService
    {
        public string GetFileName(Guid fileId)
        {
            return string.Format("{0}.txt", fileId);
        }

        public void Upload(string fileName)
        {
            Console.WriteLine("[{0}]\t{1}", Thread.CurrentThread.ManagedThreadId, "Upload " + fileName + " OK");
        }
    }
}
