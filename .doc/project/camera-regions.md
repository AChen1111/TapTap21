# 相机跟随与区块范围

相机复用 Cinemachine 3：`CameraController` 管理玩家目标和区块选择，`CinemachinePositionComposer` 提供死区与平滑跟随，`CinemachineConfiner2D` 将整个画面限制在 Polygon 内。

## 当前场景

`Assets/Scenes/GameScene.unity` 已绑定 SamplePlayer，新增 `CameraRegion_Main`。默认区块按森林 Scene 背景的世界范围建立，约 56.86×21.6。Position Composer 的 Target Offset 为 (0,1.2,0)，Camera Distance=10，位置缓动 X=0.35、Y=0.55；原有镜头大小保留。边界 Damping=0，防止画面因边界缓动越出范围。

## 角色走出死区后跟随

选中 CinemachineCamera，在 Cinemachine Position Composer > Composition > Dead Zone 中调整 Size。当前 Enabled 开启，Size=(0.3,0.3)，表示屏幕中央宽 30%、高 30% 的区域内，相机不因角色移动而跟随；超出后平滑跟上。Size.X 控制横向自由活动宽度，Size.Y 控制跳跃时的纵向自由范围。数值越大，相机越晚跟随；设为 0 则该方向立即跟随。Screen Position 调整死区在画面中的中心位置。

死区随当前相机画面移动；CameraRegion 则是世界中的最大显示范围。相机达到区块边界时，边界限制优先，角色可能继续走向画面边缘。激活相机时 Center On Activate 先进行一次初始构图。

## 手动调整

选中 `CameraRegion_Main`，在 PolygonCollider2D 中点击 Edit Collider，拖动顶点定义相机范围。保持 Is Trigger 开启、不设置 Ground Tag、不增加 Rigidbody2D，避免成为实体障碍。修改 Collider 点后重新进入游戏，使用新的边界。

区块表示整个画面允许显示的区域，不是相机中心点的活动范围。区块需要至少容纳一个相机画面；正交相机的画面高为 Orthographic Size×2，宽为高×宽高比。区块小于画面时无法保证四边都不越界，应扩大区块或减小镜头 Orthographic Size。

## 新增房间区块

拖入 `Assets/Prefabs/GamePlay/Camera/CameraRegion2D.prefab`（初始 24×14），调整位置并通过 Edit Collider 绘制房间外边界。也可以新建空对象添加 `GamePlay.CameraSystem.CameraRegion2D`，自动附带 PolygonCollider2D。

CameraController 每帧根据跟随目标所在位置选区块，无需依赖玩家 Tag 或触发事件，因此传送和重生也会重新选择。重叠区域优先选择 Priority 更高者；同优先级保持当前区块，避免边界来回切换。区块之间的过门位置应重叠或相接。没有匹配区块时使用配置的 Default Region；没有 Default Region 时保留上一个有效范围。

相机上 Auto Find Player 默认开启，未绑定或原目标被销毁时自动查找 PlayerMovement。已绑定 Follow Target 时优先使用该目标。Auto Switch Regions 控制是否按位置自动选择。

## 接口

```csharp
CameraController.Instance.Bind2Transform(player.transform);
CameraController.Instance.SetBound(polygon); // 手动模式，暂停自动选区块
CameraController.Instance.EnableAutomaticRegions(); // 恢复自动选择
```

原有 GameEvent.BindCamera2Transform 与 GameEvent.SetCameraBound 继续有效。每次更换边界会调用 InvalidateBoundingShapeCache，避免使用上一块区域的缓存。

## 验证

Unity 编译完成，无编译错误。通过 Cinemachine 计算玩家位于默认区块左端、中间、右端的最终相机状态，三处完整画面均处于区块内。当前正式场景只配置一个默认区块，多个房间的边界形状与切换位置需要随关卡继续摆放。
