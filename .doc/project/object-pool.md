# 通用 Prefab 对象池

`GameObjectPool` 是全局唯一、按 Prefab 根 `GameObject` 引用分组的运行时对象池。它不会预热；第一次取用某个 Prefab 时才创建实例。

## 完整使用示例：子弹池

使用对象池分三步：让 Prefab 实现接口、通过 `Get` 取出、不再使用时通过 `Release` 归还。

### 1. 子弹实现 `IPoolable`

将下面的 `Bullet` 脚本挂在子弹 Prefab 的根节点：

```csharp
using AChen.Pooling;
using UnityEngine;

public sealed class Bullet : MonoBehaviour, IPoolable
{
    public void OnTakenFromPool()
    {
        // 每次取出时重置速度、计时器等状态。
        Debug.Log("子弹被取出");
    }

    public void OnReturnedToPool()
    {
        // 每次归还前解绑事件、停止特效等。
        Debug.Log("子弹被归还");
    }

    void OnCollisionEnter(Collision collision)
    {
        // 命中后归还对象池，不要调用 Destroy(gameObject)。
        GameObjectPool.Instance.Release(this);
    }
}
```

`IPoolable` 组件必须位于 Prefab 根节点。不要把场景中的 Prefab 实例当作模板传入，也不要自行归还其他来源创建的对象。

### 2. 从对象池发射子弹

在发射器中声明 `Bullet` 类型的 Prefab 引用：

```csharp
using AChen.Pooling;
using UnityEngine;

public sealed class BulletSpawner : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;

    public Bullet Fire()
    {
        Bullet bullet = GameObjectPool.Instance.Get(
            bulletPrefab,
            transform.position,
            transform.rotation);

        // 可以继续设置伤害、速度等本次发射的数据。
        // bullet.SetDamage(10);
        return bullet;
    }

    public int ClearUnusedBullets()
    {
        return GameObjectPool.Instance.ClearPool(bulletPrefab);
    }
}
```

在 Inspector 中，把根节点挂有 `Bullet` 组件的子弹 Prefab 拖到 `bulletPrefab` 字段。第一次调用 `Fire` 时对象池会创建实例；归还后再次调用会优先复用原实例。

### 3. 归还子弹

子弹不再使用时调用：

```csharp
GameObjectPool.Instance.Release(bullet);
```

不要调用：

```csharp
Destroy(bullet.gameObject);
```

需要清空该 Prefab 当前所有空闲实例时调用：

```csharp
int count = GameObjectPool.Instance.ClearPool(bulletPrefab);
Debug.Log($"清除了 {count} 个空闲子弹");
```

正在使用的子弹不会被 `ClearPool` 销毁。

## 运行时层级

运行时层级由对象池自动维护：

```text
GameObjectPool
└── [Bullet]
    ├── Active
    └── InActive
```

- `Get` 优先以后进先出（LIFO）顺序复用 `InActive` 实例；取出后恢复 Prefab 缩放，并调用 `OnTakenFromPool`。
- `Release` 调用 `OnReturnedToPool` 后，将实例移至 `InActive` 并禁用。每个借出的实例只能归还一次。
- `ClearPool` 只销毁指定 Prefab 的空闲实例，返回销毁数量，不影响仍在 `Active` 中的实例。
- `GameObjectPool.Instance` 首次访问时自动创建，并跨场景保留。

## 现有音频池

`AudioSourcePool` 暂不迁移。它还包含预热、容量限制和优先级抢占语义，后续统一时需要先明确这些能力如何映射到通用对象池。
