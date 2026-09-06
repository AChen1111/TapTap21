using System.Collections;
using AChen.Pooling;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace AChen.Tests.Pooling
{
    public sealed class PoolableProbe : MonoBehaviour, IPoolable
    {
        public int TakenCount { get; private set; }
        public int ReturnedCount { get; private set; }

        public void OnTakenFromPool()
        {
            TakenCount++;
        }

        public void OnReturnedToPool()
        {
            ReturnedCount++;
        }
    }

    public sealed class GameObjectPoolTests
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            foreach (GameObjectPool pool in Object.FindObjectsByType<GameObjectPool>(
                         FindObjectsInactive.Include,
                         FindObjectsSortMode.None))
            {
                Object.Destroy(pool.gameObject);
            }

            yield return null;
        }

        [UnityTest]
        public IEnumerator FirstGet_CreatesPersistentPoolAndActivePrefabBucketWithoutPrewarming()
        {
            GameObjectPool pool = GameObjectPool.Instance;
            Assert.That(pool.name, Is.EqualTo("GameObjectPool"));
            Assert.That(pool.transform.childCount, Is.Zero);
            Assert.That(pool.gameObject.scene.name, Is.EqualTo("DontDestroyOnLoad"));

            GameObject templateObject = new GameObject("Prefab1");
            templateObject.SetActive(false);
            PoolableProbe prefab = templateObject.AddComponent<PoolableProbe>();
            Vector3 position = new Vector3(1f, 2f, 3f);
            Quaternion rotation = Quaternion.Euler(0f, 30f, 0f);

            PoolableProbe instance = pool.Get(prefab, position, rotation);

            Assert.That(instance, Is.Not.SameAs(prefab));
            Assert.That(instance.gameObject.activeSelf, Is.True);
            Assert.That(instance.transform.position, Is.EqualTo(position));
            Assert.That(instance.transform.rotation.eulerAngles.y, Is.EqualTo(30f).Within(0.01f));
            Assert.That(instance.TakenCount, Is.EqualTo(1));
            Assert.That(instance.ReturnedCount, Is.Zero);
            Assert.That(instance.transform.parent.name, Is.EqualTo("Active"));
            Assert.That(instance.transform.parent.parent.name, Is.EqualTo("[Prefab1]"));
            Assert.That(instance.transform.parent.parent.Find("InActive"), Is.Not.Null);

            Object.Destroy(templateObject);
            Object.Destroy(pool.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator DuplicateManager_DestroysItselfAndKeepsTheSingleton()
        {
            GameObjectPool original = GameObjectPool.Instance;
            GameObject duplicateObject = new GameObject("DuplicatePool");
            GameObjectPool duplicate = duplicateObject.AddComponent<GameObjectPool>();

            yield return null;

            Assert.That(duplicate == null, Is.True);
            Assert.That(GameObjectPool.Instance, Is.SameAs(original));
            Assert.That(Object.FindObjectsByType<GameObjectPool>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None).Length, Is.EqualTo(1));

            Object.Destroy(original.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Release_ReturnsInstanceToInactiveAndNextGetReusesIt()
        {
            GameObjectPool pool = GameObjectPool.Instance;
            GameObject templateObject = new GameObject("ReusablePrefab");
            templateObject.SetActive(false);
            PoolableProbe prefab = templateObject.AddComponent<PoolableProbe>();
            PoolableProbe first = pool.Get(prefab);

            pool.Release(first);

            Assert.That(first.ReturnedCount, Is.EqualTo(1));
            Assert.That(first.gameObject.activeSelf, Is.False);
            Assert.That(first.transform.parent.name, Is.EqualTo("InActive"));

            PoolableProbe second = pool.Get(prefab);

            Assert.That(second, Is.SameAs(first));
            Assert.That(second.gameObject.activeSelf, Is.True);
            Assert.That(second.TakenCount, Is.EqualTo(2));
            Assert.That(second.ReturnedCount, Is.EqualTo(1));
            Assert.That(second.transform.parent.name, Is.EqualTo("Active"));

            Object.Destroy(templateObject);
            Object.Destroy(pool.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ReusedInstance_RestoresPrefabScaleAndRequestedTransform()
        {
            GameObjectPool pool = GameObjectPool.Instance;
            GameObject templateObject = new GameObject("ScaledPrefab");
            templateObject.SetActive(false);
            templateObject.transform.localScale = new Vector3(2f, 3f, 4f);
            PoolableProbe prefab = templateObject.AddComponent<PoolableProbe>();
            PoolableProbe first = pool.Get(prefab);
            first.transform.localScale = Vector3.one * 9f;
            pool.Release(first);

            Vector3 position = new Vector3(4f, 5f, 6f);
            Quaternion rotation = Quaternion.Euler(10f, 20f, 30f);
            PoolableProbe reused = pool.Get(prefab, position, rotation);

            Assert.That(reused, Is.SameAs(first));
            Assert.That(reused.transform.localScale, Is.EqualTo(templateObject.transform.localScale));
            Assert.That(reused.transform.position, Is.EqualTo(position));
            Assert.That(Quaternion.Angle(reused.transform.rotation, rotation), Is.LessThan(0.01f));

            Object.Destroy(templateObject);
            Object.Destroy(pool.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Get_ReusesInactiveInstancesInLifoOrder()
        {
            GameObjectPool pool = GameObjectPool.Instance;
            GameObject templateObject = new GameObject("LifoPrefab");
            templateObject.SetActive(false);
            PoolableProbe prefab = templateObject.AddComponent<PoolableProbe>();
            PoolableProbe first = pool.Get(prefab);
            PoolableProbe second = pool.Get(prefab);
            pool.Release(first);
            pool.Release(second);

            Assert.That(pool.Get(prefab), Is.SameAs(second));
            Assert.That(pool.Get(prefab), Is.SameAs(first));

            Object.Destroy(templateObject);
            Object.Destroy(pool.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator DifferentPrefabs_WithSameNameUseIndependentBuckets()
        {
            GameObjectPool pool = GameObjectPool.Instance;
            GameObject templateA = new GameObject("SharedName");
            GameObject templateB = new GameObject("SharedName");
            templateA.SetActive(false);
            templateB.SetActive(false);
            PoolableProbe prefabA = templateA.AddComponent<PoolableProbe>();
            PoolableProbe prefabB = templateB.AddComponent<PoolableProbe>();

            PoolableProbe instanceA = pool.Get(prefabA);
            PoolableProbe instanceB = pool.Get(prefabB);

            Assert.That(instanceA, Is.Not.SameAs(instanceB));
            Assert.That(instanceA.transform.parent.parent, Is.Not.SameAs(instanceB.transform.parent.parent));
            Assert.That(pool.transform.childCount, Is.EqualTo(2));

            Object.Destroy(templateA);
            Object.Destroy(templateB);
            Object.Destroy(pool.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ClearPool_DestroysOnlyInactiveInstancesAndKeepsActiveUsable()
        {
            GameObjectPool pool = GameObjectPool.Instance;
            GameObject templateObject = new GameObject("ClearablePrefab");
            templateObject.SetActive(false);
            PoolableProbe prefab = templateObject.AddComponent<PoolableProbe>();
            PoolableProbe active = pool.Get(prefab);
            PoolableProbe inactive = pool.Get(prefab);
            pool.Release(inactive);

            int destroyedCount = pool.ClearPool(prefab);

            Assert.That(destroyedCount, Is.EqualTo(1));
            Assert.That(active.gameObject.activeSelf, Is.True);
            Assert.That(active.transform.parent.name, Is.EqualTo("Active"));
            Assert.That(pool.transform.childCount, Is.EqualTo(1));
            yield return null;
            Assert.That(inactive == null, Is.True);

            Assert.DoesNotThrow(() => pool.Release(active));
            Assert.That(active.transform.parent.name, Is.EqualTo("InActive"));

            Object.Destroy(templateObject);
            Object.Destroy(pool.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Get_RejectsPoolableComponentThatIsNotOnTemplateRoot()
        {
            GameObjectPool pool = GameObjectPool.Instance;
            GameObject templateRoot = new GameObject("TemplateRoot");
            GameObject child = new GameObject("PoolableChild");
            child.transform.SetParent(templateRoot.transform);
            PoolableProbe childProbe = child.AddComponent<PoolableProbe>();

            Assert.Throws<System.ArgumentException>(() => pool.Get(childProbe));

            Object.Destroy(templateRoot);
            Object.Destroy(pool.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Release_RejectsInstanceThatWasNotCreatedByThisPool()
        {
            GameObjectPool pool = GameObjectPool.Instance;
            GameObject foreignObject = new GameObject("ForeignInstance");
            PoolableProbe foreign = foreignObject.AddComponent<PoolableProbe>();

            Assert.Throws<System.InvalidOperationException>(() => pool.Release(foreign));

            Object.Destroy(foreignObject);
            Object.Destroy(pool.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Release_RejectsAnInstanceThatWasAlreadyReturned()
        {
            GameObjectPool pool = GameObjectPool.Instance;
            GameObject templateObject = new GameObject("DoubleReleasePrefab");
            templateObject.SetActive(false);
            PoolableProbe prefab = templateObject.AddComponent<PoolableProbe>();
            PoolableProbe instance = pool.Get(prefab);
            pool.Release(instance);

            Assert.Throws<System.InvalidOperationException>(() => pool.Release(instance));

            Object.Destroy(templateObject);
            Object.Destroy(pool.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ClearPool_RemovesBucketHierarchyWhenNoActiveInstancesRemain()
        {
            GameObjectPool pool = GameObjectPool.Instance;
            GameObject templateObject = new GameObject("EmptyBucketPrefab");
            templateObject.SetActive(false);
            PoolableProbe prefab = templateObject.AddComponent<PoolableProbe>();
            PoolableProbe instance = pool.Get(prefab);
            pool.Release(instance);

            int destroyedCount = pool.ClearPool(prefab);
            yield return null;

            Assert.That(destroyedCount, Is.EqualTo(1));
            Assert.That(pool.transform.childCount, Is.Zero);

            Object.Destroy(templateObject);
            Object.Destroy(pool.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator NullArguments_AreRejectedByPublicOperations()
        {
            GameObjectPool pool = GameObjectPool.Instance;
            PoolableProbe missing = null;

            Assert.Throws<System.ArgumentNullException>(() => pool.Get(missing));
            Assert.Throws<System.ArgumentNullException>(() => pool.Release(missing));
            Assert.Throws<System.ArgumentNullException>(() => pool.ClearPool(missing));

            Object.Destroy(pool.gameObject);
            yield return null;
        }
    }
}
