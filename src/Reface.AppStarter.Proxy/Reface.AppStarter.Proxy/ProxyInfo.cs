using System;
using System.Diagnostics;

namespace Reface.AppStarter.Proxy
{
    [DebuggerDisplay("{Type} : {HasProxy}")]
    /// <summary>
    /// 代理信息
    /// </summary>
    public class ProxyInfo
    {
        /// <summary>
        /// 目标类型
        /// </summary>
        public Type Type { get; private set; }
        
        /// <summary>
        /// 类型是否具有代理
        /// </summary>
        public Boolean HasProxy { get; private set; }

        /// <summary>
        /// 类型是否是动态实现类
        /// </summary>
        public bool IsDynamicImplemented { get; private set; }

        public ProxyInfo(Type type, bool hasProxy, bool isDynamicImplemented)
        {
            Type = type;
            HasProxy = hasProxy;
            IsDynamicImplemented = isDynamicImplemented;
        }

        public override string ToString()
        {
            return $"Type={Type}, HasProxy={HasProxy}";
        }
    }
}
