namespace GamePlay.Core
{
    /// <summary>
    /// 蕨苗的物品数据，表示可携带和种下的蕨类幼苗，不包含种植或场景表现逻辑。
    /// 与 Item_Fern 共用蕨类物品编号，但使用独立类型，便于区分背包蕨苗和场景蕨类。
    /// </summary>
    public class Item_FernSeed : Item
    {
        public override string Name => nameof(Item_FernSeed);

        public override int Id => 5;

        public override int State { get; set; } = 0;

        public override Item CopyItem()
        {
            return new Item_FernSeed
            {
                State = State
            };
        }
    }
}
