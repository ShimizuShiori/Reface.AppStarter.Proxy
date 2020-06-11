using Reface.AppStarter.Proxy;
using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 代理特征，被标有此特征的类会产生代理类
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public abstract class ProxyAttribute : ScannableAttribute, IProxy
    {
        public virtual void OnExecuted(ExecutedInfo executedInfo)
        {
        }

        public virtual void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {
        }

        public virtual void OnExecuting(ExecutingInfo executingInfo)
        {
        }
    }
}
