using Reface.AppStarter.Attributes;
using System;

namespace Reface.AppStarter.Proxy.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class AutoDaoAttribute : ProxyAttribute
    {
        public override void OnExecuted(ExecutedInfo executedInfo)
        {
        }

        public override void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {
        }

        public override void OnExecuting(ExecutingInfo executingInfo)
        {
            if (executingInfo.Method.ReturnType == typeof(bool))
                executingInfo.Return(true);
        }
    }
}
