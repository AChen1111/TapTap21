# 玩家输入与移动

这套代码负责：用 Input System 读键盘，按 `EInputState` 决定哪些操作生效，再驱动 2D 刚体做左右移动和跳跃。

| 模块 | 命名空间 | 入口 |
|---|---|---|
| 输入 | `Player.Input` | `PlayerInput` |
| 移动 | `Player.Movement` | `PlayerMovement` |
| 数值 | `Player.Movement` | `PlayerMovementDataSO` |
| 动作表 | 全局生成类 | `GameInput`（不要手改） |
| Flag 运算 | `Common.FlagsUtility` | `FlagsUtility` |

---

## 实现的功能

### 移动

`_inputState` 包含 `EInputState.Move` 时，A/D 控制左右移动。水平速度写成 `输入 X × MoveSpeed`，竖直速度不动，重力不受影响。松手时输入 X 为 0，水平速度立刻变成 0。W/S 在动作表里仍有 Y 分量，移动不用它。

### 跳跃

`_inputState` 包含 `EInputState.Jump` 时，空格按下的瞬间跳一次。剩余次数大于 0 才生效：先把竖直速度清成 0，再沿向上方向加 `JumpForce` 冲量，然后次数减 1。清竖直速度是为了下坠时起跳高度稳定，多段跳也不会把向上速度叠高。

脚部触发器 `_foot` 重叠到 Tag 为 `Ground` 的碰撞体时，次数回到 `JumpTimes`。身体上的实体碰撞体碰到地面不算落地。

### 多段跳跃

总次数由 `PlayerMovementDataSO.JumpTimes` 决定，当前资产是 `2`：站在地上可以跳一次，离开地面后还能再跳一次。

- 在地上起跳：次数先减 1。离开地面时，若剩余次数已经不大于 `JumpTimes - 1`，不再减。`JumpTimes = 2` 时地上跳完还剩 1，空中可以再跳。
- 不跳、直接走下平台：次数收到 `JumpTimes - 1`。`JumpTimes = 2` 时走下平台后还剩 1 次空中跳；`JumpTimes = 1` 时走下平台后剩 0，空中不能跳。
- 再次被 `_foot` 碰到 `Ground`：次数加满，多段跳重置。

把 `JumpTimes` 改成 3 就是地面一次加空中两次，规则相同。

---

## 文件

| 路径 | 作用 |
|---|---|
| `Assets/Scripts/Common/Input/GameInput.inputactions` | 动作定义。改键后重新生成 `GameInput.cs` |
| `Assets/Scripts/Common/Input/GameInput.cs` | Input System 生成的包装，业务只通过 `PlayerInput` 用它 |
| `Assets/Scripts/Player/Input/EInputState.cs` | 输入状态枚举：`None` / `Move` / `Jump` / `ALL` |
| `Assets/Scripts/Player/Input/PlayerInput.cs` | 创建动作表、按 Flag 过滤、向外发事件 |
| `Assets/Scripts/Player/Movement/PlayerMovementDataSO.cs` | 移速、跳跃冲量、跳跃次数 |
| `Assets/Scripts/Player/Movement/SO/PlayerMovementDataSO.asset` | 当前一份数据：移速 `6.82`，跳跃力 `4`，次数 `2` |
| `Assets/Scripts/Player/Movement/PlayerMovement.cs` | 订阅输入，写刚体速度 |
| `Assets/Scripts/Common/FlagsUtility/FlagsUtility.cs` | 枚举 Flag 的添加、删除、检测、清空 |

当前按键（`GameInput`）：

| 动作 | 类型 | 键 |
|---|---|---|
| `Player/Move` | `Vector2` | W A S D（2D Vector 复合） |
| `Player/Jump` | Button | Space，取 `started`（按下瞬间） |

---

## 怎么接到角色上

1. 角色物体上放 **Rigidbody2D**（Dynamic）、身体用的非 Trigger **Collider2D**，再放 `PlayerInput` 和 `PlayerMovement`。`PlayerMovement` 用 `RequireComponent` 要求后两个组件，缺了 Unity 会自动补脚本，刚体需要自己加。
2. 再放一个脚部 **Collider2D**，勾上 **Is Trigger**，不要单独再加刚体，拖到 `PlayerMovement` 的 `_foot`。只有这个触发器碰到地面才算落地。没拖会打错误日志，跳跃次数不会回满。
3. 地面碰撞体的 Tag 设为 **`Ground`**（工程 Tag 里已经有）。`_foot` 碰到这个 Tag 会回满跳跃次数，离开会按「走下平台」收一次。
4. 建一份 `PlayerMovementDataSO`：菜单 `Scriptable Objects/PlayerMovementDataSO`，拖到 `PlayerMovement` 的 `_movementDataSO`。不拖的话 `Awake` 打错误日志并直接返回，跳跃次数保持 0，角色跳不起来。
5. `PlayerInput` 的 `_isAutoEnable` 默认勾上。进 Play 后移动和跳跃都开。

