using System;
using System.Collections.Generic;
using UnityEngine;

namespace AChen.UI
{
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

    /// <summary>获取该 UIFrame 的主 Canvas。需要坐标转换或 Canvas 配置时使用。</summary>
    public Canvas MainCanvas {
        get {
            if (mainCanvas == null) {
                mainCanvas = GetComponent<Canvas>();
            }

            return mainCanvas;
        }
    }

    /// <summary>获取主 Canvas 使用的 UI 相机。进行屏幕与 UI 坐标转换时使用。</summary>
    public Camera UICamera {
        get { return MainCanvas.worldCamera; }
    }

    private void Awake() {
        if (initializeOnAwake) {
            Initialize();
        }
    }

    /// <summary>初始化 Panel / Window 两层。关闭自动初始化时由启动流程调用一次。</summary>
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

    /// <summary>直接显示或隐藏 DarkenBG 遮挡层。通常由弹窗层自动维护，仅特殊流程手动调用。</summary>
    /// <param name="visible">是否显示遮挡层。</param>
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

    /// <summary>登记可延迟实例化的界面 Prefab。运行时动态增加界面类型时调用。</summary>
    /// <param name="screenId">界面的唯一 Id，通常与 Prefab 名一致。</param>
    /// <param name="prefab">根节点带有 <see cref="IUIScreenController"/> 的 Prefab。</param>
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

    /// <summary>按 Id 显示无参数 Panel。HUD、提示条等非栈式界面使用。</summary>
    /// <param name="screenId">Panel Id。</param>
    public void ShowPanel(string screenId) {
        if (!EnsureScreen(screenId)) {
            return;
        }
        panelLayer.ShowScreenById(screenId);
    }

    /// <summary>按 Id 显示 Panel，并传入本次显示所需的强类型数据。</summary>
    /// <typeparam name="TProperties">实现 <see cref="IPanelProperties"/> 的参数类型。</typeparam>
    /// <param name="screenId">Panel Id。</param>
    /// <param name="properties">Panel 本次显示的数据。</param>
    public void ShowPanel<TProperties>(string screenId, TProperties properties)
        where TProperties : IPanelProperties {
        if (!EnsureScreen(screenId)) {
            return;
        }
        panelLayer.ShowScreenById(screenId, properties);
    }

    /// <summary>按 Id 暂时隐藏 Panel，并触发 <c>OnHide</c>；再次显示会触发 <c>OnResume</c>。</summary>
    /// <param name="screenId">Panel Id。</param>
    public void HidePanel(string screenId) {
        panelLayer.HideScreenById(screenId);
    }

    /// <summary>按 Id 打开无参数 Window。需要历史栈或排队行为的界面使用。</summary>
    /// <param name="screenId">Window Id。</param>
    public void OpenWindow(string screenId) {
        if (!EnsureScreen(screenId)) {
            return;
        }
        windowLayer.ShowScreenById(screenId);
    }

    /// <summary>按 Id 打开 Window，并传入本次打开所需的强类型数据。</summary>
    /// <typeparam name="TProperties">实现 <see cref="IWindowProperties"/> 的参数类型。</typeparam>
    /// <param name="screenId">Window Id。</param>
    /// <param name="properties">Window 本次打开的数据。</param>
    public void OpenWindow<TProperties>(string screenId, TProperties properties)
        where TProperties : IWindowProperties {
        if (!EnsureScreen(screenId)) {
            return;
        }
        windowLayer.ShowScreenById(screenId, properties);
    }

    /// <summary>按 Id 将 Window 从历史栈关闭，并触发 <c>OnClose</c>。</summary>
    /// <param name="screenId">Window Id。</param>
    public void CloseWindow(string screenId) {
        windowLayer.HideScreenById(screenId);
    }

    /// <summary>关闭当前最前方的 Window。通用返回按钮或系统返回键使用。</summary>
    public void CloseCurrentWindow() {
        if (windowLayer.CurrentWindow != null) {
            CloseWindow(windowLayer.CurrentWindow.ScreenId);
        }
    }

