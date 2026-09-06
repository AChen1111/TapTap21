using UnityEngine;

/// <summary>
/// 管理 Panel。无历史栈，可同时显示多个，例如 HUD。
/// </summary>
public class PanelUILayer : AUILayer<IPanelController> {
    [SerializeField]
    [Tooltip("按 Priority 把 Panel 挂到对应 para-layer")]
    private PanelPriorityLayerList priorityLayers = null;
    public override void ReparentScreen(IUIScreenController controller, Transform screenTransform) {
        var ctl = controller as IPanelController;
        if (ctl != null) {
            ReparentToParaLayer(ctl.Priority, screenTransform);
        }
        else {
            base.ReparentScreen(controller, screenTransform);
        }

    }
    public override void ShowScreen(IPanelController screen) {
        screen.Show();
    }

    public override void ShowScreen<TProperties>(IPanelController screen, TProperties properties) {
        screen.Show(properties);
    }

    public override void HideScreen(IPanelController screen) {
        if (screen.DestroyOnClose) {
            screen.Close();
        }
        else {
            screen.Hide();
        }
    }

    public bool IsPanelVisible(string panelId) {
        IPanelController panel;
        if (registeredScreens.TryGetValue(panelId, out panel)) {
            return panel.IsVisible;
        }

        return false;
    }

    private void ReparentToParaLayer(PanelPriority priority, Transform screenTransform) {
        Transform trans;
        if (!priorityLayers.ParaLayerLookup.TryGetValue(priority, out trans)) {
            trans = transform;
        }

        screenTransform.SetParent(trans, false);
    }
}
