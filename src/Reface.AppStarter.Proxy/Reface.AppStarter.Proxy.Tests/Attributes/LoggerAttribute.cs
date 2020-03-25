using Reface.AppStarter.Attributes;
using System;
using System.Diagnostics;

namespace Reface.AppStarter.Proxy.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class LoggerAttribute : ProxyAttribute
    {
        public override void OnExecuted(ExecutedInfo executedInfo)
        {
            Debug.WriteLine("LoggerAttribute.OnExecuted : " + executedInfo.Method.Name);
        }

        public override void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {
            Debug.WriteLine("LoggerAttribute.OnExecuteError : " + executeErrorInfo.Method.Name);
        }

        public override void OnExecuting(ExecutingInfo executingInfo)
        {
            Debug.WriteLine("LoggerAttribute.OnExecuting : " + executingInfo.Method.Name);
        }
    }
}
