using GamePlay.Core;
using UnityEngine;

namespace GamePlay.Scene
{
    /// <summary>
    /// 场景物品交互层入口。
    ///
    /// 拖拽、射线检测、背包扣除和昼夜条件由调用方负责；调用方把拖拽得到的 Item、
    /// 世界位置、命中的 SceneItemView 和已经确定的 op 传进来，本组件负责把它们串成
    /// “放置生成”或“配方融合并替换”的完整流程。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SceneItemInteractionController : MonoBehaviour
    {
        [SerializeField]
        private SceneItemSpawner _spawner;

        public SceneItemSpawner Spawner
        {
            get => _spawner;
            set => _spawner = value;
        }

        /// <summary>
        /// 把背包中的 Item 放到空场景位置。
        /// 生成成功后，调用方再扣除背包数量。
        /// </summary>
        public bool TryPlace(
            Item item,
            Vector3 worldPosition,
            Quaternion rotation,
            Transform parent,
            out SceneItemView sceneView)
        {
            sceneView = null;
            if (_spawner == null)
            {
                Debug.LogError("[SceneItemInteractionController] 没有指定 SceneItemSpawner。", this);
                return false;
            }

            return _spawner.TrySpawn(
                item,
                worldPosition,
                rotation,
                parent,
                out sceneView
            );
        }

        /// <summary>
        /// 把一个拖拽物品与目标场景物品融合。
        /// op 必须由调用方根据交互行为和环境条件确定。
        /// </summary>
        public bool TryMerge(
            Item draggedItem,
            SceneItemView target,
            int op,
            out SceneItemView replacement,
            params SceneItemView[] additionalConsumedItems)
        {
            replacement = null;
            if (_spawner == null)
            {
                Debug.LogError("[SceneItemInteractionController] 没有指定 SceneItemSpawner。", this);
                return false;
            }

            if (draggedItem == null || target == null)
            {
                Debug.LogError("[SceneItemInteractionController] 融合需要拖拽物品和目标物品。", this);
                return false;
            }

            if (!target.TryGetItem(out Item targetItem))
            {
                Debug.LogError("[SceneItemInteractionController] 目标场景物品没有绑定 Item。", target);
                return false;
            }

            Item result = MergeUtil.Merge(draggedItem, targetItem, op);
            if (result == null)
            {
                return false;
            }

            return _spawner.TryReplace(
                result,
                target,
                out replacement,
                additionalConsumedItems
            );
        }

        /// <summary>
        /// 拖拽结束后的统一入口：命中目标时融合，未命中目标时放置。
        ///
        /// draggedItem 通常来自背包；如果它本身是场景物品，调用方把对应的
        /// SceneItemView 放入 additionalConsumedItems，成功后会一并销毁。
        /// </summary>
        public bool TryDrop(
            Item draggedItem,
            Vector3 worldPosition,
            Quaternion rotation,
            Transform parent,
            SceneItemView target,
            int op,
            out SceneItemView resultView,
            params SceneItemView[] additionalConsumedItems)
        {
            if (target == null)
            {
                return TryPlace(
                    draggedItem,
                    worldPosition,
                    rotation,
                    parent,
                    out resultView
                );
            }

            return TryMerge(
                draggedItem,
                target,
                op,
                out resultView,
                additionalConsumedItems
            );
        }
    }
}
