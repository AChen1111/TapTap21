### 虚拟列表

| API                                                    | 何时使用                                  | 说明                                    |
| ------------------------------------------------------ | ----------------------------------------- | --------------------------------------- |
| `GridListController.InitList(rowPrefab, data)`         | 已通过 Inspector 持有行 Prefab 时         | 推荐重载，立即绑定                      |
| `await GridListController.InitList(resourceKey, data)` | 行 Prefab 位于 Resources 且当前没有引用时 | key 为 Resources 相对路径，不含扩展名   |
| `SelectedIndex`                                        | 需要读取当前选中数据索引时                | 未选中为 `-1`                           |
| `MoveToSelectedIfHidden()`                             | 初始化或恢复选中项后，确保选中项可见时    | 已可见则不滚动；可传时长和 DOTween Ease |
| `IRowItem<TData>.SetRowData(...)`                      | 实现行 Prefab 组件时                      | 框架刷新可见行时调用，业务不要主动调用  |
