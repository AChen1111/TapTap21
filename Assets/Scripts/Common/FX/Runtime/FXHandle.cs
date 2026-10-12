using System;

namespace FXSystem
{
    /// <summary>
    /// 一次播放的轻量句柄。对象被回收后自动失效，不必手动置空。
    /// </summary>
    public readonly struct FXHandle : IEquatable<FXHandle>
    {
        public static readonly FXHandle Invalid = default;

        internal readonly int Slot;
        internal readonly int Generation;

        internal FXHandle(int slot, int generation)
        {
            Slot = slot;
            Generation = generation;
        }

        public bool IsValid => FXManager.IsHandleValid(this);

        public void Stop(bool immediate = false)
        {
            FX.Stop(this, immediate);
        }

        /// <summary>
        /// 本次播放结束时回调（含 Stop / 回收 / 销毁）。若句柄已失效，会立刻以 Recycled 回调。
        /// </summary>
        public void OnComplete(FXCompleteCallback callback)
        {
            if (callback == null)
                return;
            if (FXManager.Instance != null)
                FXManager.Instance.Subscribe(in this, callback);
            else
                callback(this, FXCompleteReason.Recycled);
        }

        /// <summary>
        /// 仅在自然播完时回调。
        /// </summary>
        public void OnFinished(Action callback)
        {
            if (callback == null)
                return;
            OnComplete((_, reason) =>
            {
                if (reason == FXCompleteReason.Finished)
                    callback();
            });
        }

        public bool Equals(FXHandle other) => Slot == other.Slot && Generation == other.Generation;

        public override bool Equals(object obj) => obj is FXHandle other && Equals(other);

        public override int GetHashCode() => (Slot * 397) ^ Generation;

        public static bool operator ==(FXHandle a, FXHandle b) => a.Equals(b);

        public static bool operator !=(FXHandle a, FXHandle b) => !a.Equals(b);
    }
}
