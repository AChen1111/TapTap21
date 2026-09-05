using UnityEngine;
using UnityEngine.UI;

namespace dyh
{

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    /// <summary>当前滑动条控制的音量通道。</summary>
    public AudioVolumeType volumeType;
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = AudioSettingsService.Instance != null ? AudioSettingsService.Instance.GetVolume(volumeType) : 1f;
        slider.onValueChanged.AddListener(OnChanged);
    }
    /// <summary>将用户输入转发给音量设置服务。</summary>
    private void OnChanged(float value)
    {
        if (AudioSettingsService.Instance != null)
            AudioSettingsService.Instance.SetVolume(volumeType, value);
    }

    private void OnDestroy()
    {
        if (slider != null)
            slider.onValueChanged.RemoveListener(OnChanged);
    }
}
}
