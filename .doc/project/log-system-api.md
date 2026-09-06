## 日志系统（`AChen.Log`）

| API                                  | 何时使用                         | 示例                                                  |
| ------------------------------------ | -------------------------------- | ----------------------------------------------------- |
| `ALog.Log(message, category)`        | 正常流程、状态变化和调试信息     | `ALog.Log("打开商店", ALogCategories.UI)`             |
| `ALog.LogWarning(message, category)` | 可恢复但需要关注的问题           | `ALog.LogWarning("登录重试", ALogCategories.Net)`     |
| `ALog.LogError(message, category)`   | 功能失败、非法状态或关键配置缺失 | `ALog.LogError("UISettings 为空", ALogCategories.UI)` |
| `ALog.Enabled`                       | 昂贵日志内容需要延迟计算时       | 先检查再构造复杂字符串；普通日志无需手动检查          |
| `ALog.Format(category, message)`     | 只需要统一格式字符串、暂不输出时 | 普通输出不要先 Format，直接调用对应日志 API           |

分类使用 `ALogCategories.Default`、`Net`、`Event`、`UI`。新增分类应在 `ALogCategories` 中添加常量，不要在业务代码中散落字符串。

