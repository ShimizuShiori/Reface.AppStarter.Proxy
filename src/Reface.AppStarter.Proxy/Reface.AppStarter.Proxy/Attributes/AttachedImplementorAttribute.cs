using Reface.AppStarter.Attributes;
using System;

namespace Reface.AppStarter.Proxy.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AttachedImplementorAttribute : ComponentAttribute
    {
        public AttachedImplementorAttribute() : base(RegistionMode.AsSelf)
        {

        }
    }
}
