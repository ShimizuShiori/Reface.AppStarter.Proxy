using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Attachments;
using Reface.AppStarter.Proxy.Tests.Events;

namespace Reface.AppStarter.Proxy.Tests.Proxies
{
    [AttachedProxy]
    [IsRepo]
    public class ShowRepoInfo : IProxy
    {
        private readonly IWork work;

        public ShowRepoInfo(IWork work)
        {
            this.work = work;
        }

        public void OnExecuted(ExecutedInfo executedInfo)
        {
        }

        public void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {
        }

        public void OnExecuting(ExecutingInfo executingInfo)
        {
            this.work.PublishEvent(new TestEvent(executingInfo.InvokingTarget));
        }
    }
}
