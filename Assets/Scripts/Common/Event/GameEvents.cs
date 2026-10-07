using GamePlay.Gravity;
using GamePlay.Inventory;
using UI.GamePlay.HUD;
using UnityEngine;

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
        /// <summary> 给InventoryController绑定model时触发 </summary>
        public static readonly EventId<InventoryModel> ModelBinded=new($"Inventory.{nameof(ModelBinded)}");
        /// <summary>玩家鼠标开始拖拽物品栏格子</summary>
        public static readonly EventId<GameObject> SlotBeginDragged=new($"Inventory.{nameof(SlotBeginDragged)}");
        /// <summary>玩家鼠标停止拖拽物品栏格子</summary>
        public static readonly EventId<GameObject> SlotEndDragged=new($"Inventory.{nameof(SlotEndDragged)}");
        /// <summary>背包拖拽结束时的完整数据，供场景放置桥接器使用</summary>
        public static readonly EventId<InventoryDragContext> InventoryDragEnded =
            new($"Inventory.{nameof(InventoryDragEnded)}");
        /// <summary> 玩家按下交互键</summary>
        public static readonly EventId OnInteractKeyPressed=new("Input.InteractKeyPressed");
        /// <summary> 玩家在花篮停靠点按下按键召回花篮 </summary>
        public static readonly EventId<Vector3> CallFlowerBasket=new($"GamePlay.Scene.{CallFlowerBasket}");
    
    }
}
