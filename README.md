# Reface.AppStarter.Proxy

## 1 介绍

Reface.AppStarter 的功能模块，使用此模块，可以轻松的建立 AOP 和 接口的动态实现。

## 1.1 依赖

* *Reface.AppStarter*
* *Caslte.DynamicProxy*

## 1.2 Nuget 页面

https://www.nuget.org/packages/Reface.AppStarter.Proxy

## 1.3 提供的功能

* 作为 *Reface.AppStarter* 的一个子模块
* [通过 *Attribute* 创建 **AOP** 切面](docs/ProxyAttribute.md)
* [通过 *Attribute* 创建接口的动态实现](docs/ImplementorAttribute.md)
* [以非侵入式的代码向已有的类型附加代理](docs/AttachedProxy.md)
* [以非侵入式的代码向已有的接口添加动态实现](docs/AttachedImplementor.md)