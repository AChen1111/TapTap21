namespace FXSystem
{
    /// <summary>
    /// 某类特效达到 MaxActive 时的处理策略。
    /// </summary>
    public enum FXOverflowPolicy
    {
        /// <summary>回收最早播放的一个，再播新的。</summary>
        RecycleOldest = 0,

        /// <summary>允许临时超出池上限（仍受全局硬顶限制）。</summary>
        Expand = 1,

        /// <summary>丢弃本次播放，返回无效句柄。</summary>
        Drop = 2
    }
}
