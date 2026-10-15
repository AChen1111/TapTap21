# 荆棘：阻挡、伤害与供水／抽水

正常荆棘为 `GamePlay.Core.Item_Thorn`、State=1；失水荆棘为同类型、State=0。当前场景中的 `Thorn` 已配置为正常荆棘，保留原摆放位置。

## 资源与行为

| 状态 | Prefab | 行为 | 目标 Operation |
|---|---|---|---:|
| 失水 | `Assets/Prefabs/Scene/Items/Thorn_0.prefab` | 低矮占位外观，无碰撞、无伤害 | 7 |
| 正常 | `Assets/Prefabs/Scene/Items/Thorn_1.prefab` | 高大占位外观，实体碰撞，接触扣血 | 8 |

两个 Prefab 已映射到 `Assets/Config/Item/ItemPrefabCatalog.asset`。根节点使用单位缩放，Visual 子节点表达高度，底部位置不变。当前素材是 Square 占位，之后可替换 Visual 的 Sprite 和适配碰撞范围。

把水拖到正常荆棘根节点附近：op=8，抽水后替换为失水荆棘。再把水拖到失水荆棘附近：op=7，供水后恢复正常。复用现有 Excel 配方和生成器，无过渡动画。目标查找仍使用 Bridge 配置的根节点距离，默认半径 0.5；不是按整个荆棘外观范围命中。水沿用现有背包成功后扣除一份的规则。

## 伤害组件

`GamePlay.Environment.Hazard.ThornHazard2D` 位于 `Assets/Scripts/GamePlay/Environment/Hazard/ThornHazard2D.cs`。

正常荆棘通过非 Trigger BoxCollider2D 阻挡玩家。组件兼容 CollisionStay 与 TriggerStay，查找碰到对象父节点上的 PlayerHealth，无需设置 Player Tag。

- Damage 默认 1。
- Damage Interval 默认 0.8 秒。
- 持续接触每 0.8 秒最多扣一次；离开后冷却仍保留。
- 同一荆棘的多个碰撞回调共享冷却，避免玩家多个 Collider 在同帧重复扣血。
- 死亡玩家不再扣血；死亡重生由 PlayerHealth 处理。
- 冷却按荆棘对象计算，不是玩家全局无敌时间。

失水 Prefab 不挂伤害组件，也没有阻挡碰撞。正常／失水变化通过场景 Prefab 替换实现，场景结果留在原地。
