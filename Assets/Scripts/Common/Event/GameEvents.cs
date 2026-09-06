namespace AChen.Events
{
    /// <summary>项目内可发布事件的统一目录。业务只在这里声明 <see cref="EventId"/>，不要在调用处写事件字符串。</summary>
    public static class GameEvent
    {
        /// <summary>示例：得分变化，参数为最新分数。</summary>
        public static readonly EventId<int> ScoreChanged = new EventId<int>("Demo.ScoreChanged");
    }
}
