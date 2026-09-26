# 计时器

业务用名字启动一个倒计时，到点执行一次回调。实例来自全局 `GameObjectPool`，用完要 `DeleteTimer` 手动还回去。


| 模块     | 命名空间            | 入口                        |
| ------ | --------------- | ------------------------- |
| 对外 API | `Common.Timer`  | `TimerUtil`               |
| 计时实例   | `Common.Timer`  | `Timer`                   |
| 预热和取还  | 全局类             | `TimerPool`               |
| 状态     | `Common.Timer`  | `ETimerState`             |
| 对象池    | `AChen.Pooling` | `GameObjectPool.Instance` |


不要自己 `Instantiate` 计时器 Prefab，也不要直接 `GameObjectPool.Release` 一个还挂在 `TimerUtil` 字典里的实例。取还都走 `TimerUtil`。

---

## 使用方法

### 场景里要有什么

1. 场景中放一个带 `TimerPool` 的物体。`TimerPool.Instance` 只做 `FindAnyObjectByType`，找不到就是空引用，不会自动创建。
2. 把 `Assets/Prefabs/Timer/Timer.prefab` 拖到 `TimerPool` 的 `_prefab`。这个 Prefab 根节点上要有 `Timer`。
3. `_warmCount` 是进播放时预先创建并立刻归还的数量，Inspector 范围 1～100。当前场景里是 20。`_isAutoWarm` 勾上时，`Awake` 里会做这次预热。
4. 场景里可以再放一个 `GameObjectPool`。不放也行，`TimerPool` 第一次用 `GameObjectPool.Instance` 时会自己建一个并跨场景保留。全局只使用这一个池，`Timer` 按 Prefab 分在自己的桶里。



### 启动

```csharp
using Common.Timer;

TimerUtil.StartTimer("Action1", 3f, () =>
{
    // 到点执行一次
});
```


| 参数           | 含义                                                     |
| ------------ | ------------------------------------------------------ |
| `name`       | 这个计时器的名字。字典的键                                          |
| `time`       | 倒计时秒数，用 `Time.deltaTime` 累加                            |
| `OnComplete` | 到点回调，可省略。再次 `StartTimer` 同一个名字会换成这次传入的回调，传 `null` 就是清掉 |


名字还不存在：从对象池取出一个 `Timer`，放进字典，然后开始。  
名字已经存在：复用字典里的那个实例，换上新回调，再开始。不会再向对象池要一个。

```csharp
TimerUtil.HasTimer("Action1"); // 启动后为 true，DeleteTimer 之后为 false
```



### 暂停、重置、重开

```csharp
TimerUtil.PauseTimer("Action1");   // 停在当前进度
TimerUtil.ResetTimer("Action1");   // 进度清零，状态回到 Ready，不会到点
TimerUtil.ReStartTimer("Action1"); // 进度清零，用上一次的总时长再走一遍
```

三个方法在名字不存在时直接返回，不抛异常。

- `PauseTimer` 只把状态改成 `Paused`，已走过的时间还在。
- `ResetTimer` 状态改成 `Ready`，已走时间清零。之后不会自己继续走，要再调用 `StartTimer` 或 `ReStartTimer`。
- `ReStartTimer` 先重置，再用当前的 `TotalTime` 调用 `StartTimer`。它不接收新的秒数，也不换回调。

暂停之后想用新的秒数再跑，对同一个名字再调 `StartTimer`。见下面「同名再启动」。

### 归还对象池

```csharp
TimerUtil.DeleteTimer("Action1");
```

名字不存在时直接返回。存在时把这个实例 `Release` 回 `GameObjectPool`，并从字典删除。归还后物体被禁用，`HasTimer` 为 false。下次同名 `StartTimer` 会重新从池里取（池里有空闲的就复用，没有就新建）。

到点**不会**自动 `DeleteTimer`。回调跑完后，这个名字仍然占着字典里的实例。一批一次性计时器如果只 `StartTimer`、从不 `DeleteTimer`，预热数量用完后会一直 `Instantiate`。

可以在回调里自己删：

```csharp
TimerUtil.StartTimer("Action1", 3f, () =>
{
    TimerUtil.DeleteTimer("Action1");
});
```

不要在这次到点回调里 `DeleteTimer` 之后，立刻用同一个名字再 `StartTimer`。到点回调返回后，`Update` 还会对当前这个实例再执行一次 `ResetTimer()`，会把刚启动的新计时清掉。要接着开下一个，放到回调外面，或换一个名字。

### 同名再启动

```csharp
TimerUtil.StartTimer("Action1", 5f, OnNext);
```

字典里已经有 `Action1` 时，不会取新实例，也不会抛异常。它会换回调，并把总时长改成 5 秒，状态改成 `Started`。

