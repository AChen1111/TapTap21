using AChen.Events;
using GamePlay.Core;
using GamePlay.Inventory;
using UnityEngine;

namespace GamePlay.Scene
{
    /// <summary>
    /// 把背包 SlotDragger 的结束事件接到场景交互入口。
    /// 负责屏幕坐标转世界坐标和目标物体查找，具体配方仍由 op 决定。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SceneItemDragDropBridge : MonoBehaviour
    {
        [SerializeField]
        private SceneItemInteractionController _interaction;

        [SerializeField]
        private Camera _worldCamera;

        [SerializeField]
        private Transform _sceneParent;

        [SerializeField]
        private float _worldZ;

        [SerializeField, Min(0f)]
        private float _targetRadius = 0.5f;

        [SerializeField]
        private int _defaultOperation = 1;

        public SceneItemInteractionController Interaction
        {
            get => _interaction;
            set => _interaction = value;
        }

        private void Awake()
        {
            if (_interaction == null)
            {
                _interaction = GetComponent<SceneItemInteractionController>();
            }

            if (_worldCamera == null)
            {
                _worldCamera = Camera.main;
            }
        }

        private void OnEnable()
        {
            EventCenter.AddListener(GameEvent.InventoryDragEnded, OnInventoryDragEnded);
        }

        private void OnDisable()
        {
            EventCenter.RemoveListener(GameEvent.InventoryDragEnded, OnInventoryDragEnded);
        }

        private void OnInventoryDragEnded(InventoryDragContext context)
        {
            if (context == null || context.Item == null)
            {
                return;
            }

            if (_interaction == null)
            {
                Debug.LogError("[SceneItemDragDropBridge] 没有指定 SceneItemInteractionController。", this);
                return;
            }

            if (_worldCamera == null)
            {
                _worldCamera = Camera.main;
            }

            if (_worldCamera == null)
            {
                Debug.LogError("[SceneItemDragDropBridge] 没有可用的世界相机。", this);
                return;
            }

            Vector3 worldPosition = ScreenToWorld(context.ScreenPosition);
            SceneItemView target = FindTarget(worldPosition);
            int operation = ResolveOperation(target);

            bool success = _interaction.TryDrop(
                context.Item,
                worldPosition,
                Quaternion.identity,
                _sceneParent,
                target,
                operation,
                out SceneItemView resultView
            );

            if (!success)
            {
                // 失败时不取出背包物品，SlotDragger 会保留原格子内容。
                return;
            }

            if (resultView != null && resultView.TryGetItem(out Item resultItem))
            {
                Debug.Log(
                    $"[SceneItemDragDropBridge] {(target == null ? "放置" : "融合")}成功：{resultItem.GetType().FullName} State={resultItem.State}",
                    resultView
                );
            }

            if (context.Inventory == null)
            {
                return;
            }

            if (context.Inventory.GetItem(context.SlotIndex) == null)
            {
                Debug.LogError(
                    $"[SceneItemDragDropBridge] 场景生成成功，但无法扣除背包格子 {context.SlotIndex}。",
                    this
                );
            }
        }

        private Vector3 ScreenToWorld(Vector2 screenPosition)
        {
            float distance = _worldZ - _worldCamera.transform.position.z;
            Vector3 screenPoint = new(screenPosition.x, screenPosition.y, distance);
            return _worldCamera.ScreenToWorldPoint(screenPoint);
        }

        private SceneItemView FindTarget(Vector3 worldPosition)
        {
            SceneItemView[] views = FindObjectsByType<SceneItemView>(
                FindObjectsInactive.Exclude,
                FindObjectsSortMode.None
            );
            SceneItemView nearest = null;
            float nearestDistance = _targetRadius * _targetRadius;

            foreach (SceneItemView view in views)
            {
                if (view == null)
                {
                    continue;
                }

                float distance = (view.transform.position - worldPosition).sqrMagnitude;
                if (distance <= nearestDistance)
                {
                    nearest = view;
                    nearestDistance = distance;
                }
            }

            return nearest;
        }

        private int ResolveOperation(SceneItemView target)
        {
            if (target != null &&
                target.TryGetComponent(out SceneItemMergeTarget mergeTarget))
            {
                return mergeTarget.Operation;
            }

            return _defaultOperation;
        }
    }
}
