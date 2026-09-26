# UI 适配：使用与 API

组件位置：`Assets/Plugins/UIAdapter/`。

## 使用

给 UI 父容器添加对应组件，将要排列的元素作为直接子物体：

| 组件 | 用途 | 设置 |
| --- | --- | --- |
| `HorizontalAdapter` | 从左到右排列，自动调整父容器宽度 | `Gap` 设置间隙 |
| `VerticalAdapter` | 从上到下排列，自动调整父容器高度 | `Gap` 设置间隙 |
| `AngleAdapter` | 扇形或圆形排列，同时旋转子元素 | 设置角度、半径和方向 |
| `SafeAreaAdapter` | 避开刘海、系统栏等非安全区域 | 挂到 Canvas 下的内容容器 |

安全区层级：

```text
Canvas
├─ Background                 全屏背景
└─ SafeAreaRoot               挂 SafeAreaAdapter
   └─ 需要保护的 UI 内容
```

安全区支持 Overlay 和 Camera Canvas；Camera 模式需指定 Render Camera，不支持 World Space。
容器保持单位缩放、无旋转；组件控制的尺寸和位置不要同时交给 LayoutGroup 或 ContentSizeFitter。

## 外部 API

四种组件均可调用 `Adapt()` 立即刷新布局：

```csharp
var row = GetComponent<HorizontalAdapter>();
row.CalculateEveryFrame = false;
row.Gap = 12f;
row.Adapt(); // 新增、隐藏子元素或修改尺寸后再次调用
```

| API / 字段 | 所属组件 | 用途 |
| --- | --- | --- |
| `void Adapt()` | 所有组件、`AdapterBase` | 立即重新计算布局或安全区 |
| `bool CalculateEveryFrame` | 所有组件 | 是否每帧重算；横排、竖排、角度排列默认 true，安全区默认 false |
| `float Gap` | 横排、竖排 | 元素间隙，单位为 UI 坐标单位 |
| `float Gap` | 角度排列 | 相邻元素的角度间隔，单位为度 |
| `bool Clockwise` | 角度排列 | true 顺时针，false 逆时针 |
| `float BiasAngle` | 角度排列 | 第一项起始角度；0 时位于正上方 |
| `float Distance` | 角度排列 | 元素到父原点的半径 |
| `static void SafeAreaAdapter.Init(CanvasScaler scaler)` | 安全区 | 旧兼容接口，当前无需调用，无实际操作 |

组件启用时会刷新一次。关闭逐帧计算后，排列组件需在内容变化后手动调用 `Adapt()`；安全区仍会自动响应屏幕尺寸与安全区变化。
横排、竖排和角度排列会跳过未激活、非 RectTransform 的子物体。圆环可设置 `Gap = 360f / 元素数量`，角度排列子物体的锚点建议居中。

有独立 asmdef 的代码需要添加 `UIAdapter` 程序集引用；可通过 `AdapterBase` 引用统一调用 `Adapt()`。
