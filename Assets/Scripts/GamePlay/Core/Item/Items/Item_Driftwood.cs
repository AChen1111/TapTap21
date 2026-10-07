namespace GamePlay.Core
{
    /// <summary>
    /// 浮木的物品数据，不包含漂移或承载玩家的场景逻辑。
    /// </summary>
    public class Item_Driftwood : Item
    {
        public override string Name => nameof(Item_Driftwood);

        public override int Id => 12;

        public override int State { get; set; } = 0;

        public override Item CopyItem()
        {
            return new Item_Driftwood
            {
                State = this.State
            };
        }
    }
}
