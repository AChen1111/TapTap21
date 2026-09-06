using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AChen.Log
{
/// <summary>一条日志里的单个调用栈帧,FilePath为空表示是C层/无源码的帧</summary>
public class ALogFrame
{
    public string Signature;
    public string FilePath;
    public int Line;

    public bool CanJump => !string.IsNullOrEmpty(FilePath) && Line > 0;

    public string Location => CanJump ? $"{FilePath}:{Line}" : "<no source>";
}

/// <summary>
/// 把Unity的C#堆栈文本解析成结构化栈帧,供双击跳转重定向和调用堆栈可视化使用。
/// </summary>
public static class ALogStackParser
{
    //  Foo.Bar () (at Assets/Scripts/X.cs:23)
    private static readonly Regex s_csFrame = new Regex(@"^\s*(?<sig>.+?)\s*\(at\s+(?<path>.+?):(?<line>\d+)\)\s*$", RegexOptions.Compiled);

    /// <summary>ALog 自身所在目录,这些帧不是日志的真实发出位置</summary>
    private const string LogSystemDir = "Assets/Scripts/Common/Log/";

    /// <summary>解析Unity给出的C#堆栈文本</summary>
    public static List<ALogFrame> ParseCSharp(string stackTrace) {
        var frames = new List<ALogFrame>();
        if (string.IsNullOrEmpty(stackTrace))
        {
            return frames;
        }

        foreach (string raw in SplitLines(stackTrace))
        {
            Match match = s_csFrame.Match(raw);
            if (match.Success)
            {
                frames.Add(new ALogFrame
                {
                    Signature = match.Groups["sig"].Value,
                    FilePath = match.Groups["path"].Value.Replace('\\', '/'),
                    Line = int.Parse(match.Groups["line"].Value),
                });
                continue;
            }
            string trimmed = raw.Trim();
            if (trimmed.Length > 0)
            {
                frames.Add(new ALogFrame { Signature = trimmed });
            }
        }
        return frames;
    }

    /// <summary>
    /// 去掉堆栈顶部的日志转发帧(Debug.LogXXX、ALog 自身),让调用链的末端就是日志真正的发出位置。
    /// 找不到业务帧时原样返回,避免堆栈图变空。
    /// </summary>
    public static List<ALogFrame> TrimLoggingFrames(List<ALogFrame> frames) {
        if (frames == null)
        {
            return new List<ALogFrame>();
        }
        for (int i = 0; i < frames.Count; i++)
        {
            if (frames[i].CanJump && !IsLogSystem(frames[i].FilePath))
            {
                return frames.GetRange(i, frames.Count - i);
            }
        }
        return frames;
    }

    public static bool IsLogSystem(string filePath) {
        return !string.IsNullOrEmpty(filePath)
            && filePath.Replace('\\', '/').StartsWith(LogSystemDir, System.StringComparison.OrdinalIgnoreCase);
    }

    private static string[] SplitLines(string text) {
        return text.Replace("\r\n", "\n").Split('\n');
    }
}
}
