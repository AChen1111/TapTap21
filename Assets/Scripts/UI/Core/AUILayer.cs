using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// UI 层基类。打开、关闭等逻辑由子类实现。
/// </summary>
public abstract class AUILayer<TScreen> : MonoBehaviour where TScreen : IUIScreenController {
    protected Dictionary<string, TScreen> registeredScreens;

    /// <summary>显示界面。</summary>
    /// <param name="screen">要显示的控制器</param>
    public abstract void ShowScreen(TScreen screen);

    public abstract void ShowScreen<TProperties>(TScreen screen, TProperties properties)
        where TProperties : IScreenProperties;

    /// <summary>隐藏界面。</summary>
    /// <param name="screen">要隐藏的控制器</param>
    public abstract void HideScreen(TScreen screen);

    /// <summary>初始化本层。</summary>
    public virtual void Initialize() {
        registeredScreens = new Dictionary<string, TScreen>();
    }

    /// <summary>把界面挂到本层 Transform 下。</summary>
    /// <param name="controller">控制器</param>
    /// <param name="screenTransform">界面 Transform</param>
    public virtual void ReparentScreen(IUIScreenController controller, Transform screenTransform) {
        screenTransform.SetParent(transform, false);
    }

    /// <summary>按 ScreenId 注册。</summary>
    /// <param name="screenId">界面 Id</param>
    /// <param name="controller">控制器</param>
    public void RegisterScreen(string screenId, TScreen controller) {
        if (!registeredScreens.ContainsKey(screenId)) {
            ProcessScreenRegister(screenId, controller);
        }
        else {
            Debug.LogError("[AUILayerController] Screen controller already registered for id: " + screenId);
        }
    }

    /// <summary>取消注册。</summary>
    /// <param name="screenId">界面 Id</param>
    /// <param name="controller">控制器</param>
    public void UnregisterScreen(string screenId, TScreen controller) {
        if (registeredScreens.ContainsKey(screenId)) {
            ProcessScreenUnregister(screenId, controller);
        }
        else {
            Debug.LogError("[AUILayerController] Screen controller not registered for id: " + screenId);
        }
    }

    /// <summary>按 Id 查找并显示。</summary>
    /// <param name="screenId">界面 Id</param>
    public void ShowScreenById(string screenId) {
        TScreen ctl;
        if (registeredScreens.TryGetValue(screenId, out ctl)) {
            ShowScreen(ctl);
        }
        else {
            Debug.LogError("[AUILayerController] Screen ID " + screenId + " not registered to this layer!");
        }
    }

    public void ShowScreenById<TProperties>(string screenId, TProperties properties)
        where TProperties : IScreenProperties {
        TScreen ctl;
        if (registeredScreens.TryGetValue(screenId, out ctl)) {
            ShowScreen(ctl, properties);
        }
        else {
            Debug.LogError("[AUILayerController] Screen ID " + screenId + " not registered!");
        }
    }

    /// <summary>按 Id 查找并隐藏。</summary>
    /// <param name="screenId">界面 Id（默认是 Prefab 名）</param>
    public void HideScreenById(string screenId) {
        TScreen ctl;
        if (registeredScreens.TryGetValue(screenId, out ctl)) {
            HideScreen(ctl);
        }
        else {
            Debug.LogError("[AUILayerController] Could not hide Screen ID " + screenId + " as it is not registered to this layer!");
        }
    }

    /// <summary>该 Id 是否已注册到本层。</summary>
    /// <param name="screenId">界面 Id（默认是 Prefab 名）</param>
    /// <returns>已注册为 true</returns>
    public bool IsScreenRegistered(string screenId) {
        return registeredScreens.ContainsKey(screenId);
    }

    /// <summary>隐藏本层已注册的全部界面。</summary>
    public virtual void HideAll() {
        var screens = new List<TScreen>(registeredScreens.Values);
        for (int i = 0; i < screens.Count; i++) {
            if (screens[i].DestroyOnClose) {
                screens[i].Close();
            }
            else {
                screens[i].Hide();
            }
        }
    }

    protected virtual void ProcessScreenRegister(string screenId, TScreen controller) {
        controller.ScreenId = screenId;
        registeredScreens.Add(screenId, controller);
        controller.ScreenDestroyed += OnScreenDestroyed;
    }

    protected virtual void ProcessScreenUnregister(string screenId, TScreen controller) {
        controller.ScreenDestroyed -= OnScreenDestroyed;
        registeredScreens.Remove(screenId);
    }

    private void OnScreenDestroyed(IUIScreenController screen) {
        if (!string.IsNullOrEmpty(screen.ScreenId)
            && registeredScreens.ContainsKey(screen.ScreenId)) {
            UnregisterScreen(screen.ScreenId, (TScreen) screen);
        }
    }
}
