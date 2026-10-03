using UnityEngine;
namespace GamePlay.Core
{
    public class Tree : Item
    {
        public override string Name => nameof(Tree);

        public override int Id => 1;

        public override int State { 
            get => (int)_state;
            set => _state=(ETreeState)value;   
        }
        private ETreeState _state;
        public Tree()
        {

        }

        public override Item CopyItem()
        {
            var ret=new Tree();
            ret.State=this.State;
            return ret;
        }
    }

}
