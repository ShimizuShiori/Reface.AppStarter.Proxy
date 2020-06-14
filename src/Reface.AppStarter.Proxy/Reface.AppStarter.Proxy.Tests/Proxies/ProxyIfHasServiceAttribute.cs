using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Attachments;
using Reface.AppStarter.Proxy.Tests.Attributes;
using System.Diagnostics;

namespace Reface.AppStarter.Proxy.Tests.Proxies
{
    [AttachedProxy]
    [TypeHasAttribute(typeof(Service))]
    public class ProxyIfHasServiceAttribute : IProxy
    {
        public void OnExecuted(ExecutedInfo executedInfo)
        {
        }

        public void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {
        }

        public void OnExecuting(ExecutingInfo executingInfo)
        {
            Debug.WriteLine(executingInfo.ToString());
        }
    }
}
