namespace GamePlay.Core
{
    /// <summary>
    /// 铲子的物品数据，不包含移植或其他场景交互逻辑。
    /// </summary>
    public class Item_Shovel : Item
    {
        public override string Name => nameof(Item_Shovel);

        public override int Id => 9;

        public override int State { get; set; } = 0;

        public override Item CopyItem()
        {
            return new Item_Shovel
            {
                State = this.State
            };
        }
    }
}
