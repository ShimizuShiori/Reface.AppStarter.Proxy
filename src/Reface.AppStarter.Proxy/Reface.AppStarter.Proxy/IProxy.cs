namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 代理接口
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
