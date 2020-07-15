using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Attributes;
using System;

namespace Reface.AppStarter.Proxy.Tests.Proxies
{
    [AttachedProxy]
    [MethodAttachment(AttributeTypes = new Type[] { typeof(CleanCache) })]
    public class CacheProxy : IProxy
    {
        public void OnExecuted(ExecutedInfo executedInfo)
        {
        }

        public void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {
        }

        public void OnExecuting(ExecutingInfo executingInfo)
        {
        }
    }
}
