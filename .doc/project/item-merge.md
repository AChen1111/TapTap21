# 物品合成

命名空间：`GamePlay.Core`  
入口：`MergeUtil`  
配表：`ExcelData/ItemMergeTable.xlsx`  
生成类：`itemmergetable`  
数据：`Assets/Resources/DataTable/itemmergetable.bytes`

合成结果由五件事决定：物品 1 的类型、物品 1 的状态、物品 2 的类型、物品 2 的状态、操作号。命中后得到一个新的 `Item`，它的类型和 `State` 都来自表。

导表流程见 [excel-table.md](excel-table.md)。本文只写合成表怎么填、代码怎么调。

---

## 配表

`ItemMergeTable.xlsx` 第一张表，前三行是字段名、类型、说明，第 4 行起是配方。

| 字段 | 类型 | 填什么 |
|---|---|---|
| `id` | int | 行号，不参与匹配 |
| `typeIn1` | string | 物品 1 的类型名 |
| `stateIn1` | int | 物品 1 的状态 |
| `typeIn2` | string | 物品 2 的类型名 |
| `stateIn2` | int | 物品 2 的状态 |
| `op` | int | 操作号 |
| `typeOut` | string | 结果物品的类型名 |
| `stateOut` | int | 结果物品的状态 |

类型名写成类名，和 `item.GetType().ToString()` 一致。当前测试物品都在全局命名空间，所以填 `Tree`、`Water`、`BigTree`，不要加命名空间，不要加 `.cs`。

同一对物品只配一个方向。运行时会先按传入顺序查，查不到再把两个物品对调查一次。不要把 `Water+Tree` 和 `Tree+Water` 配成两条不同结果。

同一组 `类型 + 状态 + 操作` 只保留一行。重复行导入时会被丢掉，控制台有 `Excel表导入数据错误`。

改完保存，执行 `Tools > Excel > Export All (CS + Bytes)`，等编译结束再进 Play。只改单元格内容、没改列结构时，可以只跑 `Tools > Excel > Generate Bytes`。

当前示例：

| typeIn1 | stateIn1 | typeIn2 | stateIn2 | op | typeOut | stateOut |
|---|---|---|---|---|---|---|
| Water | 1 | Tree | 1 | 1 | BigTree | 1 |
| Water | 2 | Tree | 2 | 2 | BigTree | 2 |

---

## 物品类

结果类型必须是 `Item` 的具体子类，并且有无参构造函数。`CopyItem` 要新建对象，并把 `State` 抄过去。

```csharp
public class BigTree : Item
{
    public override int State { get; set; }

    public override Item CopyItem()
    {
        var copy = new BigTree();
        copy.State = State;
        return copy;
    }
}
```

物品类和 `MergeUtil` 放在同一个程序集里（现在都是 `Assembly-CSharp`）。`typeOut` 靠 `Type.GetType` 用类名创建，拆到别的程序集后短类名会创建失败，这一行配方不会进表。

参与合成的实例，`State` 必须是表里写的那个数。`new Water()` 若没有在构造函数里赋值，`State` 是 0，对不上表里的 1。

---

## 调用

```csharp
using GamePlay.Core;

BigTree result = MergeUtil.Merge<BigTree>(water, tree, 1, () =>
{
    Debug.LogError("没有这条配方");
});
if (result == null)
{
    return;
}

// result.State 来自表的 stateOut
```

| 参数 | 含义 |
|---|---|
| `item1`、`item2` | 参与合成的两个物品，顺序可以和表里相反 |
| `op` | 操作号，对应表的 `op` |
| `OnMergeFailed` | 两个顺序都没有配方时调用，可省略 |
| 返回值 | 新物品。类型是 `typeOut`，状态是 `stateOut`。没有配方，或你写的泛型不是结果类型时，返回 `null` |

泛型要写成表里的结果类型。上面这条结果是 `BigTree`，就要写 `Merge<BigTree>`。写成 `Merge<Tree>` 时配方可能已经命中，但转换失败，返回 `null`，并且不会走 `OnMergeFailed`。

返回的是复制体，改它的 `State` 不会改表里的原型，也不会改传入的 `water` / `tree`。

第一次调用 `Merge` 或 `UpdateMergeTable` 时读 bytes。Play 中途重新导出了 bytes，要再调一次：

```csharp
MergeUtil.UpdateMergeTable();
```

加载成功后，控制台会打出每一条可用配方，例如 `Water#1#Tree#1#1 -> BigTree#1`。某行没出现，就是类型名创建失败，或和已有行重复。

---

## 匹配规则

运行时用下面的字符串查表：

```text
类型1#状态1#类型2#状态2#操作
```

`Merge(water, tree, 1)` 在 `water.State == 1`、`tree.State == 1` 时，先查 `Water#1#Tree#1#1`，没有再查 `Tree#1#Water#1#1`。命中后 `CopyItem()`，并把原型上的 `stateOut` 带到副本。

五个字段都参与比较。状态差 1，或 `op` 不同，就是另一条配方。没有命中就失败，不会按“最接近的一行”凑结果。
