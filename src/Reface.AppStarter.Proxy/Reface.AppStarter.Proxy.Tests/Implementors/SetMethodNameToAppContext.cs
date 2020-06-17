using Reface.AppStarter.Proxy.Attachments;
using Reface.AppStarter.Proxy.Attributes;
using Reface.AppStarter.Proxy.Tests.Attributes;

namespace Reface.AppStarter.Proxy.Tests.Implementors
{
    [AttachedImplementor]
    [TypeHasAttribute(typeof(ShowMethodName))]
    public class SetMethodNameToAppContext : IImplementor
    {
        private readonly App app;

        public SetMethodNameToAppContext(App app)
        {
            this.app = app;
        }

        public void Intercept(InterfaceInvocationInfo info)
        {
            this.app.SetTestText(info.Method.Name);
        }
    }
}
