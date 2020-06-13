using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Attachments;

namespace Reface.AppStarter.Proxy.Tests.Proxies
{
    [AttachedProxy]
    [HasMethod
        (
            MethodName = "Plus",
            ReturnType = typeof(int)
        )]
    public class ProxyMatchMethod : IProxy
    {
        private readonly App app;

        public ProxyMatchMethod(App app)
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
