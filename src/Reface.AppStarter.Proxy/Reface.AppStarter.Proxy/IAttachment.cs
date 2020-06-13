using System;

namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 附加器，用于判断是否在指定的类型上附加代理
    /// </summary>
    public interface IAttachment
    {
        bool CanAttach(Type type);
    }
}
