using System;
using System.Collections.Generic;
using GamePlay.Core;
using UnityEngine;

namespace GamePlay.Scene
{
    /// <summary>
    /// Item 类型和 State 到场景 Prefab 的映射表。Prefab 引用属于 Unity 资源，单独配置在这里，
    /// 不写入 Excel 配方表。
    /// </summary>
    [CreateAssetMenu(fileName = "ItemPrefabCatalog", menuName = "GamePlay/Scene/Item Prefab Catalog")]
    public sealed class ItemPrefabCatalog : ScriptableObject
    {
        [Serializable]
        public sealed class Entry
        {
            [SerializeField, Tooltip("完整类型名，例如 GamePlay.Core.Item_Tree。")]
            private string _itemTypeName;

            [SerializeField, Tooltip("与 Item.State 对应的数值。")]
            private int _state;

            [SerializeField]
            private GameObject _prefab;

            public string ItemTypeName => _itemTypeName;
            public int State => _state;
            public GameObject Prefab => _prefab;

            public Entry()
            {
            }

            public Entry(string itemTypeName, int state, GameObject prefab)
            {
                _itemTypeName = itemTypeName;
                _state = state;
                _prefab = prefab;
            }
        }

        [SerializeField]
        private List<Entry> _entries = new List<Entry>();

        private Dictionary<string, GameObject> _prefabByKey;

        public IReadOnlyList<Entry> Entries => _entries;

        private void OnEnable()
        {
            RebuildCache();
        }

        private void OnValidate()
        {
            RebuildCache();
        }

        /// <summary>按 Item 的完整类型名和 State 查找 Prefab。</summary>
        public bool TryGetPrefab(Item item, out GameObject prefab)
        {
            if (item == null)
            {
                prefab = null;
                return false;
            }

            return TryGetPrefab(SceneItemView.GetTypeName(item.GetType()), item.State, out prefab);
        }

        /// <summary>按完整类型名和 State 查找 Prefab。</summary>
        public bool TryGetPrefab(string itemTypeName, int state, out GameObject prefab)
        {
            EnsureCache();
            return _prefabByKey.TryGetValue(MakeKey(itemTypeName, state), out prefab) && prefab != null;
        }

        /// <summary>
        /// 运行时补充或覆盖映射。通常应直接在 Inspector 配置，运行时接口主要供测试或动态资源系统使用。
        /// </summary>
        public void AddOrReplace(string itemTypeName, int state, GameObject prefab)
        {
            if (string.IsNullOrWhiteSpace(itemTypeName))
            {
                throw new ArgumentException("Item 类型名不能为空。", nameof(itemTypeName));
            }

            for (int i = 0; i < _entries.Count; i++)
            {
                Entry entry = _entries[i];
                if (entry.ItemTypeName == itemTypeName && entry.State == state)
                {
                    _entries[i] = new Entry(itemTypeName, state, prefab);
                    RebuildCache();
                    return;
                }
            }

            _entries.Add(new Entry(itemTypeName, state, prefab));
            RebuildCache();
        }

        private void EnsureCache()
        {
            if (_prefabByKey == null)
            {
                RebuildCache();
            }
        }

        private void RebuildCache()
        {
            _prefabByKey = new Dictionary<string, GameObject>();
            if (_entries == null)
            {
                return;
            }

            foreach (Entry entry in _entries)
            {
                if (entry == null || string.IsNullOrWhiteSpace(entry.ItemTypeName))
                {
                    continue;
                }

                string key = MakeKey(entry.ItemTypeName, entry.State);
                if (_prefabByKey.ContainsKey(key))
                {
                    Debug.LogError(
                        $"[ItemPrefabCatalog] 存在重复映射：{entry.ItemTypeName}#{entry.State}。",
                        this
                    );
                    continue;
                }

                // 空引用表示该资源还没有对应 Prefab；保留 Inspector 行，但不加入可用缓存。
                if (entry.Prefab != null)
                {
                    _prefabByKey.Add(key, entry.Prefab);
                }
            }
        }

        private static string MakeKey(string itemTypeName, int state)
        {
            return itemTypeName + "#" + state;
        }
    }
}
