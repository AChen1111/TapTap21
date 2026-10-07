using UnityEngine;
namespace GamePlay.Core
{
    /// <summary>
    /// 蕨类植物的物品数据，通过 State 区分不同高度，不包含生长或场景表现逻辑。
    /// </summary>
    public class Item_Fern : Item
    {
        public override string Name => nameof(Item_Fern);

        public override int Id => 5;

        public override int State {
            get => (int)_state;
            set => _state=(E_Item_FernState)value;
        }
        private E_Item_FernState _state;
        public Item_Fern()
        {

        }

        public override Item CopyItem()
        {
            var ret=new Item_Fern();
            ret.State=this.State;
            return ret;
        }
    }

}
