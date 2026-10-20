using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.CameraSystem
{
    /// <summary>Polygon 的范围同时用于玩家所在区块判断和 Cinemachine 画面边界。</summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PolygonCollider2D))]
    public sealed class CameraRegion2D : MonoBehaviour
    {
        private static readonly List<CameraRegion2D> Regions = new List<CameraRegion2D>();
        [SerializeField, Tooltip("区块重叠时，数值更高的优先；同优先级保持当前区块。")]
        private int _priority;
        private PolygonCollider2D _boundary;

        public PolygonCollider2D Boundary => _boundary != null ? _boundary : _boundary = GetComponent<PolygonCollider2D>();
        public int Priority => _priority;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetRegistry() => Regions.Clear();

        private void Reset()
        {
            Boundary.isTrigger = true;
        }

        private void OnEnable()
        {
            Boundary.isTrigger = true;
            if (!Regions.Contains(this)) Regions.Add(this);
        }

        private void OnDisable() => Regions.Remove(this);

        public bool Contains(Vector2 worldPosition)
        {
            return isActiveAndEnabled && Boundary.enabled && Boundary.OverlapPoint(worldPosition);
        }

        public static CameraRegion2D FindRegion(Vector2 position, CameraRegion2D current)
        {
            CameraRegion2D best = current != null && current.Contains(position) ? current : null;
            foreach (CameraRegion2D region in Regions)
            {
                if (region == null || !region.Contains(position)) continue;
                if (best == null || region.Priority > best.Priority) best = region;
            }
            return best;
        }
    }
}
