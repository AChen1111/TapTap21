namespace AChen.Events
{
    /// <summary>项目内可发布事件的统一名称定义。请使用这些常量，不要在业务代码中直接写事件字符串。</summary>
    public static class GameEvent
    {
        /// <summary>注册成功并建立本地会话时触发。参数：<c>AuthUser</c>、<c>PlayerData</c>。</summary>
        public const string PlayerRegistered = "Player.Registered";

        /// <summary>账号密码验证成功并建立本地会话时触发。参数：<c>AuthUser</c>、<c>PlayerData</c>。</summary>
        public const string PlayerLoggedIn = "Player.LoggedIn";

        /// <summary>刷新 Token 成功且玩家数据已更新时触发。参数：<c>AuthUser</c>、<c>PlayerData</c>。</summary>
        public const string PlayerTokenRefreshed = "Player.TokenRefreshed";

        /// <summary>服务端下发新的玩家资料后触发。参数：<c>PlayerData</c>。</summary>
        public const string PlayerDataChanged = "Player.DataChanged";

        /// <summary>登出或本地会话被清理后触发。参数：清理前的 <c>AuthUser</c>。</summary>
        public const string PlayerLoggedOut = "Player.LoggedOut";

        /// <summary>游戏配置快照被替换后触发。参数：<c>GameConfigSnapshot</c>、<c>bool isStale</c>。</summary>
        public const string GameConfigChanged = "GameConfig.Changed";
    }
}
