using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(menuName = "Effects/Sprite Effect", fileName = "SpriteEffect")]
    public class SpriteEffectProfile : ScriptableObject
    {
        [Tooltip("播放时长（秒）。小于等于 0 表示一直播放，直到 Stop。")]
        public float duration = 1f;

        [Tooltip("到时后从头再播。时长小于等于 0 时无效。")]
        public bool loop;

        [Tooltip("溶解和形变用的噪声图。留空则用程序噪声。")]
        public Texture2D noise;

        [Tooltip("噪声缩放。越大，溶解颗粒越碎。")]
        public float noiseScale = 4f;

        [Tooltip("横轴 0 到 1 对应该效果的时长。0 表示不溶解，1 表示完全溶解。")]
        public AnimationCurve dissolve = AnimationCurve.Linear(0f, 0f, 1f, 0f);

        [Tooltip("溶解边缘的宽度，按噪声值计算。")]
        public float dissolveEdgeWidth = 0.08f;

        public Color dissolveEdgeColor = new Color(1f, 0.45f, 0.1f, 1f);

        public Color flashColor = Color.white;

        [Tooltip("横轴 0 到 1 对应该效果的时长。0 表示原色，1 表示完全变成闪白颜色。")]
        public AnimationCurve flash = AnimationCurve.Linear(0f, 0f, 1f, 0f);

        [Tooltip("横轴 0 到 1 对应该效果的时长。这是 UV 偏移幅度，0.02 到 0.05 通常就够。")]
        public AnimationCurve distort = AnimationCurve.Linear(0f, 0f, 1f, 0f);
    }

}

