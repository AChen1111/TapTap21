# 物品类型与状态对照表

以当前物品类和状态枚举为准，共 **11 种物品**。所有物品类的命名空间均为 `GamePlay.Core`，下表完整类型名为「命名空间 + 类名」。

| Id | 中文名 | 完整类型名（命名空间 + 类名） | 状态数量 | State 数值与含义 |
| ---: | --- | --- | ---: | --- |
| 0 | 水元素 | `GamePlay.Core.Item_Water` | 1 | `0`：默认 |
| 1 | 树（树苗／大树） | `GamePlay.Core.Item_Tree` | 2 | `0 = Small`：小树苗；`1 = Big`：大树 |
| 2 | 孢子 | `GamePlay.Core.Item_Spore` | 1 | `0`：默认 |
| 3 | 蘑菇 | `GamePlay.Core.Item_Mushroom` | 1 | `0`：默认 |
| 4 | 食人花（种子／长成） | `GamePlay.Core.Item_ManEater` | 2 | `0 = Seed`：种子；`1 = Grown`：长成 |
| 5 | 蕨类植物 | `GamePlay.Core.Item_Fern` | 3 | `0 = Height1`：一格高；`1 = Height2`：两格高；`2 = Height3`：三格高 |
| 6 | 荆棘 | `GamePlay.Core.Item_Thorn` | 2 | `0 = Dry`：失水；`1 = Normal`：正常 |
| 8 | 昼夜切换道具（秒表） | `GamePlay.Core.Item_Stopwatch` | 1 | `0`：默认 |
| 9 | 铲子 | `GamePlay.Core.Item_Shovel` | 1 | `0`：默认 |
| 11 | 风种 | `GamePlay.Core.Item_WindSeed` | 1 | `0`：默认 |
| 12 | 浮木 | `GamePlay.Core.Item_Driftwood` | 1 | `0`：默认 |

## 说明

- `Id` 标识物品种类，`State` 标识该种类的状态。同一种物品的不同状态共用一个 Id，例如小树苗与大树都是 `Id = 1`。
- 状态数量指当前已定义／使用的状态；单状态物品暂使用默认值 `0`，不代表代码将 `State` 限制为只能赋值 `0`。所有物品新建时状态均为 `0`，因此荆棘默认是失水状态。
- `Id = 7`（藤蔓）和 `Id = 10`（花朵）对应的类已删除，编号留空，其余物品 Id 没有重排。
