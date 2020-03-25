using Reface.AppStarter.Proxy;
using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 动态实现，对接口标记此特征可以对接口进行动态实现
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public abstract class ImplementorAttribute : ScannableAttribute
    {
        /// <summary>
        /// 拦截。当执行接口上的方法时，会将 Method 及参数传给此方法。
        /// 由在此处产生 结果，返回给调用接口的位置
        /// </summary>
        /// <param name="info"></param>
        public abstract void Intercept(InterfaceInvocationInfo info);
    }
}
