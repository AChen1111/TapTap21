using UnityEngine;
namespace GamePlay.Core
{
    /// <summary>
    /// 荆棘的物品数据，通过 State 区分失水和正常状态，不包含收缩或碰撞变化逻辑。
    /// </summary>
    public class Item_Thorn : Item
    {
        public override string Name => nameof(Item_Thorn);

        public override int Id => 6;

        public override int State {
            get => (int)_state;
            set => _state=(E_Item_ThornState)value;
        }
        private E_Item_ThornState _state;
        public Item_Thorn()
        {

        }

        public override Item CopyItem()
        {
            var ret=new Item_Thorn();
            ret.State=this.State;
            return ret;
        }
    }

}
