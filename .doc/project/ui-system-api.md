## UI 框架（`AChen.UI`）

### 创建与获取 UIFrame

| API                                  | 何时使用                                  | 说明                                                         |
| ------------------------------------ | ----------------------------------------- | ------------------------------------------------------------ |
| `UISettings.CreateUIInstance()`      | 游戏启动时创建整套 UI                     | 创建 Frame，并实例化、注册配置中的 Panel 和 Window；通常只调用一次 |
| `UISettings.CreateUIInstance(false)` | 只需要 Frame，界面准备按需注册时          | 不实例化 `ScreensToRegister`                                 |
| `UIFrame.Initialize()`               | Inspector 关闭了 `Initialize On Awake` 时 | 启动流程手动调用一次；默认无需调用                           |
| `UIFrame.MainCanvas`                 | 需要访问 Canvas 配置或做坐标转换时        | 返回 Frame 主 Canvas                                         |
| `UIFrame.UICamera`                   | 需要屏幕坐标与 UI 坐标互转时              | 返回主 Canvas 使用的相机                                     |

### 显示与关闭界面

| API                                | 何时使用                                | 说明                                                         |
| ---------------------------------- | --------------------------------------- | ------------------------------------------------------------ |
| `ShowPanel(screenId)`              | 显示 HUD、提示条等可并存界面            | 无参数 Panel                                                 |
| `ShowPanel(screenId, properties)`  | Panel 本次显示需要数据时                | 参数类型实现 `IPanelProperties`，Panel 继承 `APanelController<TProperties>` |
| `HidePanel(screenId)`              | 暂时隐藏 Panel 时                       | 触发 `OnHide`；再次显示触发 `OnResume`                       |
| `OpenWindow(screenId)`             | 打开需要历史栈或排队行为的页面时        | 无参数 Window                                                |
| `OpenWindow(screenId, properties)` | Window 本次打开需要数据时               | 参数类型实现 `IWindowProperties`，Window 继承 `AWindowController<TProperties>` |
| `CloseWindow(screenId)`            | 明确关闭指定 Window 时                  | 从历史栈关闭并触发 `OnClose`                                 |
| `CloseCurrentWindow()`             | 返回按钮或系统返回键关闭顶层 Window 时  | 关闭当前最前方 Window                                        |
| `ShowScreen(screenId)`             | 调用方不知道目标是 Panel 还是 Window 时 | 按注册类型自动选择；已知类型时优先调用明确 API               |
| `IsPanelOpen(panelId)`             | 避免重复展示或判断 HUD 状态时           | 只判断 Panel 是否可见                                        |
| `HideAll()`                        | 登出、切换账号或整体 UI 状态切换时      | 关闭全部 Window 并隐藏全部 Panel                             |
| `HideAllPanels()`                  | 只需收起所有 HUD 时                     | 不影响 Window                                                |
| `CloseAllWindows()`                | 只需清空 Window 历史栈时                | 不影响 Panel                                                 |

不要直接对界面调用 `SetActive`，否则 Window 栈、Panel 状态和 DarkenBG 可能不同步。

### 动态注册

| API                                         | 何时使用                                  | 说明                                                |
| ------------------------------------------- | ----------------------------------------- | --------------------------------------------------- |
| `RegisterScreenPrefab(id, prefab)`          | 运行时增加可按需创建的界面类型时          | Prefab 根节点必须有 `IUIScreenController`           |
| `RegisterScreen(id, controller, transform)` | 已经创建了界面实例，需要交给 Frame 管理时 | 自动识别 Panel/Window 并调整层级                    |
| `RegisterPanel` / `RegisterWindow`          | 实例层级由调用方自己管理时                | 只注册控制器，不自动调整父节点                      |
| `UnregisterPanel` / `UnregisterWindow`      | 动态实例将被调用方移除或销毁前            | 清除对应 Layer 的注册记录                           |
| `IsScreenRegistered(id)`                    | 动态注册前检查重复 Id 时                  | 检查 Panel 和 Window 两层                           |
| `IsScreenRegistered(id, out type)`          | 还需要判断界面属于哪一层时                | `type` 为 `IPanelController` 或 `IWindowController` |
| `SetDarkenVisible(visible)`                 | 特殊流程需要手动控制遮挡层时              | 普通弹窗由 Window Layer 自动维护，不需要手动调用    |

### 编写界面控制器

| API / 回调          | 何时使用                                           |
| ------------------- | -------------------------------------------------- |
| `AddListeners()`    | 在界面 `Awake` 时绑定按钮和事件中心监听            |
| `RemoveListeners()` | 在界面销毁时解绑监听                               |
| `OnOpen()`          | 第一次显示时初始化本次打开状态                     |
| `OnResume()`        | 界面被暂时隐藏后重新显示时刷新状态                 |
| `OnHide()`          | Panel 被隐藏或 Window 被覆盖时暂停临时行为         |
| `OnClose()`         | Window 真正出栈或界面真正关闭时清理本次会话        |
| `Properties`        | 在泛型 Panel/Window 子类中读取本次显示参数         |
| `UI_Close()`        | Window 的关闭按钮使用，可直接绑定 Inspector Button |

业务 Window 继承 `AWindowController` 或 `AWindowController<TProperties>`；业务 Panel 继承 `APanelController` 或 `APanelController<TProperties>`。不要直接继承 `AUIScreenController`。
