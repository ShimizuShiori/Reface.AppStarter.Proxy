# 附加的代理

形如下面的 [显式声明代理](./ProxyAttribute.md) 是一种对目标类型有侵入性的代理声明方式。

```csharp
[Component]
[SomeProxy]
public class SomeService : ISomeService
{
    [ProxyA]
    [ProxyB]
    public void SomeMethod()
    {
        // some code here
    }
}
```

自 *Reface.AppStarter.Proxy 1.5.0* 起，提供了另一种声明方式 : **附加代理** 。

### 附加代理

附加代理 **不需要** 将代理声明 **目标类型或方法签名上** 。

而是在创建代理时声明它应当以何种方式附加到类型上。

### 最简单的例子

对所有类型名称以 *Traceable* 开头的类型添加日志代理，用于记录这些类型每个方法每个参数的信息。

**首先**，编写代理类。

该代理不会以 *Attribute* 的方式添加到目标类型或方法上，所以不需要从 *ProxyAttribute* 继承，只要实现 *IProxy* 接口即可.
```csharp
public class LoggerProxy : IProxy
{
    public override void OnExecuted(ExecutedInfo executedInfo)
    {
        // output log with result
    }

    public override void OnExecuteError(ExecuteErrorInfo executeErrorInfo)
    {
        // output log with error
    }

    public override void OnExecuting(ExecutingInfo executingInfo)
    {
        // output log with parameters
    }
}
```

代理类写好了，我们需要告诉 *Reface.AppStarter* 这是一个 **附加代理** 。
```csharp
[AttachedProxy]
public class LoggerProxy : IProxy
```

**附加代理** 需要一个 **附加器** 将这个代理加到目标类型上。

一共有两种 **附加器** :
* 类型附加器 ( *AttachmentAttribute* )，用于指定让代理附加到哪些类型上
* 方法附加器 ( *MethodAttachmentAttribute* )，用于指定让代理附加到哪些方法上

在本示例中，我们需要将代理附加到 **名称以 Traceable 开头的类型上** ，所以我们使用 *AttachmentAttribute* .

创建一个类型 *ClassNameStartWithTraceableAttachmentAttribute* ，让其继承 *AttachmentAttribute* ，并实现相关逻辑
```csharp
[AttributeUsage(AttributeTargets.Class)]
public class ClassNameStartWithTraceableAttachmentAttribute : AttachmentAttribute
{
    public override bool CanAttach(Type type)
    {
        return type.Name.StartsWith("Traceable");
    }
}
```

很简单的几行代码，一个 **附加器** 完成了，我们把它加到代理类上。

```csharp
[AttachedProxy]
[ClassNameStartWithTraceableAttachment]
public class LoggerProxy : IProxy
```

最后，在你的 *AppModule* 添加对 *ProxyScanAppModule* 和 *ComponentScanAppModule* 开启代理类扫描和组件扫描的功能。

```csharp
[ComponentScanAppModule] // 扫描组件
[ProxyScanAppModule]     // 扫描代理
public class DemoAppModule : AppModule
{

}
```

---

[Home](../README.md)