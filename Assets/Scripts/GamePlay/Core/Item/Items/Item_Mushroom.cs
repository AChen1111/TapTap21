using UnityEngine;
namespace GamePlay.Core
{
    /// <summary>
    /// 蘑菇的物品数据，不包含光照响应或场景表现逻辑。
    /// </summary>
    public class Item_Mushroom : Item
    {

        public override string Name => nameof(Item_Mushroom);

        public override int Id => 3;

        public override int State { get ; set ; }

        public Item_Mushroom()
        {
            State=0;
        }

        public override Item CopyItem()
        {
            return new Item_Mushroom();
        }
    }

}
