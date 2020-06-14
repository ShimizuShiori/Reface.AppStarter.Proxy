using Reface.AppStarter.Proxy.Tests.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter.Proxy.Tests.Services
{
    public interface IService1 { }

    [Service]
    public class DefaultService1 : IService1 { }

    public interface IService2
    {
        Type GetService1Type();
    }

    [Service]
    public class DefaultService2 : IService2
    {
        private readonly IService1 service1;

        public DefaultService2(IService1 service1)
        {
            this.service1 = service1;
        }

        public Type GetService1Type()
        {
            return this.service1.GetType();
        }
    }
}
