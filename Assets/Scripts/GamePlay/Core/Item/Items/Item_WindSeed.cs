namespace GamePlay.Core
{
    /// <summary>
    /// 风种的物品数据，不包含风场生成或施力逻辑。
    /// </summary>
    public class Item_WindSeed : Item
    {
        public override string Name => nameof(Item_WindSeed);

        public override int Id => 11;

        public override int State { get; set; } = 0;

        public override Item CopyItem()
        {
            return new Item_WindSeed
            {
                State = this.State
            };
        }
    }
}
