# 起点式 2D 风场：快速升举与末端悬浮

命名空间：`GamePlay.Wind`。

代码目录：`Assets/Scripts/GamePlay/Scene/SceneItems/Wind/`。

预制体：`Assets/Prefabs/GamePlay/Scene/Items/Wind/WindArea2D.prefab`。

## 运行规则

放置点是短边中心，沿选定方向单向延伸。受风组件按刚体质心判断，仍用 AddForce，不直接改位置或速度。

1. 前段追踪升举速度 6，速度响应时间 0.15 秒。
2. 悬浮中心前 1.5 单位逐渐从速度控制混合到位置恢复与速度阻尼控制。
3. 悬浮中心距末端内缩 0.4，目标沿风向正弦摆动，幅度 0.1、频率 0.6 Hz。使用目标速度和加速度前馈减少滞后。
4. 已进入风场的物体可在末端外 0.5 单位缓冲区继续受控；未进入的物体不会仅凭处于缓冲区被吸入。离开侧边、起点背后、整个缓冲区或停用风场后停止控制。

只控制沿风向的运动并补偿该轴重力，不锁横向运动。向上风表现为上下摆动，斜向/横向风沿对应风向摆动。位置以质心为准，未按物体尺寸内缩边界。

## 参数

| 参数 | 新增默认值 | 含义 |
| --- | ---: | --- |
| Hover At End | 开启 | 快速升举与悬浮；关闭则使用旧固定风力 |
| Rise Speed | 6 | 沿风向的目标升举速度 |
| Speed Response Time | 0.15 | 速度响应时间 |
| Slowdown Distance | 1.5 | 从悬浮中心往回计算的混合区长度 |
| Hover Inset | 0.4 | 悬浮中心距末端的距离 |
| Hover Amplitude | 0.1 | 摆动幅度，短风场自动缩小避免目标越界 |
| Hover Frequency | 0.6 | 每秒摆动次数，0 为静止悬浮 |
| Position Gain / Damping | 36 / 12 | 位置恢复和速度阻尼 |
| Max Acceleration | 30 | 净控制加速度基准上限，不含重力补偿 |
| End Buffer | 0.5 | 已进入物体的末端容错范围 |

Strength 在悬浮模式以 5 为基准缩放控制上限；固定风模式仍表示力。Response 大于 0 时缩放控制上限，0 为不受风；不直接将全部控制力及重力补偿一起乘倍率。

代码长度/宽度初值仍为 6/1。本次保留预制体已有设置：方向向上、长 9、宽 5、强度 10、风线速度 20、数量 30；其悬浮中心距起点 8.6。

悬浮模式按质量换算所需力，使不同质量有相近响应；实际仍受阻尼、碰撞和轴向约束影响。没有位置锁定或速度硬截断，高速进入仍可能超出缓冲区。

多个悬浮风场重叠时保留当前风场，首次进入选强度最大者，同强度按实例 Id 排序，不叠加相互冲突的位置控制器。固定力风场仍向量叠加，可与当前悬浮控制同时作用。

## 挂载与放置接口

受风物体 Rigidbody2D 所在节点挂 WindBody2D，自动施力要求 Dynamic 且 Simulated 开启。风场根节点和父节点保持单位缩放，方向用接口控制，Collider 尺寸由组件维护。

```csharp
using GamePlay.Wind;

// windPrefab 引用预制体上的 WindField2D。
WindField2D field = Instantiate(windPrefab);
field.Place(worldPosition, selectedDirection);
field.HoverAtEnd = true;
```

方向选择 UI、背包消耗仍由队友处理。队友接管移动时：

```csharp
windBody.AutoApply = false;
Vector2 force = windBody.GetForce();
// 在自己的 FixedUpdate 中 AddForce(force, ForceMode2D.Force)，不要重复施力。
```

GetForce 包含速度/位置控制与重力补偿，会维护物体当前悬浮风场。SampleTotalForce(worldPosition) 保留旧位置采样的基础力语义，不用于悬浮；SampleConstantForce 只叠加关闭悬浮的风场。

## 视觉与边界

保留 Stylised_Wind_VFX 的风线与拖尾、独立 URP 材质，视觉速度独立于升举速度。未接拖拽、配方、存档或修改玩家控制。PlayerMovement 仍会覆盖横向速度。本次没有修改现有场景。补充了编辑器测试代码，未编译、运行测试或试玩，实际表现尚待 Unity 验证。
