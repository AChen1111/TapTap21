# 日志系统

命名空间 `AChen.Log`。代码在 `Assets/Scripts/Common/Log/`。

## 写日志

```csharp
using AChen.Log;

ALog.Log("初始化完成");
ALog.Log("打开商店", ALogCategories.UI);
ALog.LogWarning("重试登录", ALogCategories.Net);
ALog.LogError("UISettings 为空", ALogCategories.UI);
```

输出格式：`[分类] 消息`。内置 Console 按这个前缀过滤。

## 分类

分类常量在 `ALogCategories`：`Default`、`Network`、`Event`、`UI`。

新增分类只加常量，不要在业务里手写字符串。Console 工具栏会反射这些常量生成下拉。

## Editor

打开 Window → General → Console。工具栏会多出 ALog 分类、Player Logs、Stack Graph。

- 分类下拉：复用 Console 搜索框，按 `[Category]` 过滤。
- 双击日志：跳过 `ALog` 自身栈帧，落到调用点。
- Stack Graph：把当前选中日志的调用栈画成图。
- Player Logs：对应 `ALogSettings.EnableInPlayer`。

设置资源：`Assets/Scripts/Common/Log/Resources/ALogSettings.asset`。缺失时 Editor 会自动创建。

## 注意事项

- Editor 里始终输出。正式包看 `ALogSettings.EnableInPlayer`；资源缺失时默认开启。
- 用 `ALog`，不要混用裸 `Debug.Log`，否则没有分类、也跳不过包装栈。
- 事件系统在订阅/派发时会打 `ALogCategories.Event`。`ALog.Enabled == false` 时这些调试日志也会关掉。
- 分类名就是搜索关键字，改常量等于改过滤词。
