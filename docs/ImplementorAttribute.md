# 接口的动态实现

## 1 什么是接口的动态实现

在某些场景下，
接口的实现是非常有规律的，
为了避免大量、重复的实现各种各样的 *interface* ，
我们往往将实现在程序运行时来完成，
这就是 **接口的动态实现** 。

### 1.1 举个例子

比如发起一个 Http 请求，并将结果以 Json 的形式反序列化，返回的过程，
正常情况下，我们会编写像这样的两个文件。
```csharp
public interface IUserClient
{
    User GetById(int id);
    void UpdateUser(user user);
    void DeleteUser(int id);
    void CreateUser(user user);
}

public class DefaultUserClient : IUserClient
{
    public User GetById(int id)
    {
        using(var webClient = new WebClient())
        {
            string url = string.Format("......", id);
            
            // some code here

            return Json.Parse(jsonResult);
        }
    }

    public void UpdateUser(User user)
    {
        using(var webClient = new WebClient())
        {
            string url = string.Format(".....", user.Id, user.Name);
            
            // some code here

            return Json.Parse(jsonResult);
        }
    }

    // other methods
}
```

抽象的来看其实发起 *http* 请求，反序列化结果等流程都是相同的，
不同的只是 *url* , 参数 , 反序列化时的对象 , *http verb*。

面对这样的功能，就很适用 **动态实现**。
一个期望的效果如下 : 
```csharp
[HttpClient]
public interface IUserClient
{
    [HttpGet]
    [Url(".../.../.../{id}")]
    User GetById(int id);

    [HttpPost]
    [Url(".../.../.../Update/{id})]
    void UpdateUser(int id, [FormData]User user);
}
```
通过上面的方式，我们完全可以通过得到 *MethodInfo* 和 *Arguments* 加上反射等手段，就可以不用写实现类直接使用了。

## 2 使用方法

### 2.1 创建你的动态实现类

动态实现类是以 *Attribute* 的形式定义的，且必须继承 *ImplementorAttribute*

```csharp
[AttributeUsage(AttributeTargets.Interface)]
public class HttpClientAttribute : ImplementorAttribute
{
    public void Intercept(InterfaceInvocationInfo info)
    {

    }
}
```

所有的动态实现都在 *Intercept* 里完成的。

**InterfaceInvocationInfo** 拥有以下几个成员

* **Method** , 正在被调用的方法名称
* **Arguments** , 调用当前方法的真实参数
* **ReturnValue** , 准备返回给调用方的返回值

该方法的主要内容就是利用 *Method* 和 *Arguments* 产生一个 *ReturnValue*。

---
[返回](..\README.md)