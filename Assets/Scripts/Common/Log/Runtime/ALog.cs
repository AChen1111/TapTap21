using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// 分类日志系统的运行时入口:给消息加上分类前缀后写入 Unity 控制台。
/// 使用 ALog.Log / LogWarning / LogError 写入日志,分类取 ALogCategories 中的常量。
/// 日志的浏览、过滤、跳转由内置 Console 工具栏上的 ALog 按钮提供(见 Common/Log/Editor)。
/// </summary>
public static class ALog
{
    /// <summary>Editor始终启用;正式包看 ALogSettings.EnableInPlayer</summary>
    public static bool Enabled {
        get {
#if UNITY_EDITOR
            return true;
#else
            ALogSettings settings = ALogSettings.Instance;
            return settings == null || settings.EnableInPlayer;
#endif
        }
    }

    //HideInCallstack 让 ALog 自身的帧不出现在 Console 的堆栈里,双击跳转由 ALogJumpRedirect 兜底
    [HideInCallstack]
    public static void Log(string message, string category = ALogCategories.Default) {
        if (!Enabled)
        {
            return;
        }
        Debug.Log(Format(category, message));
    }

    [HideInCallstack]
    public static void LogWarning(string message, string category = ALogCategories.Default) {
        if (!Enabled)
        {
            return;
        }
        Debug.LogWarning(Format(category, message));
    }

    [HideInCallstack]
    public static void LogError(string message, string category = ALogCategories.Default) {
        if (!Enabled)
        {
            return;
        }
        Debug.LogError(Format(category, message));
    }

    /// <summary>分类前缀,同时也是内置 Console 按分类过滤时的搜索关键字</summary>
    public static string Format(string category, string message) {
        return $"[{category}] {message}";
    }
}
