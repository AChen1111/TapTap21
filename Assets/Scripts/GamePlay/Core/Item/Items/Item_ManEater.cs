using UnityEngine;
namespace GamePlay.Core
{
    /// <summary>
    /// 食人花的物品数据，通过 State 区分种子和长成状态，不包含生长或攻击逻辑。
    /// </summary>
    public class Item_ManEater : Item
    {
        public override string Name => nameof(Item_ManEater);

        public override int Id => 4;

        public override int State {
            get => (int)_state;
            set => _state=(E_Item_ManEaterState)value;
        }
        private E_Item_ManEaterState _state;
        public Item_ManEater()
        {

        }

        public override Item CopyItem()
        {
            var ret=new Item_ManEater();
            ret.State=this.State;
            return ret;
        }
    }

}
