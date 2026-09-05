using System;
using UnityEngine;

namespace dyh
{

/// <summary>用于路由和音量控制的音频分类。</summary>
public enum AudioCategory { Music, Sfx, Voice, Ambience, UI }
/// <summary>由音量设置服务控制的音量通道。</summary>
public enum AudioVolumeType { Master, Music, Sfx, Voice, Ambience, UI }

[CreateAssetMenu(menuName = "Audio/Audio Config")]
public class AudioConfig : ScriptableObject
{
    /// <summary>兼容旧数据的音频 ID；为空时自动使用资源文件名。</summary>
    [HideInInspector] public string id;
    /// <summary>音频变体列表，每次播放时随机选择一个。</summary>
    public AudioClip[] clips;
    public AudioCategory category = AudioCategory.Sfx;
    public bool loop;
    public bool spatial3D;
    [Range(0, 1)] public float volume = 1f;
    public float pitchMin = 1f, pitchMax = 1f;
    [Range(0, 256)] public int priority = 128;
    public int maxInstances = 0;
    /// <summary>随机返回一个音频片段；未配置片段时返回 null。</summary>
    public AudioClip PickClip()
    {
        if (clips == null || clips.Length == 0)
            return null;

        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }

    /// <summary>返回注册时使用的唯一 ID。</summary>
    public string GetId()
    {
        return string.IsNullOrEmpty(id) ? name : id;
    }
}

[CreateAssetMenu(menuName = "Audio/Audio Database")]
public class AudioDatabase : ScriptableObject
{
    /// <summary>所有可用的音频配置，首次查询时建立 ID 索引。</summary>
    public AudioConfig[] entries;
    [NonSerialized] private System.Collections.Generic.Dictionary<string, AudioConfig> lookup;

    private void OnEnable()
    {
        lookup = null;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        var guids = UnityEditor.AssetDatabase.FindAssets("t:AudioConfig");
        var configs = new System.Collections.Generic.List<AudioConfig>();

        foreach (var guid in guids)
        {
            var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            var config = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioConfig>(path);
            if (config != null)
                configs.Add(config);
        }

        configs.Sort((left, right) => string.Compare(left.GetId(), right.GetId(), StringComparison.Ordinal));
        entries = configs.ToArray();
        lookup = null;
    }
#endif

    public bool TryGet(string id, out AudioConfig config)
    {
        if (lookup == null)
        {
            lookup = new System.Collections.Generic.Dictionary<string, AudioConfig>(StringComparer.Ordinal);

            if (entries != null)
            {
                foreach (var entry in entries)
                {
                    if (entry != null && !string.IsNullOrEmpty(entry.GetId()))
                    {
                        var entryId = entry.GetId();
                        if (lookup.ContainsKey(entryId))
                            Debug.LogWarning($"音频 ID 重复，后面的配置将覆盖前面的配置：{entryId}", this);

                        lookup[entryId] = entry;
                    }
                }
            }
        }
        return lookup.TryGetValue(id, out config);
    }
}
}
