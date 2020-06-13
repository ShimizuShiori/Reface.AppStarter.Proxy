using Reface.AppStarter.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.Proxy.Attachments
{
    /// <summary>
    /// 若类型中任何方法存在指定的特征则附加代理
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MethodHasAttributeAttribute : AttachmentAttribute
    {
        private readonly Type attributeType;

        public MethodHasAttributeAttribute(Type attributeType)
        {
            this.attributeType = attributeType;
        }

        public override bool CanAttach(Type type)
        {
            return type.GetMethods().FirstOrDefault(x => x.GetCustomAttribute(attributeType) != null) != null;
        }
    }
}
