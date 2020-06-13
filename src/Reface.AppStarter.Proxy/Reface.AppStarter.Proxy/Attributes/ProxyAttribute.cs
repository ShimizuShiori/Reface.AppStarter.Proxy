using Reface.AppStarter.Proxy;
using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 代理特征。<br />
    /// 开发者继承此类必实现其中的方法。并将特征加在标记了 <see cref="ComponentAttribute"/> 的类型上或方法上，即可创建相应的 AOP
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
