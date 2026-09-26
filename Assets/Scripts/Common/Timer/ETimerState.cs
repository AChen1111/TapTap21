namespace Common.Timer
{
    public enum ETimerState
    {
        /// <summary>
        /// 准备好启动
        /// </summary>
        Ready,
        /// <summary>
        /// 计时中
        /// </summary>
        Started,
        /// <summary>
        /// 暂停中
        /// </summary>
        Paused,
        None
    }
}