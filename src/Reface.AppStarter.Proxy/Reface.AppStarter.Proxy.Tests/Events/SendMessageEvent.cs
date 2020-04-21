using Reface.EventBus;

namespace Reface.AppStarter.Proxy.Tests.Events
{
    public class SendMessageEvent : Event
    {
        public string Message { get; private set; }
        public SendMessageEvent(object source, string message) : base(source)
        {
            this.Message = message;
        }
    }
}
