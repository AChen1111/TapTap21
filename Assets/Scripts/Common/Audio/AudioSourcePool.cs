using System.Collections.Generic;
using System;
using UnityEngine;

namespace dyh
{

public sealed class AudioSourcePool : MonoBehaviour
{
    /// <summary>当对象池为了高优先级请求抢占播放器时触发。</summary>
    public event Action<AudioSource> SourceStolen;

    /// <summary>暂停期间禁止租借播放器。</summary>
    public bool IsPaused { get; set; }
    /// <summary>初始化时创建的可复用播放器数量。</summary>
    [SerializeField] private int initialSize = 12;
    /// <summary>播放器数量上限；设置为 0 表示不限制。</summary>
    [SerializeField] private int maxSize;
    /// <summary>当前由对象池管理的播放器列表。</summary>
    private readonly List<AudioSource> sources = new List<AudioSource>();
    private void Awake()
    {
        for (int i = 0; i < Mathf.Max(0, initialSize); i++)
            Create();
    }
    private AudioSource Create()
    {
        var go = new GameObject("AudioSource");
        go.transform.SetParent(transform);
        var source = go.AddComponent<AudioSource>();
        source.playOnAwake = false;
        sources.Add(source);
        return source;
    }
    /// <summary>获取空闲播放器；必要时替换优先级较低的播放器。</summary>
    public AudioSource Rent(int priority)
    {
        if (IsPaused)
            return null;

        foreach (var source in sources)
        {
            if (!source.isPlaying)
                return source;
        }
        AudioSource candidate = null;
        foreach (var source in sources)
        {
            if (candidate == null || source.priority > candidate.priority)
                candidate = source;
        }

        if (candidate != null && priority < candidate.priority)
        {
            SourceStolen?.Invoke(candidate);
            candidate.Stop();
            return candidate;
        }

        if (maxSize <= 0 || sources.Count < maxSize)
            return Create();

        return null;
    }
}
}
