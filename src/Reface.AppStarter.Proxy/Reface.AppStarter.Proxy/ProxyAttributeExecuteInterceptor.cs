using Castle.Core.Interceptor;
using Reface.AppStarter.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.Proxy
{
    public class ProxyAttributeExecuteInterceptor : IInterceptor
    {
        private readonly ClassProxyOnTypeInfo proxyOnTypeInfo;

        public ProxyAttributeExecuteInterceptor(ClassProxyOnTypeInfo proxyOnTypeInfo)
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
            proxies = proxies.Concat(this.proxyOnTypeInfo.ProxiesOnMethod[methodInfo]);


            ExecutingInfo executingInfo = new ExecutingInfo(methodInfo, invocation.Arguments);

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
