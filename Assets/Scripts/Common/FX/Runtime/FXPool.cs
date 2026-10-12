using System.Collections.Generic;
using UnityEngine;

namespace FXSystem
{
    sealed class FXPool
    {
        readonly FXDefinition _definition;
        readonly Transform _root;
        readonly Stack<FXInstance> _inactive = new Stack<FXInstance>(8);
        readonly List<FXInstance> _active = new List<FXInstance>(8);

        public FXDefinition Definition => _definition;
        public int ActiveCount => _active.Count;
        public List<FXInstance> ActiveInstances => _active;

        public FXPool(FXDefinition definition, Transform managerRoot)
        {
            _definition = definition;
            var go = new GameObject("Pool_" + definition.ResolvedId);
            go.transform.SetParent(managerRoot, false);
            _root = go.transform;
        }

        public void Prewarm()
        {
            int target = Mathf.Max(0, _definition.PrewarmCount);
            int have = _inactive.Count + _active.Count;
            for (int i = have; i < target; i++)
            {
                var inst = Create();
                inst.Sleep();
                _inactive.Push(inst);
            }
        }

        public bool TryRent(out FXInstance instance, FXOverflowPolicy overflow, int globalActive, int globalMax)
        {
            instance = null;

            if (globalActive >= globalMax)
            {
                if (_active.Count > 0)
                    return TryRecycleOldest(out instance);
                return false;
            }

            if (_active.Count >= _definition.MaxActive)
            {
                switch (overflow)
                {
                    case FXOverflowPolicy.RecycleOldest:
                        return TryRecycleOldest(out instance);
                    case FXOverflowPolicy.Drop:
                        return false;
                    case FXOverflowPolicy.Expand:
                        break;
                }
            }

            if (_inactive.Count > 0)
            {
                instance = _inactive.Pop();
                _active.Add(instance);
                return true;
            }

            instance = Create();
            _active.Add(instance);
            return true;
        }

        public void Return(FXInstance instance)
        {
            if (instance == null)
                return;
            if (!_active.Remove(instance))
                return;

            instance.DetachTo(_root);
            instance.Sleep();
            _inactive.Push(instance);
        }

        public void ReturnAll()
        {
            for (int i = _active.Count - 1; i >= 0; i--)
            {
                var inst = _active[i];
                inst.RaiseComplete(FXCompleteReason.Recycled);
                Return(inst);
            }
        }

        public void NotifyDestroyed(FXInstance instance)
        {
            _active.Remove(instance);
        }

        bool TryRecycleOldest(out FXInstance instance)
        {
            if (_active.Count == 0)
            {
                instance = Create();
                _active.Add(instance);
                return true;
            }

            instance = _active[0];
            _active.RemoveAt(0);
            _active.Add(instance);
            instance.RaiseComplete(FXCompleteReason.Recycled);
            instance.DetachTo(_root);
            instance.Sleep();
            return true;
        }

        FXInstance Create()
        {
            if (_definition.Prefab == null)
                throw new System.InvalidOperationException($"FX '{_definition.ResolvedId}' 未指定 Prefab。");

            var go = Object.Instantiate(_definition.Prefab, _root);
            go.name = _definition.Prefab.name;
            if (!go.TryGetComponent(out FXInstance instance))
                instance = go.AddComponent<FXInstance>();
            instance.Prepare(_definition);
            return instance;
        }
    }
}
