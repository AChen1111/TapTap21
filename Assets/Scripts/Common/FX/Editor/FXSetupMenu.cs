#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FXSystem.Editor
{
    public static class FXSetupMenu
    {
        const string SettingsDir = "Assets/FX/Resources";
        const string CatalogPath = "Assets/FX/Resources/FXCatalog.asset";
        const string SettingsPath = "Assets/FX/Resources/FXSettings.asset";

        [MenuItem("FX/Create Default Settings")]
        public static void CreateDefaultSettings()
        {
            if (!Directory.Exists(SettingsDir))
                Directory.CreateDirectory(SettingsDir);

            var catalog = AssetDatabase.LoadAssetAtPath<FXCatalog>(CatalogPath);
            if (catalog == null)
            {
                catalog = ScriptableObject.CreateInstance<FXCatalog>();
                catalog.Definitions = new FXDefinition[0];
                AssetDatabase.CreateAsset(catalog, CatalogPath);
            }

            var settings = AssetDatabase.LoadAssetAtPath<FXSettings>(SettingsPath);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<FXSettings>();
                settings.Catalog = catalog;
                settings.GlobalMaxActive = 256;
                settings.LogWarnings = true;
                settings.ClearOnActiveSceneChange = true;
                AssetDatabase.CreateAsset(settings, SettingsPath);
            }
            else if (settings.Catalog == null)
            {
                settings.Catalog = catalog;
                EditorUtility.SetDirty(settings);
            }

            AssetDatabase.SaveAssets();
            Selection.activeObject = settings;
            EditorGUIUtility.PingObject(settings);
            Debug.Log("[FX] 已创建 Resources/FXSettings 与 FXCatalog。接下来：Create → FX → Definition，填 Prefab，再拖进 Catalog。");
        }
    }
}
#endif
