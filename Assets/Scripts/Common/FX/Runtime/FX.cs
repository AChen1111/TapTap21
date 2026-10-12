using System;
using UnityEngine;

namespace FXSystem
{
    /// <summary>
    /// 项目内调用入口。无需自己找 Manager。
    /// </summary>
    public static class FX
    {
        public static FXHandle Play(string id, Vector3 position)
        {
            return Play(id, FXPlayParams.At(position));
        }

        public static FXHandle Play(string id, Vector3 position, Quaternion rotation)
        {
            var p = FXPlayParams.At(position);
            p.Rotation = rotation;
            return Play(id, in p);
        }

        public static FXHandle Play(string id, Vector3 position, Vector2 facing)
        {
            var p = FXPlayParams.At(position);
            p.Rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg);
            return Play(id, in p);
        }

        public static FXHandle Play(string id, Vector3 position, Action onComplete)
        {
            var p = FXPlayParams.At(position);
            p.OnComplete = Wrap(onComplete);
            return Play(id, in p);
        }

        public static FXHandle Play(string id, Vector3 position, FXCompleteCallback onComplete)
        {
            var p = FXPlayParams.At(position);
            p.OnComplete = onComplete;
            return Play(id, in p);
        }

        public static FXHandle Play(string id, in FXPlayParams playParams)
        {
            var manager = FXManager.Instance;
            return manager != null ? manager.Play(id, in playParams) : FXHandle.Invalid;
        }

        public static FXHandle Play(FXDefinition definition, Vector3 position)
        {
            return Play(definition, FXPlayParams.At(position));
        }

        public static FXHandle Play(FXDefinition definition, Vector3 position, Action onComplete)
        {
            var p = FXPlayParams.At(position);
            p.OnComplete = Wrap(onComplete);
            return Play(definition, in p);
        }

        public static FXHandle Play(FXDefinition definition, Vector3 position, FXCompleteCallback onComplete)
        {
            var p = FXPlayParams.At(position);
            p.OnComplete = onComplete;
            return Play(definition, in p);
        }

        public static FXHandle Play(FXDefinition definition, in FXPlayParams playParams)
        {
            var manager = FXManager.Instance;
            return manager != null ? manager.Play(definition, in playParams) : FXHandle.Invalid;
        }

        /// <summary>
        /// 作为 parent 的子物体播放。parent 销毁时该实例会被移出池（下一次会补建）。
        /// </summary>
        public static FXHandle PlayAttached(string id, Transform parent, Vector3 localOffset = default)
        {
            return PlayAttached(id, parent, localOffset, followOnly: false);
        }

        public static FXHandle PlayAttached(FXDefinition definition, Transform parent, Vector3 localOffset = default)
        {
            return PlayAttached(definition, parent, localOffset, followOnly: false);
        }

        /// <summary>
        /// 不改变父节点，每帧跟随 parent。parent 销毁后自动回收，池不会被拆掉。
        /// </summary>
        public static FXHandle PlayFollow(string id, Transform parent, Vector3 localOffset = default)
        {
            return PlayAttached(id, parent, localOffset, followOnly: true);
        }

        public static FXHandle PlayFollow(FXDefinition definition, Transform parent, Vector3 localOffset = default)
        {
            return PlayAttached(definition, parent, localOffset, followOnly: true);
        }

        public static void Stop(FXHandle handle, bool immediate = false)
        {
            if (!FXManager.HasInstance)
                return;
            FXManager.Instance.Stop(in handle, immediate);
        }

        public static void StopAll(bool immediate = true)
        {
            if (!FXManager.HasInstance)
                return;
            FXManager.Instance.StopAll(immediate);
        }

        public static void Prewarm(string id)
        {
            FXManager.Instance?.Prewarm(id);
        }

        public static void Prewarm(FXDefinition definition)
        {
            FXManager.Instance?.Prewarm(definition);
        }

        public static void PrewarmAll()
        {
            FXManager.Instance?.PrewarmAll();
        }

        public static bool IsValid(FXHandle handle) => handle.IsValid;

        /// <summary>
        /// 屏幕像素坐标播放（左下为原点，与 Input.mousePosition 一致）。
        /// </summary>
        public static FXHandle PlayUI(string id, Vector2 screenPosition)
        {
            var p = FXPlayParams.Default;
            p.IsUI = true;
            p.UseScreenPosition = true;
            p.ScreenPosition = screenPosition;
            return Play(id, in p);
        }

        public static FXHandle PlayUI(string id, Vector2 screenPosition, Action onComplete)
        {
            var p = FXPlayParams.Default;
            p.IsUI = true;
            p.UseScreenPosition = true;
            p.ScreenPosition = screenPosition;
            p.OnComplete = Wrap(onComplete);
            return Play(id, in p);
        }

