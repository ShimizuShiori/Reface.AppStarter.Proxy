# 通过 *Attribute* 创建 **AOP** 切面

### 注意事项

无论通过哪种方式添加的 **AOP**，该实例都必须是通过 *Reface.AppStarter* 的 **IOC/DI** 容器创建出来的，
直接通过 *new* 得到的实例，是无法通过此功能创建 **AOP** 的。

---

*Reface.AppStarter.Proxy* 提供两种切面的创建方法。

* **通过 ProxyAttribute 显式得将切面标记到类型或者方法上** 
* [通过非侵入式添加 ( Reface.AppStarter.Proxy >= 1.5.0 )](./AttachedProxy.md)

本篇将介绍如何使用 *ProxyAttribute* 创建一个 **显式代理**

**首先**，
我们创建一个切面类型用于编写切面的功能代码。

开发 **显式代理** 有三项要求：
* 继承于 *ProxyAttribute* 
* 添加 *ExplicitProxy* 特征
* 对该代理所在的模块添加对 *ComponentScanAppModule* 的依赖

假设我们开发一个用于打印每个方法执行进入、返回以及异常时的代理类。

首先，创建名为 *LoggerAttribute* 的类型，继承于 *ProxyAttribute*，除了添加 *AttributeUsage* 外，
还需要添加 *ExplicitProxy*

```csharp
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
[ExplicitProxy]
public class LoggerAttribute : ProxyAttribute
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

现在，你需要为 *LoggerAttribute* 所在的 *AppModule* 添加以下依赖：
* *ComponentScanAppModule*
* *ProxyAppModule*

```csharp
[ComponentScanAppModule]
[ProxyScanAppModule]
public class TestAppModule : AppModule
{
}
}
```

将该切面加在其它类型的类型签名上，或者方法签名上即可生效。

---

**一些细节**

* 这个类是一个 *Attribute* , 因此类名请使用 *Attribute* 结尾
* 建议将 *AttributeTargets* 设置为 *Method | Class | Interface*
    * 将切面定义在 *Method* 上时，表示当执行这个 *Method* 时触发 **AOP**
    * 将切面定义在 *Class* 上时，表示当执行这个 *class* 中所有 *Method* 时都触发 **AOP**
    * 将切面定义在 *Interface* 上时，表示当执行由这个 *interface* 产生的动态实现时，每个 *Method* 都触发 **AOP**
    * 执行时，类型上的代理会先于方法上的代理执行
* *ProxyAppModule* 仅仅是开启了 *ProxyAppContainer*，没有其它功能
* *ProxyAttribute* 在执行时不是通过反射得到的实例，而是通过 **IOC** 容器创建的，所以你的 *ProxyAttribute* 最好是无参数构造的，如果必须要用有构造函数的切面请参考[《附加的代理》](./AttachedProxy.md)
* 因为 *ProxyAttribute* 是由 **IOC** 容器创建的，因此你可以为其标记 **生命周期接口** 

---

#### 三个切点

##### OnExecuting

这是切面的第一个切点，
此时，还没执行原本的 *method*，
在该阶段，可以实现以下功能
* 获取执行方法、执行参数的信息
* 替换待执行的参数
* 可以直接产生一个结果返回，并阻止原本的 *method* 执行。
参数 *ExecutingInfo* 包含以下几个成员 :

* **Method** , 原本要执行的方法的 *MethodInfo*
* **Arguments** , 执行 *method* 时的参数
* **ReplaceArgument(index, value)** , 替换参数
    * **index** , 替换的参数 *index* 值，0 表示第一个参数
    * **value** , 要替换的新值
* **ReplaceArguments(values)** , 使用一个数组替换所有参数
* **Return(value)** , 返回一个值，执行此方法会跳过原本 *method* 的执行

##### OnExecuted

这是执行后的切点，
无论 *method* 的过程是由原本的 *method* 完成的，或是由 *OnExecuting* 完成的，
都会进入 *OnExecuted* 。
在该阶段，主要的功能是
* 获取执行方法、执行参数的信息
* 获取执行后的结果
* 判断结果的来源
    * 来源于原本的方法
    * 来源于 *OnExecuting*
* 替换原来的结果

参数 *ExecutedInfo* 包含以下几个成员 :
* **Method**
* **Arguments**
* **ReturnedValue** , 执行后产生的结果
* **Source** , 结果的来源
* **ReplaceReturnValue(object)** , 替换已产生的结果

##### OnExecuteError

这是执行抛出异常后的切点
在该阶段，主要的功能是
* 获取执行方法、执行参数的信息
* 获取异常
* 阻止异常继续抛出

参数 *ExecuteErrorInfo* 包含以下几个成员 : 
* **Method**
* **Arguments**
* **Error** , 抛出的异常
* **Handle()** , 表示异常已被处理，不再继续向外抛出

### 3 应用切面类

假定接口和实现类

```csharp
public interface IService
{
    void MethodA();
    void MethodB();
}

public class DefaultService : IService
{
    public void MethodA()
    {}

    public void MethodB()
    {}
}

```

因为切面是在通过 **IOC** 容器时产生的，
因此我们需要为 *DefaultService* 加上 *ComponentAttribute*

```csharp
[Component]
public class DefaultService : IService
```

现在，我们可以将我们编写好的 *AOP Attribute* 添加到 *class* 或 *method* 上面

```csharp
[MyProxyA]
[Component]
public class DefaultService : IService
```

```csharp
[Logger]
public void MethodA()
```

最后说说不同切面类的执行顺序。

* *class* 上的切面优先于 *method* 上的切面
* 同是 *class* 中的切面，顺序不能确定
* 同时 *method* 中的切面，顺序不能确定

> todo : 预计在未来的版本中加入顺序功能


---
[返回](../README.md)