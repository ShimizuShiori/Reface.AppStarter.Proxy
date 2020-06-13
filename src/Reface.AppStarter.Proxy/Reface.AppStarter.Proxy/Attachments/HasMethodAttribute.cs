using Reface.AppStarter.Attributes;
using System;
using System.Reflection;

namespace Reface.AppStarter.Proxy.Attachments
{
    /// <summary>
    /// 当一个类型中具体某个方法时，添加一个代理类。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class HasMethodAttribute : AttachmentAttribute
    {
        public string MethodName { get; set; } = "";
        public Type ReturnType { get; set; } = null;
        public Type[] ParameterTypes { get; set; } = null;


        public override bool CanAttach(Type type)
        {
            foreach (var method in type.GetMethods())
            {
                if (CheckName(method) && CheckReturnType(method) && CheckParameters(method))
                    return true;
            }
            return false;
        }

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
    }
}
