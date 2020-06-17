namespace Reface.AppStarter.Proxy
{
    public interface IImplementor
    {
        /// <summary>
        /// 拦截。当执行接口上的方法时，会将 Method 及参数传给此方法。
        /// 由在此处产生 结果，返回给调用接口的位置。
        /// </summary>
        /// <param name="info"></param>
        void Intercept(InterfaceInvocationInfo info);
    }
}
