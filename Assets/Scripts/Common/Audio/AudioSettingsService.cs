using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace TapTap21.AudioSystem.dyh
{

public class AudioSettingsService : MonoBehaviour
{
    /// <summary>跨场景持久化的音量设置服务实例。</summary>
    public static AudioSettingsService Instance { get; private set; }
    /// <summary>包含各音量参数的 AudioMixer。</summary>
    public AudioMixer mixer;
    /// <summary>音频设置在 PlayerPrefs 中使用的键名前缀。</summary>
    const string Prefix = "Audio_";
    private readonly HashSet<string> warnedParameters = new HashSet<string>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>设置、保存并应用一个 0 到 1 范围内的音量值。</summary>
    public void SetVolume(AudioVolumeType type, float value)
    {
        value = Mathf.Clamp01(value);
        PlayerPrefs.SetFloat(Prefix + type, value);
        PlayerPrefs.Save();
        Debug.Log($"[AudioSettingsService] 音量修改并保存，type={type}，value={value:0.000}。", this);

        if (mixer != null)
        {
            var decibel = value <= 0.0001f ? -80f : Mathf.Log10(value) * 20f;
            var parameterName = type.ToString();
            if (mixer.GetFloat(parameterName, out _))
                mixer.SetFloat(parameterName, decibel);
            else if (warnedParameters.Add(parameterName))
                Debug.LogWarning($"AudioMixer 未暴露参数：{parameterName}。请在 Mixer 中暴露对应的 Volume 参数。", mixer);
        }
    }

    /// <summary>读取音量值；未保存时默认返回最大音量。</summary>
    public float GetVolume(AudioVolumeType type) => PlayerPrefs.GetFloat(Prefix + type, 1f);
    /// <summary>将所有已保存的音量值加载到混音器。</summary>
    public void LoadVolumes()
    {
        foreach (AudioVolumeType type in System.Enum.GetValues(typeof(AudioVolumeType)))
            SetVolume(type, GetVolume(type));
    }
}
}
