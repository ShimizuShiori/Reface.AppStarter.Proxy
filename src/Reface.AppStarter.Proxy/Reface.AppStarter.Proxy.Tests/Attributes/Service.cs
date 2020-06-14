using Reface.AppStarter.Attributes;
using System;

namespace Reface.AppStarter.Proxy.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Service : ComponentAttribute
    {
        public Service() : base(RegistionMode.AsInterfaces)
        {

        }
    }
}
