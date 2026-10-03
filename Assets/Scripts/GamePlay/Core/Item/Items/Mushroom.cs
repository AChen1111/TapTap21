using UnityEngine;
namespace GamePlay.Core
{
    public class Mushroom : Item
    {

        public override string Name => nameof(Mushroom);

        public override int Id => 3;

        public override int State { get ; set ; }

        public Mushroom()
        {
            State=0;
        }

        public override Item CopyItem()
        {
            return new Mushroom();
        }
    }

}
