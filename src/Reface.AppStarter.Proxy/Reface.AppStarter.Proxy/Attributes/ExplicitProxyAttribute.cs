using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 显式代理。<br />
    /// 显式代理是实现了 <see cref="ProxyAttribute"/> 的代理类，它们是直接标记在目标上的 <see cref="Attribute"/> <br />
    /// 为了提高运行速度，
    /// 不会每次都从目标类型上反射这些 <see cref="ProxyAttribute"/>，
    /// 而是缓存这它们的类型，
    /// 在使用的时候会从 IOC 容器中产出。<br />
    /// 因此需要将类型通过该特征注册到 IOC 容器中。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ExplicitProxyAttribute : ComponentAttribute
    {
        public ExplicitProxyAttribute() : base(RegistionMode.AsSelf)
        {

        }
    }
}
