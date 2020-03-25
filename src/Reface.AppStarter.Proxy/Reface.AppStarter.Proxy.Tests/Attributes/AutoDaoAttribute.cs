using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Tests.Models;
using System;
using System.Diagnostics;

namespace Reface.AppStarter.Proxy.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class AutoDaoAttribute : ImplementorAttribute
    {
        public override void Intercept(InterfaceInvocationInfo info)
        {
            Debug.WriteLine("AutoDaoAttribute.Intercept");
            if (info.Method.ReturnType == typeof(bool))
                info.ReturnValue = true;

            if (info.Method.ReturnType == typeof(User))
                info.ReturnValue = new User();
        }
    }

}
