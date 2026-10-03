namespace GamePlay.Core
{
    public abstract class Item
    {
        public abstract string Name{get;}
        public abstract int Id{get;}
        public abstract int State{get;set;}
        public abstract Item CopyItem();
    }

}
