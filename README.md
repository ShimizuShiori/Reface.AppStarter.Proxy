# Reface.AppStarter.Proxy

## 1 介绍

Reface.AppStarter 的功能模块，使用此模块，可以轻松的建立 AOP 和 接口的动态实现。

## 1.1 依赖

* *Reface.AppStarter* >= 1.1.0
* *Caslte.DynamicProxy* >= 2.2.0

## 1.2 Nuget 页面

https://www.nuget.org/packages/Reface.AppStarter.Proxy

## 1.3 提供的功能

* 作为 *Reface.AppStarter* 的一个子模块
* 通过 *Attribute* 创建 **AOP** 切面
* 通过 *Attribute* 创建接口的动态实现

## 2 使用方法

如同其它 *Reface.AppStarter* 的 *AppModule* 一样，
首先你需要创建一个 *AppModule*，
然后加上 *ProxyAppModule* 的特征，
即可开启该模块提供的功能。

下面是各种功能的详细说明
* [向 class 添加 AOP](docs/ProxyAttribute.md)
* [向 interface 添加动态实现](docs/ImplementorAttribute.md)