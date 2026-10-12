# FX System 使用说明

命名空间：`FXSystem`

---

## 1. 一次性接入

1. Unity 菜单 **FX → Create Default Settings**  
   会生成：
   - `Assets/FX/Resources/FXSettings.asset`
   - `Assets/FX/Resources/FXCatalog.asset`
2. **Create → FX → Definition**，填 Prefab 和 Id。
3. 把 Definition 拖进 Catalog 的 `Definitions` 列表。Id 不能重复；留空则用资产文件名。

进入 Play 时会自动创建 `[FXManager]`（`DontDestroyOnLoad`）。只要 Settings 放在 `Resources` 且名为 `FXSettings`，无需往场景里摆东西。

Inspector 试播：把 `FXPlayOnStart` 挂到任意物体上，填 Id 或拖 Definition。UI 试播勾选 `Play As UI`。

---

## 2. 世界特效

```csharp
using FXSystem;
using UnityEngine;

public class CombatVfx : MonoBehaviour
{
    public FXDefinition HitDef; // 也可以只在 Catalog 里配字符串 Id

    void OnHit(Vector3 point, Vector2 facing)
    {
        FX.Play("hit_slash", point);
        FX.Play("hit_slash", point, facing);          // 2D 朝向，绕 Z 转
        FX.Play(HitDef, point);

        // 挂到角色上（成为子物体；父物体销毁时该实例会从池里剔除）
        FX.PlayAttached("charge_loop", transform);

        // 跟随但不改父节点（目标销毁后自动回池，池不会被拆）
        FX.PlayFollow("aura", transform);
    }
}
```

循环特效（Definition 勾选 **Looping**）不会自动回收，必须 `Stop` 或切场景清理。

```csharp
FXHandle aura = FX.PlayAttached("charge_loop", transform);
// ...
aura.Stop();                 // 停止发射，粒子自然消完再回池
aura.Stop(immediate: true);  // 立刻清掉并回池
FX.StopAll();                // 默认立刻清掉全部
```

---

## 3. UI / Canvas 特效

Definition 的 **Space** 设为 **UI**。第一次播放 UI 特效时会自动创建 `[FXCanvas]`：

- 模式：**Screen Space - Camera**（粒子在 Overlay 下通常不可见）
- 排序、参考分辨率、平面距离在 `FXSettings` 里改

```csharp
// 屏幕像素，原点左下，与 Input.mousePosition 一致
FX.PlayUI("ui_click", (Vector2)Input.mousePosition);

// 世界坐标投影到 HUD
FX.PlayUIAtWorld("ui_lockon", enemy.position);

// 跟在某个 UI 元素上（不改父节点）
FX.PlayUI("ui_glow", buttonRect);

// 成为该 UI 的子物体
FX.PlayUIAttached("ui_charge", buttonRect);

FX.StopAllUI();
```

项目已有 HUD Canvas 时，启动时绑一次（同样建议 Screen Space - Camera）：

```csharp
void Start()
{
    FX.BindCanvas(hudCanvas);
}
```

### UI 制作注意

| 情况 | 建议 |
|------|------|
| 粒子 | Prefab 用 Particle System；Sorting Layer 用 `UI`（或与 HUD 同层） |
| Image / Animator | 没有粒子时按 Animator 播完回收；都没有则看 `ExtraLifetime` |
| 尺寸不对 | 按 UI 做的 Prefab 保持 `UIScale = 1`；把世界粒子当 UI 播时再加大 |
| 要压在 HUD 上面 | 勾选 `Override Canvas Sorting`，调 `Sorting Order` |

---

## 4. 句柄 `FXHandle`

`Play*` 都会返回句柄。这是值类型，回收后自动失效，不必置 `null`。

```csharp
FXHandle h = FX.Play("hit_slash", transform.position);

if (h.IsValid)
    h.Stop();

if (FX.IsValid(h)) { }
```

无效句柄：`FXHandle.Invalid`。播放失败（找不到 Id、没 Prefab、池满且策略为 Drop）也会返回它。

---

## 5. 播完回调

回调在实例**回池之后**触发，可以在里面 `FX.Play` 新特效。不要对**同一个**已经结束的句柄再 `Stop`。

```csharp
// 任意结束都通知
FX.Play("hit_slash", pos, () => ShowDamageNumber());

FX.Play("hit_slash", pos, (handle, reason) =>
{
    if (reason == FXCompleteReason.Finished)
        combo.NotifyHitFXDone();
});

FX.PlayUI("ui_click", (Vector2)Input.mousePosition, () => RefreshButton());

// 先播再挂
var h = FX.Play("hit_slash", pos);
h.OnFinished(() => { /* 仅自然播完 */ });
h.OnComplete((_, reason) => { /* 所有结束原因 */ });
```

写在参数里：

```csharp
var p = FXPlayParams.At(pos);
p.OnComplete = OnFxDone;
FX.Play("hit_slash", in p);
```

### `FXCompleteReason`

| 值 | 含义 |
|----|------|
| `Finished` | 粒子或动画自然播完 |
| `Stopped` | 调用了 `Stop` |
| `Recycled` | 池溢出回收最旧的、`StopAll`、切场景、跟随目标丢失 |
| `Destroyed` | 实例随父物体被销毁（`PlayAttached`） |
| `Dropped` | 根本没播出来（句柄无效）；例如池满且 Overflow = Drop |

循环特效不会出现 `Finished`，只有 Stop / 切场景等。

---

## 6. 进阶参数 `FXPlayParams`

常用字段：

