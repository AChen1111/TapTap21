using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace AChen.Prefabs.Editor
{
    /// <summary>把 Project 窗口中选中的 Prefab 名称加入 <see cref="PrefabNameSpace"/>。</summary>
    public static class PrefabNameSpaceGenerator
    {
        const string MenuPath = "Assets/AddToPrefabNameSpace";
        const string OutputPath = "Assets/Scripts/Common/Prefab/PrefabNameSpace.cs";
        const string EntryMarker = "// PrefabName: ";

        static readonly HashSet<string> s_keywords = new HashSet<string>
        {
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked",
            "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else",
            "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for",
            "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock",
            "long", "namespace", "new", "null", "object", "operator", "out", "override", "params",
            "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short",
            "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true",
            "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual",
            "void", "volatile", "while"
        };

        /// <summary>将当前选中的所有 Prefab 名称加入静态常量类。</summary>
        [MenuItem(MenuPath, false, 2000)]
        public static void AddSelectedPrefabs()
        {
            IReadOnlyList<GameObject> prefabs = GetSelectedPrefabs();
            var names = new HashSet<string>(ReadRegisteredNames(), StringComparer.Ordinal);
            int addedCount = 0;

            foreach (GameObject prefab in prefabs)
            {
                if (names.Add(prefab.name))
                {
                    addedCount++;
                }
            }

            if (addedCount == 0)
            {
                Debug.Log("[PrefabNameSpace] 选中的 Prefab 名称已经全部登记。");
                return;
            }

            File.WriteAllText(OutputPath, BuildSource(names), new UTF8Encoding(false));
            AssetDatabase.ImportAsset(OutputPath, ImportAssetOptions.ForceUpdate);
            Debug.Log($"[PrefabNameSpace] 已添加 {addedCount} 个 Prefab 名称，生成文件：{OutputPath}");
        }

        [MenuItem(MenuPath, true)]
        static bool CanAddSelectedPrefabs() => GetSelectedPrefabs().Count > 0;

        internal static string BuildSource(IEnumerable<string> prefabNames)
        {
            string[] names = prefabNames
                .Where(name => !string.IsNullOrEmpty(name))
                .Distinct(StringComparer.Ordinal)
                .OrderBy(name => name, StringComparer.Ordinal)
                .ToArray();
            var usedIdentifiers = new HashSet<string>(StringComparer.Ordinal);
            var builder = new StringBuilder();

            builder.AppendLine("// 此文件由 Project 窗口的 AddToPrefabNameSpace 菜单生成，请勿手动修改。");
            builder.AppendLine();
            builder.AppendLine("namespace AChen.Prefabs");
            builder.AppendLine("{");
            builder.AppendLine("    /// <summary>已登记 Prefab 名称的统一常量入口。</summary>");
            builder.AppendLine("    public static class PrefabNameSpace");
            builder.AppendLine("    {");

            foreach (string name in names)
            {
                string identifier = MakeUniqueIdentifier(ToIdentifier(name), usedIdentifiers);
                string encodedName = Convert.ToBase64String(Encoding.UTF8.GetBytes(name));
                builder.Append("        ").Append(EntryMarker).AppendLine(encodedName);
                builder.Append("        public const string ").Append(identifier).Append(" = \"")
                    .Append(EscapeString(name)).AppendLine("\";");
            }

            builder.AppendLine("    }");
            builder.AppendLine("}");
            return builder.ToString();
        }

        internal static string ToIdentifier(string value)
        {
            var builder = new StringBuilder(value.Length);
            foreach (char character in value)
            {
                builder.Append(char.IsLetterOrDigit(character) || character == '_' ? character : '_');
            }

            if (builder.Length == 0)
            {
                builder.Append("Prefab");
            }

            if (char.IsDigit(builder[0]) || s_keywords.Contains(builder.ToString()))
            {
                builder.Insert(0, '_');
            }

            return builder.ToString();
        }

        static IReadOnlyList<GameObject> GetSelectedPrefabs()
        {
            return Selection.objects
                .Select(AssetDatabase.GetAssetPath)
                .Where(path => path.EndsWith(".prefab", StringComparison.OrdinalIgnoreCase))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Select(AssetDatabase.LoadAssetAtPath<GameObject>)
                .Where(prefab => prefab != null)
                .ToArray();
        }

        static IEnumerable<string> ReadRegisteredNames()
        {
            if (!File.Exists(OutputPath))
            {
                yield break;
            }

            foreach (string line in File.ReadLines(OutputPath))
            {
                int markerIndex = line.IndexOf(EntryMarker, StringComparison.Ordinal);
                if (markerIndex < 0)
                {
                    continue;
                }

                string encodedName = line.Substring(markerIndex + EntryMarker.Length).Trim();
                byte[] bytes;
                try
                {
                    bytes = Convert.FromBase64String(encodedName);
                }
                catch (FormatException)
                {
                    Debug.LogWarning("[PrefabNameSpace] 忽略了无法解析的名称记录：" + line.Trim());
                    continue;
                }

                yield return Encoding.UTF8.GetString(bytes);
            }
        }

        static string MakeUniqueIdentifier(string preferred, ISet<string> usedIdentifiers)
        {
            string identifier = preferred;
            int suffix = 2;
            while (!usedIdentifiers.Add(identifier))
            {
                identifier = preferred + "_" + suffix;
                suffix++;
            }

            return identifier;
        }

        static string EscapeString(string value)
        {
            return value
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n")
                .Replace("\t", "\\t");
        }
    }
}
