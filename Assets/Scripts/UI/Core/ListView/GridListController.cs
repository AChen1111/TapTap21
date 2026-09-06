using System;
using System.Collections.Generic;
using AChen.Log;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace AChen.UI
{
/// <summary>基于 SuperScrollView 的通用网格虚拟列表入口。</summary>
public class GridListController : MonoBehaviour
{
    [SerializeField] private LoopListView2 loopListView;

    private bool mIsInited;
    private int mSelectedIndex = -1;
    private int mRowCardCount = 1;
    private Action<int> mOnSelectedCallback;
    private Func<LoopListView2, int, LoopListViewItem2> mOnGetItemHandler;
    private Tween m_MoveToSelectedTween;
    private string mCurrentPrefabName;
    /// <summary>当前选中的数据索引；没有选中项时为 -1。</summary>
    public int SelectedIndex => mSelectedIndex;

    /// <summary>从 Resources 异步加载行 Prefab 并绑定数据。Prefab 位于 Resources 时使用。</summary>
    /// <typeparam name="TData">卡片数据类型。</typeparam>
    /// <param name="rowPrefabKey">Resources 相对路径，不含扩展名。</param>
    /// <param name="dataList">要显示的数据。</param>
    /// <param name="onSelected">选中数据项后的回调。</param>
    /// <param name="selectedIndex">初始选中索引，-1 表示不选中。</param>
    public UniTask InitList<TData>(
        string rowPrefabKey,
        List<TData> dataList,
        Action<int> onSelected = null,
        int selectedIndex = -1)
    {
        CancelMoveToSelected();
        mOnSelectedCallback = onSelected;
        mSelectedIndex = selectedIndex >= 0 && dataList != null && selectedIndex < dataList.Count
            ? selectedIndex
            : -1;
        return LoadRowPrefabAsync(rowPrefabKey).ContinueWith(prefab => BindList(prefab, dataList));
    }

    /// <summary>使用已持有的行 Prefab 立即绑定数据。Prefab 已由 Inspector 或其他系统提供时优先使用。</summary>
    /// <typeparam name="TData">卡片数据类型。</typeparam>
    /// <param name="rowPrefab">根节点实现 <see cref="IRowItem{TData}"/> 的行 Prefab。</param>
    /// <param name="dataList">要显示的数据。</param>
    /// <param name="onSelected">选中数据项后的回调。</param>
    /// <param name="selectedIndex">初始选中索引，-1 表示不选中。</param>
    public void InitList<TData>(
        GameObject rowPrefab,
        List<TData> dataList,
        Action<int> onSelected = null,
        int selectedIndex = -1)
    {
        CancelMoveToSelected();
        mOnSelectedCallback = onSelected;
        mSelectedIndex = selectedIndex >= 0 && dataList != null && selectedIndex < dataList.Count
            ? selectedIndex
            : -1;
        BindList(rowPrefab, dataList);
    }

    async UniTask<GameObject> LoadRowPrefabAsync(string rowPrefabKey)
    {
        GameObject prefab = Resources.Load<GameObject>(rowPrefabKey);
        if (prefab == null)
        {
            ALog.LogError($"Row prefab not found in Resources: {rowPrefabKey}", ALogCategories.UI);
        }

        await UniTask.CompletedTask;
        return prefab;
    }

    void BindList<TData>(GameObject prefab, List<TData> dataList)
    {
        var rowItemComp = prefab.GetComponent<IRowItem<TData>>();
        int rowCardCount = rowItemComp != null ? rowItemComp.RowCardCount : 1;
        if (rowCardCount <= 0) rowCardCount = 1;
        mRowCardCount = rowCardCount;

        if (loopListView.GetItemPrefabConfData(prefab.name) == null)
        {
            float startPosOffset = 0f;
            if (loopListView.ArrangeType is ListItemArrangeType.LeftToRight or ListItemArrangeType.RightToLeft)
            {
                startPosOffset = prefab.GetComponent<RectTransform>().anchoredPosition.y;
            }

            loopListView.AddItemPrefab(new ItemPrefabConfData
            {
                mItemPrefab = prefab,
                mStartPosOffset = startPosOffset
            });
        }

        string prefabName = prefab.name;
        int totalCount = dataList != null ? dataList.Count : 0;
        int rowCount = Mathf.CeilToInt((float)totalCount / rowCardCount);

        mOnGetItemHandler = (listView, rowIndex) =>
        {
            if (rowIndex < 0 || rowIndex >= Mathf.CeilToInt((float)(dataList != null ? dataList.Count : 0) / rowCardCount))
                return null;

            LoopListViewItem2 item = listView.NewListViewItem(prefabName);
            var row = item.GetComponent<IRowItem<TData>>();
            row?.SetRowData(rowIndex, dataList, mSelectedIndex, OnCardSelected);
            return item;
        };

        bool prefabChanged = mCurrentPrefabName != prefabName;
        mCurrentPrefabName = prefabName;

        if (!mIsInited)
        {
            var scrollRect = loopListView.GetComponent<ScrollRect>();
            if (scrollRect != null)
            {
                if (scrollRect.horizontalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport)
                    scrollRect.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
                if (scrollRect.verticalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport)
                    scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
            }

            loopListView.InitListView(rowCount, OnGetItemByIndex);
            mIsInited = true;
            return;
        }

        if (prefabChanged)
        {
            loopListView.SetListItemCount(0, false);
            loopListView.SetListItemCount(rowCount, false);
            return;
        }

        loopListView.SetListItemCount(rowCount, true);
        loopListView.RefreshAllShownItem();
    }

    /// <summary>仅当选中项当前不可见时滚动到该项。初始化或恢复选中位置后使用。</summary>
    /// <param name="duration">滚动秒数；小于等于 0 时立即跳转。</param>
    /// <param name="ease">动画缓动，默认 <see cref="Ease.InOutCubic"/>。</param>
    public void MoveToSelectedIfHidden(float duration = 0, Ease ease = Ease.InOutCubic)
    {
        CancelMoveToSelected();
        if (!mIsInited || mSelectedIndex < 0) return;
        int selectedRow = mSelectedIndex / mRowCardCount;
        if (loopListView.GetShownItemByItemIndex(selectedRow) != null) return;

        if (duration <= 0f)
        {
            loopListView.MovePanelToItemIndexImmediately(selectedRow, 0);
            return;
        }

        float from = loopListView.GetFirstShownFloatItemIndexInViewPort();
        LoopListView2 list = loopListView;
        m_MoveToSelectedTween = DOTween.To(() => from, index =>
            {
                from = index;
                if (list == null) return;
                int itemIndex = Mathf.Max(0, Mathf.FloorToInt(index));
                list.MovePanelToItemIndexImmediately(itemIndex, 0);
            }, selectedRow, duration)
            .SetEase(ease)
            .SetTarget(this);
    }

    void CancelMoveToSelected()
    {
        if (m_MoveToSelectedTween != null && m_MoveToSelectedTween.IsActive())
        {
            m_MoveToSelectedTween.Kill();
        }

        m_MoveToSelectedTween = null;
    }

    void OnDestroy()
    {
        CancelMoveToSelected();
    }

    private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int rowIndex)
    {
        return mOnGetItemHandler?.Invoke(listView, rowIndex);
    }

    private void OnCardSelected(int dataIndex)
    {
        if (mSelectedIndex == dataIndex) return;
        mSelectedIndex = dataIndex;
        mOnSelectedCallback?.Invoke(dataIndex);
        loopListView.RefreshAllShownItem();
    }
}
}
