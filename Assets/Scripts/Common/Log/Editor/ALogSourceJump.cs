using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

/// <summary>把栈帧定位到 C# 源码。</summary>
public static class ALogSourceJump
{
    public static void Open(ALogFrame frame) {
        if (frame == null || !frame.CanJump)
        {
            return;
        }

        string path = frame.FilePath.Replace('\\', '/');
        if (path.StartsWith("Assets/"))
        {
            var asset = AssetDatabase.LoadAssetAtPath<Object>(path);
            if (asset != null)
            {
                AssetDatabase.OpenAsset(asset, frame.Line);
                return;
            }
        }

        if (!Path.IsPathRooted(path))
        {
            path = Path.GetFullPath(path).Replace('\\', '/');
        }
        if (!File.Exists(path))
        {
            Debug.LogWarning($"[ALog] File not found: {path}");
            return;
        }
        if (!InternalEditorUtility.OpenFileAtLineExternal(path, frame.Line))
        {
            Debug.LogWarning($"[ALog] Failed to open source. Check Preferences > External Tools: {path}:{frame.Line}");
        }
    }
}
