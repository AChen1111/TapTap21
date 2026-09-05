using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    public AudioVolumeType volumeType;
    Slider slider;
    void Awake() { slider = GetComponent<Slider>(); slider.value = AudioSettingsService.Instance != null ? AudioSettingsService.Instance.GetVolume(volumeType) : 1f; slider.onValueChanged.AddListener(OnChanged); }
    void OnChanged(float value) { if (AudioSettingsService.Instance != null) AudioSettingsService.Instance.SetVolume(volumeType, value); }
    void OnDestroy() { if (slider != null) slider.onValueChanged.RemoveListener(OnChanged); }
}
