using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Attachments;
using Reface.AppStarter.Proxy.Tests.Attributes;

namespace Reface.AppStarter.Proxy.Tests.Proxies
{
    [AttachedProxy]
    [TypeHasAttribute(typeof(OnClass))]
    public class ProxyForOnClass : IProxy
    {
        private readonly App app;

        public ProxyForOnClass(App app)
        {
            this.app = app;
        }

        public void OnExecuted(ExecutedInfo executedInfo)
        {
        }

        public void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {
        }

        public void OnExecuting(ExecutingInfo executingInfo)
        {
            this.app.SetTestText(this.GetType().FullName);
        }
    }
}
