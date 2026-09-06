using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Panel 挂到哪一层 para-layer。
/// </summary>
public enum PanelPriority {
    None = 0,
    Prioritary = 1,
    Tutorial = 2,
    Blocker = 3,
}

[System.Serializable] 
public class PanelPriorityLayerListEntry {
    [SerializeField] 
    [Tooltip("该优先级对应的 para-layer")]
    private PanelPriority priority;
    [SerializeField] 
    [Tooltip("挂载该优先级 Panel 的父节点")]
    private Transform targetParent;

    public Transform TargetParent {
        get { return targetParent; }
        set { targetParent = value; }
    }

    public PanelPriority Priority {
        get { return priority; }
        set { priority = value; }
    }

    public PanelPriorityLayerListEntry(PanelPriority prio, Transform parent) {
        priority = prio;
        targetParent = parent;
    }
}

[System.Serializable] 
public class PanelPriorityLayerList {
    [SerializeField] 
    [Tooltip("按 Priority 查找父节点。绘制顺序由这些节点在 Hierarchy 中的先后决定")]
    private List<PanelPriorityLayerListEntry> paraLayers = null;

    private Dictionary<PanelPriority, Transform> lookup;

    public Dictionary<PanelPriority, Transform> ParaLayerLookup {
        get {
            if (lookup == null || lookup.Count == 0) {
                CacheLookup();
            }

            return lookup;
        }
    }

    private void CacheLookup() {
        lookup = new Dictionary<PanelPriority, Transform>();
        for (int i = 0; i < paraLayers.Count; i++) {
            lookup.Add(paraLayers[i].Priority, paraLayers[i].TargetParent);
        }
    }

    public PanelPriorityLayerList(List<PanelPriorityLayerListEntry> entries) {
        paraLayers = entries;
    }
}
