using System;
using System.Collections.Generic;

namespace AChen.UI
{
/// <summary>虚拟列表行组件实现的绑定接口。</summary>
/// <typeparam name="TData">每张卡片使用的数据类型。</typeparam>
public interface IRowItem<TData>
{
    /// <summary>每一行容纳的卡片数量，最小为 1。</summary>
    int RowCardCount { get; }

    /// <summary>列表创建或刷新可见行时，为该行绑定数据和选中回调。</summary>
    /// <param name="rowIndex">当前行索引。</param>
    /// <param name="allData">列表使用的完整数据。</param>
    /// <param name="selectedIndex">当前选中的数据索引，未选中时为 -1。</param>
    /// <param name="onSelected">卡片被选中时传入其数据索引。</param>
    void SetRowData(int rowIndex, List<TData> allData, int selectedIndex, Action<int> onSelected);
}
}
