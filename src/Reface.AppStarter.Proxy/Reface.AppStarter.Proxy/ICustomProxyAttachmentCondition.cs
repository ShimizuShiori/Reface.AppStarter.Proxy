using System;

namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 自定义代理的附加条件。
    /// 只有符合了该条件的类型才会附加指定的代理
    /// </summary>
    public interface ICustomProxyAttachmentCondition
    {
        /// <summary>
        /// 判断允许在此类型上附加代理
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool CanAttach(Type type);
    }
}
