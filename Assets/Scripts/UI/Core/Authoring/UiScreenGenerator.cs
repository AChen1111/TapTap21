using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using UnityEngine;

[AddComponentMenu("UI/UI Screen Generator")]
public class UiScreenGenerator : MonoBehaviour
{
    public const string GeneratedTagStart = "// --tag_start: 自动生成--";
    public const string GeneratedTagEnd = "// --tag_end: 自动生成--";

    public enum Kind
    {
        Panel,
        Window
    }

    [SerializeField] Kind m_kind = Kind.Panel;
    [SerializeField] string m_className;
    [SerializeField] [FolderPath] string m_folderPath = "Assets/Scripts/UI";
    [SerializeField] UiPrefixBind[] m_uiBinds;

    [Button("收集UI引用")]
    [ContextMenu("收集UI引用")]
    public void CollectUiBinds()
    {
        var binds = UiPrefixCollector.Collect(transform);
#if UNITY_EDITOR
        var so = new UnityEditor.SerializedObject(this);
        UiPrefixCollector.WriteBinds(so, "m_uiBinds", binds);
        so.ApplyModifiedProperties();
        BindToScreenControllers(binds);
#else
        m_uiBinds = binds;
#endif
    }

    [Button("创建UI脚本")]
    [ContextMenu("创建UI脚本")]
    public void CreateUiScript()
    {
        if (!TryWriteUiScript(out string path))
        {
            return;
        }
        ALog.Log($"创建UI脚本完成: class={m_className}; path={path}; object={name}", ALogCategories.UI);
        ScheduleAttachAndBind();
    }

    [Button("重建引用")]
    [ContextMenu("重建引用")]
    public void RebuildUiBinds()
    {
        CollectUiBinds();
        if (!TryWriteUiScript(out string path))
        {
            return;
        }
        ALog.Log($"重建引用已写入脚本: class={m_className}; path={path}; object={name}", ALogCategories.UI);
        ScheduleAttachAndBind();
    }

    public bool TryAttachAndBind()
    {
#if UNITY_EDITOR
        Type type = FindGeneratedType();
        if (type == null)
        {
            return false;
        }

        var screen = GetComponent(type);
        if (screen == null)
        {
            screen = UnityEditor.Undo.AddComponent(gameObject, type);
        }

        BindToScreenControllers(GetBinds());
        return screen != null;
#else
        return false;
#endif
    }

    public string BuildCsSource()
    {
        UiPrefixBind[] binds = GetBinds();
        ScanUsings(binds, out bool useUi, out bool useTmp);
        var sb = new StringBuilder();
        sb.Append("using UnityEngine;\n");
        if (useUi)
        {
            sb.Append("using UnityEngine.UI;\n");
        }
        if (useTmp)
        {
            sb.Append("using TMPro;\n");
        }
        sb.Append('\n');
        string baseType = m_kind == Kind.Window ? "AWindowController" : "APanelController";
        sb.Append("public class ").Append(m_className).Append(" : ").Append(baseType).Append("\n{\n");
        sb.Append(BuildTaggedFieldBlock(BuildFieldLines(binds), "\n"));
        sb.Append("}\n");
        return sb.ToString();
    }

    public static string ReplaceGeneratedFields(string source, string[] fieldLines)
    {
        string nl = source.Contains("\r\n") ? "\r\n" : "\n";
        string block = BuildTaggedFieldBlock(fieldLines, nl);
        int start = source.IndexOf(GeneratedTagStart, StringComparison.Ordinal);
        int end = source.IndexOf(GeneratedTagEnd, StringComparison.Ordinal);
        if (start >= 0 && end > start)
        {
            int lineStart = source.LastIndexOf(nl, start);
            lineStart = lineStart < 0 ? 0 : lineStart + nl.Length;
            int after = end + GeneratedTagEnd.Length;
            if (source.Length >= after + nl.Length && source.Substring(after, nl.Length) == nl)
            {
                after += nl.Length;
            }
            return source.Substring(0, lineStart) + block + source.Substring(after);
        }

        Match match = Regex.Match(source, @"public class\s+\w+(?:\s*:\s*[^{\r\n]+)?\s*\{");
        int insertAt = match.Index + match.Length;
        if (source.Length >= insertAt + nl.Length && source.Substring(insertAt, nl.Length) == nl)
        {
            insertAt += nl.Length;
        }
        return source.Substring(0, insertAt) + block + nl + source.Substring(insertAt);
    }

