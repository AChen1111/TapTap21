using UnityEngine;

namespace GamePlay.Core
{
    public class MergeTester : MonoBehaviour
    {
        private void Start()
        {
            // 创建参与配方的水。
            Item_Water water = new Item_Water();

            // 创建树，并明确设置为树苗状态。
            Item_Tree smallTree = new Item_Tree
            {
                State = (int)E_Item_TreeState.Small
            };

            // 查询操作号为 1 的配方，预期结果类型是 Item_Tree。
            Item_Tree result = MergeUtil.Merge<Item_Tree>(
                water,
                smallTree,
                1
            );

            if (result == null)
            {
                Debug.LogError("[MergeTest] 没有获得合成结果。", this);
                return;
            }

            if (result.State != (int)E_Item_TreeState.Big)
            {
                Debug.LogError(
                    $"[MergeTest] 结果状态不正确，实际 State = {result.State}。",
                    this
                );
                return;
            }

            Debug.Log("[MergeTest] 合成成功：Item_Tree，状态为 Big。", this);

            Debug.Log(
                $"[MergeTest] 原始树苗的状态仍为 {(E_Item_TreeState)smallTree.State}。",
                this
            );
        }
    }
}