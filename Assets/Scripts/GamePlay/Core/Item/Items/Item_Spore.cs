using UnityEngine;
namespace GamePlay.Core
{
    public class Item_Spore : Item
    {

        public override string Name => nameof(Item_Spore);

        public override int Id => 2;

        public override int State { get ; set ; }

        public Item_Spore()
        {
            State=0;
        }

        public override Item CopyItem()
        {
            return new Item_Spore();
        }
    }

}
