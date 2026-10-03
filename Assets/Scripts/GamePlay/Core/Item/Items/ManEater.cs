using UnityEngine;
namespace GamePlay.Core
{
    public class ManEater : Item
    {
        public override string Name => nameof(ManEater);

        public override int Id => 4;

        public override int State {
            get => (int)_state;
            set => _state=(EManEaterState)value;
        }
        private EManEaterState _state;
        public ManEater()
        {

        }

        public override Item CopyItem()
        {
            var ret=new ManEater();
            ret.State=this.State;
            return ret;
        }
    }

}
