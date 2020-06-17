using Reface.AppStarter.Proxy;
using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 动态实现，对接口标记此特征可以对接口进行动态实现
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public abstract class ImplementorAttribute : ScannableAttribute, IImplementor
    {
        public abstract void Intercept(InterfaceInvocationInfo info);
    }
}
