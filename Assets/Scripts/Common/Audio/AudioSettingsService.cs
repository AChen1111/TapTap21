using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsService : MonoBehaviour
{
    public static AudioSettingsService Instance { get; private set; }
    public AudioMixer mixer;
    const string Prefix = "Audio_";
    void Awake() { if (Instance != null && Instance != this) { Destroy(gameObject); return; } Instance = this; DontDestroyOnLoad(gameObject); }
    public void SetVolume(AudioVolumeType type, float value) { value = Mathf.Clamp01(value); PlayerPrefs.SetFloat(Prefix + type, value); if (mixer != null) mixer.SetFloat(type.ToString(), value <= .0001f ? -80f : Mathf.Log10(value) * 20f); }
    public float GetVolume(AudioVolumeType type) => PlayerPrefs.GetFloat(Prefix + type, 1f);
    public void LoadVolumes() { foreach (AudioVolumeType t in System.Enum.GetValues(typeof(AudioVolumeType))) SetVolume(t, GetVolume(t)); }
}
