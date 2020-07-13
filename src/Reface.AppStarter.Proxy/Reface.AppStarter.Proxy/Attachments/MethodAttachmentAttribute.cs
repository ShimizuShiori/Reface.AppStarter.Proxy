using System;
using System.Diagnostics;
using System.Reflection;

namespace Reface.AppStarter.Proxy.Attachments
{
    /// <summary>
    /// 当一个类型中具体某个方法时，添加一个代理类。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MethodAttachmentAttribute : Attribute, IMethodAttachment
    {
        public string MethodName { get; set; } = "";
        public Type ReturnType { get; set; } = null;
        public Type[] ParameterTypes { get; set; } = null;
        public Type[] AttributeTypes { get; set; } = null;

        private bool CheckName(MethodInfo method)
        {
            if (string.IsNullOrEmpty(this.MethodName)) return true;
            return method.Name == this.MethodName;
        }

        private bool CheckReturnType(MethodInfo method)
        {
            if (this.ReturnType == null) return true;
            return this.ReturnType.IsAssignableFrom(method.ReturnType);
        }

        private bool CheckParameters(MethodInfo method)
        {
            if (this.ParameterTypes == null) return true;
            var paras = method.GetParameters();
            for (int i = 0; i < this.ParameterTypes.Length; i++)
            {
                Type requriedType = this.ParameterTypes[i];
                Type realType = paras[i].ParameterType;
                if (!requriedType.IsAssignableFrom(realType))
                    return false;
            }
            return true;
        }

        private bool CheckAttibutes(MethodInfo method)
        {
            if (this.AttributeTypes == null) return true;
            foreach (var type in this.AttributeTypes)
            {
                if (method.GetCustomAttribute(type) == null)
                    return false;
            }
            return true;
        }

        public bool CanAttachOnMethod(MethodInfo method)
        {
            var result = CheckName(method) && CheckReturnType(method) && CheckParameters(method) && CheckAttibutes(method);
            Debug.WriteLine("{1}.{0} Need To Attach [{3}]: {2}", method.ToString(), method.DeclaringType.Name, result, this.GetType().Name);
            return result;
        }
    }
}
