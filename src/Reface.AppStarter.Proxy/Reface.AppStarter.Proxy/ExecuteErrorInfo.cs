using System;
using System.Reflection;

namespace Reface.AppStarter.Proxy
{
    public class ExecuteErrorInfo
    {
        public object InvokingTarget { get; private set; }
        public MethodInfo Method { get; private set; }
        public object[] Arguments { get; private set; }
        
        /// <summary>
        /// 执行发生异常时的异常
        /// </summary>
        public Exception Error { get; private set; }

        /// <summary>
        /// 是否阻止异常的继续抛出
        /// </summary>
        public bool PreventThrow { get; private set; }

        public ExecuteErrorInfo(object invokingTarget, MethodInfo method, object[] arguments, Exception error)
        {
            InvokingTarget = invokingTarget;
            Method = method;
            Arguments = arguments;
            Error = error;
        }

        /// <summary>
        /// 将异常标记为已处理
        /// </summary>
        public void Handle()
        {
            this.PreventThrow = true;
        }
    }
}
