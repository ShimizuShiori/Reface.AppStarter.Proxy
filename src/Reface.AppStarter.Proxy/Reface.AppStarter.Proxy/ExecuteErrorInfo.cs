using System;
using System.Reflection;

namespace Reface.AppStarter.Proxy
{
    public class ExecuteErrorInfo
    {
        public MethodInfo Method { get; private set; }
        public object[] Arguments { get; private set; }
        public Exception Error { get; private set; }

        /// <summary>
        /// 是否阻止异常的继续抛出
        /// </summary>
        public bool PreventThrow { get; private set; }

        public ExecuteErrorInfo(MethodInfo method, object[] arguments, Exception error)
        {
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
