using GamePlay.Core;
using UnityEngine;

namespace GamePlay.Inventory
{
    /// <summary>
    /// 背包拖拽结束时传给场景交互层的数据。
    /// Item 是拖拽开始时的副本，避免拖拽过程中直接修改背包内部数据。
    /// </summary>
    public sealed class InventoryDragContext
    {
        public InventoryModel Inventory { get; }
        public int SlotIndex { get; }
        public Item Item { get; }
        public GameObject DragObject { get; }
        public Vector2 ScreenPosition { get; }

        public InventoryDragContext(
            InventoryModel inventory,
            int slotIndex,
            Item item,
            GameObject dragObject,
            Vector2 screenPosition)
        {
            Inventory = inventory;
            SlotIndex = slotIndex;
            Item = item?.CopyItem();
            DragObject = dragObject;
            ScreenPosition = screenPosition;
        }
    }
}
