# Super ScrollView 简单使用指南
## 推荐方式：使用项目封装

项目提供了 `AChen.UI.GridListController`，通常不需要直接操作 `LoopListView2`。

### 1. 创建列表层级

在 Canvas 下创建一个普通的 Unity `Scroll View`，然后：

1. 在 Scroll View 根物体上保留 `ScrollRect`。
2. 在同一个物体上添加 `LoopListView2`。
3. 再添加 `GridListController`。
4. 将该物体上的 `LoopListView2` 拖到 `GridListController.loopListView`。
5. 在 `LoopListView2` 中设置 `ArrangeType`，竖向列表一般选 `TopToBottom`。

`ScrollRect` 的 `Content` 和 `Viewport` 必须正确引用。Scrollbar 的 Visibility 不要使用 `Auto Hide And Expand Viewport`。

### 2. 创建行预制体

行预制体至少需要：

- `RectTransform`：尺寸就是一行的显示尺寸。
- `LoopListViewItem2`：由插件管理和复用该对象。
- 一个实现 `IRowItem<TData>` 的业务脚本。

单列示例：

```csharp
using System;
using System.Collections.Generic;
using AChen.UI;
using SuperScrollView;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//商品元素
public class ShopRowItem : MonoBehaviour, IRowItem<ShopData>
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button button;

    public int RowCardCount => 1;

    public void SetRowData(
        int rowIndex,
        List<ShopData> allData,
        int selectedIndex,
        Action<int> onSelected)
    {
        if (rowIndex < 0 || rowIndex >= allData.Count)
            return;

        ShopData data = allData[rowIndex];
        nameText.text = data.Name;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onSelected?.Invoke(rowIndex));
    }
}
```

> 列表项会被复用，所以 `SetRowData` 每次都要完整刷新文字、图片、选中状态和点击事件，不能依赖上一次显示的数据。

### 3. 初始化列表

```csharp
using System.Collections.Generic;
using AChen.UI;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GridListController shopList;
    [SerializeField] private GameObject shopRowPrefab;

    private readonly List<ShopData> dataList = new();

    private void Start()
    {
        shopList.InitList(
            shopRowPrefab,
            dataList,
            onSelected: index => Debug.Log($"Selected: {index}"),
            selectedIndex: 0);
    }
}
```

也可以从 `Resources` 加载预制体：

```csharp
await shopList.InitList("UI/ShopRow", dataList);
```

路径相对于任意 `Resources` 文件夹，且不写文件扩展名。例如：

```text
Assets/Resources/UI/ShopRow.prefab -> "UI/ShopRow"
```

### 4. 一行显示多个卡片

将 `RowCardCount` 改为每行的卡片数量，例如：

```csharp
public int RowCardCount => 3;
```

`SetRowData` 中把行索引换算为数据索引：

```csharp
int firstDataIndex = rowIndex * RowCardCount;

for (int column = 0; column < RowCardCount; column++)
{
    int dataIndex = firstDataIndex + column;
    bool hasData = dataIndex < allData.Count;

    cards[column].gameObject.SetActive(hasData);
    if (hasData)
        cards[column].Bind(allData[dataIndex], () => onSelected?.Invoke(dataIndex));
}
```

最后一行不足时，记得隐藏没有数据的卡片。

### 5. 滚动到选中项

```csharp
// 选中行不在视口中时立即滚动过去
shopList.MoveToSelectedIfHidden();

// 0.3 秒动画，动画由 DOTween 驱动
shopList.MoveToSelectedIfHidden(0.3f);
```

## 直接使用 LoopListView2

仅在项目封装无法满足需求时使用原生接口。

先在 `LoopListView2` 的 Inspector 中展开 `ItemPrefabList`，加入带有 `LoopListViewItem2` 的预制体，然后初始化：

```csharp
using SuperScrollView;
using UnityEngine;

public class SimpleList : MonoBehaviour
{
    [SerializeField] private LoopListView2 listView;
    [SerializeField] private int itemCount = 100;

    private void Start()
    {
        listView.InitListView(itemCount, OnGetItemByIndex);
    }

    private LoopListViewItem2 OnGetItemByIndex(LoopListView2 view, int index)
    {
        if (index < 0 || index >= itemCount)
            return null;

        // 参数必须和预制体的 GameObject.name 一致。
        LoopListViewItem2 item = view.NewListViewItem("ShopRow");
        item.GetComponent<ShopRowView>().SetData(index);
        return item;
    }
}
```

常用接口：

```csharp
listView.SetListItemCount(newCount);              // 修改数据数量
listView.RefreshAllShownItem();                   // 刷新当前可见项
listView.RefreshItemByItemIndex(index);           // 刷新指定项（若可见）
listView.MovePanelToItemIndex(index, 0);           // 滚动到指定项
listView.MovePanelToItemIndex(index, 0, 0.3f);     // 平滑滚动
listView.OnItemSizeChanged(index);                 // 可见项尺寸改变后重新排版
```

`InitListView` 在同一个组件上只能调用一次。后续更换数据时应使用 `SetListItemCount` 和刷新接口。

## 常见问题

- 列表是空的：检查数据数量、Item Prefab 名称，以及回调是否返回了 `LoopListViewItem2`。
- 出现空引用：检查 `ScrollRect.content`、`ScrollRect.viewport` 和 `GridListController.loopListView`。
- 滚动方向不对：检查 `ArrangeType` 与 `ScrollRect` 的 horizontal/vertical 设置。
- 点击多次触发：复用对象绑定事件前先移除旧监听。
- 行高变化后重叠：调用 `OnItemSizeChanged(index)`。
- 更换数据后内容没更新：更新数量后调用 `RefreshAllShownItem()`。

