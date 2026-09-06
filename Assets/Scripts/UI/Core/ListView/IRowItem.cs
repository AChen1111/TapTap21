using System;
using System.Collections.Generic;

namespace AChen.UI;

//
// 行数据接口
public interface IRowItem<TData>
{
    // 行卡片数量
    int RowCardCount { get; }
    // 设置行数据
    // 参数：
    // - rowIndex：行索引
    // - allData：所有数据
    // - selectedIndex：选中索引
    // - onSelected：选中回调
    void SetRowData(int rowIndex, List<TData> allData, int selectedIndex, Action<int> onSelected);
}