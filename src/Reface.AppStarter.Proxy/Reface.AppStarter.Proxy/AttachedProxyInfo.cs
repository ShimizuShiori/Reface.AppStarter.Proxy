using System;
using System.Collections.Generic;

namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 自定义代理信息
    /// </summary>
    public class AttachedProxyInfo
    {
        /// <summary>
        /// 附加器
        /// </summary>
        public IEnumerable<IAttachment> Attachments { get; set; }

        /// <summary>
        /// 代理的类型
        /// </summary>
        public Type ProxyType { get; set; }
    }
}
