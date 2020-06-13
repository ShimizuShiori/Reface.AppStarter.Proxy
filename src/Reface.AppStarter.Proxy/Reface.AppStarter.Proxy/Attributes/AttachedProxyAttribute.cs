using System;
using Reface.AppStarter.Proxy;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 额外追加的代理。
    /// 该特征应当加在实现了 <see cref="IProxy"/> 的类型上。可以非侵入性的方式添加代理类。 <br />
    /// 添加了该特征的代理还需要添加一个附加器来指定该代码应用在哪些类型。
    /// 目前系统中所有的附加器都在 Reface.AppStarter.Proxy.Attachments 中。<br />
    /// 一个代理可以添加多个附加器，每个附加器表示对某个规则下的类型添加代理。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AttachedProxyAttribute : ComponentAttribute
    {
        public AttachedProxyAttribute() : base(RegistionMode.AsSelf)
        {

        }
    }
}
