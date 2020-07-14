using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Models;
using System;
using System.Diagnostics;

namespace Reface.AppStarter.Proxy.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    [ExplicitProxy]
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