业务侧不要自己 `new GameInput()`，也不要直接改 `Rigidbody2D.linearVelocity` 来做这套移动，否则和 `PlayerMovement` 每帧写速度打架。

---

## 输入怎么用

```csharp
using Player.Input;
```

### 听事件

`PlayerMovement` 已经在 `OnEnable` / `OnDisable` 里订阅。别的系统要听的话同样成对订阅：

```csharp
void OnEnable()
{
    _playerInput.OnInputMove += OnMove;
    _playerInput.OnInputJump += OnJump;
}

void OnDisable()
{
    _playerInput.OnInputMove -= OnMove;
    _playerInput.OnInputJump -= OnJump;
}
```

| 事件 | 何时触发 |
|---|---|
| `OnInputMove` | `_inputState` 包含 `EInputState.Move` 时，**每帧**把当前 `Vector2` 发出去。松手是 `(0,0)`，不是「只在有输入时才发」 |
| `OnInputJump` | `_inputState` 包含 `EInputState.Jump`，且空格进入 `started` 时发一次。没有参数 |

两个事件在没有订阅者时是空操作（`?.Invoke`）。

### `EInputState`

`EInputState` 是 `[Flags]` 枚举，当前值存在 `PlayerInput._inputState` 里，Inspector 上只读。它表示现在允许哪些玩家操作，不是物理按键。

```csharp
public enum EInputState
{
    None = 0,
    Move = 1 << 0,
    Jump = 1 << 1,
    ALL  = Move | Jump,
}
```

| 方法 | 行为 |
|---|---|
| `EnableAllInput()` | `_inputState = ALL`，并 `GameInput.Enable()` |
| `DisableAllInput()` | Flag 清成 `None`，并 `GameInput.Disable()`。之后按键进不来 |
| `EnableInput(state)` | 把 `state` 并进当前 Flag，并重新 `Enable()` 整张动作表。`None` 会被拒绝并打 Warning |
| `DisableInput(state)` | 只从 Flag 里拿掉对应位，**不会**关掉动作表 |

`Awake` 里 `_isAutoEnable == true` 走全开，否则走全关。

组合示例：

```csharp
_playerInput.DisableInput(EInputState.Jump);   // 还能走，不能跳
_playerInput.EnableInput(EInputState.Jump);    // 跳恢复

_playerInput.DisableAllInput();
_playerInput.EnableInput(EInputState.Move);    // 只恢复移动；跳跃 Flag 仍是关的
```

Flag 运算在 `FlagsUtility`：`Add` / `Remove` / `Has` / `Clear` 都返回新值，调用处必须写回，例如 `_inputState = _inputState.Add(state)`。内部用 `Enum.ToObject` 转回枚举，避免 `int` 枚举和 `long` 拆箱时的 `InvalidCastException`。

### 动作表生命周期

`Awake` 里 `new GameInput()`。组件启用时订阅 `Jump.started`，禁用时退订。物体销毁时：

```csharp
_input.Player.Disable();
_input.Dispose();
_input = null;
```

`Dispose` 只销毁内部的 `InputActionAsset`。生成代码的析构会检查 `Player` 这张表是否已经 `Disable`，所以两步都要做，否则销毁后控制台报动作表泄漏。

---

## 移动怎么用

改手感只改 `PlayerMovementDataSO`：

| 字段 | 含义 |
|---|---|
| `MoveSpeed` | 水平速度，单位是每秒。当前 `6.82` |
| `JumpForce` | 向上冲量，交给 `AddForce(..., ForceMode2D.Impulse)`。当前 `4` |
| `JumpTimes` | 落地后拥有的跳跃次数，Inspector 限制在 1～999。当前 `2`（地面一次 + 空中一次） |

运行时三个值都是只读属性，代码里不要改 SO。

`PlayerMovement` 没有对外方法。玩法脚本若要禁移动或禁跳，改 `PlayerInput` 的 `EInputState`，不要关 `PlayerMovement` 组件来暂停操作——关掉组件只会退订事件，刚体上一次的水平速度还在。

