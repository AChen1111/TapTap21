using UnityEngine;

namespace Effects
{
    public enum ScreenEffectKind
    {
        Blit = 0,
        Outline = 1,
    }

    [CreateAssetMenu(menuName = "Effects/Screen Effect", fileName = "ScreenEffect")]
    public class ScreenEffectProfile : ScriptableObject
    {
        public ScreenEffectKind kind;

        [Tooltip("播放时长（秒）。小于等于 0 表示一直播放，直到 Stop。")]
        public float duration = 0.2f;

        [Tooltip("到时后从头再播。时长小于等于 0 时无效。")]
        public bool loop;

        [Header("Blit")]
        [Tooltip("全屏材质。播放器每帧把曲线写入 _Intensity。")]
        public Material material;

        [Tooltip("横轴 0 到 1 对应该效果的时长，纵轴写入材质的 _Intensity。")]
        public AnimationCurve intensity = AnimationCurve.Linear(0f, 1f, 1f, 1f);

        
    }

}
