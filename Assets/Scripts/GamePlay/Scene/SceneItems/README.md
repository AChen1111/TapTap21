# 场景物品 Prefab 生成与替换

命名空间：`GamePlay.Scene`。

## 当前已配置

- 映射资产：`Assets/Resources/Scene/ItemPrefabCatalog.asset`，已预填 17 条类型与状态组合，Prefab 引用全部留空。
- 场景：`Assets/Scenes/SampleScene.unity`，根节点 `SceneItemSystem` 已挂 `SceneItemSpawner` 并关联上述资产。
- 后续只需在上述资产的 Entries 中补上对应 Prefab 引用；现在不具备实际生成条件，调用生成接口会失败并保留旧物体。

这套代码把物品数据和场景表现连接起来：

```text
Item 类型 + State
        ↓
ItemPrefabCatalog 查找 Prefab
        ↓
SceneItemSpawner 生成 Prefab
        ↓
SceneItemView 绑定 Item 数据
```

融合时由外部先调用 `MergeUtil`，得到结果 `Item` 后再调用替换接口：

```text
旧场景物体 + 旧场景物体
        ↓
MergeUtil.Merge(...)
        ↓
结果 Item
        ↓
SceneItemSpawner.TryReplace(...)
        ↓
生成结果 Prefab 成功后，删除旧 Prefab
```

## 一、配置映射表

1. 在 Project 窗口右键，选择 `Create > GamePlay > Scene > Item Prefab Catalog`。
2. 建议保存为 `Assets/Resources/Scene/ItemPrefabCatalog.asset` 或场景专用的资源目录。
3. 在 `Entries` 中添加类型、状态和 Prefab 映射。

映射键是：

```text
完整类型名#State
```

例如：

| Item 类型名 | State | 场景 Prefab |
|---|---:|---|
| `GamePlay.Core.Item_Tree` | 0 | 小树苗 Prefab |
| `GamePlay.Core.Item_Tree` | 1 | 大树 Prefab |
| `GamePlay.Core.Item_Spore` | 0 | 孢子 Prefab |
| `GamePlay.Core.Item_Mushroom` | 0 | 蘑菇 Prefab |
| `GamePlay.Core.Item_ManEater` | 0 | 食人花种子 Prefab |
| `GamePlay.Core.Item_ManEater` | 1 | 成熟食人花 Prefab |
| `GamePlay.Core.Item_FernSeed` | 0 | 蕨苗 Prefab |
| `GamePlay.Core.Item_Fern` | 0 | 一格高蕨类 Prefab |
| `GamePlay.Core.Item_Fern` | 1 | 两格高蕨类 Prefab |
| `GamePlay.Core.Item_Fern` | 2 | 三格高蕨类 Prefab |
| `GamePlay.Core.Item_Thorn` | 0 | 失水荆棘 Prefab |
| `GamePlay.Core.Item_Thorn` | 1 | 正常荆棘 Prefab |

类型名必须和 `item.GetType().ToString()` 一致，当前物品类使用完整命名空间，例如 `GamePlay.Core.Item_FernSeed`。同一个类型和 State 只能配置一条映射。

每个结果 Prefab 建议在根节点挂 `SceneItemView`。如果漏挂，`SceneItemSpawner` 会在运行时自动添加，但正式资源仍建议显式挂上，方便检查和后续交互组件绑定。

## 二、挂载生成器

在场景中的管理物体上添加 `SceneItemSpawner`，将刚才创建的 `ItemPrefabCatalog` 拖到 `Catalog`。

也可以通过代码指定：

```csharp
using GamePlay.Scene;

SceneItemSpawner spawner = ...;
spawner.Catalog = catalog;
```

## 三、从 Item 生成场景 Prefab

```csharp
using GamePlay.Core;
using GamePlay.Scene;
using UnityEngine;

Item_Tree tree = new Item_Tree
{
    State = (int)E_Item_TreeState.Big
};

SceneItemView view = spawner.Spawn(
    tree,
    worldPosition,
    Quaternion.identity,
    parent
);

if (view == null)
{
    // 没有映射或生成失败，调用方保留原来的数据和物体。
    return;
}
```

生成器会：

1. 根据类型名和 State 查找 Prefab；
2. 实例化 Prefab；
3. 获取或补充 `SceneItemView`；
4. 将 Item 的副本绑定到新物体；
5. 返回场景物品组件。

传入的 Item 不会被组件持有的状态直接修改。`SceneItemView.Item` 和 `TryGetItem` 返回的是副本。

## 四、融合后替换旧物体

外部负责先调用配方表：

```csharp
using GamePlay.Core;
using GamePlay.Scene;

SceneItemView waterView = ...;
SceneItemView treeView = ...;

if (!waterView.TryGetItem(out Item water) ||
    !treeView.TryGetItem(out Item tree))
{
    return;
}

Item result = MergeUtil.Merge<Item_Tree>(water, tree, 1);
if (result == null)
{
    return;
}

bool replaced = spawner.TryReplace(
    result,
    treeView,
    out SceneItemView replacement,
    waterView
);

if (!replaced)
{
    // 找不到结果 Prefab 或生成失败；waterView 和 treeView 都仍然存在。
    return;
}
```

`TryReplace` 使用锚点 `treeView` 的位置、旋转和父节点生成结果。生成成功后才销毁锚点和传入的消耗物体。传入的旧物体会自动去重，锚点不需要重复传入。

如果两个场景物体融合后结果应该放在另一个位置，先把那个物体作为锚点传入；生成器不会自行猜测位置。

## 五、铲取和背包回收

铲子操作不一定要生成场景 Prefab。比如铲蘑菇得到孢子，可以先读取场景物体的 Item，再把结果交给背包：

```csharp
Item result = MergeUtil.Merge<Item_Spore>(
    shovelItem,
    mushroomView.Item,
    9
);

if (result != null)
{
    inventory.PutItem(result);
    Destroy(mushroomView.gameObject);
}
```

如果结果要留在场景中，则使用 `Spawn` 或 `TryReplace`。`SceneItemSpawner` 不会自动操作背包，也不会自动消耗输入物品。

## 六、失败规则和边界

- `ItemPrefabCatalog` 没有映射时，生成失败并保留旧场景物体。
- 结果 Prefab 生成或绑定失败时，不会删除旧 Prefab。
- 生成器不判断 `op` 是否正确；配方匹配由 `MergeUtil` 完成。
- 生成器不判断白天、黑夜或直射阳光；这些条件由调用方决定是否调用对应配方。
- `SceneItemView` 只保存类型名和 State。场景重新加载时会通过无参构造恢复 Item 数据。
- Prefab 的碰撞体、动画、受风组件和交互脚本由各自 Prefab 配置，生成器不会复制旧物体的速度、缩放或组件状态。
