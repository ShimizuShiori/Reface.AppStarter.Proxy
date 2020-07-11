using Reface.EventBus;

namespace Reface.AppStarter.Proxy.Tests.Events
{
    public class TestEvent : Event
    {
        public TestEvent(object source) : base(source)
        {
        }
    }
}
