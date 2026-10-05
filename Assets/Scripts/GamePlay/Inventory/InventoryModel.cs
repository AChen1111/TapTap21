using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GamePlay.Core;
namespace GamePlay.Inventory
{
    public class InventoryModel
    {
        private List<ItemStack> _items = new();
        /// <summary>
        /// 物品栏的最大容纳数量
        /// </summary>
        public int Size =>  _items.Count;

        /// <summary>
        /// 从物品栏获取物品失败时触发
        /// </summary>
        public event Action OnGetItemFailed;
        /// <summary>
        /// 把物品放至物品栏失败时触发 
        /// </summary>
        public event Action OnPutItemFailed;
        /// <summary>
        /// 物品栏内部物品发生变动时触发
        /// </summary>
        public event Action<InventoryModel> OnInventoryUpdated;

        public InventoryModel(int size)
        {
            InitInventory(size);
        }
        public void InitInventory(int size)
        {
            _items=Enumerable.Repeat<ItemStack>(null,size).ToList();
            OnInventoryUpdated?.Invoke(this);
        }

        public ItemStack this[int index]
        {
            get{
                if (IsIndexInRange(index))
                {
                    return _items[index];
                }
                else
                {
                    return null;
                }
            }
            private set{
                if (IsIndexInRange(index))
                {
                    _items[index]=value;
                }
                else
                {
                    return;
                }
            }
        }


        public Item GetItem(int index)
        {
            if (IsIndexInRange(index)&& _items[index] != null && _items[index].Count > 0)
            {
                _items[index].RemoveItem();
                var ret=_items[index].Item;
                if (_items[index].Count == 0)
                {
                    _items[index]=null;
                }
                OnInventoryUpdated?.Invoke(this);
                return ret.CopyItem();
            }
            else {
                OnGetItemFailed?.Invoke();
                return null;
            }
        }
        public void PutItem(Item i,int index)
        {
            if (i == null)
            {
                OnPutItemFailed?.Invoke();
                return;
            }
            var item=i.CopyItem();
            if (IsIndexInRange(index)&& _items[index] == null )
            {
                _items[index]=new(item,1);
            }
            else if (IsIndexInRange(index) && _items[index] != null && item.Id == _items[index].Item.Id&&item.State == _items[index].Item.State)
            {
                _items[index].AddItem();
            }
            else
            {
                OnPutItemFailed?.Invoke();
                return;
            }
            OnInventoryUpdated?.Invoke(this);
        
        }

        public void ClearInventory()
        {
            for(int i = 0; i < Size; ++i)
            {
                _items[i]=null;
            }
            OnInventoryUpdated?.Invoke(this);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsIndexInRange(int index)
        {
            return index>=0&&index<Size;
        }
    }
}
