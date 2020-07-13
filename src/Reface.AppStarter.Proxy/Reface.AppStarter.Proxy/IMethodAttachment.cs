using System.Reflection;

namespace Reface.AppStarter.Proxy
{
    public interface IMethodAttachment
    {
        bool CanAttachOnMethod(MethodInfo method);
    }
}
