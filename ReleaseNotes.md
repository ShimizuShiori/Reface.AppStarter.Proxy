# ReleaseNote

## version
**date**

* 为 *ExecutingInfo* 增加了方法 *InvokeOriginalMethod(bool)* 用于执行原方法，参数决定是否将原方法的结果作为真正的结果返回
* 修改 *InterfaceInvocationInfo* 的成员，添加 *InterfaceType Type* 以解决接口继承关系下的动态实现，无法仅通过 *MethodInfo* 来获取到目标接口类型。
* 在获取类型上代理信息时，针对动态实现的情况，从接口以及其所有父类上获取。顺序是由先父类后子类
* 添加了新的定义代理的方式，可以以非侵入的方式定义一个代理类