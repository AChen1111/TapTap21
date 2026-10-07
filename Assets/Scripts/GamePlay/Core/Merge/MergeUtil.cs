
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AChen.Log;
namespace GamePlay.Core
{
    public static class MergeUtil
    {
        private static Dictionary<string,Item> _canMergeTable;
        static MergeUtil()
        {
            UpdateMergeTable();
        }
        public static void UpdateMergeTable()
        {
            bool isSuccess=true;
            _canMergeTable=new();
            var itemMergeTable=itemmergetable.LoadBytes();
            foreach(var mt in itemMergeTable)
            {
                try{
                    Type typeOut=Type.GetType(mt.typeOut);

                    Item item=Activator.CreateInstance(typeOut) as Item;
                    item.State=mt.stateOut;
                    string s=$"{mt.typeIn1}#{mt.stateIn1}#{mt.typeIn2}#{mt.stateIn2}#{mt.op}";
                    _canMergeTable.Add(s,item);
                }
                catch(Exception e)
                {
                    isSuccess=false;
                    ALog.LogError("Excel表导入数据错误! "+e.ToString());
                }
                
            }
            if (isSuccess)
            {
                StringBuilder sb=new("[MergeUtil] 所有合成配方已经成功导入游戏:\n");
                foreach (var msg in _canMergeTable)
                {
                    sb.AppendLine(msg.Key+" -> "+msg.Value.GetType()+"#"+msg.Value.State);
                }
                ALog.Log(sb.ToString());
            }
            
        }

        /// <summary>
        /// 根据两个输入物品和操作号查找配方，返回配方表中配置的结果物品。
        /// 交互层不知道结果具体类型时使用这个重载。
        /// </summary>
        public static Item Merge(Item item1, Item item2, int op, Action OnMergeFailed = null)
        {
            if (item1 == null || item2 == null)
            {
                OnMergeFailed?.Invoke();
                ALog.LogError("[MergeUtil] 无法合成! 输入物品不能为 null");
                return null;
            }

            string cmp1=$"{item1.GetType()}#{item1.State}#{item2.GetType()}#{item2.State}#{op}";
            string cmp2=$"{item2.GetType()}#{item2.State}#{item1.GetType()}#{item1.State}#{op}";
            if (_canMergeTable.ContainsKey(cmp1))
            {
                return _canMergeTable[cmp1].CopyItem();
            }else if (_canMergeTable.ContainsKey(cmp2))
            {
                return _canMergeTable[cmp2].CopyItem();
            }
            else
            {
                OnMergeFailed?.Invoke();
                ALog.LogError("[MergeUtil] 无法合成! 配方:" + cmp1);
                return null;
            }
        }

        /// <summary>
        /// 泛型重载用于调用方已经知道结果类型的场景。结果类型仍以配方表为准。
        /// </summary>
        public static T Merge<T>(Item item1, Item item2, int op, Action OnMergeFailed = null)
            where T : Item
        {
            return Merge(item1, item2, op, OnMergeFailed) as T;
        }
    }
}
