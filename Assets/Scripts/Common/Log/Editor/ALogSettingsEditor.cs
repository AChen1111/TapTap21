using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>确保 ALogSettings 资源存在,供控制台开关读写并打进出包体</summary>
public static class ALogSettingsEditor
{
    public static ALogSettings GetOrCreate() {
        var settings = AssetDatabase.LoadAssetAtPath<ALogSettings>(ALogSettings.AssetPath);
        if (settings != null)
        {
            ALogSettings.SetEditorInstance(settings);
            return settings;
        }

        string dir = Path.GetDirectoryName(ALogSettings.AssetPath);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        settings = ScriptableObject.CreateInstance<ALogSettings>();
        settings.EnableInPlayer = true;
        AssetDatabase.CreateAsset(settings, ALogSettings.AssetPath);
        AssetDatabase.SaveAssets();
        ALogSettings.SetEditorInstance(settings);
        return settings;
    }

    [InitializeOnLoadMethod]
    private static void EnsureAsset() {
        GetOrCreate();
    }
}