已走过的时间不会清零。如果上次已经走到 2 秒，这次 5 秒的计时会从 2 秒继续。需要从 0 开始时，先 `ResetTimer` 再 `StartTimer`，或直接 `ReStartTimer`（总时长仍是上一次的，不改秒数）。

到点之后实例会被 `ResetTimer`，已走时间已经是 0。这时同名再 `StartTimer` 就是从头开始。

### 查询状态和已走时间

```csharp
ETimerState state = TimerUtil.GetTimerState("Action1");
float elapsed = TimerUtil.GetTimerElapsed("Action1");
```


| 方法                | 名字存在                        | 名字不存在                       |
| ----------------- | --------------------------- | --------------------------- |
| `GetTimerState`   | 打错误日志，返回 `ETimerState.None` | 打错误日志，返回 `ETimerState.None` |
| `GetTimerElapsed` | 返回 `_curTime`（已走秒数）         | 打错误日志，返回 `-1`               |


`GetTimerState` 目前无论名字在不在，都返回 `None`，不会读到 `Ready` / `Started` / `Paused`。

到点之后实例会被 `ResetTimer`，已走时间变成 `0`，状态回到 `Ready`。要看结束前的进度，在到点回调里先读，再重置或 `DeleteTimer`。

`ETimerState`：


| 值         | 含义                          |
| --------- | --------------------------- |
| `Ready`   | 未在计时。刚取出、重置后、到点后都是这个        |
| `Started` | `Update` 里累加时间              |
| `Paused`  | 停住，进度保留                     |
| `None`    | 查询时表示没有这个名字。正常计时过程中不会处于这个状态 |




### 谁拿着实例

`TimerUtil._timers` 是 `Dictionary<string, Timer>`。它表示「这个名字当前对应哪一个池对象」，不是对象池的空闲栈。

```
StartTimer 名字不存在
    TimerPool.Get → GameObjectPool.Get
    OnTakenFromPool → ResetTimer（Ready，时间为 0）
    字典 Add
    写入 OnComplete
    Timer.StartTimer：TotalTime = time，状态 Started

StartTimer 名字已存在
    取出字典里的同一个 Timer
    覆盖 OnComplete
    Timer.StartTimer：不改 _curTime

DeleteTimer
    GameObjectPool.Release
    OnReturnedToPool → ResetTimer
    物体 SetActive(false)，放进该 Prefab 桶的 InActive
    字典 Remove
```

`TimerPool.Warm` 在 `Awake` 里按 `_warmCount` 做一遍 `Get` 再 `Release`，让空闲栈里先有实例。这是唯一在 `TimerUtil` 之外调用 `Release` 的地方。

### 计时怎么走

只有状态是 `Started` 时，`Update` 才执行 `_curTime += Time.deltaTime`。用的是受 `timeScale` 影响的缩放时间。

时间是一帧一帧加上去的。某一帧加上 `Time.deltaTime` 之后，`_curTime` 第一次大于等于 `TotalTime`，就在这一帧调用回调。所以到点时刻最多晚大约一帧（60 帧时大约 16 毫秒），没有额外的固定延迟。`timeScale` 小于 1 时，按墙上的钟会走得更慢；某一帧卡很久时，回调会跟着那一帧才发生。

```csharp
if (_curTime >= TotalTime)
{
    OnComplete?.Invoke();
    ResetTimer();
}
```

到点后状态变回 `Ready`，时间为 0，所以下一帧不会再进回调。回调本身抛错的话，后面的 `ResetTimer` 不会执行，下一帧还会再进一次。

`TotalTime` 的 setter 拒绝小于等于 0 的值，并打错误日志，原总时长不变。但 `StartTimer` 仍然会把状态设成 `Started`。对一个刚取出的实例传入 0 或负数时，总时长还是字段初值 1 秒。

`PauseTimer` 只改状态。`ResetTimer` 把状态设为 `Ready` 并把 `_curTime` 设为 0，总时长不动。`ReStartTimer` 是 `ResetTimer` 再 `StartTimer(_totalTime)`。

### 和对象池的关系

`Timer` 实现 `IPoolable`。`GameObjectPool` 按 Prefab 根物体分桶，取出时激活并调用 `OnTakenFromPool`，归还时调用 `OnReturnedToPool`、禁用、放进 `InActive`。

`DeleteTimer` 先 `Release` 再从字典删除。名字不存在则什么都不做。不要在 `Release` 之后还用这个引用启动它，下一次应通过 `TimerUtil.StartTimer` 重新取。

到点只重置、不归还，是为了同名计时器可以留在字典里反复 `StartTimer` / `ReStartTimer`，不用每次都进对象池。一次性计时器由调用方在不需要这个名字时 `DeleteTimer`。