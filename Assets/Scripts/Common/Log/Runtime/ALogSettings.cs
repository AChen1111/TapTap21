using UnityEngine;

namespace AChen.Log
{
/// <summary>日志系统运行时设置。出包时若 EnableInPlayer=false,ALog 不再写日志。</summary>
public class ALogSettings : ScriptableObject
{
    public const string ResourceName = "ALogSettings";
    public const string AssetPath = "Assets/Scripts/Common/Log/Resources/ALogSettings.asset";

    [Tooltip("Enable ALog output in release players.")]
    public bool EnableInPlayer = true;

    private static ALogSettings s_instance;

    public static ALogSettings Instance {
        get {
            if (s_instance == null)
            {
                s_instance = Resources.Load<ALogSettings>(ResourceName);
            }
            return s_instance;
        }
    }

#if UNITY_EDITOR
    public static void SetEditorInstance(ALogSettings settings) {
        s_instance = settings;
    }
#endif
}
}
