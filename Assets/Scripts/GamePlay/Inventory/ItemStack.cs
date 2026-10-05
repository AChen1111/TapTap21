using AChen.Log;
using GamePlay.Core;
namespace GamePlay.Inventory{
    public class ItemStack
    {
        public Item Item{get;}
        public int Count{get;private set;}
        public ItemStack(Item item ,int Count)
        {
            if (item == null)
            {
                ALog.LogError("item不能为null!");
            }
            this.Item=item;
            this.Count=Count;
        }
        public void AddItem(int addtion=1)
        {
            Count+=addtion;
        }
        public void RemoveItem(int ct=1)
        {
            if (Count - ct < 0)
            {
                ALog.LogError("从ItemStack取出Item失败! 为负数");
            }
            Count-=ct;
        }

    }
}