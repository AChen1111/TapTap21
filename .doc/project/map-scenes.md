# 多地图与常驻玩家

正式入口为 Menu。启动后自动以 Additive 加载 PersistentScene，点击开始后卸载 Menu 并进入 Scene_1。Build Settings 前三项已配置为 Menu、PersistentScene、Scene_1。

PersistentScene 保留管理器、对象池、EventSystem、游戏 UI 和唯一玩家实例。Scene_1（原 GameScene，GUID 保留）只保存地图、相机、相机边界、交互系统与出生点。玩家由 GameSceneManager 首次进入地图时从 PersistentPlayer.prefab 创建，并归属 PersistentScene。后续切换地图不重建玩家，生命值及同一份背包模型持续保留。初始背包仍采用原测试配置的树苗和水。

## 配置下一张地图

1. 新建 Scene_2，并加入 Build Settings。
2. 地图中配置地形、Main Camera/CinemachineCamera、CameraRegion 和 SceneItemSystem。不要重复放玩家、EventSystem 或管理器。
3. 空对象添加 MapSpawnPoint，放在安全落脚位置，Spawn Id 填 Default。也可配置 LeftEntrance、RightEntrance 等不同入口。
4. 出口节点添加 MapExit2D，BoxCollider2D 勾选 Is Trigger，Target Scene 填 Scene_2，Target Spawn Id 填目标入口 Id。
5. 回程出口用相同方式指向 Scene_1 的入口。

代码也可以调用 `FindFirstObjectByType<GameSceneManager>().LoadMap("Scene_2", "LeftEntrance")`。切图期间暂时隐藏玩家，完成后移到入口、清空速度、恢复移动状态，绑定新相机并更新当前重生点。没有新增地图美术或自动复制第二张地图。

## 两种出口与渐变过场

MapExit2D 的 Activation 有两种模式：Automatic 放在地图尽头，玩家进入 Trigger 后自动过图；Interact 放在门、洞口等位置，玩家进入范围后按现有交互键 F 过图。Interaction Prompt 可引用一个提示子对象，交互范围内显示，离开后隐藏。不要把出口根节点自身当成提示对象。目标出生点要放在安全位置，避免与回程出口重叠。

两种出口共用 LoadMap：锁定重复请求、暂停移动和物理、0.35 秒淡到黑色、Additive 异步加载目标、验证目标入口、卸载旧地图、移动原玩家、绑定相机、0.35 秒淡入，最后恢复移动。渐变使用真实时间，不受 Time.timeScale 影响，遮罩属于 PersistentScene，加载时阻挡 UI 点击。Fade Duration 在 GameSceneManager 上调整。目标出生点缺失时取消切换并卸载新场景，旧地图和玩家位置保留。

切换结束后出口冷却 0.5 秒。出口统计玩家多个 Collider，避免退出一个碰撞体后提前撤销交互范围。当前没有第二张正式地图，因此提供 Automatic 和 Interact 可复用出口 Prefab，由关卡配置 Target Scene 和 Target Spawn Id。

直接在编辑器打开 Scene_1 并 Play 时，也会自动加载 PersistentScene、创建玩家和游戏 UI。菜单界面仅在 Menu 启动流程可见。每张地图必须有匹配的出生点；缺少时会输出错误。当前地图对象的种植变化不会自动跨卸载恢复，地图状态 JSON 存储仍需单独接入。
