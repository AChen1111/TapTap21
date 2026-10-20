# 2D Effects

namespace: `Effects`

溶解、闪白、扭曲用的是同一份 `SpriteEffectProfile`。Sprite、UI、TextMesh都调用它。每个新外观都需要一份新配置。

## 精灵效果

用在某一个 `SpriteRenderer` 上，例如溶解、闪白、轻微扭曲。

1. 把材质 `Materials/SpriteEffects.mat` 赋给该 Sprite。
2. 在同一个物体上添加 `SpriteEffectPlayer`。
3. 调用播放：

```csharp
public SpriteEffectProfile fireDissolve;
public SpriteEffectProfile hitFlash;

void Hit()
{
    GetComponent<SpriteEffectPlayer>().Play(hitFlash);
}

void Die()
{
    GetComponent<SpriteEffectPlayer>().Play(fireDissolve);
}
```

`Stop()` 停掉这个物体上的全部精灵效果。`Stop(某个配置)` 只停那一个。

现成配置：

| 资产 | 作用 |
| --- | --- |
| `Profiles/FireDissolve.asset` | 约 1.25 秒烧完，带橙色边缘和一点扭曲 |
| `Profiles/HitFlash.asset` | 约 0.18 秒闪白 |

自己做一份：菜单 **Effects / Sprite Effect**。曲线横轴是 0 到 1，对应该配置的时长。

- `dissolve`：0 是原图，1 是完全溶解
- `flash`：0 是原色，1 是变成 `flashColor`
- `distort`：UV 偏移幅度，0.02 到 0.05 通常就够
- `noise` 留空时用程序噪声

时长小于等于 0 表示一直播，直到 `Stop`。勾选循环后，到时从头再播。

播完可以带一个回调。只有时长走完才会调用；循环、一直播放，以及手动 `Stop`，都不会调用。

```csharp
player.Play(fireDissolve, () => Destroy(gameObject));
```

同一个 Sprite 上后播放的配置会盖住先播放的同名属性。想让溶解和闪白同时出现，把两条曲线做在同一份配置里。

这个 Shader 是 Unlit 类，不受光照影响，如需应另做 Lit shader。

## UI 图片

用在 `Image` 或 `RawImage` 上。文字不要挂这个组件。

1. 在物体上添加 `UIEffectPlayer`。它会自己换成 `Effects/UIEffects`，不用手动赋材质。
2. 用和精灵相同的配置播放：

```csharp
public SpriteEffectProfile hitFlash;

void Awake()
{
    GetComponent<UIEffectPlayer>().Play(hitFlash);
}
```

`Stop()` 和 `Stop(某个配置)` 的用法与精灵相同。关掉组件后，会换回原来的 UI 材质。

Mask、RectMask2D 仍然可用。曲线含义和 `Profiles/FireDissolve.asset`、`Profiles/HitFlash.asset` 一样。

## TextMesh Pro

用在 `TextMeshPro - Text (UI)` 或 3D 的 `TextMeshPro` 上。

1. 先指定好字体材质。
2. 添加 `TMPEffectPlayer`。它会复制这份字体材质，再换成 `Effects/TMPEffects`。字色、描边、Underlay 投影会留下。倒角和发光不会留下。
3. 播放同一份配置：

```csharp
public SpriteEffectProfile fireDissolve;

void Die()
{
    GetComponent<TMPEffectPlayer>().Play(fireDissolve);
}
```

溶解按文字的本地坐标铺开，不是按字体图集。扭曲幅度比精灵小，避免采样到旁边的字形。关掉组件后，会换回原来的字体材质。

## 屏幕效果

用在某一台摄像机上，例如全屏闪白、暗角、某一层角色的外描边。效果只影响挂了播放器的那台摄像机。

1. 确认 2D Renderer 上有 `ScreenEffectFeature`（当前项目已经加上）。
2. 在摄像机上添加 `ScreenEffectPlayer`。
3. 调用播放：

```csharp
public ScreenEffectProfile screenHitFlash;
public ScreenEffectProfile vignette;
public ScreenEffectProfile enemyOutline;

ScreenEffectPlayer screen;

void Awake()
{
    screen = Camera.main.GetComponent<ScreenEffectPlayer>();
}

void OnPlayerHurt()
{
    screen.Play(screenHitFlash); // 到时自己停
}

void ShowDanger()
{
    screen.Play(vignette);       // 一直留到 Stop
}
```

现成配置：

| 资产 | 类型 | 作用 |
| --- | --- | --- |
| `Profiles/ScreenHitFlash.asset` | Blit | 约 0.15 秒全屏闪白 |
| `Profiles/Vignette.asset` | Blit | 暗角，一直播放 |

新效果后续逐步添加。

自制：菜单 **Effects / Screen Effect**。

- **Blit**：指定一张全屏材质。播放器把 `intensity` 曲线写进材质的 `_Intensity`。闪白用 `Materials/ScreenFlash.mat`，暗角用 `Materials/ScreenVignette.mat`。
