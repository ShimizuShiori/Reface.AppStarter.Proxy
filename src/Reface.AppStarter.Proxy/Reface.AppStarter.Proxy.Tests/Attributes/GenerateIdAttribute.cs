using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Models;
using System;
using System.Diagnostics;

namespace Reface.AppStarter.Proxy.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    [Component(RegistionMode.AsSelf)]
    public class GenerateIdAttribute : ProxyAttribute
    {
        public override void OnExecuted(ExecutedInfo executedInfo)
        {
            Debug.WriteLine("GenerateIdAttribute.OnExecuted");
            User user = (User)executedInfo.ReturnedValue;
            user.Id = Guid.NewGuid();
        }

        public override void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {
            Debug.WriteLine("GenerateIdAttribute.OnExecuteError");
        }

        public override void OnExecuting(ExecutingInfo executingInfo)
        {
            Debug.WriteLine("GenerateIdAttribute.OnExecuting");
        }
    }
}
