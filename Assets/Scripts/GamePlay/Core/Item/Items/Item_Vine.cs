using UnityEngine;
namespace GamePlay.Core
{
    public class Item_Vine : Item
    {
        public override string Name => nameof(Item_Vine);

        public override int Id => 7;

        public override int State {
            get => (int)_state;
            set => _state=(E_Item_VineState)value;
        }
        private E_Item_VineState _state;
        public Item_Vine()
        {

        }

        public override Item CopyItem()
        {
            var ret=new Item_Vine();
            ret.State=this.State;
            return ret;
        }
    }

}
