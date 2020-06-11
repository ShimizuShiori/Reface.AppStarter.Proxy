using System;

namespace Reface.AppStarter.Proxy
{
    public class CustomProxyRuntimeInfo
    {
        /// <summary>
        /// 附加代理的条件
        /// </summary>
        public ICustomProxyAttachmentCondition AttachmentCondition { get; set; }

        /// <summary>
        /// 代理的类型
        /// </summary>
        public Type ProxyType { get; set; }
    }
}
