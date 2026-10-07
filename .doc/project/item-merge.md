# 物品配方与操作号

命名空间：`GamePlay.Core`  
入口：`MergeUtil`  
配表：`ExcelData/ItemMergeTable.xlsx`  
生成类：`itemmergetable`  
当前运行时数据：`Assets/Scripts/GamePlay/Core/Item/Resources/DataTable/itemmergetable.bytes`

当前 Excel 已配置 **13 条配方**。每条配方由两个输入物品的类型、状态和 `op` 决定输出物品类型与状态。输入顺序可以相反，`MergeUtil` 会按两个顺序查找。

运行时通过 `Resources.Load<TextAsset>("DataTable/itemmergetable")` 加载数据，使用的是 Resources 相对路径。背包拖拽、场景放置和融合替换的完整流程见 [scene-item-prefab.md](scene-item-prefab.md)。

## 配方总表

表中统一把操作道具写在 `typeIn1`，被操作对象写在 `typeIn2`。

| id | typeIn1 | stateIn1 | typeIn2 | stateIn2 | op | 条件 | typeOut | stateOut | 说明 |
|---:|---|---:|---|---:|---:|---|---|---:|---|
| 1 | `GamePlay.Core.Item_Water` | 0 | `GamePlay.Core.Item_Tree` | 0 | 1 | 有阳光直接照射 | `GamePlay.Core.Item_Tree` | 1 | 小树苗浇水变大树 |
| 2 | `GamePlay.Core.Item_Water` | 0 | `GamePlay.Core.Item_Spore` | 0 | 2 | 夜晚或没有阳光直射 | `GamePlay.Core.Item_Mushroom` | 0 | 孢子浇水变蘑菇 |
| 3 | `GamePlay.Core.Item_Water` | 0 | `GamePlay.Core.Item_ManEater` | 0 | 3 | 无额外环境条件 | `GamePlay.Core.Item_ManEater` | 1 | 食人花种子浇水变食人花 |
| 4 | `GamePlay.Core.Item_Water` | 0 | `GamePlay.Core.Item_FernSeed` | 0 | 4 | 无额外环境条件 | `GamePlay.Core.Item_Fern` | 0 | 蕨苗第一次浇水变一格高 |
| 5 | `GamePlay.Core.Item_Water` | 0 | `GamePlay.Core.Item_Fern` | 0 | 5 | 无额外环境条件 | `GamePlay.Core.Item_Fern` | 1 | 一格高蕨类变两格高 |
| 6 | `GamePlay.Core.Item_Water` | 0 | `GamePlay.Core.Item_Fern` | 1 | 6 | 无额外环境条件 | `GamePlay.Core.Item_Fern` | 2 | 两格高蕨类变三格高 |
| 7 | `GamePlay.Core.Item_Water` | 0 | `GamePlay.Core.Item_Thorn` | 0 | 7 | 无额外环境条件 | `GamePlay.Core.Item_Thorn` | 1 | 失水荆棘浇水变正常荆棘 |
| 8 | `GamePlay.Core.Item_Water` | 0 | `GamePlay.Core.Item_Thorn` | 1 | 8 | 无额外环境条件 | `GamePlay.Core.Item_Thorn` | 0 | 正常荆棘抽水变失水荆棘 |
| 9 | `GamePlay.Core.Item_Shovel` | 0 | `GamePlay.Core.Item_Mushroom` | 0 | 9 | 场景中存在蘑菇 | `GamePlay.Core.Item_Spore` | 0 | 铲蘑菇获得孢子 |
| 10 | `GamePlay.Core.Item_Shovel` | 0 | `GamePlay.Core.Item_Spore` | 0 | 10 | 场景中存在已放置孢子 | `GamePlay.Core.Item_Spore` | 0 | 铲孢子获得孢子 |
| 11 | `GamePlay.Core.Item_Shovel` | 0 | `GamePlay.Core.Item_Fern` | 0 | 11 | 场景中存在一格高蕨类 | `GamePlay.Core.Item_FernSeed` | 0 | 铲一格高蕨类获得蕨苗 |
| 12 | `GamePlay.Core.Item_Shovel` | 0 | `GamePlay.Core.Item_Fern` | 1 | 12 | 场景中存在两格高蕨类 | `GamePlay.Core.Item_FernSeed` | 0 | 铲两格高蕨类获得蕨苗 |
| 13 | `GamePlay.Core.Item_Shovel` | 0 | `GamePlay.Core.Item_Fern` | 2 | 13 | 场景中存在三格高蕨类 | `GamePlay.Core.Item_FernSeed` | 0 | 铲三格高蕨类获得蕨苗 |

