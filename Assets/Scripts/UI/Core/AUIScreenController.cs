using UnityEngine;
using System;

/// <summary>
/// UI 界面基类。业务请继承 AWindowController 或 APanelController。
/// </summary>
public abstract class AUIScreenController : MonoBehaviour, IUIScreenController
{
    [Tooltip("关闭后销毁物体。下次打开会从 Prefab 再创建。Window 仅 Close 时销毁，被盖住的 Hide 不销毁。")]
    [SerializeField]
    bool m_destroyOnClose;

    bool m_opened;

    /// <summary>界面 Id，默认与 Prefab 名相同。</summary>
    public string ScreenId { get; set; }

    protected IScreenProperties Properties { get; private set; }

    /// <summary>请求所属 Layer 关闭自己。</summary>
    public Action<IUIScreenController> CloseRequest { get; set; }

    /// <summary>界面被销毁时通知 Layer。</summary>
    public Action<IUIScreenController> ScreenDestroyed { get; set; }

    /// <summary>当前是否可见。</summary>
    public bool IsVisible { get; private set; }

    /// <summary>关闭后是否销毁。Window 被盖住时的 Hide 不会销毁。</summary>
    public bool DestroyOnClose {
        get { return m_destroyOnClose; }
    }

    //它所属的UIFrame
    protected UIFrame m_UIFrame;

    internal void SetUIFrame(UIFrame uiFrame)
    {
        m_UIFrame = uiFrame;
    }

    protected virtual void Awake()
    {
        AddListeners();
        m_UIFrame = GetComponentInParent<UIFrame>();//获取它所属的UIFrame
    }

    protected virtual void OnDestroy()
    {
        m_opened = false;
        if (ScreenDestroyed != null)
        {
            ScreenDestroyed(this);
        }

        CloseRequest = null;
        ScreenDestroyed = null;
        RemoveListeners();
    }

    /// <summary>绑定事件，默认在 Awake 调用。</summary>
    protected virtual void AddListeners()
    {
    }

    /// <summary>解绑事件，默认在 OnDestroy 调用。</summary>
    protected virtual void RemoveListeners()
    {
    }

    /// <summary>首次打开。</summary>
    protected virtual void OnOpen()
    {
    }

    protected virtual void SetProperties(IScreenProperties properties)
    {
        Properties = properties;
    }

    /// <summary>暂时隐藏（例如 Window 被盖住），之后再显示会走 OnResume。</summary>
    protected virtual void OnHide()
    {
    }

    /// <summary>真正关闭（例如 Window 出栈）。之后再打开会走 OnOpen。</summary>
    protected virtual void OnClose()
    {
    }

    /// <summary>曾经打开过、隐藏后再显示。</summary>
    protected virtual void OnResume()
    {
    }

    /// <summary>Hierarchy 调整后、打开前调用。Window 默认把自己放到同层最后。</summary>
    protected virtual void HierarchyFixOnShow()
    {
    }

    /// <summary>暂时隐藏。Panel 的 HidePanel、Window 被盖住时走这里，回调 OnHide。</summary>
    public void Hide()
    {
        OnHide();
        gameObject.SetActive(false);
        IsVisible = false;
    }

    /// <summary>真正关闭。Window 出栈时走这里，回调 OnClose。勾选销毁则 Destroy。</summary>
    public void Close()
    {
        m_opened = false;
        OnClose();
        gameObject.SetActive(false);
        IsVisible = false;
        if (m_destroyOnClose)
        {
            if (ScreenDestroyed != null)
            {
                ScreenDestroyed(this);
            }
            ScreenDestroyed = null;
            DestroyScreenObject();
        }
    }

    /// <summary>显示界面。首次走 OnOpen，再次走 OnResume。</summary>
    public void Show(IScreenProperties properties = null)
    {
        if (properties != null)
        {
            SetProperties(properties);
        }

        HierarchyFixOnShow();
        gameObject.SetActive(true);
        IsVisible = true;
        if (m_opened)
        {
            OnResume();
        }
        else
        {
            OnOpen();
            m_opened = true;
        }
    }

    void DestroyScreenObject()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            DestroyImmediate(gameObject);
            return;
        }
#endif
        Destroy(gameObject);
    }
}
