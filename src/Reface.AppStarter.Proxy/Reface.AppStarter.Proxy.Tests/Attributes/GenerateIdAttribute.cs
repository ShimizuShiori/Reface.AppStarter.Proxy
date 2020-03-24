using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Models;
using System;

namespace Reface.AppStarter.Proxy.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GenerateIdAttribute : ProxyAttribute
    {
        public override void OnExecuted(ExecutedInfo executedInfo)
        {
            User user = (User)executedInfo.ReturnedValue;
            user.Id = Guid.NewGuid();
        }

        public override void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {
        }

        public override void OnExecuting(ExecutingInfo executingInfo)
        {
        }
    }
}
