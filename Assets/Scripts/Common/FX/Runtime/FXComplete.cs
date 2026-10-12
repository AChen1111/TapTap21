namespace FXSystem
{
    public enum FXCompleteReason
    {
        /// <summary>粒子/动画自然播完。</summary>
        Finished = 0,

        /// <summary>业务调用了 Stop。</summary>
        Stopped = 1,

        /// <summary>溢出回收、StopAll、切场景，或跟随目标丢失。</summary>
        Recycled = 2,

        /// <summary>实例随父物体销毁。</summary>
        Destroyed = 3,

        /// <summary>池满丢弃，没有真正播出来。</summary>
        Dropped = 4
    }

    public delegate void FXCompleteCallback(FXHandle handle, FXCompleteReason reason);
}
