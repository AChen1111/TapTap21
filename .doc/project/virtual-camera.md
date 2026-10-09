# 虚拟相机

业务不直接改 Cinemachine。换跟随目标、换画面边界，都通过 `GameEvent` 派发。`CameraController` 挂在虚拟相机上，负责听这两个事件并写到组件上。

事件定义在 `Assets/Scripts/Common/Event/GameEvents.cs`，监听在 `Assets/Scripts/GamePlay/Camera/CameraController.cs`。`SampleScene` 里已有 `Main Camera`（`CinemachineBrain`）和 `CinemachineCamera`（`CinemachineCamera`、`CinemachineFollow`、`CameraController`）。

| 事件 | 参数 | 作用 |
|---|---|---|
| `GameEvent.BindCamera2Transform` | `Transform` | 把虚拟相机的 Follow 设成这个物体，相机跟着它走 |
| `GameEvent.SetCameraBound` | `PolygonCollider2D` | 把这个多边形设成画面边界，相机不会跟出这块区域 |

事件名分别是 `GamePlay.Camera.BindCamera2Transform`、`GamePlay.Camera.SetCameraBound`。调用处传 `GameEvent` 上的字段，不要自己 `new EventId`。

---

## 绑定 Follow

玩家出生、切控制对象、或镜头要改跟另一个物体时派发。后一次派发覆盖上一次的目标。

```csharp
using AChen.Events;
using UnityEngine;

void FollowPlayer(Transform player)
{
    EventCenter.Dispatch(GameEvent.BindCamera2Transform, player);
}
```

`CameraController` 收到后执行 `CinemachineCamera.Follow = tf`。跟随的手感（阻尼、偏移）在同一物体的 `CinemachineFollow` 上调，事件不改这些值。`SampleScene` 里当前偏移是 `(0, 0, -10)`，三轴阻尼都是 `1`。

当前正在跟谁，读：

```csharp
Transform target = CameraController.Instance.nowFollowedTransf;
```

`CameraController` 是场景单例，虚拟相机物体启用后才能派发到。物体被关掉时没有监听者，`Dispatch` 是空操作，Follow 保持原样。

---

## 设置边界

1. 在关卡里放一个物体，加上 **PolygonCollider2D**，用多边形圈出相机允许活动的范围。这个碰撞体只给 Cinemachine 读形状，不必和角色发生物理碰撞。
2. 在 `CinemachineCamera` 上加上 **Cinemachine Confiner 2D**。`SampleScene` 里还没有这个组件，用边界之前要先加上。
3. 进入该区域时派发边界：

```csharp
using AChen.Events;
using UnityEngine;

void ApplyLevelBound(PolygonCollider2D levelBound)
{
    EventCenter.Dispatch(GameEvent.SetCameraBound, levelBound);
}
```

`CameraController` 收到后把 `CinemachineConfiner2D.BoundingShape2D` 设成这块碰撞体。再派发一次会换成新的多边形。换关、换房间时传那一关自己的 `PolygonCollider2D`。

虚拟相机物体上没有 `CinemachineConfiner2D` 时，这次派发会空引用。先加组件，再派事件。

---

## Inspector 里试

`CameraController` 上有两个 Odin 按钮，走的是同一条事件，方便在运行时试：

| 按钮 | 参数 | 效果 |
|---|---|---|
| `DebugInvokeBindEvent` | 一个 `Transform` | 派发 `BindCamera2Transform` |
| `DebugInvokeSetBoundEvent` | 一个 `PolygonCollider2D` | 派发 `SetCameraBound` |

进 Play 后在 Inspector 里填参数再点按钮。正式逻辑用上面的 `EventCenter.Dispatch`，不要依赖这两个按钮。
