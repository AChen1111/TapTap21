using System;
using Sirenix.OdinInspector;
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
    [Header("音频文件列表")] public AudioClip[] clips;
    [Header("音频类别")] public AudioCategory category = AudioCategory.Sfx;
    [Header("音频是否循环")] public bool loop;
    [Header("是否采用3D音效")] public bool spatial3D;
    [Header("音量高低")][Range(0, 1)] public float volume = 1f;
    [Header("音高范围")] public float pitchMin = 1f, pitchMax = 1f;
    [Header("播放优先级")][Tooltip("数值越小，优先级越高。")][Range(0, 256)] public int priority = 128;
    [Header("同一 ID 的最大同时播放数量，0 表示不限制。Music不存在同时播放")]public int maxInstances = 0;
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
        var found = lookup.TryGetValue(id, out config);
        if (found)
            Debug.Log($"[AudioDatabase] 查找成功，id={id}，config={config.name}。", this);
        else
            Debug.LogWarning($"[AudioDatabase] 查找失败，id={id}。", this);
        return found;
    }
}
}
