# 单向平台

组件：`GamePlay.Environment.Platform.OneWayPlatform2D`

代码位置：`Assets/Scripts/GamePlay/Environment/Platform/OneWayPlatform2D.cs`

## 配置

在平台根节点挂载 `OneWayPlatform2D`。根节点或子节点放置一个非 Trigger 的 `BoxCollider2D`，并将平台物体设为 `Ground` Tag。组件会自动打开 `PlatformEffector2D` 的单向碰撞配置，并将子节点碰撞体设置为 `Used By Effector`。

平台碰撞体只做可站立的薄横条，放在蘑菇顶部、蕨类顶端或树枝表面。平台根节点的本地上方向是承重方向。

运行规则：

- 玩家从下方上跳时可以穿过平台；
- 玩家向下运动且脚部位于平台顶面附近时才算落地；
- 从平台上按住 S 时，PlayerMovement 调用 `DropThrough` 临时忽略玩家与平台碰撞，玩家向下穿过；
- `DropThroughTime` 到期后恢复碰撞；
- 玩家移动平台时仍会继承平台的水平速度。

## Inspector 参数

| 参数 | 默认值 | 作用 |
|---|---:|---|
| `Drop Through Time` | 0.25 | 按 S 下穿时暂时忽略碰撞的时间 |
| `Use One Way` | 开启 | 开启单向平台 |
| `Use One Way Grouping` | 开启 | 玩家多个 Collider 一起遵循单向规则 |
| `Use Side Friction` | 关闭 | 关闭侧面摩擦，避免边缘卡住 |
| `Use Side Bounce` | 关闭 | 关闭侧面反弹 |

## 代码调用

```csharp
using GamePlay.Environment.Platform;

OneWayPlatform2D platform = GetComponent<OneWayPlatform2D>();
platform.DropThrough(player.gameObject);
```

PlayerMovement 已经接收移动输入的 Y 分量：向下输入时，如果脚下目标是 `OneWayPlatform2D`，会调用 `DropThrough`。当前输入资产的 S 已经属于 Move Action，因此无需增加新的输入 Action。

