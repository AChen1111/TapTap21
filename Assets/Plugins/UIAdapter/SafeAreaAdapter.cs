using UnityEngine;
using UnityEngine.UI;

/// <summary>Map screen safe area through this object's own Canvas into parent anchors.</summary>
public class SafeAreaAdapter : AdapterBase
{
    [Header("是否每一帧都计算")]
    public bool CalculateEveryFrame = false;
    private RectTransform rect;
    private Rect lastSafeArea;
    private Rect lastParentRect;
    private Rect lastPixelRect;
    private Vector2Int lastScreenSize;

    // Compatibility with the original API; a shared CanvasScaler is no longer needed.
    public static void Init(CanvasScaler scaler) { }

    private void Update()
    {
        var parent = transform.parent as RectTransform;
        var canvas = GetComponentInParent<Canvas>();
        if (parent == null || canvas == null) return;
        if (CalculateEveryFrame || lastSafeArea != Screen.safeArea || lastParentRect != parent.rect ||
            lastPixelRect != canvas.rootCanvas.pixelRect ||
            lastScreenSize != new Vector2Int(Screen.width, Screen.height)) Adapt();
    }

    public override void Adapt()
    {
        if (rect == null) rect = GetComponent<RectTransform>();
        var parent = transform.parent as RectTransform;
        var canvas = GetComponentInParent<Canvas>();
        if (rect == null || parent == null || canvas == null || Screen.width <= 0 || Screen.height <= 0) return;
        canvas = canvas.rootCanvas;
        if (canvas.renderMode == RenderMode.WorldSpace || parent.rect.width <= 0 || parent.rect.height <= 0) return;
        var camera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera && camera == null) return;

        Rect safeArea = Screen.safeArea;
        Rect viewport = canvas.pixelRect;
        var min = Vector2.Max(safeArea.min, viewport.min);
        var max = Vector2.Min(safeArea.max, viewport.max);
        if (min.x >= max.x || min.y >= max.y) return;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, min, camera, out var localMin) ||
            !RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, max, camera, out var localMax)) return;
        var parentBounds = parent.rect;
        var anchorMin = new Vector2(
            Mathf.Clamp01((localMin.x - parentBounds.xMin) / parentBounds.width),
            Mathf.Clamp01((localMin.y - parentBounds.yMin) / parentBounds.height));
        var anchorMax = new Vector2(
            Mathf.Clamp01((localMax.x - parentBounds.xMin) / parentBounds.width),
            Mathf.Clamp01((localMax.y - parentBounds.yMin) / parentBounds.height));
        if (anchorMin.x > anchorMax.x || anchorMin.y > anchorMax.y) return;
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        lastSafeArea = safeArea;
        lastParentRect = parentBounds;
        lastPixelRect = viewport;
        lastScreenSize = new Vector2Int(Screen.width, Screen.height);
    }
}
