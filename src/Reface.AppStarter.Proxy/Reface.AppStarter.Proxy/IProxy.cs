using Reface.AppStarter.Attributes;

namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 代理接口。<br />
    /// 你可以通过 <see cref="ProxyAttribute"/> 和 <see cref="AttachedProxyAttribute"/> 两种方法使用。
    /// </summary>
    public interface IProxy
    {
        /// <summary>
        /// 当方法执行时（执行前拦截）
        /// </summary>
        /// <param name="executingInfo"></param>
        void OnExecuting(ExecutingInfo executingInfo);
        /// <summary>
        /// 当方法执行后（执行后拦截）
        /// </summary>
        /// <param name="executedInfo"></param>
        void OnExecuted(ExecutedInfo executedInfo);
        /// <summary>
        /// 当方法执行异常后（异常后拦截）
        /// </summary>
        /// <param name="executeErrorInfo"></param>
        void OnExecuteError(ExecuteErrorInfo executeErrorInfo);
    }
}
