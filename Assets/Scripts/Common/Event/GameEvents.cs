using GamePlay.Gravity;

namespace AChen.Events
{
    /// <summary>项目内可发布事件的统一目录。业务只在这里声明 <see cref="EventId"/>，不要在调用处写事件字符串。</summary>
    public static class GameEvent
    {
        /// <summary>示例：得分变化，参数为最新分数。</summary>
        public static readonly EventId<int> ScoreChanged = new EventId<int>("Demo.ScoreChanged");
        /// <summary>调用LoadSceneWithPic时触发,加载场景时显示一张神图 </summary>
        public static readonly EventId<float> StartShowShenTu=new EventId<float>($"SceneLoader.{nameof(StartShowShenTu)}");

        /// <summary>场景重力变化时触发，参数为新重力乘积因子与当前重力方向</summary>
        public static readonly EventId<float, EGravityDirection> GravityChanged = new EventId<float, EGravityDirection>("GravityService.GravityChanged");
        /// <summary>仅当场景重力翻转时触发，参数为当前重力方向</summary>
        public static readonly EventId<EGravityDirection> GravityFlipped = new EventId<EGravityDirection>("GravityService.GravityFlipped");
    }
}
