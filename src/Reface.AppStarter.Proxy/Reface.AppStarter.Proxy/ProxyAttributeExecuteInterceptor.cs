using Castle.Core.Interceptor;
using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;
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
        private readonly ProxyOnTypeRuntimeInfo proxyInfo;

        public ProxyAttributeExecuteInterceptor(ProxyOnTypeRuntimeInfo proxyInfo)
        {
            this.proxyInfo = proxyInfo;
        }

        public void Intercept(IInvocation invocation)
        {
            MethodInfo methodInfo;
            ReturnedValueSources source = ReturnedValueSources.OriginalMethod;

            object invokingTarget = invocation.InvocationTarget;

            methodInfo = invocation.MethodInvocationTarget?.GetBaseDefinition();
            if (methodInfo == null)
                methodInfo = invocation.Method.GetBaseDefinition();

            var proxies = this.proxyInfo.ProxyOnClass;
            IEnumerable<IProxy> proxiesOnMethod = this.proxyInfo
                .GetProxiesOnMethod(methodInfo);

            proxies = proxies.Concat(proxiesOnMethod).ToList();

            ExecutingInfo executingInfo = new ExecutingInfo(invokingTarget, methodInfo, invocation.Arguments, invocation);

            foreach (IProxy proxy in proxies)
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
                }
                catch (Exception ex)
                {
                    ExecuteErrorInfo executeErrorInfo = new ExecuteErrorInfo(invokingTarget, methodInfo, invocation.Arguments, ex);
                    foreach (IProxy proxy in proxies)
                        proxy.OnExecuteError(executeErrorInfo);

                    if (executeErrorInfo.PreventThrow) return;

                    throw;
                }
            }

            ExecutedInfo executedInfo = new ExecutedInfo(invokingTarget, methodInfo, invocation.Arguments, invocation.ReturnValue, source);
            foreach (IProxy proxy in proxies)
                proxy.OnExecuted(executedInfo);
        }
    }
}