| 字段 | 作用 |
|------|------|
| `Position` / `Rotation` / `Scale` | 世界姿态；Scale 会乘上 Prefab 原始缩放 |
| `FlipX` | 2D 翻转 |
| `Color` + `UseColor` | 覆盖粒子 Start Color |
| `Speed` | 粒子 `simulationSpeed` / Animator `speed` |
| `SortingOrderOffset` | 叠在 Definition 的 Sorting Order 上 |
| `Parent` + `Follow` | 跟随：`Follow=false` 成为子物体，`true` 只同步位置 |
| `IsUI` / `UseScreenPosition` / `ScreenPosition` | UI 播放 |
| `WorldCamera` | 世界坐标投到 Canvas 时用的相机，默认 `Camera.main` |
| `OnComplete` | 结束回调 |

```csharp
var p = FXPlayParams.At(hitPoint);
p.FlipX = facingLeft;
p.UseColor = true;
p.Color = Color.red;
p.Speed = 1.2f;
p.SortingOrderOffset = 10;
FX.Play("hit_slash", in p);
```

字符串 Id 和 `FXDefinition` 都可以播，方便脚本用 Id、Inspector 拖资产。

---

## 7. Definition 配置

资产：**Create → FX → Definition**

| 字段 | 说明 |
|------|------|
| `Id` | 业务字符串。空则用文件名 |
| `Prefab` | 粒子、或 Image+Animator |
| `Space` | `World` / `UI` |
| `UIScale` | 仅 UI 额外缩放 |
| `OverrideCanvasSorting` | UI 时在实例上覆盖 Canvas 排序 |
| `Looping` | 循环，必须手动 Stop |
| `ExtraLifetime` | 粒子/动画结束后再延迟回收的秒数；纯静态图也靠它 |
| `PrewarmCount` | 启动预热数量 |
| `MaxActive` | 该类型同时存在上限 |
| `Overflow` | 超上限策略 |
| `SortingLayer` / `SortingOrder` | 2D 渲染排序 |
| `RecycleOnSceneChange` | 切主场景时是否强制回收 |

### 溢出策略 `FXOverflowPolicy`

- **RecycleOldest**（默认）：回收最早的一个再播新的，旧的回调 `Recycled`
- **Expand**：允许暂时超出本类型上限，仍受全局硬顶
- **Drop**：丢弃本次播放，返回无效句柄，回调 `Dropped`

全局硬顶在 `FXSettings.GlobalMaxActive`（默认 256）。

---

## 8. 全局设置 `FXSettings`

必须能被 `Resources.Load("FXSettings")` 找到。

| 字段 | 说明 |
|------|------|
| `Catalog` | 特效目录 |
| `GlobalMaxActive` | 所有实例总数上限 |
| `LogWarnings` | 找不到 Id、池满等是否打 Warning |
| `ClearOnActiveSceneChange` | 切主场景时回收勾了 `RecycleOnSceneChange` 的实例 |
| `UICanvasSortingOrder` | 自动创建的 FX Canvas 排序 |
| `UIPlaneDistance` | Screen Space Camera 平面距离 |
| `UIReferenceResolution` | CanvasScaler 参考分辨率，默认 1920×1080 |

预热：

```csharp
FX.Prewarm("hit_slash");
FX.Prewarm(hitDef);
FX.PrewarmAll(); // 启动时 Catalog 里的项会预热一次
```

---

## 9. 运行时行为（简要）

- 每种 Definition 一个对象池，实例自己不 `Update`，由 Manager 统一检查是否结束。
- 句柄用 generation，回池后旧句柄失效。
- 切主场景默认清世界/UI 特效，Manager 和 Canvas 常驻。
- `PlayAttached`：父物体销毁会毁掉该实例（`Destroyed`），下次播放会重新 Instantiate。
- `PlayFollow` / `PlayUIFollow`：不改层级，目标没了会回池（`Recycled`）。

---

## 10. 最小战斗示例

```csharp
using FXSystem;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] string slashId = "hit_slash";
    [SerializeField] string chargeId = "charge_loop";

    FXHandle _charge;

    public void BeginCharge()
    {
        _charge = FX.PlayFollow(chargeId, transform);
    }

    public void Release(Vector3 hitPoint, Vector2 facing)
    {
        if (_charge.IsValid)
            _charge.Stop();

        FX.Play(slashId, hitPoint, facing, (handle, reason) =>
        {
            if (reason == FXCompleteReason.Finished)
                Debug.Log("刀光结束");
        });
    }
}
```

`Play(string, Vector3, Vector2, FXCompleteCallback)` 没有单独重载时，用参数结构体：

```csharp
var p = FXPlayParams.At(hitPoint);
p.Rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg);
p.OnComplete = (h, r) => { };
FX.Play(slashId, in p);
```

---

## 11. 常见问题

**Console 提示未找到 Catalog**  
先跑菜单 **FX → Create Default Settings**，确认文件在 `Assets/FX/Resources/FXSettings.asset`。

**提示未找到 Id**  
Definition 没拖进 Catalog，或 Id 与代码字符串不一致。

**UI 粒子看不见**  
不要用 Overlay Canvas。用自动创建的 `[FXCanvas]`，或 `BindCanvas` 到 Screen Space - Camera。Sorting Layer 设为 `UI`。

**特效尺寸不对**  
世界粒子放到 UI 上会被 Canvas 缩放；UI Definition 调 `UIScale`。世界特效检查 Prefab 本身缩放。

**循环特效停不掉**  
勾了 Looping 就不会自动结束，必须 `handle.Stop()` 或 `FX.StopAll()`。

**回调没来**  
Looping 不会 `Finished`。只想自然结束用 `OnFinished`。池满 Drop 时走 `Dropped`，且句柄无效。
