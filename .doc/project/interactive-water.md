# Interactive-water （可交互水）

namespace: `GamePlay.Environment.Water`

基本作用：`提供浮力、流动与阻尼`

prefab: `Assets/Prefabs/GamePlay/Water`

| 组件                    | 作用                         |
| ----------------------- | ---------------------------- |
| Interactive Water       | 控制水体特性与网格生成       |
| Water Trigger Handler   | 负责接收与物体的交互         |
| Water Visual Controller | 负责控制水体外观             |
| Buoyancy Effector 2D    | 浮力、阻尼、流动             |
| Sorting Group           | 水体的渲染层                 |
| Edge Collider 2D        | 负责水面碰撞                 |
| Box Collider 2D         | 负责与 Buoyancy 组件提供浮力 |

---

> [!CAUTION]
>
> 出于某些原因，将 Water prefab 拖入场景后需 ***Unpack Completely*** 才能正常使用！
>
> 以及缩放水体时，请使用脚本生成的缩放框（初始为绿色），不要使用 Unity 的缩放框
>
> 添加新 Sorting Layer 时，请确保会经过水体渲染的物体处于 ***Water*** 层之前，不经过水体渲染的物体处于 ***Water*** 层之后，如：`0. Default 1. Background 2.Mid 3. Water 4. Frontground`

## Interactive Water

### 参数说明：

#### Water Body:

| 参数                   |                             作用                             |                                          说明 |
| :--------------------- | :----------------------------------------------------------: | --------------------------------------------: |
| Enable Vertex Per Unit | 启用后，将根据 Vertex Per Unit 参数动态设置顶点数量，可保证不同长度下顶点密度均匀 |                                      建议启用 |
| Vertex Per Unit        |              启用上述开关后，可用来控制顶点密度              |                                           --- |
| Nums Of X Vertices     |                       手动控制顶点数量                       |            仅在 Enable Vertex Per Unit 禁用时 |
| Width                  |                            水体宽                            | 通过绿色选择框更改，不要使用 Unity 内置缩放框 |
| Height                 |                            水体高                            |                                          同上 |
| Material               |                        水体使用的材质                        |                                           --- |

#### Simulation:

#### Spring:

| 参数       |            作用            |             说明 |
| ---------- | :------------------------: | ---------------: |
| Stiffness  | 控制水采样点弹簧的弹性系数 |     越大回弹越快 |
| Resistance |    控制水采样点速度衰减    | 越大水面平复越快 |

#### Wave:

| 参数                |       作用       |                           说明 |
| ------------------- | :--------------: | -----------------------------: |
| Wave Speed          | 控制水波传递速度 |                 越大波传递越快 |
| Simulation Substeps |  控制子物理步数  | 越大单帧内水波影响越广，低性能 |

#### Force:

| 参数             |           作用           |                   说明 |
| ---------------- | :----------------------: | ---------------------: |
| Force Multiplier |  将物体作用与水的力放大  | 越大物体对水面影响越大 |
| Force Max        | 限制物体对水的最大作用力 | 避免鬼畜Generate Mesh: |

`Generate Mesh`: 生成水体网格

`Place Collider`: 自动对齐水面碰撞体与水体碰撞体

---

## Water Trigger Handler

`Water Mask`: 指定可以与水面交互的 Layer mask

---

## Water Visual Component

略

