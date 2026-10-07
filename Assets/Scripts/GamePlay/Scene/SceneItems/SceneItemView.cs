using System;
using GamePlay.Core;
using UnityEngine;

namespace GamePlay.Scene
{
    /// <summary>
    /// 场景物品 Prefab 上的数据绑定组件。它只记录场景物体对应的 Item 类型和状态，
    /// 不负责配方判断、背包扣除或场景交互。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SceneItemView : MonoBehaviour, ISceneItem
    {
        [SerializeField, HideInInspector]
        private string _itemTypeName;

        [SerializeField, HideInInspector]
        private int _state;

        private Item _item;

        /// <summary>当前绑定的物品数据副本；未绑定时返回 null。</summary>
        public Item Item => _item == null ? null : _item.CopyItem();

        /// <summary>当前绑定物品的完整类型名；未绑定时为空。</summary>
        public string ItemTypeName => _itemTypeName;

        /// <summary>当前绑定物品的状态；未绑定时为序列化的最后状态。</summary>
        public int State => _item != null ? _item.State : _state;

        public bool IsBound => _item != null || !string.IsNullOrWhiteSpace(_itemTypeName);

        private void Awake()
        {
            RestoreSerializedItem();
        }

        /// <summary>
        /// 将 Item 的副本绑定到场景物体，并同步可保存的类型名和状态。
        /// </summary>
        public bool TryBind(Item item)
        {
            if (item == null)
            {
                Debug.LogError("[SceneItemView] 不能绑定空的 Item。", this);
                return false;
            }

            _item = item.CopyItem();
            _itemTypeName = GetTypeName(_item.GetType());
            _state = _item.State;
            return true;
        }

        /// <summary>读取当前物品数据副本，避免外部直接修改组件内部状态。</summary>
        public bool TryGetItem(out Item item)
        {
            item = Item;
            return item != null;
        }

        /// <summary>清除绑定数据；物体本身不会被销毁。</summary>
        public void ClearBinding()
        {
            _item = null;
            _itemTypeName = string.Empty;
            _state = 0;
        }

        private void RestoreSerializedItem()
        {
            if (string.IsNullOrWhiteSpace(_itemTypeName))
            {
                return;
            }

            Type itemType = Type.GetType(_itemTypeName);
            if (itemType == null || !typeof(Item).IsAssignableFrom(itemType))
            {
                Debug.LogError(
                    $"[SceneItemView] 无法从类型名恢复 Item：{_itemTypeName}。",
                    this
                );
                return;
            }

            try
            {
                _item = Activator.CreateInstance(itemType) as Item;
                if (_item == null)
                {
                    Debug.LogError(
                        $"[SceneItemView] 类型不是有效的 Item：{_itemTypeName}。",
                        this
                    );
                    return;
                }

                _item.State = _state;
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    $"[SceneItemView] 恢复 Item 失败：{_itemTypeName}\n{exception}",
                    this
                );
            }
        }

        internal static string GetTypeName(Type itemType)
        {
            return itemType.FullName ?? itemType.ToString();
        }
    }
}
