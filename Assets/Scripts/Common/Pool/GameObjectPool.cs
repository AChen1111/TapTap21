using System;
using System.Collections.Generic;
using UnityEngine;

namespace AChen.Pooling
{
    /// <summary>按 Prefab 引用分池的全局 GameObject 对象池。</summary>
    public sealed class GameObjectPool : MonoBehaviour
    {
        sealed class PoolBucket
        {
            public readonly Component Prefab;
            public readonly Transform Root;
            public readonly Transform ActiveRoot;
            public readonly Transform InactiveRoot;

            readonly HashSet<Component> m_active = new HashSet<Component>();
            readonly Stack<Component> m_inactive = new Stack<Component>();

            public int ActiveCount => m_active.Count;

            public PoolBucket(Component prefab, Transform owner)
            {
                Prefab = prefab;
                Root = CreateNode($"[{prefab.name}]", owner);
                ActiveRoot = CreateNode("Active", Root);
                InactiveRoot = CreateNode("InActive", Root);
            }

            public bool TryTakeInactive(out Component instance)
            {
                while (m_inactive.Count > 0)
                {
                    instance = m_inactive.Pop();
                    if (instance != null)
                    {
                        return true;
                    }
                }

                instance = null;
                return false;
            }

            public void MarkTaken(Component instance) => m_active.Add(instance);

            public bool TryMarkReturned(Component instance) => m_active.Remove(instance);

            public void StoreInactive(Component instance) => m_inactive.Push(instance);

            public IEnumerable<Component> DrainInactive()
            {
                while (m_inactive.Count > 0)
                {
                    yield return m_inactive.Pop();
                }
            }

            static Transform CreateNode(string name, Transform parent)
            {
                GameObject node = new GameObject(name);
                node.transform.SetParent(parent, false);
                return node.transform;
            }
        }

        static GameObjectPool s_instance;

        readonly Dictionary<GameObject, PoolBucket> m_buckets =
            new Dictionary<GameObject, PoolBucket>();
        readonly Dictionary<Component, PoolBucket> m_owners =
            new Dictionary<Component, PoolBucket>();

