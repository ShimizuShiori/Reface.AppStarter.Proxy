using System.Reflection;

namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 方法附加器。将代理附加到方法上的判断器。
    /// </summary>
    public interface IMethodAttachment
    {
        /// <summary>
        /// 判断是否允许将代理附加到指定的方法上。
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        bool CanAttachOnMethod(MethodInfo method);
    }
}
