using UnityEngine;
namespace GamePlay.Core
{
    public class Vine : Item
    {
        public override string Name => nameof(Vine);

        public override int Id => 7;

        public override int State {
            get => (int)_state;
            set => _state=(EVineState)value;
        }
        private EVineState _state;
        public Vine()
        {

        }

        public override Item CopyItem()
        {
            var ret=new Vine();
            ret.State=this.State;
            return ret;
        }
    }

}
