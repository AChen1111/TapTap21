using System;

/// <summary>
/// 所有 UI 界面都要实现的接口。
/// </summary>
public interface IUIScreenController
{
    string ScreenId { get; set; }
    bool IsVisible { get; }
    bool DestroyOnClose { get; }

    void Show(IScreenProperties properties = null);
    void Hide();
    void Close();

    Action<IUIScreenController> CloseRequest { get; set; }
    Action<IUIScreenController> ScreenDestroyed { get; set; }
}

/// <summary>
/// Window 接口。
/// </summary>
public interface IWindowController : IUIScreenController
{
    bool HideOnForegroundLost { get; }
    bool IsPopup { get; }
    WindowPriority WindowPriority { get; }
}

/// <summary>
/// Panel 接口。
/// </summary>
public interface IPanelController : IUIScreenController
{
    PanelPriority Priority { get; }
}

public interface IScreenProperties { }

public interface IWindowProperties : IScreenProperties { }

public interface IPanelProperties : IScreenProperties { }