    public static string EnsureUsings(string source, bool useUi, bool useTmp)
    {
        string nl = source.Contains("\r\n") ? "\r\n" : "\n";
        if (source.IndexOf("using UnityEngine;", StringComparison.Ordinal) < 0)
        {
            source = InsertUsing(source, "using UnityEngine;", nl);
        }
        if (useUi && source.IndexOf("using UnityEngine.UI;", StringComparison.Ordinal) < 0)
        {
            source = InsertUsing(source, "using UnityEngine.UI;", nl);
        }
        if (useTmp && source.IndexOf("using TMPro;", StringComparison.Ordinal) < 0)
        {
            source = InsertUsing(source, "using TMPro;", nl);
        }
        return source;
    }

    static string InsertUsing(string source, string usingLine, string nl)
    {
        MatchCollection matches = Regex.Matches(source, @"^using\s+[^;]+;", RegexOptions.Multiline);
        if (matches.Count == 0)
        {
            return usingLine + nl + source;
        }
        Match last = matches[matches.Count - 1];
        return source.Insert(last.Index + last.Length, nl + usingLine);
    }

    static string BuildTaggedFieldBlock(string[] fieldLines, string nl)
    {
        var sb = new StringBuilder();
        sb.Append("    ").Append(GeneratedTagStart).Append(nl);
        if (fieldLines != null)
        {
            for (int i = 0; i < fieldLines.Length; i++)
            {
                sb.Append("    ").Append(fieldLines[i]).Append(nl);
            }
        }
        sb.Append("    ").Append(GeneratedTagEnd).Append(nl);
        return sb.ToString();
    }

    string[] BuildFieldLines(UiPrefixBind[] binds)
    {
        if (binds == null || binds.Length == 0)
        {
            return Array.Empty<string>();
        }

        var lines = new string[binds.Length];
        for (int i = 0; i < binds.Length; i++)
        {
            lines[i] = "[SerializeField] " + CsTypeName(FieldType(binds[i])) + " " + binds[i].fieldName + ";";
        }
        return lines;
    }

    bool TryWriteUiScript(out string path)
    {
        path = null;
        if (string.IsNullOrWhiteSpace(m_className) || string.IsNullOrWhiteSpace(m_folderPath))
        {
            ALog.LogError($"写入UI脚本失败: 类名或目录为空; object={name}", ALogCategories.UI);
            return false;
        }

        string folder = m_folderPath.Replace('\\', '/').TrimEnd('/');
        Directory.CreateDirectory(folder);
        path = folder + "/" + m_className + ".cs";
        UiPrefixBind[] binds = GetBinds();
        ScanUsings(binds, out bool useUi, out bool useTmp);
        string[] fields = BuildFieldLines(binds);
        string text;
        if (File.Exists(path))
        {
            text = File.ReadAllText(path);
            text = EnsureUsings(text, useUi, useTmp);
            text = ReplaceGeneratedFields(text, fields);
        }
        else
        {
            text = BuildCsSource();
        }
        File.WriteAllText(path, text);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.ImportAsset(path);
#endif
        return true;
    }

    void ScheduleAttachAndBind()
    {
#if UNITY_EDITOR
        // 新增字段要等编译结束后才能绑上,先记下物体,Reload 后再绑一次
        UnityEditor.SessionState.SetInt(PendingAttachKey, (int)GetEntityId());
        if (TryAttachAndBind())
        {
            ALog.Log($"已挂载并绑定UI脚本: class={m_className}; object={name}", ALogCategories.UI);
        }
#endif
    }

    UiPrefixBind[] GetBinds()
    {
#if UNITY_EDITOR
        return ReadBindsFromSerialized();
#else
        return m_uiBinds;
#endif
    }

