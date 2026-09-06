# UI 系统

命名空间 `AChen.UI`。脚本在 `Assets/Scripts/UI/Core/`，预制体在 `Assets/UI/Prefab/`。业务脚本放 `Assets/Scripts/UI/`，对应预制体放 `Assets/UI/Prefab/`，不要混放。

## 两层界面

| 类型 | 基类 | 行为 |
| --- | --- | --- |
| Panel | `APanelController` | HUD 类，可同时显示多个，无历史栈 |
| Window | `AWindowController` | 一次只交互一个，有历史栈和队列 |

业务不要直接继承 `AUIScreenController`。

界面 Id 默认等于 Prefab 名。打开、关闭都用这个 Id。

## 生命周期

| 回调 | 何时 |
| --- | --- |
| `AddListeners` / `RemoveListeners` | `Awake` / `OnDestroy` |
| `OnOpen` | 第一次显示 |
| `OnResume` | 曾经打开过、再显示（Window 被盖住后回到前台） |
| `OnHide` | 暂时隐藏：`HidePanel`，或 Window 被另一扇窗盖住 |
| `OnClose` | 真正关闭：`CloseWindow` 出栈 |

勾选 `Destroy On Close` 后，`OnClose` 会销毁物体。下次打开从 Prefab 再实例化。Window 被盖住走 `OnHide`，不会销毁。

## 创建 Frame

菜单：

- `Assets/Create/deVoid UI/UI Frame Prefab`
- `Assets/Create/deVoid UI/UI Frame in Scene`
- `Assets/Create/deVoid UI/UI Settings`

现成模板：`Assets/UI/Prefab/BaseUI/UIFrame.prefab`、`UIFrameLogin.prefab`。

```csharp
using AChen.UI;

UISettings settings = /* 自己持有的 UISettings 资源 */;
UIFrame frame = settings.CreateUIInstance();

frame.ShowPanel("PreGameUIPanel");
frame.HidePanel("PreGameUIPanel");

frame.OpenWindow("ShopWindows");
frame.CloseWindow("ShopWindows");
frame.CloseCurrentWindow();

frame.ShowScreen("ShopWindows"); // 按已注册类型自动走 Panel 或 Window
frame.HideAll();
```

`UISettings` 里填 Frame Prefab 和要预注册的 Screen Prefab。`CreateUIInstance` 会实例化 Frame，并按 Prefab 名注册各界面。

也可以运行时 `RegisterScreenPrefab` + `RegisterScreen`。未注册且没有 Prefab 的 Id 打开会打 Error。

## 写一个 Window

```csharp
using AChen.UI;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : AWindowController
{
    [SerializeField] Button m_BtnClose;

    protected override void AddListeners()
    {
        m_BtnClose.onClick.AddListener(UI_Close);
    }

    protected override void RemoveListeners()
    {
        m_BtnClose.onClick.RemoveListener(UI_Close);
    }

    protected override void OnOpen()
    {
        // 首次打开
    }
}
```

`UI_Close()` 可直接绑 Inspector 按钮。弹窗勾选 `Is Popup`，会走 Priority Window 层并显示 DarkenBG。

`WindowPriority.ForceForeground`：立刻盖住当前窗。`Enqueue`：进队列，等当前窗关掉再开。

带参数：

```csharp
public class TipWindow : AWindowController<TipProperties> { }

frame.OpenWindow("TipWindow", new TipProperties { Text = "ok" });
```

Panel 同理，用 `APanelController` / `ShowPanel`。`PanelPriority` 决定挂到哪一层 para-layer（在 Panel Layer 上配）。

## 生成界面脚本

物体上挂 `UiScreenGenerator`（菜单 UI/UI Screen Generator）。

子节点按前缀命名才会被收集：

| 前缀 | 组件 | 生成字段 |
| --- | --- | --- |
| `Btn_` | Button | `m_BtnXxx` |
| `Img_` | Image | `m_ImgXxx` |
| `Txt_` | TextMeshProUGUI | `m_TxtXxx` |
| `Tog_` | Toggle | `m_TogXxx` |
| `Sld_` | Slider | `m_SldXxx` |
| `Inp_` | TMP_InputField | `m_InpXxx` |
| `Scr_` | ScrollRect | `m_ScrXxx` |
| `Raw_` | RawImage | `m_RawXxx` |
| `Drop_` | TMP_Dropdown | `m_DropXxx` |
| `Go_` | GameObject | `m_GoXxx` |

例如子物体名 `Btn_Close` → 字段 `m_BtnClose`。

Inspector 按钮：收集 UI 引用 → 创建 UI 脚本 → 编译后自动挂脚本并绑引用。生成的类会 `using AChen.UI`，并继承 `APanelController` 或 `AWindowController`。

## 列表

`GridListController` 包一层 SuperScrollView `LoopListView2`。行物体实现 `IRowItem<TData>`。

```csharp
// 推荐：直接传预制体
list.InitList(rowPrefab, dataList, onSelected: i => { }, selectedIndex: 0);

// 或从 Resources 加载，key 为 Resources 相对路径（不含扩展名）
await list.InitList("UI/ShopRow", dataList);
```

本工程尚未接 Addressables。字符串重载不再走 Addressable key。选中项滚出视口时用 `MoveToSelectedIfHidden`，动画是 DOTween。

## 注意事项

- 必须先有带 `PanelUILayer`、`WindowUILayer` 的 UIFrame，再打开界面。缺层会在 Initialize 时报错。
- Screen 预制体上必须有 `IUIScreenController`（Panel/Window 基类）。丢进 `UISettings` 的物体没有控制器会被自动剔除。
- 脚本在 `AChen.UI`。业务界面若自己写类，记得 `using AChen.UI`。
- 打开/关闭用 Frame API，不要对 Screen 自己 `SetActive`，否则栈和 DarkenBG 会对不上。
- Window 出栈才是 Close；被盖住是 Hide。两者生命周期不同。
- 当前没有迁业务登录/大厅界面，也没有 UITween（原工程依赖 LitMotion / Spine）。
- `ScreenFitter` 挂在 Frame 上做分辨率适配，逻辑高度按 960。