没有配方的物品：

- `GamePlay.Core.Item_Stopwatch`：没有物品融合配方，直接作为昼夜切换入口。
- `GamePlay.Core.Item_WindSeed`：直接放置生成风场，不经过 `MergeUtil`。
- `GamePlay.Core.Item_Driftwood`：直接作为场景物使用，不经过 `MergeUtil`。

## `op` 操作号

| op | 含义 |
|---:|---|
| 1 | 有直射阳光时给树苗浇水 |
| 2 | 没有直射阳光时给孢子浇水 |
| 3 | 给食人花种子浇水 |
| 4 | 给蕨苗第一次浇水 |
| 5 | 给一格高蕨类浇水 |
| 6 | 给两格高蕨类浇水 |
| 7 | 给失水荆棘浇水 |
| 8 | 从正常荆棘抽水 |
| 9 | 铲蘑菇 |
| 10 | 铲孢子 |
| 11 | 铲一格高蕨类 |
| 12 | 铲两格高蕨类 |
| 13 | 铲三格高蕨类 |

`op` 是运行时匹配字段。树和孢子的光照条件由调用方先判断：满足条件时传入对应的 `op`，不满足条件时不调用该配方。Excel 表本身不会读取昼夜或阳光状态。

## Excel 字段

| 字段 | 类型 | 作用 |
|---|---|---|
| `id` | int | 配方行编号，不参与匹配 |
| `typeIn1` | string | 第一个输入物品的完整类型名 |
| `stateIn1` | int | 第一个输入物品状态 |
| `typeIn2` | string | 第二个输入物品的完整类型名 |
| `stateIn2` | int | 第二个输入物品状态 |
| `op` | int | 操作号，参与匹配 |
| `typeOut` | string | 输出物品的完整类型名 |
| `stateOut` | int | 输出物品状态 |

`id` 和 `op` 当前都按 1～13 编号，但两者职责不同：`id` 只是行号，`op` 才是 `MergeUtil` 的匹配条件。

类型名必须和 `item.GetType().ToString()` 一致，例如：

```text
GamePlay.Core.Item_FernSeed
```

同一组「输入类型 + 输入状态 + 操作号」只能有一行。输入顺序不影响命中，不要为同一条规则再反向配置一行。

## 调用

```csharp
using GamePlay.Core;

Item result = MergeUtil.Merge(water, tree, 1);
if (result == null)
{
    return;
}

// result 的类型和 State 来自 typeOut、stateOut。
```

返回的是新 Item，不会修改传入的物品。没有配方时返回 `null`，并调用可选的失败回调。

非泛型 `Merge` 直接返回配方指定的结果类型，当前场景交互入口使用这个重载。调用方已经知道结果类型时，也可以使用 `MergeUtil.Merge<Item_Tree>(water, tree, 1)`；如果泛型类型与结果不兼容，转换返回 `null`。

首次调用 `Merge` 或手动调用 `MergeUtil.UpdateMergeTable()` 时会加载 `itemmergetable.bytes`。修改 Excel 后必须运行：

```text
Tools > Excel > Export All (CS + Bytes)
```

只修改数据时也可以运行 `Tools > Excel > Generate Bytes`。不要手动修改生成的 C# 或 `.bytes` 文件。
