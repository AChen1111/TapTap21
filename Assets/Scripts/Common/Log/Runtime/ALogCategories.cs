namespace AChen.Log
{
/// <summary>业务日志分类。新增分类时在此添加常量，避免调用方手写字符串。</summary>
public static class ALogCategories
{
    /// <summary>没有明确业务归属时使用。</summary>
    public const string Default = "Default";
    /// <summary>网络请求、连接和协议相关日志。</summary>
    public const string Net = "Network";
    /// <summary>事件订阅、取消订阅和派发相关日志。</summary>
    public const string Event = "Event";
    /// <summary>UI 打开、关闭、绑定和显示异常相关日志。</summary>
    public const string UI = "UI";
}
}
