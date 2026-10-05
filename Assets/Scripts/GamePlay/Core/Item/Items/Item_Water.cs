using UnityEngine;
namespace GamePlay.Core
{
    public class Item_Water : Item
    {

        public override string Name => nameof(Item_Water);

        public override int Id => 0;

        public override int State { get ; set ; }

        public Item_Water()
        {
            State=0;
        }

        public override Item CopyItem()
        {
            return new Item_Water();
        }
    }

}
