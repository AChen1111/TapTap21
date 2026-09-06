# 音频系统调用说明

命名空间：

```csharp
using TapTap21.AudioSystem.dyh;
```

## 一、运行前配置

1. 创建 `AudioConfig`：

   `Create → Audio → Audio Config`

2. 为配置设置：

   - `clips`：一个或多个 `AudioClip`
   - `category`：`Music`、`Sfx`、`Voice`、`Ambience` 或 `UI`
   - `loop`：是否循环
   - `spatial3D`：是否启用 3D 空间音效
   - `volume`：基础音量，范围 0~1
   - `pitchMin`、`pitchMax`：随机音调范围
   - `priority`：优先级，数值越小优先级越高
   - `maxInstances`：同一 ID 的最大同时播放数量，0 表示不限制

3. 创建 `AudioDatabase`：

   `Create → Audio → Audio Database`

   编辑器会自动扫描项目中的 `AudioConfig`。修改配置后，确保资源已经保存。

4. 在场景中创建一个对象，添加：

   - `AudioManager`
   - `AudioSettingsService`

   将 `AudioDatabase` 赋值给 `AudioManager.database`，将 `Master.mixer` 赋值给 `AudioSettingsService.mixer`。

5. 新增或重命名音频配置后，执行：

   `Tools → Audio → 生成 AudioIds`

## 二、音频 ID

推荐使用自动生成的常量：

```csharp
AudioManager.Instance.PlaySfx(Audio.SFX);
AudioManager.Instance.PlayMusic(Audio.Music_1);
```

业务代码统一通过 `Audio.xxx` 常量访问音频，不要直接写字符串 ID。

ID 默认使用 `AudioConfig` 资源名；如果手动填写了 `id`，则使用填写的 ID。

## 三、播放普通音效

播放 2D 音效：

```csharp
AudioManager.Instance.PlaySfx(Audio.Button_Click);
```

播放指定位置的音效：

```csharp
AudioManager.Instance.PlaySfx(
    Audio.Explosion,
    explosionTransform.position);
```

返回值是实际使用的 `AudioSource`；如果 ID 不存在、没有音频片段或对象池无法提供音源，则返回 `null`。

## 四、播放 UI 音效

```csharp
public void OnButtonClick()
{
    AudioManager.Instance.PlayUI(Audio.Button_Click);
}
```

UI 音效会进入 `UI` Mixer 分组，通常用于按钮、菜单、提示和界面切换。

## 五、播放环境音

播放全局环境音：

```csharp
AudioManager.Instance.PlayAmbience(Audio.Forest_Ambience);
```

播放带空间位置的环境音：

```csharp
AudioManager.Instance.PlayAmbience(
    Audio.Waterfall,
    waterfallTransform.position);
```

环境音会进入 `Ambience` Mixer 分组。

## 六、播放背景音乐

```csharp
AudioManager.Instance.PlayMusic(Audio.Explore_Music);
```

指定淡入淡出时间：

```csharp
AudioManager.Instance.PlayMusic(
    Audio.Battle_Music,
    1.5f);
```

系统使用两个音乐音源进行交叉淡化，适合场景切换、进入战斗或 Boss 出场。

停止当前音乐并淡出：

```csharp
AudioManager.Instance.StopMusic(1f);
```

## 七、循环音效

为对象启动循环音效：

```csharp
private void StartEngine()
{
    AudioManager.Instance.PlayLoop(
        Audio.Vehicle_Engine,
        gameObject);
}
```

停止对象的循环音效：

```csharp
private void StopEngine()
{
    AudioManager.Instance.StopLoop(
        Audio.Vehicle_Engine,
        gameObject);
}
```

同一个 `id + owner` 重复调用不会重复创建音效。owner 可以是 `GameObject` 或 `Component`：

```csharp
AudioManager.Instance.PlayLoop(Audio.Generator_Loop, generatorGameObject);
AudioManager.Instance.PlayLoop(Audio.Fan_Loop, fanComponent);
```

循环音效会跟随 owner 的位置。owner 被销毁、音源被对象池抢占或音效自然停止时，系统会自动清理记录。

## 八、语音播放

立即播放语音：

```csharp
AudioManager.Instance.PlayVoice(Audio.Npc_Greeting);
```

