using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI 入口。业务通过它打开/关闭 Panel 和 Window。
/// </summary>
public class UIFrame : MonoBehaviour
{
    [Tooltip("取消勾选后需自行调用 Initialize")]
    [SerializeField] private bool initializeOnAwake = true;

    private PanelUILayer panelLayer;
    private WindowUILayer windowLayer;
    private WindowParaLayer windowParaLayer;
    private Dictionary<string, GameObject> screenPrefabs;

    private Canvas mainCanvas;

    /// <summary>主 Canvas。</summary>
    public Canvas MainCanvas {
        get {
            if (mainCanvas == null) {
                mainCanvas = GetComponent<Canvas>();
            }

            return mainCanvas;
        }
    }

    /// <summary>主 Canvas 使用的相机。</summary>
    public Camera UICamera {
        get { return MainCanvas.worldCamera; }
    }

    private void Awake() {
        if (initializeOnAwake) {
            Initialize();
        }
    }

    /// <summary>初始化 Panel / Window 两层。需要额外 Layer 时可重写。</summary>
    public virtual void Initialize() {
        if (panelLayer == null) {
            panelLayer = gameObject.GetComponentInChildren<PanelUILayer>(true);
            if (panelLayer == null) {
                Debug.LogError("[UI Frame] UI Frame lacks Panel Layer!");
            }
            else {
                panelLayer.Initialize();
            }
        }

        if (windowLayer == null) {
            windowLayer = gameObject.GetComponentInChildren<WindowUILayer>(true);
            if (windowLayer == null) {
                Debug.LogError("[UI Frame] UI Frame lacks Window Layer!");
            }
            else {
                windowLayer.Initialize();
            }
        }

        if (screenPrefabs == null) {
            screenPrefabs = new Dictionary<string, GameObject>();
        }

        if (windowParaLayer == null) {
            windowParaLayer = gameObject.GetComponentInChildren<WindowParaLayer>(true);
        }
    }

    /// <summary>打开或关闭 DarkenBG 遮挡层。</summary>
    public void SetDarkenVisible(bool visible) {
        if (windowParaLayer == null) {
            windowParaLayer = gameObject.GetComponentInChildren<WindowParaLayer>(true);
        }

        if (windowParaLayer == null) {
            return;
        }

        if (visible) {
            windowParaLayer.DarkenBG();
        }
        else {
            windowParaLayer.HideDarken();
        }
    }

    /// <summary>登记界面 Prefab。关闭销毁后，下次打开会按 Id 再实例化。</summary>
    public void RegisterScreenPrefab(string screenId, GameObject prefab) {
        if (screenPrefabs == null) {
            screenPrefabs = new Dictionary<string, GameObject>();
        }
        screenPrefabs[screenId] = prefab;
    }

    bool EnsureScreen(string screenId) {
        if (IsScreenRegistered(screenId)) {
            return true;
        }

        GameObject prefab;
        if (screenPrefabs == null || !screenPrefabs.TryGetValue(screenId, out prefab) || prefab == null) {
            Debug.LogError("[UI Frame] Screen ID " + screenId + " is not registered and has no Prefab.");
            return false;
        }

        var instance = Instantiate(prefab);
        var controller = instance.GetComponent<IUIScreenController>();
        if (controller == null) {
            Debug.LogError("[UI Frame] Prefab for " + screenId + " has no ScreenController.");
            Destroy(instance);
            return false;
        }

        RegisterScreen(screenId, controller, instance.transform);
        if (instance.activeSelf) {
            instance.SetActive(false);
        }
        return true;
    }

    /// <summary>按 Id 显示 Panel。</summary>
    /// <param name="screenId">Panel Id</param>
    public void ShowPanel(string screenId) {
        if (!EnsureScreen(screenId)) {
            return;
        }
        panelLayer.ShowScreenById(screenId);
    }

    public void ShowPanel<TProperties>(string screenId, TProperties properties)
        where TProperties : IPanelProperties {
        if (!EnsureScreen(screenId)) {
            return;
        }
        panelLayer.ShowScreenById(screenId, properties);
    }

    /// <summary>按 Id 隐藏 Panel（OnHide）。</summary>
    /// <param name="screenId">Panel Id</param>
    public void HidePanel(string screenId) {
        panelLayer.HideScreenById(screenId);
    }

    /// <summary>按 Id 打开 Window。</summary>
    /// <param name="screenId">Window Id</param>
    public void OpenWindow(string screenId) {
        if (!EnsureScreen(screenId)) {
            return;
        }
        windowLayer.ShowScreenById(screenId);
    }

    public void OpenWindow<TProperties>(string screenId, TProperties properties)
        where TProperties : IWindowProperties {
        if (!EnsureScreen(screenId)) {
            return;
        }
        windowLayer.ShowScreenById(screenId, properties);
    }

    /// <summary>按 Id 关闭 Window（OnClose）。</summary>
    /// <param name="screenId">Window Id</param>
    public void CloseWindow(string screenId) {
        windowLayer.HideScreenById(screenId);
    }

