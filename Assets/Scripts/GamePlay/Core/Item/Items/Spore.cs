using UnityEngine;
namespace GamePlay.Core
{
    public class Spore : Item
    {

        public override string Name => nameof(Spore);

        public override int Id => 2;

        public override int State { get ; set ; }

        public Spore()
        {
            State=0;
        }

        public override Item CopyItem()
        {
            return new Spore();
        }
    }

}
