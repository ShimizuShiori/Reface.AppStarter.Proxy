using Reface.AppStarter.Attributes;
using System;

namespace Reface.AppStarter.Proxy.Attachments
{
    /// <summary>
    /// 当类型可以转换为 <see cref="CanCastAsAttribute"/> 规定的类型时则添加代理的附加器。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CanCastAsAttribute : AttachmentAttribute
    {
        private readonly Type tagetType;

        public CanCastAsAttribute(Type tagetType)
        {
            this.tagetType = tagetType;
        }

        public override bool CanAttach(Type type)
        {

            return this.tagetType.IsAssignableFrom(type);
        }
    }
}
