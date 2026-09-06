# 业务 API 使用指南

本文只列业务代码可以直接调用或实现的入口。UI Layer、对象池 Bucket、日志 Editor 工具等内部类型不应由业务直接调用。

## UI 框架（`AChen.UI`）

### 创建与获取 UIFrame

| API | 何时使用 | 说明 |
| --- | --- | --- |
| `UISettings.CreateUIInstance()` | 游戏启动时创建整套 UI | 创建 Frame，并实例化、注册配置中的 Panel 和 Window；通常只调用一次 |
| `UISettings.CreateUIInstance(false)` | 只需要 Frame，界面准备按需注册时 | 不实例化 `ScreensToRegister` |
| `UIFrame.Initialize()` | Inspector 关闭了 `Initialize On Awake` 时 | 启动流程手动调用一次；默认无需调用 |
| `UIFrame.MainCanvas` | 需要访问 Canvas 配置或做坐标转换时 | 返回 Frame 主 Canvas |
| `UIFrame.UICamera` | 需要屏幕坐标与 UI 坐标互转时 | 返回主 Canvas 使用的相机 |

### 显示与关闭界面

| API | 何时使用 | 说明 |
| --- | --- | --- |
| `ShowPanel(screenId)` | 显示 HUD、提示条等可并存界面 | 无参数 Panel |
| `ShowPanel(screenId, properties)` | Panel 本次显示需要数据时 | 参数类型实现 `IPanelProperties`，Panel 继承 `APanelController<TProperties>` |
| `HidePanel(screenId)` | 暂时隐藏 Panel 时 | 触发 `OnHide`；再次显示触发 `OnResume` |
| `OpenWindow(screenId)` | 打开需要历史栈或排队行为的页面时 | 无参数 Window |
| `OpenWindow(screenId, properties)` | Window 本次打开需要数据时 | 参数类型实现 `IWindowProperties`，Window 继承 `AWindowController<TProperties>` |
| `CloseWindow(screenId)` | 明确关闭指定 Window 时 | 从历史栈关闭并触发 `OnClose` |
| `CloseCurrentWindow()` | 返回按钮或系统返回键关闭顶层 Window 时 | 关闭当前最前方 Window |
| `ShowScreen(screenId)` | 调用方不知道目标是 Panel 还是 Window 时 | 按注册类型自动选择；已知类型时优先调用明确 API |
| `IsPanelOpen(panelId)` | 避免重复展示或判断 HUD 状态时 | 只判断 Panel 是否可见 |
| `HideAll()` | 登出、切换账号或整体 UI 状态切换时 | 关闭全部 Window 并隐藏全部 Panel |
| `HideAllPanels()` | 只需收起所有 HUD 时 | 不影响 Window |
| `CloseAllWindows()` | 只需清空 Window 历史栈时 | 不影响 Panel |

不要直接对界面调用 `SetActive`，否则 Window 栈、Panel 状态和 DarkenBG 可能不同步。

### 动态注册

| API | 何时使用 | 说明 |
| --- | --- | --- |
| `RegisterScreenPrefab(id, prefab)` | 运行时增加可按需创建的界面类型时 | Prefab 根节点必须有 `IUIScreenController` |
| `RegisterScreen(id, controller, transform)` | 已经创建了界面实例，需要交给 Frame 管理时 | 自动识别 Panel/Window 并调整层级 |
| `RegisterPanel` / `RegisterWindow` | 实例层级由调用方自己管理时 | 只注册控制器，不自动调整父节点 |
| `UnregisterPanel` / `UnregisterWindow` | 动态实例将被调用方移除或销毁前 | 清除对应 Layer 的注册记录 |
| `IsScreenRegistered(id)` | 动态注册前检查重复 Id 时 | 检查 Panel 和 Window 两层 |
| `IsScreenRegistered(id, out type)` | 还需要判断界面属于哪一层时 | `type` 为 `IPanelController` 或 `IWindowController` |
| `SetDarkenVisible(visible)` | 特殊流程需要手动控制遮挡层时 | 普通弹窗由 Window Layer 自动维护，不需要手动调用 |

### 编写界面控制器

| API / 回调 | 何时使用 |
| --- | --- |
| `AddListeners()` | 在界面 `Awake` 时绑定按钮和事件中心监听 |
| `RemoveListeners()` | 在界面销毁时解绑监听 |
| `OnOpen()` | 第一次显示时初始化本次打开状态 |
| `OnResume()` | 界面被暂时隐藏后重新显示时刷新状态 |
| `OnHide()` | Panel 被隐藏或 Window 被覆盖时暂停临时行为 |
| `OnClose()` | Window 真正出栈或界面真正关闭时清理本次会话 |
| `Properties` | 在泛型 Panel/Window 子类中读取本次显示参数 |
| `UI_Close()` | Window 的关闭按钮使用，可直接绑定 Inspector Button |

业务 Window 继承 `AWindowController` 或 `AWindowController<TProperties>`；业务 Panel 继承 `APanelController` 或 `APanelController<TProperties>`。不要直接继承 `AUIScreenController`。

