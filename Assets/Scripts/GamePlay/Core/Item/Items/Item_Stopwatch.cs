using UnityEngine;
namespace GamePlay.Core
{
    /// <summary>
    /// 昼夜切换道具（秒表）的物品数据，不包含时间切换逻辑。
    /// </summary>
    public class Item_Stopwatch : Item
    {

        public override string Name => nameof(Item_Stopwatch);

        public override int Id => 8;

        public override int State { get ; set ; }

        public Item_Stopwatch()
        {
            State=0;
        }

        public override Item CopyItem()
        {
            return new Item_Stopwatch();
        }
    }

}
