using UnityEngine;

namespace AChen.UI
{
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

    /// <summary>被其他 Window 覆盖时是否暂时隐藏。</summary>
    public bool HideOnForegroundLost {
        get { return hideOnForegroundLost; }
    }

    /// <summary>是否显示为带 DarkenBG 的弹窗。</summary>
    public bool IsPopup {
        get { return isPopup; }
    }

    /// <summary>打开时立即置顶或进入等待队列。</summary>
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
    /// <summary>最近一次打开时传入的强类型 Window 参数。</summary>
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
}
