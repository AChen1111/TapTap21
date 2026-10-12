# 玩家生命值与三个心 HUD

生命值代码位于 `Assets/Scripts/GamePlay/Player/Health/PlayerHealth.cs`，命名空间为 `GamePlay.Player.Health`。默认三点生命，使用项目已有 JSON SaveSystem 保存当前生命值。

## 已接入资源

- `Assets/Prefabs/GamePlay/Player/SamplePlayer.prefab` 根节点已挂 `PlayerHealth`。
- `Assets/UI/Prefab/HUD/HealthBar.prefab` 继承现有 `APanelController`，包含三个 Image 显示位。
- `Assets/Config/UI/HUD/Inventory/Inventory.asset` 的 `screensToRegister` 注册 HealthBar，`panelsToShow` 配置自动显示 HealthBar。
- `UISettings.CreateUIInstance()` 创建现有 UIFrame 后自动显示该 Panel。没有增加另一套 UIManager。

## 使用

1. 把 SamplePlayer 拖到场景。自己的玩家也可以直接添加 PlayerHealth 组件。
2. 在场景中新建空对象 RespawnPoint，放在安全位置，把它拖进玩家 PlayerHealth 的 Respawn Point 字段。不配置时使用玩家本次启动时的位置和旋转。
3. 使用当前 UISettings 创建 UI，或者在已有 UIFrame 注册 HealthBar 后调用 `frame.ShowPanel("HealthBar")`。
4. 在 HealthBar Prefab 的 Heart Sprites 数组里替换三个 Sprite，当前为 Square 占位。满血显示正常颜色，缺失生命显示低透明度颜色，三个位置一直保留。

SampleScene 当前没有 PlayerMovement 玩家对象。UI 启动后会等待玩家出现；拖入 SamplePlayer 后才能绑定生命值。系统不会主动往场景新增玩家或改变关卡地形。

## 行为

正常扣血从 3 到 2、1，UI 在生命值变化事件中即时刷新。生命值降到 0 时死亡，只触发一次死亡事件。死亡暂停 PlayerMovement 和刚体模拟；默认等待 0.5 秒后自动重生，返回指定重生位置，清空线速度、角速度和旧移动平台惯性，恢复跳跃次数及满血，并恢复死亡前的移动启用和物理模拟状态。

Auto Respawn 可以关闭，由队友的死亡动画或重生界面在结束时调用 Respawn。Respawn Delay 使用真实时间，不受 Time.timeScale 影响。

## 调用接口

```csharp
using GamePlay.Player.Health;

PlayerHealth health = player.GetComponent<PlayerHealth>();
health.TakeDamage(1); // 大嘴花每次有效咬击
health.Kill();        // 掉水直接死亡，不受普通扣血数量限制
health.Heal(1);       // 恢复一点，上限为最大生命
health.Respawn();     // 外部死亡流程结束后重生
health.SetRespawnPoint(checkpointTransform);
health.SetHealth(2);  // 设为指定生命，0 会进入死亡流程
```

CurrentHealth、MaxHealth、IsDead 为只读属性。死亡期间 TakeDamage、Heal、SetHealth 不改变生命。水体与大嘴花的具体碰撞和攻击脚本尚未接入；这些脚本应通过碰撞到的玩家根节点取得 PlayerHealth 后调用上述接口。大嘴花负责“一次咬击只调用一次”，系统没有自动攻击间隔或受伤无敌时间。

组件事件：

| 事件 | 参数与用途 |
|---|---|
| HealthChanged | `(int current, int max)`，UI 更新 |
| Died | 无参数，死亡表现或外部重生流程 |
| Respawned | 无参数，重生完成后通知 |

HealthBarView.Bind(health) 可以明确绑定某个玩家。默认自动查找场景中的单个 PlayerHealth，支持 UI 先创建、玩家后创建以及重新显示 Panel。

## JSON 留存

使用 `SaveDefinitions.Player` 和 `SaveSlots.Slot0`，文件位于 `Application.persistentDataPath/Saves/slot_0/player.json`。SaveSystem 文件包含版本、校验和及 dataJson 外层结构，生命值字段位于内部玩家数据。

PlayerSaveData 新增 `health = 3`、`respawnPending`。每次生命值变化和死亡、重生时立即保存，读取已有玩家数据后只更新生命值字段，保留 level、coins。不存在存档时从默认生命开始；旧版不含 health 的玩家数据使用字段初值 3。读取已有文件失败时记录日志并保留原文件，不覆盖损坏存档。

普通受伤后退出，下次读取受伤后的生命。死亡期间退出，会保存 0 和 respawnPending；下次进入在 Start 中回到当前场景配置的重生点并恢复满血。重生点 Transform 只作为场景配置，不写入 JSON，也不负责跨场景复活或保存探索位置。

Load Save On Awake、Save On Change 可以在 Inspector 关闭，供外部统一玩家存档流程接管。独立重生动画、黑屏、音效、水体伤害检测、大嘴花攻击判定和跨场景重生均通过公开方法及事件接入。
