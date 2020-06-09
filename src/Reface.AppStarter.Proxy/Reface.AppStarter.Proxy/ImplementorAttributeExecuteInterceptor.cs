using Castle.Core.Interceptor;
using Reface.AppStarter.Attributes;
using System;

namespace Reface.AppStarter.Proxy
{
    class ImplementorAttributeExecuteInterceptor : IInterceptor
    {
        public Type InterfaceType { get; private set; }
        public ImplementorAttribute Implementor { get; private set; }

        public ImplementorAttributeExecuteInterceptor(Type interfaceType, ImplementorAttribute implementor)
        {
            this.InterfaceType = interfaceType;
            this.Implementor = implementor;
        }

        public void Intercept(IInvocation invocation)
        {
            InterfaceInvocationInfo info = new InterfaceInvocationInfo(this.InterfaceType, invocation.Method.GetBaseDefinition(), invocation.Arguments);
            this.Implementor.Intercept(info);
            invocation.ReturnValue = info.ReturnValue;
        }
    }
}
