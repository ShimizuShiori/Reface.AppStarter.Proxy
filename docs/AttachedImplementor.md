# 附加的动态实现

在阅读本篇之前，建议先阅读 [《附加的代理》](./AttachedProxy.md) 了解与附加器有关的内容。


与 **附加的代理** 类似，**附加的动态实现** 是由以下成员组成
* *IImplementor*
* *AttachmentAttribute*
* *AttachedImplementorAttribute*

### 示例

假定我们有一个 **多语言** 的功能。

在 **RoleAppModule** 中，我们假定有一个 **多语言组件**

```csharp
public interface IRoleLanguageProvider
{
    // 返回一个表示角色不存在的异常消息
    string getRoleNotExists(string roleCode);

    // 返回一个表示角色已存在的异常消息
    string getRoleHasAlreadExists(string roleCode);

    string getXXX();

    string getYYY();

    string getZZZ();

    // more gets here
}
```

这个组件的会从形如下面的文件中读取相应的内容，并返回

```properties
RoleNotExists: {roleCode}不存在
RoleHasAlreadExists: {roleCode}已存在
XXX: ...
YYY: ...
ZZZ: ...
# more infos here
```

为这个 **ILanguageProvider** 中的每一个方法都写一个实现一定是个恶梦。

我们可以使用 **动态实现** 来完成这样的功能，我们不需要写实现类，**IOC** + **动态实现** 帮助我们实现。

#### 创建一个实现器

**实现器** 是实现了 *IImplementor* 的类型，每当外部调用某个方法时，会将调用信息传递给 **实现器**，包含调用的方法信息、参数列表。

**实现器** 可以根据这些信息执行逻辑并返回结果给外。

我们编写实现我们 **多语言** 组件的逻辑。

```csharp
public class I18nImplementor : IImplementor
{
    public void Intercept(InterfaceInvocationInfo info)
    {
        string text = GetTextFromFile(info);
        info.ReturnValue = text;
    }

    private string GetTextFromFile(InterfaceInvocationInfo info)
    {
        // here are some code that about get text from file.
    }
}
```

我们需要通知 *Reface.AppStarter* 这是一个 **附加的实现器**

```csharp
[AttachedImplementor]
public class I18nImplementor : IImplementor
```

**我们约定所有类型名以 *LanguageProvider* 结尾的类型，都是我们需要动态实现的类型**

基于这个约定，我们创建 **附加器**

> 关于附加器，可以通过 [《附加的代理》](./AttachedProxy.md) 了解。

```csharp
[AttributeUsage(AttributeTargets.Class)]
public class NameEndWithLanguageProviderAttachment : AttachmentAttribute
{
    public override bool CanAttach(Type type)
    {
        return type.Name.EndsWith("LanguageProvider");
    }
}
```

与 **附加的代理** 相同，我们需要将 **附加器** 指定到 **实现器** 上

```csharp
[AttachedImplementor]
[NameEndWithLanguageProviderAttachment]
public class I18nImplementor : IImplementor
```

#### 添加依赖

最后，为你的 *AppModule* 添加 *ImplementorScanAppModule* 和 *ComponentScanAppModule* 的依赖，就可以启动了。

```csharp
[ComponentScanAppModule]
[ImplementorScanAppModule]
public class RoleAppModule  : AppModule
{

}
```

---

[Home](../README.md)