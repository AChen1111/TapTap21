# SceneLoader API

命名空间：`ZZ.SceneLoader`  
主入口：`SceneLoader`（静态类）  
配套组件：`ShenTuLoader`、`DontDestroyOnLoad`（全局脚本，无命名空间）  
事件：`AChen.Events.GameEvent.StartShowShenTu`  
演示：`Assets/Scripts/Common/SceneLoader/Tester.cs`（挂在 `SampleScene` 的 Tester 物体上）

业务代码只通过 `SceneLoader` 的公开方法切场景，不要直接调 `SceneManager.LoadScene` 来做「带持久场景的 Single 切换」，否则 persistent 保护不会生效。

---

## 能做什么

- 按场景名异步加载（`LoadScene`）。
- 加载期间播一张「神图」过场，后台并行加载，神图时间到且进度就绪后立刻激活新场景（`LoadSceneWithPic`）。
- 注册 / 解注册 **persistent scene**：之后用 `LoadSceneMode.Single` 切换时，这些场景不会被卸载。

所有公开加载方法都是 **fire-and-forget**（内部 `UniTask.Forget()`），调用后立即返回。完成通知靠可选回调。

---

## 前置条件

1. 目标场景必须在 **File → Build Settings** 里启用。当前工程已启用：
   - `Assets/Scenes/SampleScene.unity`
   - `Assets/Scenes/TestScene.unity`
