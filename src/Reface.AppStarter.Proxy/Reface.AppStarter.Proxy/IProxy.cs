using Reface.AppStarter.Attributes;

namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 代理接口。<br />
    /// 你有两种方式声明一个代理：
    /// 1. 通过继承 <see cref="ProxyAttribute"/> 创建代理 <br />
    /// 
    /// 也可以通过实现 <see cref="IProxy"/> 接口来实现一个代理。
    /// 详情可访问 : https://github.com/ShimizuShiori/Reface.AppStarter.Proxy/blob/Future_MethodAttachment/docs/AttachedProxy.md
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
