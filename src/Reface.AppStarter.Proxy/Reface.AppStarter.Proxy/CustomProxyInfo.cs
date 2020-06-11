using System;

namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 自定义代理信息
    /// </summary>
    public class CustomProxyInfo
    {
        /// <summary>
        /// 附加代理的条件
        /// </summary>
        public Type AttachmentConditionType { get; set; }

        /// <summary>
        /// 代理的类型
        /// </summary>
        public Type ProxyType { get; set; }
    }
}
