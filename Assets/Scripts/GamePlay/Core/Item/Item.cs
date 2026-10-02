namespace GamePlay.Core
{
    public abstract class Item
    {
        public abstract int State{get;set;}
        public abstract Item CopyItem();
    }

}
