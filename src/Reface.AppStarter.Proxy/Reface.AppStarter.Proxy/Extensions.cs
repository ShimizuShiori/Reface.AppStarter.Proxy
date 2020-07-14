using Castle.Core.Interceptor;
using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.Proxy
{
    static class Extensions
    {
        /// <summary>
        /// 获取指定成员上的代理特征
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static IEnumerable<ProxyAttribute> GetProxies(this MemberInfo member)
        {
            return member.GetCustomAttributes<ProxyAttribute>();
        }

        /// <summary>
        /// 判断一个对象是不是动态实现类
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDynamicImplemented(this object value)
        {
            if (!(value is IProxyTargetAccessor)) return false;
            IProxyTargetAccessor proxyTargetAccessor = value as IProxyTargetAccessor;
            IInterceptor[] interceptors = proxyTargetAccessor.GetInterceptors();
            if (interceptors == null) return false;
            IEnumerable<ImplementorAttributeExecuteInterceptor> myInterceptor
                 = interceptors.OfType<ImplementorAttributeExecuteInterceptor>();
            if (!myInterceptor.Any()) return false;
            return true;
        }
    }
}