### 虚拟列表

| API | 何时使用 | 说明 |
| --- | --- | --- |
| `GridListController.InitList(rowPrefab, data)` | 已通过 Inspector 持有行 Prefab 时 | 推荐重载，立即绑定 |
| `await GridListController.InitList(resourceKey, data)` | 行 Prefab 位于 Resources 且当前没有引用时 | key 为 Resources 相对路径，不含扩展名 |
| `SelectedIndex` | 需要读取当前选中数据索引时 | 未选中为 `-1` |
| `MoveToSelectedIfHidden()` | 初始化或恢复选中项后，确保选中项可见时 | 已可见则不滚动；可传时长和 DOTween Ease |
| `IRowItem<TData>.SetRowData(...)` | 实现行 Prefab 组件时 | 框架刷新可见行时调用，业务不要主动调用 |

## 日志系统（`AChen.Log`）

| API | 何时使用 | 示例 |
| --- | --- | --- |
| `ALog.Log(message, category)` | 正常流程、状态变化和调试信息 | `ALog.Log("打开商店", ALogCategories.UI)` |
| `ALog.LogWarning(message, category)` | 可恢复但需要关注的问题 | `ALog.LogWarning("登录重试", ALogCategories.Net)` |
| `ALog.LogError(message, category)` | 功能失败、非法状态或关键配置缺失 | `ALog.LogError("UISettings 为空", ALogCategories.UI)` |
| `ALog.Enabled` | 昂贵日志内容需要延迟计算时 | 先检查再构造复杂字符串；普通日志无需手动检查 |
| `ALog.Format(category, message)` | 只需要统一格式字符串、暂不输出时 | 普通输出不要先 Format，直接调用对应日志 API |

分类使用 `ALogCategories.Default`、`Net`、`Event`、`UI`。新增分类应在 `ALogCategories` 中添加常量，不要在业务代码中散落字符串。

## 对象池（`AChen.Pooling`）

| API | 何时使用 | 说明 |
| --- | --- | --- |
| `GameObjectPool.Instance` | 访问全局对象池时 | 首次访问自动创建，跨场景保留 |
| `Get(prefab)` | 取出后会由调用方设置 Transform 时 | 默认世界坐标为零、旋转为单位旋转 |
| `Get(prefab, position, rotation)` | 子弹、特效等生成时就要定位时 | 优先 LIFO 复用，否则创建新实例 |
| `Release(instance)` | 对象本次使用结束时 | 替代 `Destroy`；只能归还本池当前借出的实例一次 |
| `ClearPool(prefab)` | 切换大场景或释放某类空闲缓存时 | 只销毁该 Prefab 的空闲实例，返回销毁数量 |
| `IPoolable.OnTakenFromPool()` | 实现池化组件时 | 重置速度、计时器、生命值等状态 |
| `IPoolable.OnReturnedToPool()` | 实现池化组件时 | 解绑事件、停止特效、清理外部引用 |

`IPoolable` 必须由 Prefab 根节点组件实现。池化对象结束使用时调用 `Release`，不要调用 `Destroy(instance.gameObject)`。

## 事件中心（`AChen.Events`）

### 定义事件

| API | 何时使用 |
| --- | --- |
| `new EventId(name)` | 定义无参数通知，例如“游戏开始” |
| `new EventId<T>(name)` | 定义携带一个强类型数据的事件，例如“分数变化” |
| `new EventId<T1, T2>(name)` | 两项数据天然属于同一通知时；更多数据建议封装成一个参数对象 |

事件统一定义在 `GameEvent`，业务调用处不要临时创建 `EventId`。事件名建议使用“模块.事件”格式并保持唯一。

### 订阅与派发

| API | 何时使用 | 说明 |
| --- | --- | --- |
| `EventCenter.AddListener(eventId, handler)` | 对象启用并需要接收通知时 | 推荐在 `OnEnable` 调用；参数类型由 EventId 推断 |
| `EventCenter.RemoveListener(eventId, handler)` | 对象禁用或销毁前 | 推荐在 `OnDisable` 调用，必须与订阅成对 |
| `EventCenter.Dispatch(eventId)` | 发布无参数通知时 | 没有监听器是正常空操作 |
| `EventCenter.Dispatch(eventId, arg)` | 发布单参数通知时 | 编译器检查参数类型 |
| `EventCenter.Dispatch(eventId, arg1, arg2)` | 发布双参数通知时 | 所有监听器按注册顺序同步执行 |

事件中心适合同一进程内模块解耦，不用于跨进程通信、持久化消息或需要返回值的请求。派发是同步的，监听器中不要执行耗时工作。

## 快速选择

| 需求 | 使用 |
| --- | --- |
| 打开页面或 HUD | `UIFrame.OpenWindow` / `ShowPanel` |
| 输出可筛选日志 | `ALog.Log` / `LogWarning` / `LogError` |
| 高频创建和销毁同类 Prefab | `GameObjectPool.Get` / `Release` |
| 一个模块通知多个无直接依赖的模块 | `EventCenter.Dispatch` + `AddListener` |
