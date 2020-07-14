//using Reface.AppStarter.Attributes;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Reface.AppStarter.Proxy.Tests.Attributes
//{
//    [AttributeUsage(AttributeTargets.Method)]
//    public class SubThreadAttribute : ProxyAttribute
//    {
//        public override void OnExecuted(ExecutedInfo executedInfo)
//        {
//        }

//        public override void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
//        {
//        }

//        public override void OnExecuting(ExecutingInfo executingInfo)
//        {
//            if (executingInfo.Method.ReturnType == typeof(void))
//            {
//                Task task = new Task(() =>
//                {
//                    executingInfo.InvokeOriginalMethod();
//                });
//                task.Start();
//                executingInfo.Return(null);
//            }
//            else
//            {
//                Task<object> task = new Task<object>(() =>
//                {
//                    Console.WriteLine("[{0}]\tDoing Task", Thread.CurrentThread.ManagedThreadId);
//                    return executingInfo.InvokeOriginalMethod(false);
//                });
//                task.Start();
//                executingInfo.Return(task.Result);
//            }
//        }
//    }
//}
