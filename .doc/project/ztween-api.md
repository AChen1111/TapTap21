# ZTween API

命名空间：`ZZ.ZTween`  
用法：扩展方法，业务代码不要直接调 DOTween。

```csharp
using ZZ.ZTween;

panel.ZPopup();
player.ZHit(Vector3.right);
```

所有动画方法返回 `ZTween`。最后一个参数一般都是 `Action onCompleteCallBack=null`。

`loops = -1` 表示无限循环，用 `ZTweenUtility.Kill(...)` 或空格切演示时的 `KillAll()` 停掉。

---

## 核心类型

### ZTween

| 成员 | 说明 |
|---|---|
| `tween` | 内部 DOTween `Tween` |
| `active` | 对应内部 tween 是否还活着 |

### 枚举

```csharp
enum ZDirection   { Left, Right, Up, Down }
enum ZShowType    { Fade, Popup, Slide }
enum ZButtonState { Normal, Hover, Pressed, Selected, Disabled }
```

---

## UI

作用对象主要是 `RectTransform` / `CanvasGroup` / `Image` / `Text` / `Button`。

### 出现 / 消失

| 方法 | 对象 | 默认参数 | 效果 |
|---|---|---|---|
| `ZFadeIn(time, ease)` | CanvasGroup / Image / Text | `ease=Linear` | 透明度 0 → 1 |
| `ZFadeOut(time, ease)` | CanvasGroup / Image / Text | `ease=Linear` | 透明度 → 0 |
| `ZFade(endValue, time, ease)` | CanvasGroup / Image / Text | `ease=Linear` | 淡到指定透明度 |
| `ZScale(endValue, time, ease)` | RectTransform | `ease=OutCubic` | 缩放到 `endValue` |
| `ZMove(endValue, time, ease)` | RectTransform | `ease=OutCubic` | 锚点位移 |
| `ZRotate(z, time, ease)` | RectTransform | `ease=OutCubic` | 旋转 Z |
| `ZPopup(time, maxScale)` | RectTransform | `0.5, 1.2` | 0 → 1.2 → 1 |
| `ZPopupFade(time, maxScale)` | RectTransform | `0.5, 1.2` | 弹出 + 淡入 |
| `ZClose(time, minScale)` | RectTransform | `0.4, 0.9` | 1 → 0.9 → 0，有 CanvasGroup/Graphic 时一起淡出 |
| `ZSlideIn(direction, time, ease)` | RectTransform | `Left, 0.35, OutCubic` | 从屏外滑入 |
| `ZSlideOut(direction, time, ease)` | RectTransform | `Right, 0.35, InCubic` | 滑出 |
| `ZShow(time, showType, direction)` | CanvasGroup | `0.3, Fade, Up` | 显示并打开交互 |
| `ZHide(time, showType, direction)` | CanvasGroup | `0.3, Fade, Down` | 关掉交互，播完 `SetActive(false)` |

`ZShow` 开始时会 `SetActive(true)`，动画期间 `interactable/blocksRaycasts = false`，结束后打开。  
`ZHide` 一开始就关掉点击，结束后隐藏物体。

```csharp
panelGroup.ZShow(0.35f, ZShowType.Popup);
panelGroup.ZHide(0.3f, ZShowType.Slide, ZDirection.Down);
```

### 按钮 / 状态

| 方法 | 对象 | 默认 | 效果 |
|---|---|---|---|
| `ZButtonPress(time)` | RectTransform / Button | `0.2` | 1 → 0.9 → 1.05 → 1 |
| `ZHoverEnter(time, maxScale)` | RectTransform / Button | `0.15, 1.05` | 悬停放大 |
| `ZHoverExit(time)` | RectTransform / Button | `0.15` | 回到 1 |
| `ZSelect(time, maxScale)` | RectTransform | `0.15, 1.08` | 选中放大 |
| `ZDeselect(time)` | RectTransform | `0.15` | 取消选中 |
| `ZDisable(time)` | CanvasGroup / Graphic | `0.2` | 变淡；CanvasGroup 会关掉点击 |
| `ZEnable(time)` | CanvasGroup / Graphic | `0.2` | 恢复；CanvasGroup 会打开点击 |
| `ZSetState(state, time)` | RectTransform | `0.15` | 按 `ZButtonState` 切到对应动画 |

### 颜色 / 反馈

