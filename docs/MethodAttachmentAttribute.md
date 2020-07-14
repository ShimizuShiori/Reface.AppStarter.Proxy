# MethodAttachmentAttribute

实现了 *IProxy* 接口的代理，可以通过编写自定义的 *AttachmentAttribute* 附加到类型上 ([示例](./AttachedProxy.md))，也可以使用 *MethodAttachmentAttribute* 附加到某个方法上。

*MethodAttachmentAttribute* 不是抽象类形，你可以直接将它标记到 *IProxy* 的实现类上。

*MethodAttachmentAttribute* 通过四个条件来决定是否附加到某个方法上
* 方法名称
* 参数列表
* 返回类型
* *Attributes*

当你向 *IProxy* 声明 *MethodAttachment* 时，你会看到这四个条件分别对应的属性
* *MethodName*
* *ReturnType*
* *ParameterTypes*
* *AttributeTypes*

你可以使用全部的条件，也可以使用部分条件，也可以一个条件都不用。

**没有被赋值的条件将不会参与条件的判断 , 所以如果一个条件都不用 就等同于 附加到所有方法上**

#### MethodName

当设置了 **MethodName** 后，代理会附加到与指定名称相同的方法上。

```csharp
[MethodAttachment
    (
        MethodName = "ToString"  
    )]
```

#### ReturnType

当指定了 **ReturnType** 后，代理会附加到 **返回类型可以转换至指定类型** 的方法上。

假定有以下的类型结构

```csharp
public class User : IEntity { }
public class Order : IEntity { }
```

下面的附加器会附加到类型是 *User* 或是 *Order* 的方法上

```csharp
[MethodAttachment
    (
        ReturnType = typeof(IEntity)
    )]
```

#### ParameterTypes

需要指定一个 *Type[]* 类型。

指定了该属性后，代理会附加到 **参数列表中一一对应的类型可以转换指定类型** 的方法上。

假定有以下的类型结构和 **Method**

```csharp
public class User : IEntity { }
public class Role : IEntity { }

[Component]
public class UserService : IUseService
{
    public void Update(int id, User user)
    {}
}

[Component]
public class RoleService : IRoleService
{
    public void Update(int id, Role role)
    {}
} 
```

那么下面的附加器会将代理附加在两个 *Update* 方法上。

```csharp
[MethodAttachment
    (
        ParameterTypes = new Type[]
        {
            typeof(int),
            typeof(IEntity)
        }
    )]
```

#### AttributeTypes

需要指定一个 *Type[]* 类型，这些类型应当都是 *Attribute*。

代理会附加到 **所有指定的特征都被标记** 的方法上。

---
[Home](../README.md)