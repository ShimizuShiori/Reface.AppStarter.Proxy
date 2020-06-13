using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reface.AppStarter.Proxy.Attachments
{
    /// <summary>
    /// 组合的附加器。 <br />
    /// 在 <see cref="GroupAttachmentAttribute"/> 子类上添加多个 <see cref="AttachmentAttribute"/> 表示每一个都通过后才可以附加指定的代理。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class GroupAttachmentAttribute : AttachmentAttribute
    {
        public override bool CanAttach(Type type)
        {
            IEnumerable<IAttachment> attachments = this.GetType().GetCustomAttributes<AttachmentAttribute>();
            foreach (var attachment in attachments)
            {
                if (!attachment.CanAttach(type))
                    return false;
            }
            return true;
        }
    }
}