        /// <summary>获取全局对象池。首次访问时自动创建并跨场景保留。</summary>
        public static GameObjectPool Instance
        {
            get
            {
                if (s_instance == null)
                {
                    GameObject root = new GameObject(nameof(GameObjectPool));
                    s_instance = root.AddComponent<GameObjectPool>();
                }

                return s_instance;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetStaticState()
        {
            s_instance = null;
        }

        void Awake()
        {
            if (s_instance != null && s_instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void OnDestroy()
        {
            if (s_instance == this)
            {
                s_instance = null;
            }
        }

        /// <summary>在世界原点以单位旋转取出实例。适合取出后由调用方自行设置位置的对象。</summary>
        /// <typeparam name="T">Prefab 根节点上实现 <see cref="IPoolable"/> 的组件类型。</typeparam>
        /// <param name="prefab">作为分池键和实例模板的 Prefab 根组件。</param>
        /// <returns>已激活并执行 <see cref="IPoolable.OnTakenFromPool"/> 的实例。</returns>
        public T Get<T>(T prefab)
            where T : Component, IPoolable =>
            Get(prefab, Vector3.zero, Quaternion.identity);

        /// <summary>在指定世界坐标和旋转取出实例。适合子弹、特效等生成即定位的对象。</summary>
        /// <typeparam name="T">Prefab 根节点上实现 <see cref="IPoolable"/> 的组件类型。</typeparam>
        /// <param name="prefab">作为分池键和实例模板的 Prefab 根组件。</param>
        /// <param name="position">实例取出后的世界坐标。</param>
        /// <param name="rotation">实例取出后的世界旋转。</param>
        /// <returns>已激活并执行取出回调的实例。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="prefab"/> 为空。</exception>
        /// <exception cref="ArgumentException"><paramref name="prefab"/> 组件不在模板根节点。</exception>
        public T Get<T>(T prefab, Vector3 position, Quaternion rotation)
            where T : Component, IPoolable
        {
            ValidatePrefab(prefab);

            if (!m_buckets.TryGetValue(prefab.gameObject, out PoolBucket bucket))
            {
                bucket = new PoolBucket(prefab, transform);
                m_buckets.Add(prefab.gameObject, bucket);
            }
            else if (bucket.Prefab.GetType() != prefab.GetType())
            {
                throw new InvalidOperationException(
                    "A Prefab root can only be pooled through one IPoolable component type.");
            }

            T instance;
            if (bucket.TryTakeInactive(out Component inactive))
            {
                instance = (T)inactive;
                instance.transform.SetParent(bucket.ActiveRoot, false);
                instance.transform.SetPositionAndRotation(position, rotation);
            }
            else
            {
                instance = Instantiate(prefab, position, rotation, bucket.ActiveRoot);
                m_owners.Add(instance, bucket);
            }

            instance.transform.localScale = prefab.transform.localScale;
            instance.gameObject.SetActive(true);
            bucket.MarkTaken(instance);
            instance.OnTakenFromPool();
            return instance;
        }

        /// <summary>归还当前正在使用的实例。归还后实例会被禁用，不能重复归还。</summary>
        /// <typeparam name="T">池化组件类型。</typeparam>
        /// <param name="instance">由本对象池 <see cref="Get{T}(T)"/> 取出的实例。</param>
        /// <exception cref="ArgumentNullException"><paramref name="instance"/> 为空。</exception>
        /// <exception cref="InvalidOperationException">实例不是本池创建、当前未借出或已被归还。</exception>
        public void Release<T>(T instance)
            where T : Component, IPoolable
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (!m_owners.TryGetValue(instance, out PoolBucket bucket) || !bucket.TryMarkReturned(instance))
            {
                throw new InvalidOperationException("The instance is not currently rented from this pool.");
            }

            try
            {
                instance.OnReturnedToPool();
            }
            finally
            {
                instance.transform.SetParent(bucket.InactiveRoot, false);
                instance.transform.localPosition = Vector3.zero;
                instance.transform.localRotation = Quaternion.identity;
                instance.gameObject.SetActive(false);
                bucket.StoreInactive(instance);
            }
        }

        /// <summary>销毁指定 Prefab 分池内的全部空闲实例，不影响仍在使用的实例。</summary>
        /// <typeparam name="T">Prefab 根节点上的池化组件类型。</typeparam>
        /// <param name="prefab">要清理的 Prefab 根组件。</param>
        /// <returns>本次请求销毁的空闲实例数量。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="prefab"/> 为空。</exception>
        /// <exception cref="ArgumentException"><paramref name="prefab"/> 组件不在模板根节点。</exception>
        public int ClearPool<T>(T prefab)
            where T : Component, IPoolable
        {
            ValidatePrefab(prefab);

            if (!m_buckets.TryGetValue(prefab.gameObject, out PoolBucket bucket))
            {
                return 0;
            }

            int destroyedCount = 0;
            foreach (Component instance in bucket.DrainInactive())
            {
                if (instance == null)
                {
                    continue;
                }

                m_owners.Remove(instance);
                Destroy(instance.gameObject);
                destroyedCount++;
            }

            if (bucket.ActiveCount == 0)
            {
                m_buckets.Remove(prefab.gameObject);
                Destroy(bucket.Root.gameObject);
            }

            return destroyedCount;
        }

        static void ValidatePrefab<T>(T prefab)
            where T : Component, IPoolable
        {
            if (prefab == null)
            {
                throw new ArgumentNullException(nameof(prefab));
            }

            if (prefab.transform.parent != null)
            {
                throw new ArgumentException(
                    "Poolable component must be on the Prefab root.",
                    nameof(prefab));
            }
        }
    }
}
