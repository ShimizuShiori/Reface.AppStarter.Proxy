using Reface.AppStarter.Attributes;
using System.Diagnostics;

namespace Reface.AppStarter.Proxy.Tests.Proxies
{
    [CustomProxy(typeof(SetHelloToContextAttachmentCondition))]
    public class SetHelloToContext : IProxy
    {
        public void OnExecuted(ExecutedInfo executedInfo)
        {
            Debug.WriteLine("OnExecuted");
        }

        public void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {
            Debug.WriteLine("OnExecuteError");
        }

        public void OnExecuting(ExecutingInfo executingInfo)
        {
            Debug.WriteLine("OnExecuting");
        }
    }
}
