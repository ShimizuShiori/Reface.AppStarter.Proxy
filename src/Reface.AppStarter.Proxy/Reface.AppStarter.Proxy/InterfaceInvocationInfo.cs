using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 接口调用信息
    /// </summary>
    public class InterfaceInvocationInfo
    {
        public MethodInfo Method { get; private set; }
        public object[] Arguments { get; private set; }

        public object ReturnValue { get; set; }

        public InterfaceInvocationInfo(MethodInfo method, object[] arguments)
        {
            Method = method;
            Arguments = arguments;
        }
    }
}
