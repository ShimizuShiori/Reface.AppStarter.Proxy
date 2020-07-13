using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Attachments;
using Reface.AppStarter.Proxy.Tests.Attributes;
using System;

namespace Reface.AppStarter.Proxy.Tests.Proxies
{
    [AttachedProxy]
    [MethodAttachment
        (
            AttributeTypes = new Type[] 
            { 
                typeof(OnMethod)
            }
        )]
    public class ProxyForOnMethod : IProxy
    {
        private readonly App app;

        public ProxyForOnMethod(App app)
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
