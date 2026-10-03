using UnityEngine;
namespace GamePlay.Core
{
    public class Fern : Item
    {
        public override string Name => nameof(Fern);

        public override int Id => 5;

        public override int State {
            get => (int)_state;
            set => _state=(EFernState)value;
        }
        private EFernState _state;
        public Fern()
        {

        }

        public override Item CopyItem()
        {
            var ret=new Fern();
            ret.State=this.State;
            return ret;
        }
    }

}
