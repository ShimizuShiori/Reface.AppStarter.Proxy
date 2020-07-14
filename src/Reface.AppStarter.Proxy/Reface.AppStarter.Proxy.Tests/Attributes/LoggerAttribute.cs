using Reface.AppStarter.Attributes;
using System;
using System.Diagnostics;

namespace Reface.AppStarter.Proxy.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    [ExplicitProxy]
    public class LoggerAttribute : ProxyAttribute
    {
        public override void OnExecuted(ExecutedInfo executedInfo)
        {
        }

        public override void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {
        }

        public override void OnExecuting(ExecutingInfo executingInfo)
        {
        }
    }
}
