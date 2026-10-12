using UnityEngine;

namespace FXSystem
{
    /// <summary>
    /// 特效目录。管理器启动时建立 Id → Definition 映射。
    /// </summary>
    [CreateAssetMenu(menuName = "FX/Catalog", fileName = "FXCatalog")]
    public class FXCatalog : ScriptableObject
    {
        public FXDefinition[] Definitions;
    }
}
