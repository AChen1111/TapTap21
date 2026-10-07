using System.Collections.Generic;
using GamePlay.Core;
using UnityEngine;

namespace GamePlay.Scene
{
    /// <summary>
    /// 根据 Item 生成场景 Prefab，并在生成成功后安全替换旧场景物品。
    /// 不负责配方判断、拖拽、背包数量或交互提示。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SceneItemSpawner : MonoBehaviour
    {
        [SerializeField]
        private ItemPrefabCatalog _catalog;

        public ItemPrefabCatalog Catalog
        {
            get => _catalog;
            set => _catalog = value;
        }

        /// <summary>生成并绑定场景物品；失败时返回 null。</summary>
        public SceneItemView Spawn(
            Item item,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null)
        {
            return TrySpawn(item, position, rotation, parent, out SceneItemView result)
                ? result
                : null;
        }

        /// <summary>生成并绑定场景物品，失败原因通过日志说明。</summary>
        public bool TrySpawn(
            Item item,
            Vector3 position,
            Quaternion rotation,
            Transform parent,
            out SceneItemView result)
        {
            result = null;
            if (item == null)
            {
                Debug.LogError("[SceneItemSpawner] 不能生成空的 Item。", this);
                return false;
            }

            if (_catalog == null)
            {
                Debug.LogError("[SceneItemSpawner] 没有指定 ItemPrefabCatalog。", this);
                return false;
            }

            if (!_catalog.TryGetPrefab(item, out GameObject prefab) || prefab == null)
            {
                Debug.LogError(
                    $"[SceneItemSpawner] 找不到 Prefab：{SceneItemView.GetTypeName(item.GetType())}#{item.State}。",
                    this
                );
                return false;
            }

            GameObject instance = Instantiate(prefab, position, rotation, parent);
            SceneItemView view = instance.GetComponent<SceneItemView>();
            if (view == null)
            {
                view = instance.AddComponent<SceneItemView>();
            }

            if (!view.TryBind(item))
            {
                Destroy(instance);
                return false;
            }

            result = view;
            return true;
        }

        /// <summary>
        /// 在 anchor 的位置生成结果，并在生成成功后销毁 anchor 以及传入的其他旧物体。
        /// </summary>
        public bool TryReplace(
            Item resultItem,
            SceneItemView anchor,
            out SceneItemView replacement,
            params SceneItemView[] consumedItems)
        {
            replacement = null;
            if (anchor == null)
            {
                Debug.LogError("[SceneItemSpawner] 替换必须指定位置锚点。", this);
                return false;
            }

            Transform anchorTransform = anchor.transform;
            if (!TrySpawn(
                    resultItem,
                    anchorTransform.position,
                    anchorTransform.rotation,
                    anchorTransform.parent,
                    out replacement))
            {
                // 生成失败时不触碰任何旧物体。
                return false;
            }

            var uniqueItems = new HashSet<SceneItemView>();
            uniqueItems.Add(anchor);
            if (consumedItems != null)
            {
                foreach (SceneItemView item in consumedItems)
                {
                    if (item != null)
                    {
                        uniqueItems.Add(item);
                    }
                }
            }

            foreach (SceneItemView item in uniqueItems)
            {
                if (item != null && item.gameObject != replacement.gameObject)
                {
                    Destroy(item.gameObject);
                }
            }

            return true;
        }
    }
}
