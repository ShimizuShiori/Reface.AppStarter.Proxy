using System;
using System.Reflection;

namespace Reface.AppStarter.Proxy
{
    public class ExecutedInfo
    {
        public object InvokingTarget { get; private set; }
        public MethodInfo Method { get; private set; }
        public object[] Arguments { get; private set; }

        /// <summary>
        /// 拦截到的结果
        /// </summary>
        public object ReturnedValue { get; private set; }

        /// <summary>
        /// 拦截到的结果来源
        /// </summary>
        public ReturnedValueSources Source { get; private set; }

        public ExecutedInfo(object invokingTarget, MethodInfo method, object[] arguments, object returnedValue, ReturnedValueSources source)
        {
            InvokingTarget = invokingTarget;
            Method = method;
            Arguments = arguments;
            ReturnedValue = returnedValue;
            Source = source;
        }



        /// <summary>
        /// 替换返回的值
        /// </summary>
        /// <param name="value"></param>
        public void ReplaceReturnValue(object value)
        {
            Type resultType = this.Method.ReturnType;
            if (resultType.IsValueType && value == null)
                throw new ApplicationException("值类型不能为 null");
            if (value == null)
            {
                this.ReturnedValue = null;
                return;
            }

            Type typeOfValue = value.GetType();
            if (!resultType.IsAssignableFrom(typeOfValue))
                throw new ApplicationException($"新的结果 {typeOfValue.FullName} 无法转换为 {resultType.FullName}");

            this.ReturnedValue = value;
        }

    }
}
