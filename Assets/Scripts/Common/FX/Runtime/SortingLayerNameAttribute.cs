using UnityEngine;

namespace FXSystem
{
    /// <summary>
    /// Inspector 里显示项目 Sorting Layer 下拉，避免手打层名（Layer1 和 Layer 1 不是同一层）。
    /// </summary>
    public sealed class SortingLayerNameAttribute : PropertyAttribute
    {
    }
}
