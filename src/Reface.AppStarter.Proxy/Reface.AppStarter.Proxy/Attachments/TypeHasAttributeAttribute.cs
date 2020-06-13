using Reface.AppStarter.Attributes;
using System;
using System.Reflection;

namespace Reface.AppStarter.Proxy.Attachments
{
    /// <summary>
    /// 表示类型上有指定的 <see cref="Attribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TypeHasAttributeAttribute : AttachmentAttribute
    {
        private readonly Type attributeType;

        public TypeHasAttributeAttribute(Type attributeType)
        {
            this.attributeType = attributeType;
        }

        public override bool CanAttach(Type type)
        {
            return type.GetCustomAttribute(this.attributeType) != null;
        }
    }
}