| 方法 | 对象 | 默认 | 效果 |
|---|---|---|---|
| `ZColor(endValue, time, ease)` | Graphic | `0.2, Linear` | 变色 |
| `ZTextColor(endValue, time, ease)` | Text | `0.2, Linear` | 文字变色 |
| `ZFlash(flashColor, time)` | Graphic | `0.1` | 闪一下再回到原色 |
| `ZShake(time, strength)` | RectTransform | `0.35, 24` | 左右抖 |
| `ZError(time)` | RectTransform | `0.4` | 抖动 + 闪红 |
| `ZWarning(time)` | RectTransform | `0.4` | 抖动 + 闪黄 |
| `ZSuccess(time)` | RectTransform | `0.35` | Punch + 闪绿 |
| `ZPunchScale(time, punch)` | RectTransform | `0.3, 0.2` | 缩放冲击 |
| `ZPunchPosition(time, punch)` | RectTransform | `0.3, (0,20)` | 位移冲击 |
| `ZPunchRotation(time, punch)` | RectTransform | `0.3, 15` | 旋转冲击 |
| `ZBounce(time)` | RectTransform | `0.45` | 1 → 1.2 → 0.95 → 1.05 → 1 |
| `ZAttention(time)` | RectTransform | `0.4` | 引起注意的 Punch |

### 文本

| 方法 | 对象 | 说明 |
|---|---|---|
| `ZNumber(from, to, time)` | Text | 整数滚动 |
| `ZNumber(from, to, time, format)` | Text | 浮点滚动，默认 `0.##` |
| `ZCurrency(from, to, time, prefix)` | Text | 货币，默认前缀 `$` |
| `ZTypewriter(content, time)` | Text | 打字机 |
| `ZReveal(time)` | Text | 文字淡入，默认 `0.25` |
| `ZShakeText(time)` | Text | 文字抖动，默认 `0.3` |
| `ZPunchText(time)` | Text | 文字 Punch，默认 `0.3` |

```csharp
scoreText.ZNumber(0, 500, 0.8f);
goldText.ZCurrency(100, 250, 0.5f);
title.ZTypewriter("GAME OVER", 0.5f);
```

### 图标 / Toast / 提示

| 方法 | 对象 | 默认 | 效果 |
|---|---|---|---|
| `ZIconPopup(time)` | RectTransform | `0.35` | 图标弹出 |
| `ZIconRotate(time)` | RectTransform | `0.4` | 转一圈 |
| `ZIconBounce(time)` | RectTransform | `0.4` | 弹跳 |
| `ZIconFloat(time, offset, loops)` | RectTransform | `0.8, 12, -1` | 上下漂 |
| `ZToast(stay, direction, time)` | RectTransform | `2, Up, 0.3` | 进入 → 停留 → 退出 |
| `ZNotification(stay, direction, time)` | RectTransform | `2, Right, 0.3` | 滑入，停留后滑出淡出 |
| `ZTooltipShow(time)` | RectTransform | `0.2` | 0.95→1 + 淡入 |
| `ZTooltipHide(time)` | RectTransform | `0.15` | 收起后隐藏 |
| `ZBadgeShow(time)` | RectTransform | `0.3` | 徽章弹出 + Punch |
| `ZBadgePunch(time)` | RectTransform | `0.25` | 徽章 Punch |

这些都是对**已有 UI 物体**做动画，不会凭空创建 Toast。

### 页 / 列表 / 滚动

| 方法 | 对象 | 说明 |
|---|---|---|
| `ZTabSelect(time)` | RectTransform | 选中放大，透明度到 1 |
| `ZTabDeselect(time)` | RectTransform | 回到 1，透明度到 0.6 |
| `ZPageSwitch(newPage, direction, time)` | RectTransform | 当前页沿 `direction` 出，新页从反方向进 |
| `ZStagger(anim, interval)` | `IList<RectTransform>` | 交错播放，默认间隔 `0.05` |
| `ZListShow(time, interval)` | `IList<RectTransform>` | 依次 Popup |
| `ZListHide(time, interval)` | `IList<RectTransform>` | 反向 Close |
| `ZScrollTo(target, time, ease)` | ScrollRect | 滚到子节点 |
| `ZScrollToIndex(index, time, ease)` | ScrollRect | 滚到第 n 个 |
| `ZScrollSnap(index, time)` | ScrollRect | 吸附，`OutBack` |

```csharp
items.ZStagger(item => item.ZPopup(0.3f), 0.05f);
oldPage.ZPageSwitch(newPage, ZDirection.Left);
```

### 弹窗 / 进度 / 加载

