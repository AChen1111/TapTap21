# 事件系统

命名空间 `AChen.Events`。代码在 `Assets/Scripts/Common/Event/`。进程内总线，不跨进程、不持久化。

类型写在事件上，不写在调用处。`AddListener` / `Dispatch` 本身不带你手写的泛型，编译器从 `EventId<T>` 推断，参数对不上会编译失败；同一事件名被两种签名使用时运行时也会抛错。

## 先定义事件

在 `GameEvent` 里声明，不要在业务里 `new EventId`。

```csharp
namespace AChen.Events
{
    public static class GameEvent
    {
        public static readonly EventId GameStarted = new EventId("Demo.GameStarted");
        public static readonly EventId<int> ScoreChanged = new EventId<int>("Demo.ScoreChanged");
        public static readonly EventId<int, string> RankChanged = new EventId<int, string>("Demo.RankChanged");
    }
}
```

`EventId` / `EventId<T>` / `EventId<T1, T2>` 分别对应 0 / 1 / 2 个参数。

## 订阅 / 取消 / 发布

```csharp
using AChen.Events;

void OnEnable()
{
    EventCenter.AddListener(GameEvent.ScoreChanged, OnScoreChanged);
}

void OnDisable()
{
    EventCenter.RemoveListener(GameEvent.ScoreChanged, OnScoreChanged);
}

void OnScoreChanged(int score)
{
    // 刷新 UI
}

void AddScore(int score)
{
    EventCenter.Dispatch(GameEvent.ScoreChanged, score);
}
```

无参数、两个参数：

```csharp
EventCenter.AddListener(GameEvent.GameStarted, OnStarted);
EventCenter.Dispatch(GameEvent.GameStarted);

EventCenter.AddListener(GameEvent.RankChanged, OnRankChanged);
EventCenter.Dispatch(GameEvent.RankChanged, 1, "ok");
```

`Dispatch(GameEvent.ScoreChanged, "错")` 编译不过。`GameEvent` 里已有示例 `ScoreChanged`。

## 注意事项

- 新事件只在 `GameEvent` 加 `EventId`，调用处传这个字段。
- 必须成对 `RemoveListener`，否则物体销毁后仍会被调用。建议在 `OnEnable` / `OnDisable` 绑定。
- 没有订阅者时 `Dispatch` 是空操作，不会报错。
- 监听器按注册顺序同步执行。回调里不要再改同一事件的订阅列表，也不要做重活。
- 调试日志走 `AChen.Log` 的 Event 分类。关 Player Logs 后不再打印订阅/派发明细。
- 这是静态中心，Domain Reload 会清空。Play Mode 停掉后再进，旧订阅不会留下。
