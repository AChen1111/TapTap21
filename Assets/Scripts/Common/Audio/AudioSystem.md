# Unity 音频系统使用说明

## 系统组成

音频系统位于 `Assets/Scripts/Common/Audio/`。

所有音频类型都位于 `dyh` 命名空间中。业务脚本调用前添加：

```csharp
using dyh;
```

| 文件 | 作用 |
| --- | --- |
| `AudioManager.cs` | 统一播放音乐、音效、语音、环境音和循环音效 |
| `AudioTypes.cs` | 定义音频分类、音量类型、`AudioConfig` 和 `AudioDatabase` |
| `AudioSourcePool.cs` | 复用和扩容 `AudioSource` |
| `AudioSettingsService.cs` | 管理、保存和加载音量 |
| `VolumeSlider.cs` | 将 UI Slider 绑定到音量设置 |
| `Editor/AudioIdsGenerator.cs` | 自动生成音频 ID 常量 |

基本流程：

```text
业务代码 → AudioManager → AudioDatabase → AudioSourcePool → AudioSource
```

## 创建 AudioConfig

在 Project 窗口中右键：

```text
Create → Audio → Audio Config
```

建议按用途命名资源：

```text
player_attack.asset
explosion.asset
button_click.asset
battle_theme.asset
engine_loop.asset
```

当 `id` 为空时，系统自动使用资源文件名作为 ID，因此通常不需要重复填写 ID。

主要字段：

- `Clips`：音频片段列表，播放时随机选择一个变体。
- `Category`：`Music`、`Sfx`、`Voice`、`Ambience` 或 `UI`。
- `Loop`：是否循环播放。
- `Spatial 3D`：是否启用 3D 空间效果。
- `Volume`：音频基础音量，范围为 0 到 1。
- `Pitch Min/Max`：音高随机范围。
- `Priority`：数值越小，优先级越高。
- `Max Instances`：同一 ID 的最大同时播放数量，0 表示不限制。

`MixerGroup` 不需要在 AudioConfig 中重复选择，系统会按照 `Category` 自动路由。

## AudioDatabase

创建方式：

```text
Create → Audio → Audio Database
```

将 AudioDatabase 引用到场景中的 `AudioManager.Database`。

数据库在 Unity 编辑器中会自动扫描项目内的 AudioConfig 并更新 Entries。运行时使用字典进行 ID 查询。打包后不会执行编辑器扫描，因此正式运行必须保留一个已经保存好的 AudioDatabase 资源。

## AudioIds

新增或重命名 AudioConfig 后执行：

```text
Tools → Audio → 生成 AudioIds
```

系统会生成 `Assets/Scripts/Common/Audio/AudioIds.cs`。

调用示例：

```csharp
AudioManager.Instance.PlaySfx(AudioIds.player_attack);
```

`AudioIds.cs` 为自动生成文件，不建议手动修改。

## 场景配置

创建一个场景对象并挂载：

```text
AudioManager
AudioSettingsService
```

配置：

- `AudioManager.Database`：AudioDatabase 资源。
- `AudioManager.Pool`：AudioSourcePool，可为空，系统会自动创建。
- `AudioManager.Settings`：AudioSettingsService。
- `AudioSettingsService.Mixer`：主 AudioMixer。

AudioManager 会跨场景保留，项目中只应保留一个实例。

## AudioMixer 设置

建议创建一个主 Mixer：

```text
MainAudioMixer
└── Master
    ├── Music
    ├── Sfx
    ├── Voice
    ├── Ambience
    └── UI
```

Group 名称建议与 AudioCategory 完全一致。还需要将各 Group 的 Volume 暴露给脚本，参数名称必须为：

```text
Master
Music
Sfx
Voice
Ambience
UI
```

分类路由关系：

```text
Music    → Music
Sfx      → Sfx
Voice    → Voice
Ambience → Ambience
UI       → UI
```

## 播放接口

```csharp
AudioManager.Instance.PlaySfx(AudioIds.player_attack);

AudioManager.Instance.PlaySfx(
    AudioIds.explosion,
    explosionPosition);

AudioManager.Instance.PlayUI(AudioIds.button_click);

AudioManager.Instance.PlayAmbience(AudioIds.wind_loop);

AudioManager.Instance.PlayMusic(
    AudioIds.battle_theme,
    1.5f);

AudioManager.Instance.StopMusic(1f);
```

## 循环音效

```csharp
AudioManager.Instance.PlayLoop(
    AudioIds.engine_loop,
    vehicle);

AudioManager.Instance.StopLoop(
    AudioIds.engine_loop,
    vehicle);
```

循环音效会记录音频 ID 和 owner，并支持：

- 跟随 owner 的位置更新 3D 音效。
- owner 被销毁时自动清理。
- 被对象池抢占时同步清理。
- 被外部停止后再次启动。

## 语音

立即播放语音，当前语音会被打断：

```csharp
AudioManager.Instance.PlayVoice(AudioIds.npc_greeting);
```

加入语音队列：

```csharp
AudioManager.Instance.EnqueueVoice(AudioIds.npc_next_line);
```

## 对象池

对象池在初始化时创建 `initialSize` 个播放器。全部占用时，会优先抢占低优先级音效；无法抢占时自动扩容。

```text
initialSize：初始播放器数量
maxSize：最大播放器数量，0 表示不限制
```

通用系统默认 `maxSize=0`。具体项目可以设置为 32、64 或其他合适的上限。

## 音量设置

支持的音量通道：

```text
Master、Music、Sfx、Voice、Ambience、UI
```

代码设置音量：

```csharp
AudioSettingsService.Instance.SetVolume(
    AudioVolumeType.Music,
    0.5f);
```

音量值范围为 0 到 1，系统会转换为分贝并应用到 AudioMixer，同时保存到 PlayerPrefs。

## VolumeSlider

在 UI Slider 对象上添加 `VolumeSlider`，然后在 Inspector 中选择 `Volume Type`，例如 `Music`。组件会自动读取当前音量、监听滑动变化并调用 `AudioSettingsService`。

## 暂停和恢复

```csharp
AudioManager.Instance.PauseAll();
AudioManager.Instance.ResumeAll();
```

暂停期间不会租借新的播放器、消费语音队列，也不会推进音乐淡入淡出计时。

## 新增音频流程

```text
1. 创建 AudioConfig
2. 使用音频用途命名资源
3. 设置 Clips 和 Category
4. 确认 AudioDatabase 已引用该配置
5. 执行“生成 AudioIds”
6. 在代码中调用 AudioManager
```

## 导出 Unity Package

通用音频系统选择：

```text
Assets/Scripts/Common/Audio/
Assets/Master.mixer
```

并勾选 `Include dependencies`。

如果需要导出当前项目的实际音频，还需要选择 AudioDatabase、AudioConfig 和音频文件。测试场景和测试脚本通常不属于正式 Package。
