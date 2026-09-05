using System.Collections.Generic;
using UnityEngine;

public sealed class AudioSourcePool : MonoBehaviour
{
    [SerializeField] private int initialSize = 12;
    private readonly List<AudioSource> sources = new List<AudioSource>();
    void Awake() { for (int i = 0; i < initialSize; i++) Create(); }
    AudioSource Create() { var go = new GameObject("AudioSource"); go.transform.SetParent(transform); var s = go.AddComponent<AudioSource>(); s.playOnAwake = false; sources.Add(s); return s; }
    public AudioSource Rent(int priority)
    {
        foreach (var s in sources) if (!s.isPlaying) return s;
        AudioSource candidate = null; foreach (var s in sources) if (candidate == null || s.priority > candidate.priority) candidate = s;
        if (candidate != null && priority < candidate.priority) { candidate.Stop(); return candidate; }
        return null;
    }
}
