# 角色动画自动切换

组件：`GamePlay.Player.Anim.PlayerAnimatorController`，挂在与 Animator、Rigidbody2D、PlayerMovement 相同的玩家根节点。SamplePlayer 已配置，场景中的现有玩家也已接入。

复用已有 PlayerAnimController 的 Base Layer 状态，代码在状态改变时调用 Animator.Play，不需要额外配置 Transition、参数或 Exit Time，不会每帧重播当前动画。

| 判断 | 状态 |
|---|---|
| 脚下有向上的实体接触且没有向上飞离，横向输入为零 | Idle |
| 接地且有横向移动输入 | Run |
| 离地、竖直速度大于 Vertical Threshold | Frame（跳跃上升） |
| 离地且开始下落 | Fall |
| 离地且 WindBody2D 计算到有效风力，或显式开启飞行 | Fly |

接地使用 Rigidbody2D 的物理接触法线，Trigger 和侧面接触不作为地面。Run 使用主动移动输入，因此玩家静止站在移动平台上不会仅因平台搬运播放跑步。单向平台向上穿越期间没有支撑接触，会继续使用空中动画。

横向输入改变 SpriteRenderer.flipX，不翻转玩家 Collider。Art Faces Right 表示原素材是否朝右；素材朝向相反时取消勾选。死亡时暂停动画播放，重生后恢复判断；当前没有死亡或受伤 Clip，未新增相应状态。

Inspector 可以修改五个 State 字符串和竖直速度阈值。CurrentState 和 IsGrounded 可从代码读取。后续机关可调用 `SetFlying(true)` 强制使用空中飞行状态，结束时调用 `SetFlying(false)` 恢复自动判断。

Frame.anim 原先录入了根节点固定位置轨道；已移除该轨道，仅保留绘制好的 Sprite 动画，避免 Animator 覆盖刚体位置。Animator.ApplyRootMotion 关闭。

## 动画节奏与对齐

走路素材画布为 420×520，跳跃素材画布为 720×720，而且跳跃帧内已包含明显的角色上下位移。当前使用到的 29 张 Sprite 已设置自定义 Pivot：统一各素材组的横向原点，将每张图片的可见脚底对齐到玩家本地碰撞底部（约 y=-0.645）。原 PNG 和绘制内容未修改。

Frame、Fall 已关闭循环：起跳或下落动画播完保持最后一帧，不会在空中重复完整动作。每次成功跳跃（包括二段跳）重新播放上升动画，并按当前上升速度和重力估计到顶点时间，调整上升动画播放速度。顶点额外保持上升姿势约 0.06 秒，随后进入 Fall。地面接触丢失允许 0.06 秒短暂容错，主动起跳会立刻清除容错。

Run 的播放倍率根据相对平台的实际水平速度决定；移动输入存在但被墙挡住时使用 Idle。Run Reference Speed 默认 2.5，角色以默认速度 3 移动时播放倍率约 1.2。若之后调整移动速度或更换步幅素材，可通过此字段校准脚步节奏。降低该值会加快脚步，提高则减慢。

当前是 Sprite 逐帧切换，没有额外绘制转身或落地过渡帧。素材姿势本身的差异仍由原图决定，Pivot 对齐解决位置跳动，不会生成缺失动作。
