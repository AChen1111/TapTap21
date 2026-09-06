namespace AChen.UI
{
﻿using System;

/// <summary>
/// 所有 UI 界面都要实现的接口。
/// </summary>
public interface IUIScreenController
{
    /// <summary>界面的唯一 Id，默认使用 Prefab 名。</summary>
    string ScreenId { get; set; }
    /// <summary>界面当前是否可见。</summary>
    bool IsVisible { get; }
    /// <summary>真正关闭后是否销毁实例。</summary>
    bool DestroyOnClose { get; }

    /// <summary>显示界面；首次显示走 OnOpen，再次显示走 OnResume。</summary>
    /// <param name="properties">本次显示参数，无参数界面传空。</param>
    void Show(IScreenProperties properties = null);
    /// <summary>暂时隐藏界面，不从 Window 历史栈中真正关闭。</summary>
    void Hide();
    /// <summary>真正关闭界面，并按配置决定是否销毁。</summary>
    void Close();

    /// <summary>请求所属 Layer 关闭该界面。业务通常通过 <c>UI_Close</c> 间接触发。</summary>
    Action<IUIScreenController> CloseRequest { get; set; }
    /// <summary>界面销毁时通知所属 Layer 清理注册记录。</summary>
    Action<IUIScreenController> ScreenDestroyed { get; set; }
}

/// <summary>
/// Window 接口。
/// </summary>
public interface IWindowController : IUIScreenController
{
    /// <summary>被更高层 Window 覆盖时是否暂时隐藏。</summary>
    bool HideOnForegroundLost { get; }
    /// <summary>是否作为带遮挡背景的弹窗显示。</summary>
    bool IsPopup { get; }
    /// <summary>Window 打开时相对当前 Window 的排队策略。</summary>
    WindowPriority WindowPriority { get; }
}

/// <summary>
/// Panel 接口。
/// </summary>
public interface IPanelController : IUIScreenController
{
    /// <summary>Panel 所属的显示层优先级。</summary>
    PanelPriority Priority { get; }
}

/// <summary>所有 UI 显示参数的标记接口。</summary>
public interface IScreenProperties { }

/// <summary>Window 强类型显示参数的标记接口。</summary>
public interface IWindowProperties : IScreenProperties { }

/// <summary>Panel 强类型显示参数的标记接口。</summary>
public interface IPanelProperties : IScreenProperties { }
}
