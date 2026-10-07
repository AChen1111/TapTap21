using UnityEngine;

namespace GamePlay.Scene
{
    /// <summary>
    /// 可选的场景融合操作配置。没有挂载时由拖拽桥接器使用默认 op。
    /// 环境条件变化时，调用方可以直接更新 Operation。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SceneItemMergeTarget : MonoBehaviour
    {
        [SerializeField]
        private int _operation;

        public int Operation
        {
            get => _operation;
            set => _operation = value;
        }
    }
}
