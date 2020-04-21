using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 接口调用信息
    /// </summary>
    public class InterfaceInvocationInfo
    {
        /// <summary>
        /// 调用的方法
        /// </summary>
        public MethodInfo Method { get; private set; }

        /// <summary>
        /// 调用的参数
        /// </summary>
        public object[] Arguments { get; private set; }

        /// <summary>
        /// 通过动态实现返回的值，你可以手动为其赋值
        /// </summary>
        public object ReturnValue { get; set; }

        public InterfaceInvocationInfo(MethodInfo method, object[] arguments)
        {
            Method = method;
            Arguments = arguments;
        }
    }
}
