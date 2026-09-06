using UnityEngine;

namespace TapTap21.AudioSystem.dyh
{
    /// <summary>单个音频资源的播放配置。</summary>
    [CreateAssetMenu(menuName = "Audio/Audio Config")]
    public class AudioConfig : ScriptableObject
    {
        /// <summary>音频 ID；为空时使用资源文件名。</summary>
        [HideInInspector]
        public string id;

        /// <summary>音频片段变体列表，每次播放时随机选择一个。</summary>
        [Header("音频文件列表")]
        public AudioClip[] clips;

        [Header("音频类别")]
        public AudioCategory category = AudioCategory.Sfx;

        [Header("音频是否循环")]
        public bool loop;

        [Header("是否采用 3D 音效")]
        public bool spatial3D;

        [Header("音量")]
        [Range(0, 1)]
        public float volume = 1f;

        [Header("音高范围")]
        public float pitchMin = 1f;
        public float pitchMax = 1f;

        [Header("播放优先级")]
        [Tooltip("数值越小，优先级越高。")]
        [Range(0, 256)]
        public int priority = 128;

        [Header("同一 ID 的最大同时播放数量，0 表示不限制。")]
        public int maxInstances;

        /// <summary>随机返回一个音频片段；未配置片段时返回 null。</summary>
        public AudioClip PickClip()
        {
            if (clips == null || clips.Length == 0)
                return null;

            return clips[Random.Range(0, clips.Length)];
        }

        /// <summary>返回注册时使用的唯一 ID。</summary>
        public string GetId()
        {
            return string.IsNullOrEmpty(id) ? name : id;
        }
    }
}
