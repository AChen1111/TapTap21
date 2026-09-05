using System;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioCategory { Music, Sfx, Voice, Ambience, UI }
public enum AudioVolumeType { Master, Music, Sfx, Voice, Ambience, UI }

[CreateAssetMenu(menuName = "Audio/Audio Config")]
public class AudioConfig : ScriptableObject
{
    public string id;
    public AudioClip[] clips;
    public AudioCategory category = AudioCategory.Sfx;
    public bool loop;
    public bool spatial3D;
    [Range(0, 1)] public float volume = 1f;
    public float pitchMin = 1f, pitchMax = 1f;
    [Range(0, 256)] public int priority = 128;
    public int maxInstances = 0;
    public AudioMixerGroup mixerGroup;
    public AudioClip PickClip() => clips == null || clips.Length == 0 ? null : clips[UnityEngine.Random.Range(0, clips.Length)];
}

[CreateAssetMenu(menuName = "Audio/Audio Database")]
public class AudioDatabase : ScriptableObject
{
    public AudioConfig[] entries;
    [NonSerialized] private System.Collections.Generic.Dictionary<string, AudioConfig> lookup;
    public bool TryGet(string id, out AudioConfig config)
    {
        if (lookup == null) { lookup = new System.Collections.Generic.Dictionary<string, AudioConfig>(StringComparer.Ordinal); if (entries != null) foreach (var e in entries) if (e != null && !string.IsNullOrEmpty(e.id)) lookup[e.id] = e; }
        return lookup.TryGetValue(id, out config);
    }
}
