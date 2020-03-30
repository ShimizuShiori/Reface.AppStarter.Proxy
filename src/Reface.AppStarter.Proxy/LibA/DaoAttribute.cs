using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy;
using System;

namespace LibA
{
    public class DaoAttribute : ImplementorAttribute
    {
        public override void Intercept(InterfaceInvocationInfo info)
        {
            Console.WriteLine($"Dao.Intercept");
        }
    }
}
