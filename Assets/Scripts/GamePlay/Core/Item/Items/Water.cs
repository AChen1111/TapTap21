using UnityEngine;
namespace GamePlay.Core
{
    public class Water : Item
    {

        public override string Name => nameof(Water);

        public override int Id => 0;

        public override int State { get ; set ; }

        public Water()
        {
            State=0;
        }

        public override Item CopyItem()
        {
            return new Water();
        }
    }

}
