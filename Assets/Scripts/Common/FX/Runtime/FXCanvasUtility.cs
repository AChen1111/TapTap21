using UnityEngine;
using UnityEngine.UI;

namespace FXSystem
{
    /// <summary>
    /// 标记 FX 专用 Canvas，切场景后仍可找回。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class FXCanvasHost : MonoBehaviour
    {
        public Canvas Canvas;
        public RectTransform Content;
    }

    static class FXCanvasUtility
    {
        public static void Stretch(RectTransform rect)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.localScale = Vector3.one;
            rect.localRotation = Quaternion.identity;
        }

        public static Camera ResolveEventCamera(Canvas canvas)
        {
            if (canvas == null)
                return Camera.main;
            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                return null;
            return canvas.worldCamera != null ? canvas.worldCamera : Camera.main;
        }

        public static Camera ResolveWorldCamera(Camera explicitCamera)
        {
            if (explicitCamera != null)
                return explicitCamera;
            return Camera.main;
        }

        public static bool TryScreenToCanvasWorld(Canvas canvas, Vector2 screen, out Vector3 world)
        {
            world = default;
            if (canvas == null)
                return false;

            var rect = canvas.transform as RectTransform;
            if (rect == null)
                return false;

            return RectTransformUtility.ScreenPointToWorldPointInRectangle(
                rect,
                screen,
                ResolveEventCamera(canvas),
                out world);
        }

        public static bool TryWorldToCanvasWorld(Canvas canvas, Vector3 worldPoint, Camera worldCamera, out Vector3 canvasWorld)
        {
            canvasWorld = default;
            var cam = ResolveWorldCamera(worldCamera);
            if (cam == null)
                return TryScreenToCanvasWorld(canvas, worldPoint, out canvasWorld);

            Vector3 screen = cam.WorldToScreenPoint(worldPoint);
            if (screen.z < 0f)
                return false;
            return TryScreenToCanvasWorld(canvas, screen, out canvasWorld);
        }

        public static Canvas CreateDefaultCanvas(FXSettings settings, Transform parent)
        {
            var go = new GameObject("[FXCanvas]", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(FXCanvasHost));
            if (parent != null)
                go.transform.SetParent(parent, false);
            else
                Object.DontDestroyOnLoad(go);

            int uiLayer = LayerMask.NameToLayer("UI");
            if (uiLayer >= 0)
                go.layer = uiLayer;

            var canvas = go.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.overrideSorting = true;
            canvas.sortingOrder = settings != null ? settings.UICanvasSortingOrder : 200;
            canvas.planeDistance = settings != null ? settings.UIPlaneDistance : 10f;
            BindCamera(canvas);

            var scaler = go.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = settings != null ? settings.UIReferenceResolution : new Vector2(1920f, 1080f);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            var contentGo = new GameObject("FXUIRoot", typeof(RectTransform));
            contentGo.layer = go.layer;
            var content = contentGo.GetComponent<RectTransform>();
            content.SetParent(go.transform, false);
            Stretch(content);

            var host = go.GetComponent<FXCanvasHost>();
            host.Canvas = canvas;
            host.Content = content;
            return canvas;
        }

        public static void BindCamera(Canvas canvas)
        {
            if (canvas == null || canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                return;

            var cam = Camera.main;
            if (cam == null)
                cam = Object.FindFirstObjectByType<Camera>();
            canvas.worldCamera = cam;
        }
    }
}