| 方法 | 对象 | 说明 |
|---|---|---|
| `ZModalShow(panel, time, bgAlpha)` | CanvasGroup 背景 | 遮罩淡入 + 面板弹出 |
| `ZModalHide(panel, time)` | CanvasGroup 背景 | 面板关闭 + 遮罩淡出，结束后都隐藏 |
| `ZDialogShow(time)` | RectTransform | `ZPopupFade` |
| `ZDialogHide(time)` | RectTransform | `ZClose`，结束后隐藏 |
| `ZProgress(from, to, time)` | Image | `fillAmount` 平滑变化 |
| `ZHealthBar(from, to, time, delayedBar)` | Image | 当前条先掉，延迟条后掉 |
| `ZSlider(endValue, time)` | Slider | 滑条值 |
| `ZSpinner(time, loops)` | RectTransform | 转圈，默认无限 |
| `ZLoadingDots(baseText, time, loops)` | Text | `Loading` → `...` |
| `ZLoadingProgress(progress, time)` | Image | 从当前 fill 到目标 |
| `ZWipe(direction, time)` | Image | 按方向 fill 铺满 |
| `ZTransitionFadeOut(time)` | CanvasGroup | 转场压黑（遮罩淡入） |
| `ZTransitionFadeIn(time)` | CanvasGroup | 转场揭开（遮罩淡出后隐藏） |
| `ZCountdown(from, stepTime, goText)` | Text | `3 2 1 GO!` |
| `ZTimerBar(duration)` | Image | 从满到空 |
| `ZCooldown(duration, numberText, punchTarget)` | Image 遮罩 | 径向/填充冷却，可带数字和完成 Punch |

### 循环 / 引导

| 方法 | 对象 | 默认 | 效果 |
|---|---|---|---|
| `ZFloat(time, offset, loops)` | RectTransform | `0.8, 12, -1` | 上下漂 |
| `ZPulse(time, maxScale, loops)` | RectTransform | `0.4, 1.05, -1` | 呼吸缩放 |
| `ZBlink(time, loops)` | CanvasGroup / Graphic | `0.35, -1` | 闪烁 |
| `ZRotateLoop(time, loops)` | RectTransform | `1, -1` | 持续转 |
| `ZHighlight(others, dim, time)` | CanvasGroup | `0.3, 0.25` | 自己亮，别人压暗 |
| `ZSpotlight(others, dim, time)` | CanvasGroup | `0.2, 0.25` | 更强压暗 |
| `ZTutorialShow(background, time, bgAlpha)` | CanvasGroup | `0.3, 0.55` | 步骤弹出 + 背景压暗 |
| `ZTutorialHide(background, time)` | CanvasGroup | `0.25` | 收起并隐藏 |

### 卡牌 / 物品 / 反馈

| 方法 | 对象 | 效果 |
|---|---|---|
| `ZPointerEnter/Exit/Click` | RectTransform | 指针进入/离开/点击 |
| `ZCardHover(time, y)` | RectTransform | 放大 + 上移 |
| `ZCardSelect(time, y, z)` | RectTransform | 放大 + 上移 + 旋转 |
| `ZCardPlay(endValue, time)` | RectTransform | 飞出 + 旋转缩小淡出 |
| `ZCardDiscard(time)` | RectTransform | 旋转缩小淡出 |
| `ZItemAdd/Remove/Select/Hover` | RectTransform | 获得 / 移除 / 选中 / 悬停 |
| `ZDragBegin/End/Invalid` | RectTransform | 拖起放大 / 放下 / 非法抖动 |
| `ZFocus/Unfocus` | RectTransform | 手柄焦点 |
| `ZRewardShow(time)` | RectTransform | 放大旋转 Punch |
| `ZLevelUp(time)` | Text | 升级字：放大 + Punch + 闪黄 |
| `ZAchievementShow(stay, direction, time)` | RectTransform | 成就条滑入滑出 |

---

## Actor（Transform / Sprite）

世界空间角色用这一套。缩放类会保留 `scale.x` 正负，左右翻转的角色不会被弹回正面。  
这些方法带 `SetLink`，物体销毁时 tween 一起停。

有 `SpriteRenderer`（自己或子节点）时，`ZHit` / `ZSpawn` / `ZDespawn` / `ZCollect` 会自动带上闪白或淡入淡出。

有 `Rigidbody2D` 时，位移不要和物理抢同一个 Transform；挤压 / Punch 打在视觉子节点上。

### Transform

| 方法 | 默认 | 效果 |
|---|---|---|
| `ZMoveTo(endValue, time, ease)` | `ease=OutCubic` | 世界坐标移动 |
| `ZLocalMove(endValue, time, ease)` | `ease=OutCubic` | 本地坐标移动 |
| `ZJump(height, time)` | `1.5, 0.4` | 原地跳 |
| `ZJumpTo(endValue, height, time)` | `1.5, 0.45` | 跳到目标点 |
| `ZPunchScale(time, punch)` | `0.2, 0.2` | 缩放冲击 |
| `ZPunchPosition(time, punch)` | `0.2, (0.2,0,0)` | 位移冲击 |
| `ZPunchRotation(time, punch)` | `0.2, 18` | 旋转冲击 |
| `ZShake(time, strength)` | `0.18, 0.18` | 抖动 |
| `ZPop(time)` | `0.25` | 0 → 1.15 → 原缩放 |
| `ZSquash(time)` | `0.22` | 落地压扁再弹回 |

