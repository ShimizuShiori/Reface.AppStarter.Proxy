using Reface.AppStarter.Proxy;
using System;
using System.Reflection;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 向某个符合条件的方法添加代理类。<br />
    /// 未指定条件则表示不对此属性判断，多个条件之间为且关系。<br />
    /// 你可以向你的代理类添加多个 <see cref="MethodAttachmentAttribute"/> 表示或的关系。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MethodAttachmentAttribute : Attribute, IMethodAttachment
    {
        /// <summary>
        /// 对方法名称的附加条件
        /// </summary>
        public string MethodName { get; set; } = "";

        /// <summary>
        /// 对返回类型的附加条件。<br />
        /// 某个方法的返回值与该属性相同，或可以换转至该属性的值，就会附加代理。
        /// </summary>
        public Type ReturnType { get; set; } = null;

        /// <summary>
        /// 对参数列表的附加条件。
        /// 某个方法中的所有参数都与该属性指定的参数一一对应，换均可以转换至该属性要求的类型，就可以附加代理。
        /// </summary>
        public Type[] ParameterTypes { get; set; } = null;

        /// <summary>
        /// 对标记在方法上的 <see cref="Attribute"/> 附加的条件。
        /// 该参数指定的 <see cref="Attribute"/> 必须全部出现在方法上才会附加该代理。
        /// </summary>
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
            return CheckName(method) && CheckReturnType(method) && CheckParameters(method) && CheckAttibutes(method);
            //var result = CheckName(method) && CheckReturnType(method) && CheckParameters(method) && CheckAttibutes(method);
            //Debug.WriteLine("{1}.{0} Need To Attach [{3}]: {2}", method.ToString(), method.DeclaringType.Name, result, this.GetType().Name);
            //return result;
        }
    }
}
