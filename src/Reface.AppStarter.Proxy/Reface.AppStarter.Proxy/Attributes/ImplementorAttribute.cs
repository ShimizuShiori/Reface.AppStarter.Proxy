using Reface.AppStarter.Proxy;
using System;

namespace Reface.AppStarter.Attributes
{
    [AttributeUsage(AttributeTargets.Interface)]
    public abstract class ImplementorAttribute : ScannableAttribute
    {
        public abstract void Intercept(InterfaceInvocationInfo info);
    }
}