`RectTransform` 上已有同名 `ZPunchScale` / `ZShake` 等时，会走 UI 版本（锚点抖动），不要拿 UI 去调世界位移。

### SpriteRenderer

| 方法 | 默认 | 效果 |
|---|---|---|
| `ZFadeIn(time, ease)` | `0.2, Linear` | 0 → 1 |
| `ZFadeOut(time, ease)` | `0.2, Linear` | → 0 |
| `ZFade(endValue, time, ease)` | `0.2, Linear` | 淡到指定透明度 |
| `ZColor(endValue, time, ease)` | `0.15, Linear` | 变色 |
| `ZTint(endValue, time)` | `0.15` | 染色，同 `ZColor` |
| `ZFlash(flashColor, time)` | `Color, 0.12` | 闪一下回原色 |
| `ZBlink(time, loops)` | `0.12, -1` | 无敌闪烁 |
| `ZFlipX(time)` | Transform, `0.12` | `scale.x` 过渡转身 |

### 语义动作

`Transform` 和 `GameObject` 都能调。

| 方法 | 默认 | 效果 |
|---|---|---|
| `ZSpawn(time)` | `0.25` | 激活 + 弹出 + 淡入 |
| `ZDespawn(time)` | `0.2` | 缩小淡出，结束后隐藏，缩放还原 |
| `ZLand(time)` | `0.22` | 落地挤压，等同 `ZSquash` |
| `ZHit(hitDir, time)` | `Vector3.left, 0.16` | 闪白 + 后坐 + 短抖 |
| `ZDash(endValue, time)` | `0.18` | 快移 + 拉伸再收回 |
| `ZKnockback(hitDir, distance, time)` | `left, 0.7, 0.22` | 沿方向弹开，再落回一点 |
| `ZCollect(target, time)` | `0.35` | 飞向目标 + 缩小淡出 |
| `ZIdleBob(time, offset, loops)` | `0.7, 0.12, -1` | 待机上下漂 |
| `ZAttackSwing(time, angle)` | `0.16, 22` | 近战挥击 |

```csharp
player.ZHit(Vector3.right);
player.ZJump();
player.ZLand();
player.transform.ZDash(player.position + Vector3.right * 2f);
coin.ZCollect(player.position);
enemy.ZDespawn();
sprite.ZFlash(Color.white);
```

`hitDir` 只看 XY，`default` 时按左。

---

## 序列

```csharp
ZTweenUtility.ZSequence()
    .ZAppend(background.ZFadeIn(0.25f))
    .ZAppend(panel.ZPopup())
    .ZAppendInterval(0.2f)
    .ZAppend(title.ZTypewriter("GAME OVER", 0.5f))
    .ZJoin(scoreText.ZPunchText())
    .ZAppendCallback(() => Debug.Log("完成"));
```

| 方法 | 说明 |
|---|---|
| `ZSequence()` | 空序列 |
| `ZAppend(other)` | 接在后面 |
| `ZJoin(other)` | 和当前段同时播 |
| `ZAppendInterval(time)` | 停顿 |
| `ZAppendCallback(callback)` | 回调 |
| `Reverse(zTween)` | 倒放 |

---

## 生命周期

```csharp
ZTweenUtility.Kill(tween);
ZTweenUtility.Kill(transform);      // 杀掉这个物体上的 tween
ZTweenUtility.KillAll();
ZTweenUtility.Pause(tween);
ZTweenUtility.Resume(tween);
ZTweenUtility.Play(tween);
ZTweenUtility.ReStart(tween);
ZTweenUtility.Rewind(tween);
ZTweenUtility.IsPlaying(tween);
ZTweenUtility.IsPlaying(transform);
```

`Pause/Play/Resume/Kill` 也有 `Component` 重载。

进战斗、切场景、物体回收时记得 `Kill`，尤其是 `ZIdleBob` / `ZBlink` / `ZPulse` 这种循环。

---

## 演示

场景 `Assets/Scenes/ZzTestScene.unity`：

| 脚本 | 用途 | 怎么开 |
|---|---|---|
| `UITest` | UI 动效，空格下一个 | 打开 `Canvas` + `ZTweenDemo`，关掉 `ActorDemoHost` |
| `ActorTest` | 角色动效，空格下一个 | 打开 `ActorDemoHost`，关掉 `Canvas` + `ZTweenDemo` |

不要两套同时开，都会听空格。
