﻿using Reface.AppStarter.Attributes;
using Reface.AppStarter.Proxy.Attachments;
using Reface.AppStarter.Proxy.Tests.Services;
using System.Diagnostics;

namespace Reface.AppStarter.Proxy.Tests.Proxies
{
    [AttachedProxy]
    [CanCastAs(typeof(IFileService))]
    public class SetHelloToContext : IProxy
    {
        private readonly App app;

        public SetHelloToContext(App app)
        {
            this.app = app;
        }

        public void OnExecuted(ExecutedInfo executedInfo)
        {
        }

        public void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
        {
        }

        public void OnExecuting(ExecutingInfo executingInfo)
        {
            this.app.SetTestText(this.GetType().FullName);
        }
    }
}