---

## 大致实现

### 输入过滤

```
Awake: new GameInput，按 _isAutoEnable 全开或全关
每帧 Update:
    没有 Move 位 -> 不发事件
    有 Move 位   -> 读 Player/Move，Invoke OnInputMove
空格 started:
    没有 Jump 位 -> 丢掉
    有 Jump 位   -> Invoke OnInputJump
```

移动是轮询，跳跃是按下边沿。W/S 仍在 `Move` 的 Y 分量里，移动逻辑不用 Y。

### 写速度

`Move` 只改水平速度，竖直速度留给重力和跳跃：

```csharp
_rb2d.linearVelocity = new Vector2(moveDir.x * _movementDataSO.MoveSpeed, _rb2d.linearVelocity.y);
```

这里不乘 `Time.deltaTime`。`linearVelocity` 已经是每秒位移，物理步会自己用 `fixedDeltaTime` 积分。再乘 `deltaTime` 会把速度缩小成约一帧的位移，角色几乎不动，而且跟着帧率变。

因为 `_inputState` 包含 `EInputState.Move` 时每帧都会收到向量（含零向量），松手时 `moveDir.x == 0`，水平速度会被写成 0，所以松手即停。

`Jump`：

```csharp
if (_jumpTimes <= 0) return;
_rb2d.linearVelocity = new Vector2(_rb2d.linearVelocity.x, 0);
_rb2d.AddForce(Vector2.up * _movementDataSO.JumpForce, ForceMode2D.Impulse);
_jumpTimes--;
```

起跳前先把竖直速度清零，避免下坠速度抵消冲量，也避免多段跳把向上速度叠得越来越高。然后扣一次次数。

### 跳跃次数

`Awake` 把 `_jumpTimes` 设成 SO 里的 `JumpTimes`。

| 情况 | 次数怎么变 |
|---|---|
| `_foot` 持续重叠 Tag 为 `Ground` 的碰撞体（`OnTriggerStay2D`，且 `_foot.IsTouching`） | 回满到 `JumpTimes` |
| 按下跳跃且次数 > 0 | 减 1，并给向上冲量 |
| `_foot` 离开 `Ground`，且当前次数大于 `JumpTimes - 1` | 收到 `JumpTimes - 1` |

第三条是为了「走下平台」扣掉原本站在地上的那一次，同时不要把刚才已经跳过的那一次再扣一遍。

`JumpTimes = 2` 时：

- 站在地上走下去：`2` 收到 `1`，空中还能跳一次。
- 站在地上按下跳跃：先变成 `1`，离开地面时 `1` 并不大于 `1`，不再减。空中再跳一次变成 `0`。
- 落地：回到 `2`。

`JumpTimes = 1` 时走下平台会收到 `0`，空中不能跳；在地上跳会先变成 `0`，离地不再减。

---

## 使用时要注意

- 地面碰撞体不要勾 Trigger，角色刚体是 Dynamic。`_foot` 要勾 **Is Trigger**，并挂在这个刚体上（可以是子物体，但不能再挂一个 `Rigidbody2D`）。落地走 `OnTriggerStay2D`，离地走 `OnTriggerExit2D`。触发器回调里的 `other` 是对方碰撞体，所以再用 `_foot.IsTouching(other)` 确认是脚碰到的。对方仍要带 `Ground` Tag。身体侧面或头顶碰到地面不会改跳跃次数。
- `_foot` 从一块 `Ground` 走到另一块时，离开前一块会先按离地把次数收到 `JumpTimes - 1`。只要 `_foot` 还重叠着另一块，同一物理帧里的 `OnTriggerStay2D` 会把次数加满，所以换到另一块地面不会少一次跳。次数只在 `_foot` 完全离开所有 `Ground` 之后才会停在 `JumpTimes - 1`。
- `DisableInput(EInputState.Move)` 之后 `MovementInput` 直接 return，不再把水平速度写成 0，角色会带着关掉前的速度滑行。需要立刻停下时，去掉 `EInputState.Move` 之后自己把 `linearVelocity.x` 置 0。
- `EnableInput` 打开的是整张 `GameInput`，不是单独一个 Action。真正拦不拦得住，靠 Flag 上的 `Has`。
- 改键编辑 `GameInput.inputactions` 并重新生成，不要改 `GameInput.cs`。生成文件头写了手改会在下次生成时丢掉。