        public static FXHandle PlayUI(string id, Vector2 screenPosition, FXCompleteCallback onComplete)
        {
            var p = FXPlayParams.Default;
            p.IsUI = true;
            p.UseScreenPosition = true;
            p.ScreenPosition = screenPosition;
            p.OnComplete = onComplete;
            return Play(id, in p);
        }

        public static FXHandle PlayUI(FXDefinition definition, Vector2 screenPosition)
        {
            var p = FXPlayParams.Default;
            p.IsUI = true;
            p.UseScreenPosition = true;
            p.ScreenPosition = screenPosition;
            return Play(definition, in p);
        }

        /// <summary>
        /// 把世界坐标投到 Canvas 上播放。
        /// </summary>
        public static FXHandle PlayUIAtWorld(string id, Vector3 worldPosition, Camera worldCamera = null)
        {
            var p = FXPlayParams.At(worldPosition);
            p.IsUI = true;
            p.WorldCamera = worldCamera;
            return Play(id, in p);
        }

        public static FXHandle PlayUIAtWorld(FXDefinition definition, Vector3 worldPosition, Camera worldCamera = null)
        {
            var p = FXPlayParams.At(worldPosition);
            p.IsUI = true;
            p.WorldCamera = worldCamera;
            return Play(definition, in p);
        }

        /// <summary>
        /// 在 UI 元素中心播放，不改变父节点。
        /// </summary>
        public static FXHandle PlayUI(string id, RectTransform target, Vector3 localOffset = default)
        {
            return PlayUIFollow(id, target, localOffset);
        }

        public static FXHandle PlayUI(FXDefinition definition, RectTransform target, Vector3 localOffset = default)
        {
            return PlayUIFollow(definition, target, localOffset);
        }

        public static FXHandle PlayUIAttached(string id, RectTransform parent, Vector3 localOffset = default)
        {
            return PlayUIParented(id, parent, localOffset, followOnly: false);
        }

        public static FXHandle PlayUIAttached(FXDefinition definition, RectTransform parent, Vector3 localOffset = default)
        {
            return PlayUIParented(definition, parent, localOffset, followOnly: false);
        }

        public static FXHandle PlayUIFollow(string id, RectTransform parent, Vector3 localOffset = default)
        {
            return PlayUIParented(id, parent, localOffset, followOnly: true);
        }

        public static FXHandle PlayUIFollow(FXDefinition definition, RectTransform parent, Vector3 localOffset = default)
        {
            return PlayUIParented(definition, parent, localOffset, followOnly: true);
        }

        public static void StopAllUI(bool immediate = true)
        {
            FXManager.Instance?.StopAll(FXSpace.UI, immediate);
        }

        public static void BindCanvas(Canvas canvas)
        {
            FXManager.Instance?.BindCanvas(canvas);
        }

        public static void Register(FXDefinition definition, bool prewarm = true)
        {
            FXManager.Instance?.RegisterDefinition(definition, prewarm);
        }

        static FXHandle PlayUIParented(string id, RectTransform parent, Vector3 localOffset, bool followOnly)
        {
            var manager = FXManager.Instance;
            if (manager == null || !manager.TryGetDefinition(id, out var def))
                return FXHandle.Invalid;
            return PlayUIParented(def, parent, localOffset, followOnly);
        }

        static FXHandle PlayUIParented(FXDefinition definition, RectTransform parent, Vector3 localOffset, bool followOnly)
        {
            var manager = FXManager.Instance;
            if (manager == null)
                return FXHandle.Invalid;

            var p = FXPlayParams.Default;
            p.IsUI = true;
            p.Parent = parent;
            p.Follow = followOnly;
            p.WorldPositionStays = true;
            p.Position = parent != null ? parent.TransformPoint(localOffset) : localOffset;
            p.Rotation = Quaternion.identity;
            return manager.Play(definition, in p);
        }

        static FXHandle PlayAttached(string id, Transform parent, Vector3 localOffset, bool followOnly)
        {
            var manager = FXManager.Instance;
            if (manager == null || !manager.TryGetDefinition(id, out var def))
                return FXHandle.Invalid;
            return PlayAttached(def, parent, localOffset, followOnly);
        }

        static FXHandle PlayAttached(FXDefinition definition, Transform parent, Vector3 localOffset, bool followOnly)
        {
            var manager = FXManager.Instance;
            if (manager == null)
                return FXHandle.Invalid;

            var p = FXPlayParams.Default;
            p.Parent = parent;
            p.Follow = followOnly;
            p.WorldPositionStays = true;
            p.Position = parent != null ? parent.TransformPoint(localOffset) : localOffset;
            p.Rotation = Quaternion.identity;
            return manager.Play(definition, in p);
        }

        static FXCompleteCallback Wrap(Action onComplete)
        {
            if (onComplete == null)
                return null;
            return (_, __) => onComplete();
        }
    }
}
