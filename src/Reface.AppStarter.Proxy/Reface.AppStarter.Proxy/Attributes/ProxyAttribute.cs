using Reface.AppStarter.Proxy;
using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 代理特征，被标有此特征的类会产生代理类
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public abstract class ProxyAttribute : ScannableAttribute
    {
        /// <summary>
        /// 当方法执行时（执行前拦截）
        /// </summary>
        /// <param name="executingInfo"></param>
        public abstract void OnExecuting(ExecutingInfo executingInfo);
        /// <summary>
        /// 当方法执行后（执行后拦截）
        /// </summary>
        /// <param name="executedInfo"></param>
        public abstract void OnExecuted(ExecutedInfo executedInfo);
        /// <summary>
        /// 当方法执行异常后（异常后拦截）
        /// </summary>
        /// <param name="executeErrorInfo"></param>
        public abstract void OnExecuteError(ExecuteErrorInfo executeErrorInfo);
    }
}
