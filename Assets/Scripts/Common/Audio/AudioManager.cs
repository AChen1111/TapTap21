using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace dyh
{

public class AudioManager : MonoBehaviour
{
    /// <summary>跨场景持久化的全局音频管理器。</summary>
    public static AudioManager Instance { get; private set; }
    /// <summary>根据字符串 ID 查找音频配置的运行时数据库。</summary>
    public AudioDatabase database;
    /// <summary>用于一次性音效和循环音效的播放器对象池。</summary>
    public AudioSourcePool pool;
    /// <summary>负责应用并保存音量设置的服务。</summary>
    public AudioSettingsService settings;
    private AudioSource musicA, musicB, activeMusic;
    private readonly Dictionary<string, int> counts = new Dictionary<string, int>();
    private readonly Dictionary<string, LoopPlayback> loops = new Dictionary<string, LoopPlayback>();
    private readonly Queue<string> voiceQueue = new Queue<string>();
    private AudioSource activeVoice;
    private string activeVoiceId;
    private Coroutine musicFadeCoroutine;
    private bool isPaused;

    private sealed class LoopPlayback
    {
        public Object Owner;
        public AudioSource Source;
        public string Id;
        public bool HasOwner;
    }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (pool == null)
        {
            var poolObject = new GameObject("AudioSourcePool");
            poolObject.transform.SetParent(transform);
            pool = poolObject.AddComponent<AudioSourcePool>();
        }

        if (settings == null)
            settings = GetComponent<AudioSettingsService>();

        musicA = CreateMusicSource("MusicA");
        musicB = CreateMusicSource("MusicB");
        activeMusic = musicA;

        if (settings != null)
            settings.LoadVolumes();

        pool.SourceStolen += OnPooledSourceStolen;
    }

    private void OnDestroy()
    {
        if (pool != null)
            pool.SourceStolen -= OnPooledSourceStolen;

        if (Instance == this)
            Instance = null;
    }

    private void Update()
    {
        if (isPaused)
            return;

        CleanupDestroyedLoopOwners();

        if ((activeVoice == null || !activeVoice.isPlaying) && voiceQueue.Count > 0)
            PlayVoice(voiceQueue.Dequeue());
    }
    private AudioSource CreateMusicSource(string name)
    {
        var source = new GameObject(name).AddComponent<AudioSource>();
        source.transform.SetParent(transform);
        source.playOnAwake = false;
        source.loop = true;
        return source;
    }

    /// <summary>播放普通音效，可选传入世界坐标。</summary>
    public AudioSource PlaySfx(string id, Vector3? position = null) => Play(id, position, AudioCategory.Sfx);

    /// <summary>播放环境音，可选传入世界坐标。</summary>
    public AudioSource PlayAmbience(string id, Vector3? position = null)
    {
        return Play(id, position, AudioCategory.Ambience);
    }
    /// <summary>播放二维 UI 音效。</summary>
    public AudioSource PlayUI(string id) => Play(id, null, AudioCategory.UI);
    /// <summary>播放语音或旁白。</summary>
    public AudioSource PlayVoice(string id)
    {
        if (activeVoice != null && activeVoice.isPlaying)
        {
            activeVoice.Stop();
            ReleaseCountImmediately(activeVoiceId);
        }

        activeVoice = Play(id, null, AudioCategory.Voice);
        activeVoiceId = activeVoice != null ? id : null;
        return activeVoice;
    }

    /// <summary>将语音加入队列，在当前语音结束后自动播放。</summary>
    public void EnqueueVoice(string id)
    {
        if (activeVoice == null || !activeVoice.isPlaying)
            PlayVoice(id);
        else
            voiceQueue.Enqueue(id);
    }
    /// <summary>为指定对象启动循环音效，避免重复播放。</summary>
    public AudioSource PlayLoop(string id, Object owner)
    {
        var key = id + "@" + (owner ? owner.GetInstanceID().ToString() : "global");
        if (loops.TryGetValue(key, out var existing))
            return existing.Source;

        var component = owner as Component;
        var source = Play(
            id,
            component ? component.transform.position : (Vector3?)null,
            AudioCategory.Sfx,
            true);

        if (source != null)
        {
            loops[key] = new LoopPlayback
            {
                Owner = owner,
                Source = source,
                Id = id,
                HasOwner = owner != null
            };
        }
        return source;
    }

    /// <summary>停止指定对象关联的循环音效。</summary>
    public void StopLoop(string id, Object owner)
    {
        var key = id + "@" + (owner ? owner.GetInstanceID().ToString() : "global");
        if (loops.TryGetValue(key, out var playback))
        {
            playback.Source.Stop();
            ReleaseCountImmediately(id);
            loops.Remove(key);
        }
    }

    private void CleanupDestroyedLoopOwners()
    {
        var keysToRemove = new List<string>();

        foreach (var pair in loops)
        {
            if (pair.Value.HasOwner && pair.Value.Owner == null)
            {
                pair.Value.Source.Stop();
                ReleaseCountImmediately(pair.Value.Id);
                keysToRemove.Add(pair.Key);
            }
            else if (!pair.Value.Source.isPlaying)
            {
                ReleaseCountImmediately(pair.Value.Id);
                keysToRemove.Add(pair.Key);
            }
            else if (pair.Value.Owner is Component component)
            {
                pair.Value.Source.transform.position = component.transform.position;
            }
            else if (pair.Value.Owner is GameObject gameObject)
            {
                pair.Value.Source.transform.position = gameObject.transform.position;
            }
        }

        foreach (var key in keysToRemove)
            loops.Remove(key);
    }

    private void OnPooledSourceStolen(AudioSource source)
    {
        var loopKeys = new List<string>();
        foreach (var pair in loops)
        {
            if (pair.Value.Source == source)
            {
                ReleaseCountImmediately(pair.Value.Id);
                loopKeys.Add(pair.Key);
            }
        }

        foreach (var key in loopKeys)
            loops.Remove(key);

        if (activeVoice == source)
        {
            ReleaseCountImmediately(activeVoiceId);
            activeVoice = null;
            activeVoiceId = null;
        }
    }
    private AudioSource Play(string id, Vector3? pos, AudioCategory category, bool forceLoop = false)
    {
        if (database == null || !database.TryGet(id, out var c) || c == null)
            return null;

        if (c.category != category && category != AudioCategory.Sfx)
            category = c.category;

        if (c.maxInstances > 0 && counts.TryGetValue(id, out var n) && n >= c.maxInstances)
            return null;

        var s = pool.Rent(c.priority);
        if (s == null)
            return null;
        s.transform.position = pos ?? transform.position;
        s.clip = c.PickClip();

        if (!s.clip)
            return null;

        s.outputAudioMixerGroup = GetMixerGroup(c.category);
        s.volume = c.volume;
        s.pitch = Random.Range(c.pitchMin, c.pitchMax);
        s.spatialBlend = c.spatial3D ? 1f : 0f;
        s.priority = c.priority;
        s.loop = forceLoop || c.loop;
        s.Play();
        counts[id] = counts.TryGetValue(id, out var count) ? count + 1 : 1;
        if (!s.loop)
            StartCoroutine(ReleaseCount(s, id));


        return s;
    }
    private IEnumerator ReleaseCount(AudioSource source, string id)
    {
        yield return new WaitWhile(() => source != null && source.isPlaying);

        if (!counts.ContainsKey(id))
            yield break;

        counts[id]--;
        if (counts[id] <= 0)
            counts.Remove(id);
    }

    private void ReleaseCountImmediately(string id)
    {
        if (string.IsNullOrEmpty(id) || !counts.ContainsKey(id))
            return;

        counts[id]--;
        if (counts[id] <= 0)
            counts.Remove(id);
    }
    /// <summary>播放音乐并进行交叉淡入淡出；管理器会跨场景保留。</summary>
    public void PlayMusic(string id, float fadeTime = 1f)
    {
        if (database == null || !database.TryGet(id, out var config) || config == null)
            return;

        var next = activeMusic == musicA ? musicB : musicA;
        var clip = config.PickClip();
        if (clip == null)
            return;

        next.clip = clip;
        next.outputAudioMixerGroup = GetMixerGroup(AudioCategory.Music);
        next.volume = 0f;
        next.Play();

        if (musicFadeCoroutine != null)
            StopCoroutine(musicFadeCoroutine);

        musicFadeCoroutine = StartCoroutine(CrossFade(activeMusic, next, config.volume, fadeTime));
        activeMusic = next;
    }
    /// <summary>执行音乐淡出协程。</summary>
    private IEnumerator CrossFade(AudioSource from, AudioSource to, float target, float duration)
    {
        float elapsed = 0f;
        float startVolume = from.volume;

        while (elapsed < duration)
        {
            if (isPaused)
            {
                yield return null;
                continue;
            }

            elapsed += Time.unscaledDeltaTime;
            float progress = duration <= 0f ? 1f : elapsed / duration;
            from.volume = Mathf.Lerp(startVolume, 0f, progress);
            to.volume = Mathf.Lerp(0f, target, progress);
            yield return null;
        }

        from.Stop();
        from.volume = 0f;
        to.volume = target;
        musicFadeCoroutine = null;
    }
    public void StopMusic(float fadeTime = 1f)
    {
        if (activeMusic != null)
        {
            if (musicFadeCoroutine != null)
                StopCoroutine(musicFadeCoroutine);

            musicFadeCoroutine = StartCoroutine(FadeOut(activeMusic, fadeTime));
        }
    }
    /// <summary>Pauses every managed audio source.</summary>
    /// <summary>暂停所有由管理器维护的音频播放器。</summary>
    public void PauseAll()
    {
        isPaused = true;
        if (pool != null)
            pool.IsPaused = true;

        foreach (var source in GetComponentsInChildren<AudioSource>())
            source.Pause();
    }

    /// <summary>恢复所有由管理器维护的音频播放器。</summary>
    public void ResumeAll()
    {
        isPaused = false;
        if (pool != null)
            pool.IsPaused = false;

        foreach (var source in GetComponentsInChildren<AudioSource>())
            source.UnPause();
    }

    private IEnumerator FadeOut(AudioSource source, float duration)
    {
        float elapsed = 0f;
        float startVolume = source.volume;

        while (elapsed < duration)
        {
            if (isPaused)
            {
                yield return null;
                continue;
            }

            elapsed += Time.unscaledDeltaTime;
            float progress = duration <= 0f ? 1f : elapsed / duration;
            source.volume = Mathf.Lerp(startVolume, 0f, progress);
            yield return null;
        }

        source.Stop();
        source.volume = 0f;
        musicFadeCoroutine = null;
    }

    /// <summary>根据音频分类查找对应的 AudioMixer 分组。</summary>
    private AudioMixerGroup GetMixerGroup(AudioCategory category)
    {
        if (settings == null || settings.mixer == null)
            return null;

        var groupName = category.ToString();
        var groups = settings.mixer.FindMatchingGroups(groupName);

        foreach (var group in groups)
        {
            if (group != null && group.name == groupName)
                return group;
        }

        return groups.Length > 0 ? groups[0] : null;
    }
}
}