    /// <summary>不知道界面属于 Panel 还是 Window 时，按已注册类型自动显示。</summary>
    /// <param name="screenId">界面 Id。</param>
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

    /// <summary>注册已存在的界面实例，并可将其挂到对应 UI 层。动态创建界面实例时使用。</summary>
    /// <param name="screenId">界面 Id。</param>
    /// <param name="controller">实例上的界面控制器。</param>
    /// <param name="screenTransform">实例根节点；不为空时自动调整父节点。</param>
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

    /// <summary>注册已由调用方管理层级的 Panel 控制器。</summary>
    /// <param name="screenId">Panel Id</param>
    /// <param name="controller">控制器</param>
    /// <typeparam name="TPanel">控制器类型</typeparam>
    public void RegisterPanel<TPanel>(string screenId, TPanel controller) where TPanel : IPanelController {
        panelLayer.RegisterScreen(screenId, controller);
    }

    /// <summary>取消注册 Panel。调用方自行销毁或移除动态 Panel 前使用。</summary>
    /// <param name="screenId">Panel Id</param>
    /// <param name="controller">控制器</param>
    /// <typeparam name="TPanel">控制器类型</typeparam>
    public void UnregisterPanel<TPanel>(string screenId, TPanel controller) where TPanel : IPanelController {
        panelLayer.UnregisterScreen(screenId, controller);
    }

    /// <summary>注册已由调用方管理层级的 Window 控制器。</summary>
    /// <param name="screenId">Window Id</param>
    /// <param name="controller">控制器</param>
    /// <typeparam name="TWindow">控制器类型</typeparam>
    public void RegisterWindow<TWindow>(string screenId, TWindow controller) where TWindow : IWindowController {
        windowLayer.RegisterScreen(screenId, controller);
    }

    /// <summary>取消注册 Window。调用方自行销毁或移除动态 Window 前使用。</summary>
    /// <param name="screenId">Window Id</param>
    /// <param name="controller">控制器</param>
    /// <typeparam name="TWindow">控制器类型</typeparam>
    public void UnregisterWindow<TWindow>(string screenId, TWindow controller) where TWindow : IWindowController {
        windowLayer.UnregisterScreen(screenId, controller);
    }

    /// <summary>检查指定 Panel 当前是否可见。</summary>
    /// <param name="panelId">Panel Id。</param>
    /// <returns>Panel 已注册且当前可见时为 <see langword="true"/>。</returns>
    public bool IsPanelOpen(string panelId) {
        return panelLayer.IsPanelVisible(panelId);
    }

    /// <summary>关闭全部 Window 并隐藏全部 Panel。切换游戏大状态或登出时使用。</summary>
    public void HideAll() {
        CloseAllWindows();
        HideAllPanels();
    }

    /// <summary>隐藏全部 Panel，但不关闭 Window。</summary>
    public void HideAllPanels() {
        panelLayer.HideAll();
    }

    /// <summary>关闭全部 Window，但不隐藏 Panel。</summary>
    public void CloseAllWindows() {
        windowLayer.HideAll();
    }

    /// <summary>检查界面 Id 是否已注册到 Panel 或 Window 层。</summary>
    /// <param name="screenId">界面 Id。</param>
    /// <returns>已注册时为 <see langword="true"/>。</returns>
    public bool IsScreenRegistered(string screenId) {
        if (windowLayer != null && windowLayer.IsScreenRegistered(screenId)) {
            return true;
        }

        if (panelLayer != null && panelLayer.IsScreenRegistered(screenId)) {
            return true;
        }

        return false;
    }

    /// <summary>检查界面 Id 是否已注册，并返回其所属的 Window 或 Panel 接口类型。</summary>
    /// <param name="screenId">界面 Id。</param>
    /// <param name="type">成功时为 <see cref="IWindowController"/> 或 <see cref="IPanelController"/>。</param>
    /// <returns>已注册时为 <see langword="true"/>。</returns>
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
}
