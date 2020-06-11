using Reface.AppStarter.Proxy;
using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 自定义代理类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomProxyAttribute : ScannableAttribute
    {
        /// <summary>
        /// 自定义代理的附加条件
        /// </summary>
        public Type AttachmentConditionType { get; private set; }

        public CustomProxyAttribute(Type attachmentConditionType)
        {
            if (!typeof(ICustomProxyAttachmentCondition).IsAssignableFrom(attachmentConditionType))
                throw new ArgumentException("类型必须是实现 ICustomProxyAttachmentCondition", nameof(attachmentConditionType));
            AttachmentConditionType = attachmentConditionType;
        }
    }
}
