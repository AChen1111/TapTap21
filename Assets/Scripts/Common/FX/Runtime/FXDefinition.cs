using UnityEngine;

namespace FXSystem
{
    /// <summary>
    /// 一条特效配置：Prefab、池上限、2D 排序、切场行为。
    /// Id 为空时使用资产文件名。
    /// </summary>
    [CreateAssetMenu(menuName = "FX/Definition", fileName = "FX_")]
    public class FXDefinition : ScriptableObject
    {
        [Tooltip("业务侧字符串 ID。留空则使用资产名。")]
        public string Id;

        public GameObject Prefab;

        [Tooltip("World：场景特效。UI：放到 FX Canvas 下，可用 PlayUI 接口。")]
        public FXSpace Space = FXSpace.World;

        [Tooltip("仅 UI。1 表示按 Prefab 原尺寸；粒子按世界单位制作时可调大。")]
        public float UIScale = 1f;

        [Tooltip("UI 播放时在实例上覆盖 Canvas 排序，便于叠在 HUD 之上。")]
        public bool OverrideCanvasSorting = true;

        [Tooltip("循环特效不会自动回收，必须 Stop 或切场清理。")]
        public bool Looping;

        [Min(0)]
        [Tooltip("粒子全部结束后再延迟回收的秒数。")]
        public float ExtraLifetime;

        [Min(0)]
        public int PrewarmCount = 4;

        [Min(1)]
        [Tooltip("该类型同时存在的最大实例数。")]
        public int MaxActive = 24;

        public FXOverflowPolicy Overflow = FXOverflowPolicy.RecycleOldest;

        [SortingLayerName]
        public string SortingLayer = "Default";

        public int SortingOrder;

        [Tooltip("切场景时是否强制回收。世界特效建议勾选。")]
        public bool RecycleOnSceneChange = true;

        public string ResolvedId => string.IsNullOrWhiteSpace(Id) ? name : Id;
    }
}
