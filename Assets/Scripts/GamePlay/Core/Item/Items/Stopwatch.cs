using UnityEngine;
namespace GamePlay.Core
{
    public class Stopwatch : Item
    {

        public override string Name => nameof(Stopwatch);

        public override int Id => 8;

        public override int State { get ; set ; }

        public Stopwatch()
        {
            State=0;
        }

        public override Item CopyItem()
        {
            return new Stopwatch();
        }
    }

}
