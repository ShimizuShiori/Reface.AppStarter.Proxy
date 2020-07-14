using Reface.AppStarter.Attributes;
using Reface.AppStarter.ComponentLifetimeListeners;
using Reface.AppStarter.Proxy.Tests.Events;
using Reface.EventBus;
using System;

namespace Reface.AppStarter.Proxy.Tests.Modules.A.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    [ExplicitProxy]
    public class AProxy : ProxyAttribute, IOnCreated
    {
        private IEventBus eventBus;

        public void OnCreated(CreateArguments arguments)
        {
            this.eventBus = arguments.ComponentManager.CreateComponent<IEventBus>();
        }

        public override void OnExecuted(ExecutedInfo executedInfo)
        {
            this.eventBus.Publish(new SendMessageEvent(this, "A2"));
        }

        public override void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {

        }

        public override void OnExecuting(ExecutingInfo executingInfo)
        {
            this.eventBus.Publish(new SendMessageEvent(this, "A1"));
        }
    }
}