    static void ScanUsings(UiPrefixBind[] binds, out bool useUi, out bool useTmp)
    {
        useUi = false;
        useTmp = false;
        if (binds == null)
        {
            return;
        }

        for (int i = 0; i < binds.Length; i++)
        {
            Type type = FieldType(binds[i]);
            if (type != null && type.Namespace == "UnityEngine.UI")
            {
                useUi = true;
            }
            if (type != null && type.Namespace == "TMPro")
            {
                useTmp = true;
            }
        }
    }

    static Type FieldType(UiPrefixBind bind)
    {
        return bind.component != null ? bind.component.GetType() : typeof(GameObject);
    }

    static string CsTypeName(Type type)
    {
        return type.Name;
    }

#if UNITY_EDITOR
    const string PendingAttachKey = "UiScreenGenerator.PendingAttach";

    [UnityEditor.InitializeOnLoadMethod]
    static void QueuePendingAttach()
    {
        UnityEditor.EditorApplication.delayCall += ConsumePendingAttach;
    }

    static void ConsumePendingAttach()
    {
        if (UnityEditor.EditorApplication.isCompiling || UnityEditor.EditorApplication.isUpdating)
        {
            UnityEditor.EditorApplication.delayCall += ConsumePendingAttach;
            return;
        }

        EntityId id = (EntityId)UnityEditor.SessionState.GetInt(PendingAttachKey, 0);
        if (id == EntityId.None)
        {
            return;
        }

        UnityEditor.SessionState.EraseInt(PendingAttachKey);
        var gen = UnityEditor.EditorUtility.EntityIdToObject(id) as UiScreenGenerator;
        if (gen == null)
        {
            return;
        }

        if (gen.TryAttachAndBind())
        {
            ALog.Log($"编译后挂载并绑定UI脚本完成: object={gen.name}; class={gen.m_className}", ALogCategories.UI);
        }
        else
        {
            ALog.LogError($"编译后挂载UI脚本失败: object={gen.name}; class={gen.m_className}", ALogCategories.UI);
        }
    }

    void BindToScreenControllers(UiPrefixBind[] binds)
    {
        var behaviours = GetComponents<MonoBehaviour>();
        for (int i = 0; i < behaviours.Length; i++)
        {
            if (behaviours[i] is IUIScreenController && behaviours[i] != this)
            {
                var screenSo = new UnityEditor.SerializedObject(behaviours[i]);
                UiPrefixCollector.ApplySerializedFields(screenSo, binds);
                screenSo.ApplyModifiedProperties();
            }
        }
    }

    Type FindGeneratedType()
    {
        if (string.IsNullOrWhiteSpace(m_className))
        {
            return null;
        }

        var script = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEditor.MonoScript>(GetScriptPath());
        if (script != null)
        {
            Type fromScript = script.GetClass();
            if (fromScript != null)
            {
                return fromScript;
            }
        }

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        for (int i = 0; i < assemblies.Length; i++)
        {
            Type type = assemblies[i].GetType(m_className);
            if (type != null && typeof(MonoBehaviour).IsAssignableFrom(type) && !type.IsAbstract)
            {
                return type;
            }
        }
        return null;
    }

    string GetScriptPath()
    {
        return m_folderPath.Replace('\\', '/').TrimEnd('/') + "/" + m_className + ".cs";
    }

    UiPrefixBind[] ReadBindsFromSerialized()
    {
        var so = new UnityEditor.SerializedObject(this);
        var prop = so.FindProperty("m_uiBinds");
        var binds = new UiPrefixBind[prop.arraySize];
        for (int i = 0; i < prop.arraySize; i++)
        {
            var elem = prop.GetArrayElementAtIndex(i);
            binds[i] = new UiPrefixBind
            {
                fieldName = elem.FindPropertyRelative("fieldName").stringValue,
                target = (GameObject)elem.FindPropertyRelative("target").objectReferenceValue,
                component = (Component)elem.FindPropertyRelative("component").objectReferenceValue
            };
        }
        return binds;
    }
#endif
}