调用 `PlayVoice` 时，如果已有语音正在播放，当前语音会被立即打断。

将多句语音加入队列：

```csharp
AudioManager.Instance.EnqueueVoice(Audio.Npc_Line_01);
AudioManager.Instance.EnqueueVoice(Audio.Npc_Line_02);
AudioManager.Instance.EnqueueVoice(Audio.Npc_Line_03);
```

当前语音播放结束后，系统会按入队顺序自动播放下一句。

## 九、暂停和恢复

暂停所有由音频系统管理的音源：

```csharp
public void OpenPauseMenu()
{
    AudioManager.Instance.PauseAll();
}
```

恢复播放：

```csharp
public void ClosePauseMenu()
{
    AudioManager.Instance.ResumeAll();
}
```

暂停期间不会租借新的对象池音源，也不会消费语音队列；音乐淡入淡出计时也会暂停。

## 十、音量设置

设置音量，范围为 0~1：

```csharp
AudioSettingsService.Instance.SetVolume(
    AudioVolumeType.Master,
    0.8f);

AudioSettingsService.Instance.SetVolume(
    AudioVolumeType.Music,
    0.5f);

AudioSettingsService.Instance.SetVolume(
    AudioVolumeType.Sfx,
    1f);
```

读取当前音量：

```csharp
float musicVolume = AudioSettingsService.Instance.GetVolume(
    AudioVolumeType.Music);
```

音量会保存到 `PlayerPrefs`，键名格式为 `Audio_<Type>`。启动时 `AudioManager` 会自动调用 `LoadVolumes()`。

## 十一、绑定音量滑块

在 Unity UI `Slider` 对象上添加 `VolumeSlider`，然后在 Inspector 中选择 `Volume Type`。

也可以通过代码设置：

```csharp
using UnityEngine.UI;
using TapTap21.AudioSystem.dyh;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;

    private void Awake()
    {
        musicSlider.value = AudioSettingsService.Instance.GetVolume(
            AudioVolumeType.Music);

        musicSlider.onValueChanged.AddListener(value =>
        {
            AudioSettingsService.Instance.SetVolume(
                AudioVolumeType.Music,
                value);
        });
    }
}
```

## 十二、完整玩法示例

```csharp
using UnityEngine;
using TapTap21.AudioSystem.dyh;

public class BattleAudioExample : MonoBehaviour
{
    [SerializeField] private Transform explosionPoint;
    [SerializeField] private GameObject vehicle;

    public void EnterStage()
    {
        AudioManager.Instance.PlayMusic(Audio.Explore_Music, 1f);
        AudioManager.Instance.PlayAmbience(Audio.Forest_Ambience);
    }

    public void StartVehicle()
    {
        AudioManager.Instance.PlayLoop(Audio.Vehicle_Engine, vehicle);
    }

    public void Fire()
    {
        AudioManager.Instance.PlaySfx(Audio.Weapon_Fire);
    }

    public void Explode()
    {
        AudioManager.Instance.PlaySfx(
            Audio.Explosion,
            explosionPoint.position);
    }

    public void EnterBattle()
    {
        AudioManager.Instance.PlayMusic(Audio.Battle_Music, 1.5f);
        AudioManager.Instance.EnqueueVoice(Audio.Boss_Intro_01);
        AudioManager.Instance.EnqueueVoice(Audio.Boss_Intro_02);
    }

    public void OpenPause()
    {
        AudioManager.Instance.PauseAll();
    }

    public void ClosePause()
    {
        AudioManager.Instance.ResumeAll();
    }

    public void ExitStage()
    {
        AudioManager.Instance.StopLoop(Audio.Vehicle_Engine, vehicle);
        AudioManager.Instance.StopMusic(1f);
    }
}
```

## 十三、对象池参数建议

`AudioSourcePool` 默认初始创建 12 个音源。

- 普通项目：`initialSize = 12`，`maxSize = 0`
- 大量战斗音效：`initialSize = 24`，`maxSize = 64`
- 移动端或音效较少的项目：`initialSize = 8`，`maxSize = 32`

当音源全部占用时，系统会优先复用空闲音源；必要时抢占优先级较低的音源；达到 `maxSize` 后，如果仍无法满足请求，则本次播放失败并返回 `null`。
