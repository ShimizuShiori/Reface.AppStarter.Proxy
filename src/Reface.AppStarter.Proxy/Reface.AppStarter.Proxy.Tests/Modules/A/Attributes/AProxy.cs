using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Events;
using Reface.EventBus;
using System;

namespace Reface.AppStarter.Proxy.Tests.Modules.A.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AProxy : ProxyAttribute
    {
        public IEventBus EventBus { get; set; }

        public override void OnExecuted(ExecutedInfo executedInfo)
        {
            this.EventBus.Publish(new SendMessageEvent(this, "A2"));
        }

        public override void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {

        }

        public override void OnExecuting(ExecutingInfo executingInfo)
        {
            this.EventBus.Publish(new SendMessageEvent(this, "A1"));
        }
    }
}