    /// <summary>关闭当前 Window。</summary>
    public void CloseCurrentWindow() {
        if (windowLayer.CurrentWindow != null) {
            CloseWindow(windowLayer.CurrentWindow.ScreenId);
        }
    }

    /// <summary>在 Panel / Window 层查找并打开。</summary>
    /// <param name="screenId">界面 Id</param>
    public void ShowScreen(string screenId) {
        if (!EnsureScreen(screenId)) {
            return;
        }
        Type type;
        if (IsScreenRegistered(screenId, out type)) {
            if (type == typeof(IWindowController)) {
                OpenWindow(screenId);
            }
            else if (type == typeof(IPanelController)) {
                ShowPanel(screenId);
            }
        }
        else {
            Debug.LogError(string.Format("Tried to open Screen id {0} but it's not registered as Window or Panel!",
                screenId));
        }
    }

    /// <summary>注册界面。传入 Transform 时会挂到对应层。注册后才能打开。</summary>
    /// <param name="screenId">界面 Id</param>
    /// <param name="controller">控制器</param>
    /// <param name="screenTransform">不为空则改父节点到对应层</param>
    public void RegisterScreen(string screenId, IUIScreenController controller, Transform screenTransform) {
        if (controller is AUIScreenController screenController) {
            screenController.SetUIFrame(this);
        }

        IWindowController window = controller as IWindowController;
        if (window != null) {
            windowLayer.RegisterScreen(screenId, window);
            if (screenTransform != null) {
                windowLayer.ReparentScreen(controller, screenTransform);
            }

            return;
        }

        IPanelController panel = controller as IPanelController;
        if (panel != null) {
            panelLayer.RegisterScreen(screenId, panel);
            if (screenTransform != null) {
                panelLayer.ReparentScreen(controller, screenTransform);
            }
        }
    }

    /// <summary>注册 Panel。</summary>
    /// <param name="screenId">Panel Id</param>
    /// <param name="controller">控制器</param>
    /// <typeparam name="TPanel">控制器类型</typeparam>
    public void RegisterPanel<TPanel>(string screenId, TPanel controller) where TPanel : IPanelController {
        panelLayer.RegisterScreen(screenId, controller);
    }

    /// <summary>取消注册 Panel。</summary>
    /// <param name="screenId">Panel Id</param>
    /// <param name="controller">控制器</param>
    /// <typeparam name="TPanel">控制器类型</typeparam>
    public void UnregisterPanel<TPanel>(string screenId, TPanel controller) where TPanel : IPanelController {
        panelLayer.UnregisterScreen(screenId, controller);
    }

    /// <summary>注册 Window。</summary>
    /// <param name="screenId">Window Id</param>
    /// <param name="controller">控制器</param>
    /// <typeparam name="TWindow">控制器类型</typeparam>
    public void RegisterWindow<TWindow>(string screenId, TWindow controller) where TWindow : IWindowController {
        windowLayer.RegisterScreen(screenId, controller);
    }

    /// <summary>取消注册 Window。</summary>
    /// <param name="screenId">Window Id</param>
    /// <param name="controller">控制器</param>
    /// <typeparam name="TWindow">控制器类型</typeparam>
    public void UnregisterWindow<TWindow>(string screenId, TWindow controller) where TWindow : IWindowController {
        windowLayer.UnregisterScreen(screenId, controller);
    }

    /// <summary>该 Panel 是否正在显示。</summary>
    /// <param name="panelId">Panel Id</param>
    public bool IsPanelOpen(string panelId) {
        return panelLayer.IsPanelVisible(panelId);
    }

    /// <summary>关闭全部 Window，并隐藏全部 Panel。</summary>
    public void HideAll() {
        CloseAllWindows();
        HideAllPanels();
    }

    /// <summary>隐藏全部 Panel。</summary>
    public void HideAllPanels() {
        panelLayer.HideAll();
    }

    /// <summary>关闭全部 Window。</summary>
    public void CloseAllWindows() {
        windowLayer.HideAll();
    }

    /// <summary>该 Id 是否已注册到 Panel 或 Window 层。</summary>
    /// <param name="screenId">界面 Id</param>
    public bool IsScreenRegistered(string screenId) {
        if (windowLayer != null && windowLayer.IsScreenRegistered(screenId)) {
            return true;
        }

        if (panelLayer != null && panelLayer.IsScreenRegistered(screenId)) {
            return true;
        }

        return false;
    }

    /// <summary>该 Id 是否已注册，并返回是 Window 还是 Panel。</summary>
    /// <param name="screenId">界面 Id</param>
    /// <param name="type">界面类型</param>
    public bool IsScreenRegistered(string screenId, out Type type) {
        if (windowLayer != null && windowLayer.IsScreenRegistered(screenId)) {
            type = typeof(IWindowController);
            return true;
        }

        if (panelLayer != null && panelLayer.IsScreenRegistered(screenId)) {
            type = typeof(IPanelController);
            return true;
        }

        type = null;
        return false;
    }
}
