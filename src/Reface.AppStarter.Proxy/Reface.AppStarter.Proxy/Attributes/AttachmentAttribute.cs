using Reface.AppStarter.Proxy;
using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 附加器，用于指定 <see cref="AttachedProxyAttribute"/> 附加在哪些类型上。
    /// 必须继承该抽象类，并实现其中的逻辑。
    /// 你可以在一个代理上指定多个附加器，这些附加器是以或的形式添加代理的。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class AttachmentAttribute : Attribute, IAttachment
    {
        public abstract bool CanAttach(Type type);
    }
}
