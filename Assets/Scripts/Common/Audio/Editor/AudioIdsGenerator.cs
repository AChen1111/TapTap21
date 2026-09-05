#if UNITY_EDITOR
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace dyh
{

/// <summary>根据 AudioConfig 资源名称生成音频 ID 常量类。</summary>
public static class AudioIdsGenerator
{
    private const string OutputPath = "Assets/Scripts/Common/Audio/AudioIds.cs";

    /// <summary>扫描项目中的 AudioConfig，并生成 AudioIds.cs。</summary>
    [MenuItem("Tools/Audio/生成 AudioIds")]
    public static void Generate()
    {
        var builder = new StringBuilder();
        builder.AppendLine("// 此文件由 Tools/Audio/生成 AudioIds 自动生成，请勿手动修改。\n");
        builder.AppendLine("namespace dyh { public static class AudioIds");
        builder.AppendLine("{");
        var identifiers = new HashSet<string>();

        foreach (var guid in AssetDatabase.FindAssets("t:AudioConfig"))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var config = AssetDatabase.LoadAssetAtPath<AudioConfig>(path);
            if (config == null)
                continue;

            var id = config.GetId();
            var identifier = ToIdentifier(id);
            if (!identifiers.Add(identifier))
            {
                Debug.LogWarning($"音频 ID 生成的常量名重复，已跳过：{id}");
                continue;
            }

            builder.AppendLine($"    public const string {identifier} = \"{id}\";");
        }

        builder.AppendLine("} }");
        File.WriteAllText(OutputPath, builder.ToString(), Encoding.UTF8);
        AssetDatabase.Refresh();
        Debug.Log($"已生成音频 ID：{OutputPath}");
    }

    private static string ToIdentifier(string value)
    {
        var builder = new StringBuilder();
        foreach (var character in value)
            if (char.IsLetterOrDigit(character) || character == '_')
                builder.Append(character);

        if (builder.Length == 0 || char.IsDigit(builder[0]))
            builder.Insert(0, '_');

        return builder.ToString();
    }
}
}
#endif
