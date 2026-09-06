## 对象池（`AChen.Pooling`）

| API                               | 何时使用                          | 说明                                           |
| --------------------------------- | --------------------------------- | ---------------------------------------------- |
| `GameObjectPool.Instance`         | 访问全局对象池时                  | 首次访问自动创建，跨场景保留                   |
| `Get(prefab)`                     | 取出后会由调用方设置 Transform 时 | 默认世界坐标为零、旋转为单位旋转               |
| `Get(prefab, position, rotation)` | 子弹、特效等生成时就要定位时      | 优先 LIFO 复用，否则创建新实例                 |
| `Release(instance)`               | 对象本次使用结束时                | 替代 `Destroy`；只能归还本池当前借出的实例一次 |
| `ClearPool(prefab)`               | 切换大场景或释放某类空闲缓存时    | 只销毁该 Prefab 的空闲实例，返回销毁数量       |
| `IPoolable.OnTakenFromPool()`     | 实现池化组件时                    | 重置速度、计时器、生命值等状态                 |
| `IPoolable.OnReturnedToPool()`    | 实现池化组件时                    | 解绑事件、停止特效、清理外部引用               |

`IPoolable` 必须由 Prefab 根节点组件实现。池化对象结束使用时调用 `Release`，不要调用 `Destroy(instance.gameObject)`。
