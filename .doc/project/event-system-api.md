## 事件中心（`AChen.Events`）

### 定义事件

| API                         | 何时使用                                                   |
| --------------------------- | ---------------------------------------------------------- |
| `new EventId(name)`         | 定义无参数通知，例如“游戏开始”                             |
| `new EventId<T>(name)`      | 定义携带一个强类型数据的事件，例如“分数变化”               |
| `new EventId<T1, T2>(name)` | 两项数据天然属于同一通知时；更多数据建议封装成一个参数对象 |

事件统一定义在 `GameEvent`，业务调用处不要临时创建 `EventId`。事件名建议使用“模块.事件”格式并保持唯一。

### 订阅与派发

| API                                            | 何时使用                 | 说明                                            |
| ---------------------------------------------- | ------------------------ | ----------------------------------------------- |
| `EventCenter.AddListener(eventId, handler)`    | 对象启用并需要接收通知时 | 推荐在 `OnEnable` 调用；参数类型由 EventId 推断 |
| `EventCenter.RemoveListener(eventId, handler)` | 对象禁用或销毁前         | 推荐在 `OnDisable` 调用，必须与订阅成对         |
| `EventCenter.Dispatch(eventId)`                | 发布无参数通知时         | 没有监听器是正常空操作                          |
| `EventCenter.Dispatch(eventId, arg)`           | 发布单参数通知时         | 编译器检查参数类型                              |
| `EventCenter.Dispatch(eventId, arg1, arg2)`    | 发布双参数通知时         | 所有监听器按注册顺序同步执行                    |

事件中心适合同一进程内模块解耦，不用于跨进程通信、持久化消息或需要返回值的请求。派发是同步的，监听器中不要执行耗时工作。

## 快速选择

| 需求                             | 使用                                   |
| -------------------------------- | -------------------------------------- |
| 打开页面或 HUD                   | `UIFrame.OpenWindow` / `ShowPanel`     |
| 输出可筛选日志                   | `ALog.Log` / `LogWarning` / `LogError` |
| 高频创建和销毁同类 Prefab        | `GameObjectPool.Get` / `Release`       |
| 一个模块通知多个无直接依赖的模块 | `EventCenter.Dispatch` + `AddListener` |
| 业务代码需要引用 Prefab 名称     | `PrefabNameSpace.常量名`               |
