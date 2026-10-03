using UnityEngine;
namespace GamePlay.Core
{
    public class Thorn : Item
    {
        public override string Name => nameof(Thorn);

        public override int Id => 6;

        public override int State {
            get => (int)_state;
            set => _state=(EThornState)value;
        }
        private EThornState _state;
        public Thorn()
        {

        }

        public override Item CopyItem()
        {
            var ret=new Thorn();
            ret.State=this.State;
            return ret;
        }
    }

}
