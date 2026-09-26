# 导入记录

源目录：`C:/Users/ldc20/OneDrive/Desktop/存档/Unity-UI-Adapter/Unity-UI-Adapter/Assets/Plugins/UIAdapter`。

保留组件名称、Inspector 字段和原有脚本 GUID；按工程要求移除命名空间。
原项目的 Apache-2.0 许可证保存在 `LICENSE.txt`。

本次修改：程序集显式引用 Unity.ugui；组件启用时执行初次布局；
布局跳过非 RectTransform 和未激活的子物体；安全区改为从当前 Canvas 转换屏幕坐标，
支持窗口尺寸、安全区、父矩形和 Canvas 视口变化，无全局 CanvasScaler 状态。
`SafeAreaAdapter.Init(CanvasScaler)` 保留兼容签名，已无需调用。

使用文档：项目根 `.doc/project/ui-adapter.md`。
