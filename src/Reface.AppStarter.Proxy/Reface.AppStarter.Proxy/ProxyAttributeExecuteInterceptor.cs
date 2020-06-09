using Castle.Core.Interceptor;
using Reface.AppStarter.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.Proxy
{
    /// <summary>
    /// 执行加载由 <see cref="ProxyAttribute"/> 提供的切面的中断器。
    /// 该类仅向 Caslte.ProxyDynamic2 提供功能
    /// </summary>
    class ProxyAttributeExecuteInterceptor : IInterceptor
    {
        private readonly ProxyOnTypeInfo proxyOnTypeInfo;

        public ProxyAttributeExecuteInterceptor(ProxyOnTypeInfo proxyOnTypeInfo)
        {
            this.proxyOnTypeInfo = proxyOnTypeInfo;
        }

        public void Intercept(IInvocation invocation)
        {
            MethodInfo methodInfo;
            ReturnedValueSources source = ReturnedValueSources.OriginalMethod;

            methodInfo = invocation.MethodInvocationTarget?.GetBaseDefinition();
            if (methodInfo == null)
            {
                methodInfo = invocation.Method.GetBaseDefinition();
            }
            var proxies = this.proxyOnTypeInfo.ProxiesOnClass;
            proxies = proxies.Concat(this.proxyOnTypeInfo.ProxiesOnMethod[methodInfo.ToString()]);

            ExecutingInfo executingInfo = new ExecutingInfo(methodInfo, invocation.Arguments, invocation);

            foreach (ProxyAttribute proxy in proxies)
                proxy.OnExecuting(executingInfo);

            if (executingInfo.SkipExecuteOriginalMethod)
            {
                invocation.ReturnValue = executingInfo.ReturnedValue;
                source = ReturnedValueSources.Proxy;
            }
            else
            {
                try
                {
                    invocation.Proceed();
                    //invocation.Method.Invoke(invocation.Proxy, invocation.Arguments);
                }
                catch (Exception ex)
                {
                    ExecuteErrorInfo executeErrorInfo = new ExecuteErrorInfo(methodInfo, invocation.Arguments, ex);
                    foreach (ProxyAttribute proxy in proxies)
                        proxy.OnExecuteError(executeErrorInfo);

                    if (executeErrorInfo.PreventThrow) return;

                    throw;
                }
            }

            ExecutedInfo executedInfo = new ExecutedInfo(methodInfo, invocation.Arguments, invocation.ReturnValue, source);
            foreach (ProxyAttribute proxy in proxies)
                proxy.OnExecuted(executedInfo);
        }
    }
}
