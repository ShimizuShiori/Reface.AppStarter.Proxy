using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Events;
using Reface.EventBus;
using System.Text;

namespace Reface.AppStarter.Proxy.Tests.Listeners
{
    [Listener]
    public class MessageGetter : IEventListener<SendMessageEvent>
    {
        private readonly App app;

        public MessageGetter(App app)
        {
            this.app = app;
        }

        public void Handle(SendMessageEvent @event)
        {
            StringBuilder sb = this.app.Context.GetOrCreate("MB", key => new StringBuilder());
            sb.Append(@event.Message);
        }
    }
}