2. 场景名参数是 **Scene 资源名**（不含路径、不含 `.unity`），例如 `"TestScene"`。
3. `LoadSceneWithPic` 需要场景里有能响应神图事件的物体（见 [神图过场](#神图过场shentu)）。

---

## `SceneLoader`

```csharp
using ZZ.SceneLoader;
using UnityEngine.SceneManagement;
```

### `LoadScene`

```csharp
public static void LoadScene(
    string sceneName,
    LoadSceneMode mode = LoadSceneMode.Single,
    Action onSceneLoadCompleteCallback = null)
```

异步加载 `sceneName`。默认按「切换」语义处理（见 [Single 的实际行为](#single-的实际行为)）。

| 参数 | 说明 |
|---|---|
| `sceneName` | Build Settings 中的场景名 |
| `mode` | `Single`：切换到该场景并卸载未保护场景；`Additive`：只叠加加载，不卸载其它场景 |
| `onSceneLoadCompleteCallback` | 新场景激活、该卸的场景卸完后调用。失败时**不会**调用 |

```csharp
SceneLoader.LoadScene("TestScene");
SceneLoader.LoadScene("TestScene", LoadSceneMode.Single, () =>
{
    Debug.Log("切到 TestScene 了");
});
```

### `LoadSceneWithPic`

```csharp
public static void LoadSceneWithPic(
    string sceneName,
    float picShowTime = 2f,
    LoadSceneMode mode = LoadSceneMode.Single,
    Action onSceneLoadCompleteCallback = null)
```

在切场景时播神图。加载与过场 **并行**：

1. 开始 `LoadSceneAsync`，若需要切换则先 `allowSceneActivation = false`（Unity 进度会停在 `0.9`）。
2. 派发 `GameEvent.StartShowShenTu`，时长为本次的 **`picShowTime`**（默认 2 秒，小于 0 按 0）。
3. `WhenAll`：等神图播完 **并且** `progress >= 0.9`。
4. `allowSceneActivation = true`，等待真正激活。
5. 若 `mode == Single`，把新场景设为 Active，卸载非 persistent 的其它已加载场景。
6. 调用完成回调。

| 参数 | 说明 |
|---|---|
| `sceneName` | Build Settings 中的场景名 |
| `picShowTime` | 神图总时长（秒）。前半淡入、后半淡出 |
| `mode` | 同 `LoadScene` |
| `onSceneLoadCompleteCallback` | 同 `LoadScene` |

加载比 `picShowTime` 快时：神图播完即可切，几乎没有额外等待。  
加载比 `picShowTime` 慢时：会等到进度就绪再激活（神图仍按该时长淡入淡出，多出来的等待可能露出旧场景）。

目标场景 **已经在内存里** 且 `mode == Single` 时：仍会按本次 `picShowTime` 播神图，然后只做激活 + 卸载其它场景，不再重复 `LoadSceneAsync`。

```csharp
SceneLoader.LoadSceneWithPic("TestScene", 1.5f, LoadSceneMode.Single, OnArrived);
SceneLoader.LoadSceneWithPic("TestScene"); // 默认播 2 秒
```

### `RegisterPersistentScene`

```csharp
public static void RegisterPersistentScene(string sceneName)
```

把 `sceneName` 加入 persistent 集合。之后凡是走 `LoadScene` / `LoadSceneWithPic` 且 `mode == Single` 的切换，**都不会卸载**该场景。

- 场景名为空：打 Error，不注册。
- 已在集合中：再注册是空操作（`HashSet`）。
- 场景尚未加载：会 **Additive** 异步加载；方法本身立刻返回，加载完成没有回调。
- 场景已经加载：只登记，不再加载。

应在「会被 Single 切走」之前注册。若先 `LoadScene(..., Single)` 再注册，当前这次切换已经把未保护场景卸掉了。

```csharp
SceneLoader.RegisterPersistentScene("SampleScene");
SceneLoader.LoadScene("TestScene");
// SampleScene 仍在，TestScene 为 Active
```

### `UnregisterPersistentScene`

```csharp
public static void UnregisterPersistentScene(string sceneName)
```

从 persistent 集合移除。

- **不会立刻卸载**该场景。
- 空字符串直接忽略。
- 下次 `LoadScene` / `LoadSceneWithPic`（`Single`）时，该场景若不是这次要留下的目标，就会被卸载。

```csharp
SceneLoader.UnregisterPersistentScene("SampleScene");
SceneLoader.LoadScene("TestScene");
// SampleScene 会被卸掉（若当前目标是 TestScene）
```

### `IsPersistentScene`

```csharp
public static bool IsPersistentScene(string sceneName)
```

是否已在 persistent 集合中。只看注册表，**不保证**该场景当前已加载。空名返回 `false`。

---

## Single 的实际行为

Unity 原生 `LoadSceneMode.Single` 会卸载 **所有** 已加载场景，persistent 无法存活。因此本模块对 `Single` 做了替换实现：

1. 用 **Additive** 加载目标场景（若尚未加载）。
2. `SceneManager.SetActiveScene(目标)`。
3. 遍历当前已加载场景，卸载「不是目标、也不在 persistent 集合里」的场景。

`mode == Additive` 时不走第 2、3 步：只加载，不改 Active，不卸载。

| 调用 | 效果 |
|---|---|
| `LoadScene("B")` | 加载 B，Active=B，卸掉除 B 与 persistent 以外的场景 |
| `LoadScene("B", Additive)` | 只把 B 叠加上来 |
| `LoadScene("A")` 且 A 已加载 | 不再加载，Active=A，按同样规则卸载其它 |

不要对「当前已加载且仍想完整重载」的场景依赖本 API：已加载的 `Single` 目标 **不会 Reload**。

---

## 神图过场（ShenTu）

### 事件

`LoadSceneWithPic` 会：

```csharp
EventCenter.Dispatch(GameEvent.StartShowShenTu, time); // time = 本次 picShowTime
```

`GameEvent.StartShowShenTu` 类型是 `EventId<float>`，参数为过场总时长（秒）。

### `ShenTuLoader`

挂在带 **CanvasGroup** 的 UI 上（例如全屏图）。

- `OnEnable` / `OnDisable` 订阅、退订 `StartShowShenTu`。
- 收到事件后：`ZFadeIn(time/2)` → `ZFadeOut(time/2)`（ZTween）。
- `Awake` 把 `alpha` 设为 0。缺少 `CanvasGroup` 会空引用。

过场 UI 若在将被卸载的场景里，切走后图会一起消失。需要盖住切换全过程时，给同一物体再挂 `DontDestroyOnLoad`（见下）。

### `DontDestroyOnLoad`

```csharp
// Assets/Scripts/Common/SceneLoader/DontDestroyOnLoad.cs
```

`Awake` 里对所在 GameObject 调用 `UnityEngine.Object.DontDestroyOnLoad`。挂在神图根节点上，切场景时 overlay 不会被卸掉。

注意：该脚本 **不会** 做单例去重。反复 Additive / 重新加载带该组件的原场景，可能复制多份 DontDestroy 物体。

---

## 回调与失败

- 完成回调在「激活 +（Single 时）卸载其它场景」之后、同一异步流程里调用。
- 没有失败回调。下列情况通常 **不会** 调完成回调：
  - 场景不在 Build Settings，`LoadSceneAsync` 返回 `null`
  - 场景名为空（仅注册 persistent 时会打 Error）
- 完成回调里抛错不会被 `SceneLoader` 单独兜住（会进 UniTask 的 `Forget` 异常处理）。
- 没有进度回调、没有取消、没有 `UniTask` 对外返回值。
- 没有并发保护：连续两次 `LoadScene` 会重叠，不建议连点。

等待完成的写法（业务侧自己包一层）：

```csharp
var tcs = new UniTaskCompletionSource();
SceneLoader.LoadScene("TestScene", LoadSceneMode.Single, () => tcs.TrySetResult());
await tcs.Task;
```

---

## 日志

失败时用 `AChen.Log.ALog.LogError`，前缀 `[SceneLoader]`：

- 注册 persistent 时场景名为空
- 持久场景 `LoadSceneAsync` 失败
- 普通加载 `LoadSceneAsync` 失败

成功路径默认不打日志。

---

## 场景与物体约定（本工程）

| 资源 | 作用 |
|---|---|
| `SampleScene` | 含 Tester、神图 UI（`ShenTuLoader` + `DontDestroyOnLoad`） |
| `TestScene` | 切换目标 |

`Tester` 在 `Awake` 里也会 `DontDestroyOnLoad`，避免切到 `TestScene` 后测试脚本被销毁。

---

## `Tester`（仅调试）

类名：`Tester`（全局）。Play 后可在 Inspector 用 Odin 按钮，或读静态字段给外部查询。

| 按钮 / 成员 | 作用 |
|---|---|
| `RunPipelineSuite` | 跑完整套件：注册 persistent → 切 TestScene → 解注册再切 → `LoadSceneWithPic` 回 SampleScene |
| `LoadTestSceneWithPic` | `LoadSceneWithPic("TestScene")` |
| `RegisterSamplePersistent` / `UnregisterSamplePersistent` | 注册 / 解注册 `SampleScene` |
| `LoadTestScene` | `LoadScene("TestScene")` |
| `LastReport` / `SuiteDone` / `SuitePassed` | 套件结果 |

套件断言超时 20 秒。不要在正式关卡逻辑里依赖 `Tester`。

---

## 推荐用法

**只切关卡：**

```csharp
SceneLoader.LoadScene("Battle");
```

**带神图：**

```csharp
SceneLoader.LoadSceneWithPic("Battle", 2f, LoadSceneMode.Single, () =>
{
    // 新关卡已 Active，非 persistent 旧场景已卸
});
```

**常驻 UI / 管理器场景：**

```csharp
SceneLoader.RegisterPersistentScene("UI");
// UI 未加载时会 Additive 加载；已在当前场景则可先加载再注册
SceneLoader.LoadScene("Battle");
```

离开常驻：

```csharp
SceneLoader.UnregisterPersistentScene("UI");
SceneLoader.LoadScene("Login"); // 下一次 Single 会卸掉 UI
```

---

## 限制（实现现状）

- 神图时长由每次 `LoadSceneWithPic` 的 `picShowTime` 传入（默认 2 秒）。
- `LoadSceneWithPic` 的淡出与真实加载时长不对齐时，慢加载可能在图淡完后仍露出旧场景。
- persistent 按 **场景名** 匹配；重名场景会一起被保护或一起留下。
- `RegisterPersistentScene` 的加载完成不可 await。
- 不提供 Unload 独立 API；卸载只发生在 `Single` 切换时。
- 不处理 `SceneManager.LoadSceneAsync` 之外的 Addressables / 分包加载。
