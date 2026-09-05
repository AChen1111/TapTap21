using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioDatabase database;
    public AudioSourcePool pool;
    public AudioSettingsService settings;
    private AudioSource musicA, musicB, activeMusic;
    private readonly Dictionary<string, int> counts = new Dictionary<string, int>();
    private readonly Dictionary<string, AudioSource> loops = new Dictionary<string, AudioSource>();
    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject);
        if (pool == null) { var go = new GameObject("AudioSourcePool"); go.transform.SetParent(transform); pool = go.AddComponent<AudioSourcePool>(); }
        if (settings == null) settings = GetComponent<AudioSettingsService>();
        musicA = CreateMusicSource("MusicA"); musicB = CreateMusicSource("MusicB"); activeMusic = musicA;
        if (settings != null) settings.LoadVolumes();
    }
    AudioSource CreateMusicSource(string n) { var s = new GameObject(n).AddComponent<AudioSource>(); s.transform.SetParent(transform); s.playOnAwake = false; s.loop = true; return s; }
    public AudioSource PlaySfx(string id, Vector3? position = null) => Play(id, position, AudioCategory.Sfx);
    public AudioSource PlayUI(string id) => Play(id, null, AudioCategory.UI);
    public AudioSource PlayVoice(string id) => Play(id, null, AudioCategory.Voice);
    public AudioSource PlayLoop(string id, Object owner) { var key = id + "@" + (owner ? owner.GetInstanceID().ToString() : "global"); if (loops.ContainsKey(key)) return loops[key]; var component = owner as Component; var s = Play(id, component ? component.transform.position : (Vector3?)null, AudioCategory.Sfx, true); if (s != null) loops[key] = s; return s; }
    public void StopLoop(string id, Object owner) { var key = id + "@" + (owner ? owner.GetInstanceID().ToString() : "global"); if (loops.TryGetValue(key, out var s)) { s.Stop(); loops.Remove(key); } }
    AudioSource Play(string id, Vector3? pos, AudioCategory category, bool forceLoop = false)
    {
        if (database == null || !database.TryGet(id, out var c) || c == null) return null;
        if (c.category != category && category != AudioCategory.Sfx) category = c.category;
        if (c.maxInstances > 0 && counts.TryGetValue(id, out var n) && n >= c.maxInstances) return null;
        var s = pool.Rent(c.priority); if (s == null) return null;
        s.transform.position = pos ?? transform.position; s.clip = c.PickClip(); if (!s.clip) return null;
        s.outputAudioMixerGroup = c.mixerGroup; s.volume = c.volume; s.pitch = Random.Range(c.pitchMin, c.pitchMax); s.spatialBlend = c.spatial3D ? 1f : 0f; s.priority = c.priority; s.loop = forceLoop || c.loop; s.Play();
        if (!s.loop) StartCoroutine(ReleaseCount(s, id)); else counts[id] = counts.TryGetValue(id, out var x) ? x + 1 : 1;
        return s;
    }
    IEnumerator ReleaseCount(AudioSource s, string id) { counts[id] = counts.TryGetValue(id, out var x) ? x + 1 : 1; yield return new WaitWhile(() => s != null && s.isPlaying); if (counts.ContainsKey(id)) { counts[id]--; if (counts[id] <= 0) counts.Remove(id); } }
    public void PlayMusic(string id, float fadeTime = 1f) { if (database == null || !database.TryGet(id, out var c) || c == null) return; var next = activeMusic == musicA ? musicB : musicA; next.clip = c.PickClip(); next.outputAudioMixerGroup = c.mixerGroup; next.volume = 0; next.Play(); StartCoroutine(CrossFade(activeMusic, next, c.volume, fadeTime)); activeMusic = next; }
    IEnumerator CrossFade(AudioSource from, AudioSource to, float target, float duration) { float t = 0, start = from.volume; while (t < duration) { t += Time.unscaledDeltaTime; float k = duration <= 0 ? 1 : t / duration; from.volume = Mathf.Lerp(start, 0, k); to.volume = Mathf.Lerp(0, target, k); yield return null; } from.Stop(); from.volume = 0; to.volume = target; }
    public void StopMusic(float fadeTime = 1f) { if (activeMusic != null) StartCoroutine(CrossFade(activeMusic, activeMusic, 0, fadeTime)); }
    public void PauseAll() { foreach (var s in GetComponentsInChildren<AudioSource>()) s.Pause(); }
    public void ResumeAll() { foreach (var s in GetComponentsInChildren<AudioSource>()) s.UnPause(); }
}
