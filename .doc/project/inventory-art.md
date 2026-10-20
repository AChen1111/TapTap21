# 物品栏素材与布局

采用项目原素材 `Assets/Art/UI_Items/0461E2BB924A7752C04BA417E5359AE1.png`，从透明图集中裁出独立 PNG，原图保持不变。裁切资源位于 `Assets/UI/Sprite/HUD/Inventory/Forest`。

| 资源 | 用途 |
|---|---|
| InventoryBoard | 木质物品栏底板 |
| InventorySlot | 单个浅色格子背景，复用为十格 |
| ItemWater | GamePlay.Core.Item_Water |
| ItemWind | GamePlay.Core.Item_WindSeed |
| ItemShovel | GamePlay.Core.Item_Shovel |
| ItemSoil_Unassigned | 土堆，仅裁切，暂不绑定物品 |

背包容量仍为 10。UIManager 创建现有背包模型，SlotController 和 SlotDragger 继续处理显示与拖拽；没有增加固定物品按钮，也没有修改初始物品。

## 调整位置

打开 `Assets/UI/Prefab/HUD/Inventory/InventoryCanvas.prefab`，选中 `PanelLayer/Inventory`，修改 RectTransform 的 Anchored Position 调整整体位置。当前使用左上角锚点，位置 (215,-8)，大小 (1000,239)。木板按原始宽高比显示。

`Inventory/Content` 使用单排 GridLayoutGroup，Cell Size=(74,74)、Spacing=(10,0)，共十格。格子资源为 `Assets/UI/Prefab/HUD/Inventory/Slot.prefab`。图标区域为 60×60，保持宽高比；数量字号 18，位于右下角。血条根节点位置调整为 (8,-77)，与物品栏格子中心线对齐。

三种图标引用保存在 `Assets/Config/UI/HUD/Inventory/Sprite2ItemTable.asset`。其他物品沿用现有映射；本次没有把土堆推断成新道具。拖拽时复制图标到根 Canvas 顶层并关闭其 Raycast Target，避免被其他格子遮住。

已运行检查 1280×720 Game View：十格位于木板内、与血条和右上角按钮无重叠；临时填入水、风种和铲子确认显示与数量；验证拖拽图标处于 Canvas 顶层且不拦截射线。临时物品不写入正式初始配置。
