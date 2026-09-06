using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AChen.Log
{
/// <summary>
/// 分类日志系统的运行时入口:给消息加上分类前缀后写入 Unity 控制台。
/// 使用 ALog.Log / LogWarning / LogError 写入日志,分类取 ALogCategories 中的常量。
/// 日志的浏览、过滤、跳转由内置 Console 工具栏上的 ALog 按钮提供(见 Common/Log/Editor)。
/// </summary>
public static class ALog
{
    /// <summary>日志系统当前是否启用。Editor 始终启用，Player 由 <see cref="ALogSettings.EnableInPlayer"/> 控制。</summary>
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
    /// <summary>记录普通流程信息，例如初始化完成或状态切换。</summary>
    /// <param name="message">日志正文。</param>
    /// <param name="category">用于 Console 筛选的分类，优先使用 <see cref="ALogCategories"/> 常量。</param>
    [HideInCallstack]
    public static void Log(string message, string category = ALogCategories.Default) {
        if (!Enabled)
        {
            return;
        }
        Debug.Log(Format(category, message));
    }

    /// <summary>记录可恢复但需要关注的问题，例如重试或配置回退。</summary>
    /// <param name="message">警告正文。</param>
    /// <param name="category">用于 Console 筛选的分类。</param>
    [HideInCallstack]
    public static void LogWarning(string message, string category = ALogCategories.Default) {
        if (!Enabled)
        {
            return;
        }
        Debug.LogWarning(Format(category, message));
    }

    /// <summary>记录导致功能失败或状态异常的错误。</summary>
    /// <param name="message">错误正文。</param>
    /// <param name="category">用于 Console 筛选的分类。</param>
    [HideInCallstack]
    public static void LogError(string message, string category = ALogCategories.Default) {
        if (!Enabled)
        {
            return;
        }
        Debug.LogError(Format(category, message));
    }

    /// <summary>生成带分类前缀的文本。仅在需要格式化但不立即输出时使用。</summary>
    /// <param name="category">日志分类。</param>
    /// <param name="message">日志正文。</param>
    /// <returns>格式为 <c>[分类] 正文</c> 的字符串。</returns>
    public static string Format(string category, string message) {
        return $"[{category}] {message}";
    }
}
}
