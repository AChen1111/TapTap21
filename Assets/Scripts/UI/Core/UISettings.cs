using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI 配置：Frame Prefab 以及实例化时要注册的界面。
/// </summary>

[CreateAssetMenu(fileName = "UISettings", menuName = "deVoid UI/UI Settings")]
public class UISettings : ScriptableObject
{
    [Tooltip("UI Frame Prefab")]
    [SerializeField] private UIFrame templateUIPrefab = null;
    [Tooltip("实例化 UI 时要创建并注册的界面 Prefab（Panel 和 Window）")]
    [SerializeField] private List<GameObject> screensToRegister = null;
    [Tooltip("实例化后是否自动把仍处于激活状态的界面关掉")]
    [SerializeField] private bool deactivateScreenGOs = true;

    public UIFrame TemplateUIPrefab => templateUIPrefab;
    public IReadOnlyList<GameObject> ScreensToRegister => screensToRegister;

    /// <summary>
    /// 实例化 UI Frame。默认同时实例化并注册配置里的界面。
    /// </summary>
    /// <param name="instanceAndRegisterScreens">是否实例化并注册配置中的界面</param>
    /// <returns>新的 UI Frame</returns>
    public UIFrame CreateUIInstance(bool instanceAndRegisterScreens = true) {
        var newUI = Instantiate(templateUIPrefab);

        if (instanceAndRegisterScreens) {
            foreach (var screen in screensToRegister) {
                    var screenInstance = Instantiate(screen);
                    var screenController = screenInstance.GetComponent<IUIScreenController>();

                    if (screenController != null) {
                        newUI.RegisterScreenPrefab(screen.name, screen);
                        newUI.RegisterScreen(screen.name, screenController, screenInstance.transform);
                    if (deactivateScreenGOs && screenInstance.activeSelf) {
                        screenInstance.SetActive(false);
                    }
                }
                else {
                    Debug.LogError("[UIConfig] Screen doesn't contain a ScreenController! Skipping " + screen.name);
                }
            }
        }

        return newUI;
    }
    
    private void OnValidate() {
        List<GameObject> objectsToRemove = new List<GameObject>();
        for(int i = 0; i < screensToRegister.Count; i++) {
            var screenCtl = screensToRegister[i].GetComponent<IUIScreenController>();
            if (screenCtl == null) {
                objectsToRemove.Add(screensToRegister[i]);
            }
        }

        if (objectsToRemove.Count > 0) {
            Debug.LogError("[UISettings] Some GameObjects that were added to the Screen Prefab List didn't have ScreenControllers attached to them! Removing.");
            foreach (var obj in objectsToRemove) {
                Debug.LogError("[UISettings] Removed " + obj.name + " from " + name + " as it has no Screen Controller attached!");
                screensToRegister.Remove(obj);
            }
        }
    }        
}
