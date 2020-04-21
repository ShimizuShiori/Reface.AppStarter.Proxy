using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Events;
using Reface.EventBus;
using System;

namespace Reface.AppStarter.Proxy.Tests.Modules.B.Attributes
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class BImplementor : ImplementorAttribute
    {
        public IEventBus EventBus { get; set; }

        public override void Intercept(InterfaceInvocationInfo info)
        {
            this.EventBus.Publish(new SendMessageEvent(this, "B"));
        }
    }
}
