using System;
using System.Reflection;

namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 接口调用信息
    /// </summary>
    public class InterfaceInvocationInfo
    {
        /// <summary>
        /// 调用的接口类型
        /// </summary>
        public Type InterfaceType { get; private set; }

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

        internal InterfaceInvocationInfo(Type interfaceType, MethodInfo method, object[] arguments)
        {
            InterfaceType = interfaceType;
            Method = method;
            Arguments = arguments;
        }
    }
}
