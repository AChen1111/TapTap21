namespace GamePlay.Core
{
    /// <summary>
    /// 物品数据基类，定义名称、类型编号、状态和复制接口，不包含 UI 或场景行为。
    /// </summary>
    public abstract class Item
    {
        public abstract string Name{get;}
        public abstract int Id{get;}
        public abstract int State{get;set;}
        public abstract Item CopyItem();
    }

}
