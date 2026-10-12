using UnityEngine;

namespace FXSystem
{
    /// <summary>
    /// 全局设置。放到 Resources 下并命名为 FXSettings，即可自动引导。
    /// </summary>
    [CreateAssetMenu(menuName = "FX/Settings", fileName = "FXSettings")]
    public class FXSettings : ScriptableObject
    {
        public FXCatalog Catalog;

        [Min(1)]
        [Tooltip("所有特效实例的全局硬顶，防止 Expand 策略失控。")]
        public int GlobalMaxActive = 256;

        public bool LogWarnings = true;

        [Tooltip("主场景切换时回收 RecycleOnSceneChange 的实例。")]
        public bool ClearOnActiveSceneChange = true;

        [Header("UI")]
        [Tooltip("FX Canvas 的 sortingOrder。粒子在 Overlay Canvas 下通常不可见，因此默认使用 Screen Space - Camera。")]
        public int UICanvasSortingOrder = 200;

        [Min(0.1f)]
        public float UIPlaneDistance = 10f;

        public Vector2 UIReferenceResolution = new Vector2(1920f, 1080f);
    }
}
