using System;
using System.Reflection;

namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 方法在执行时的信息载体
    /// </summary>
    public class ExecutingInfo
    {
        /// <summary>
        /// 方法信息
        /// </summary>
        public MethodInfo Method { get; private set; }

        /// <summary>
        /// 执行的参数
        /// </summary>
        public object[] Arguments { get; private set; }

        /// <summary>
        /// 是否跳过执行原方法，
        /// 当 <see cref="Reface.AppStarter.Attributes.ProxyAttribute.OnExecuting(ExecutingInfo)"/> 
        /// 阶段产生了 <see cref="ReturnedValue"/> 时
        /// 会跳过
        /// </summary>
        public bool SkipExecuteOriginalMethod { get; private set; } = false;

        /// <summary>
        /// 当 <see cref="Attributes.ProxyAttribute.OnExecuting(ExecutingInfo)"/>
        /// 阶段调用了 <see cref="Return(object)"/> 时，
        /// 值会被赋给该属性
        /// </summary>
        public object ReturnedValue { get; private set; } = null;

        public ExecutingInfo(MethodInfo method, object[] arguments)
        {
            Method = method;
            Arguments = arguments;
        }



        /// <summary>
        /// 替换参数
        /// </summary>
        /// <param name="arguments"></param>
        public void ReplaceArguments(object[] arguments)
        {
            if (this.Arguments.Length != arguments.Length)
                throw new ApplicationException("替换参数时，参数的数量不能发生变化");
            for (int i = 0; i < arguments.Length; i++)
            {
                this.ReplaceArgument(i, arguments[i]);
            }
        }

        /// <summary>
        /// 替换一个参数
        /// </summary>
        /// <param name="index">参数列表中的序号，0表示第一个参数</param>
        /// <param name="argument">新的参数</param>
        public void ReplaceArgument(int index, object argument)
        {
            ParameterInfo parameterInfo = this.Method.GetParameters()[index];
            Type paraType = parameterInfo.ParameterType;
            if (paraType.IsValueType && argument == null)
                throw new ApplicationException("值类型不能为空");
            if (argument != null && !paraType.IsAssignableFrom(argument.GetType()))
                throw new ApplicationException($"argument的类型 {argument.GetType().FullName} 无法转化为 {paraType.FullName}");
            this.Arguments[index] = argument;
        }

        /// <summary>
        /// 提前返回值
        /// </summary>
        /// <param name="value"></param>
        public void Return(object value)
        {
            this.SkipExecuteOriginalMethod = true;
            this.ReturnedValue = value;
        }
    }
}
