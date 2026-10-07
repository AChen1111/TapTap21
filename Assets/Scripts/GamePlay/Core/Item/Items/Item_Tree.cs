using UnityEngine;
namespace GamePlay.Core
{
    /// <summary>
    /// 树的物品数据，通过 State 区分小树苗和大树，不包含生长或场景表现逻辑。
    /// </summary>
    public class Item_Tree : Item
    {
        public override string Name => nameof(Item_Tree);

        public override int Id => 1;

        public override int State { 
            get => (int)_state;
            set => _state=(E_Item_TreeState)value;   
        }
        private E_Item_TreeState _state;
        public Item_Tree()
        {
        }

        public override Item CopyItem()
        {
            var ret=new Item_Tree();
            ret.State=this.State;
            return ret;
        }
    }

}
