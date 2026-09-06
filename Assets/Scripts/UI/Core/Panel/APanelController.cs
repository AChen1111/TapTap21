using UnityEngine;

namespace AChen.UI
{
/// <summary>
/// Panel 基类。
/// </summary>
public abstract class APanelController : AUIScreenController, IPanelController {
    [SerializeField]
    [Tooltip("按优先级挂到不同 para-layer。可在 Panel Layer 上配置")]
    PanelPriority priority;

    /// <summary>该 Panel 应挂载到的显示层优先级。</summary>
    public PanelPriority Priority {
        get { return priority; }
    }
}

public abstract class APanelController<TProperties> : APanelController
    where TProperties : IPanelProperties
{
    /// <summary>最近一次显示时传入的强类型 Panel 参数。</summary>
    protected new TProperties Properties => (TProperties)base.Properties;

    protected override void SetProperties(IScreenProperties properties)
    {
        if (properties is TProperties typedProperties)
        {
            base.SetProperties(typedProperties);
            return;
        }

        Debug.LogError($"[APanelController] Properties type {properties.GetType()} does not match {typeof(TProperties)}.");
    }
}
}
