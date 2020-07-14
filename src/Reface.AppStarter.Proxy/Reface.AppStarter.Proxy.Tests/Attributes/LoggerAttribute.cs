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
            Debug.WriteLine($"{executedInfo.Method.DeclaringType.FullName}.{executedInfo.Method.Name} Executed");
        }

        public override void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {
            Debug.WriteLine($"{executeErrorInfo.Method.DeclaringType.FullName}.{executeErrorInfo.Method.Name} Error");
        }

        public override void OnExecuting(ExecutingInfo executingInfo)
        {
            Debug.WriteLine($"{executingInfo.Method.DeclaringType.FullName}.{executingInfo.Method.Name} Executing");
        }
    }
}
