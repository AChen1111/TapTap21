using UnityEngine;
namespace GamePlay.Core
{
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
