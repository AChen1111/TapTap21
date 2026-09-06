using UnityEngine;

/// <summary>
/// Window 基类。
/// </summary>
public abstract class AWindowController : AUIScreenController, IWindowController
{
    [SerializeField]
    bool hideOnForegroundLost = true;

    [SerializeField]
    WindowPriority windowPriority = WindowPriority.ForceForeground;

    [SerializeField]
    bool isPopup;

    public bool HideOnForegroundLost {
        get { return hideOnForegroundLost; }
    }

    public bool IsPopup {
        get { return isPopup; }
    }

    public WindowPriority WindowPriority {
        get { return windowPriority; }
    }

    /// <summary>
    /// 给 Inspector 绑按钮用的关闭入口。真正出栈清理走 OnClose。
    /// </summary>
    public virtual void UI_Close() {
        CloseRequest(this);
    }

    protected override void HierarchyFixOnShow() {
        transform.SetAsLastSibling();
    }
}

public abstract class AWindowController<TProperties> : AWindowController
    where TProperties : IWindowProperties
{
    protected new TProperties Properties => (TProperties)base.Properties;

    protected override void SetProperties(IScreenProperties properties)
    {
        if (properties is TProperties typedProperties)
        {
            base.SetProperties(typedProperties);
            return;
        }

        Debug.LogError($"[AWindowController] Properties type {properties.GetType()} does not match {typeof(TProperties)}.");
    }
}
