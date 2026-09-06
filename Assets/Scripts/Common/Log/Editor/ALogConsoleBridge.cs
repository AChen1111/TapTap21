using System;
using System.Reflection;
using UnityEditor;

/// <summary>
/// 对 Unity 内置 Console(UnityEditor.ConsoleWindow / LogEntries)的反射访问封装。
/// 这些都是 internal API,集中在这里以便 Unity 版本变动时只改一处;成员缺失时各方法安全降级。
/// </summary>
public static class ALogConsoleBridge
{
    private const BindingFlags AnyStatic = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
    private const BindingFlags AnyInstance = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    private static readonly Type s_consoleType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ConsoleWindow");
    private static readonly Type s_logEntriesType = typeof(EditorWindow).Assembly.GetType("UnityEditor.LogEntries");

    private static readonly FieldInfo s_consoleInstance = s_consoleType?.GetField("ms_ConsoleWindow", AnyStatic);
    private static readonly FieldInfo s_activeText = s_consoleType?.GetField("m_ActiveText", AnyInstance);
    private static readonly FieldInfo s_callstackStart = s_consoleType?.GetField("m_CallstackTextStart", AnyInstance);
    private static readonly MethodInfo s_setFilter = s_consoleType?.GetMethod("SetFilter", AnyInstance, null, new[] { typeof(string) }, null);
    private static readonly EventInfo s_toolbarGui = s_consoleType?.GetEvent("drawCustomToolbarGui", AnyStatic);
    private static readonly MethodInfo s_getFilteringText = s_logEntriesType?.GetMethod("GetFilteringText", AnyStatic);

    public static bool IsAvailable => s_consoleType != null && s_toolbarGui != null;

    /// <summary>把 GUI 回调挂到内置 Console 工具栏的自定义绘制点</summary>
    public static void AddToolbarGui(Action gui) {
        s_toolbarGui?.GetAddMethod(true)?.Invoke(null, new object[] { gui });
    }

    private static EditorWindow Window => s_consoleInstance?.GetValue(null) as EditorWindow;

    /// <summary>当前选中日志的完整文本(消息 + 堆栈),未选中时为空</summary>
    public static string ActiveText {
        get {
            EditorWindow window = Window;
            return window == null ? string.Empty : s_activeText?.GetValue(window) as string ?? string.Empty;
        }
    }

    /// <summary>选中日志文本里堆栈部分的起始下标,取不到时返回 0</summary>
    private static int CallstackStart {
        get {
            EditorWindow window = Window;
            if (window == null || s_callstackStart == null)
            {
                return 0;
            }
            return s_callstackStart.GetValue(window) is int start ? start : 0;
        }
    }

    /// <summary>选中日志的消息部分(不含堆栈)</summary>
    public static string ActiveMessage {
        get {
            string text = ActiveText;
            int start = CallstackStart;
            return start > 0 && start <= text.Length ? text.Substring(0, start).TrimEnd() : text;
        }
    }

    /// <summary>选中日志的堆栈部分</summary>
    public static string ActiveCallstack {
        get {
            string text = ActiveText;
            int start = CallstackStart;
            return start > 0 && start < text.Length ? text.Substring(start) : text;
        }
    }

    /// <summary>写入内置 Console 的搜索框,同时驱动过滤和输入框显示</summary>
    public static void SetSearchText(string text) {
        EditorWindow window = Window;
        if (window == null || s_setFilter == null)
        {
            return;
        }
        s_setFilter.Invoke(window, new object[] { text ?? string.Empty });
        window.Repaint();
    }

    public static string GetSearchText() {
        return s_getFilteringText?.Invoke(null, null) as string ?? string.Empty;
    }
}
