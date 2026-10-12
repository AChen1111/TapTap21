using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FXSystem
{
    [DefaultExecutionOrder(-1000)]
    [DisallowMultipleComponent]
    public class FXManager : MonoBehaviour
    {
        public const string SettingsResourceName = "FXSettings";

        static FXManager _instance;

        [SerializeField] FXSettings settings;

        readonly Dictionary<string, FXDefinition> _definitions = new Dictionary<string, FXDefinition>();
        readonly Dictionary<FXDefinition, FXPool> _pools = new Dictionary<FXDefinition, FXPool>();
        readonly List<FXInstance> _slots = new List<FXInstance>(64);
        readonly Stack<int> _freeSlots = new Stack<int>(32);
        readonly List<PendingComplete> _pendingCompletes = new List<PendingComplete>(16);

        struct PendingComplete
        {
            public FXCompleteCallback Callback;
            public FXHandle Handle;
            public FXCompleteReason Reason;
        }

        Canvas _uiCanvas;
        RectTransform _uiContent;
        bool _warnedOverlay;
        int _activeCount;

        public static FXManager Instance => _instance;
        public static bool HasInstance => _instance != null;
        public FXSettings Settings => settings;
        public Canvas UICanvas
        {
            get
            {
                EnsureUICanvas();
                return _uiCanvas;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Bootstrap()
        {
            if (_instance != null)
                return;

            var go = new GameObject("[FXManager]");
            DontDestroyOnLoad(go);
            go.AddComponent<FXManager>();
        }

        public static bool IsHandleValid(in FXHandle handle)
        {
            if (_instance == null || handle.Generation == 0)
                return false;
            if ((uint)handle.Slot >= (uint)_instance._slots.Count)
                return false;
            var inst = _instance._slots[handle.Slot];
            return inst != null && inst.Generation == handle.Generation && inst.isActiveAndEnabled;
        }

        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
            BuildCatalog();
            PrewarmAll();
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
            if (_instance == this)
                _instance = null;
        }

        void LateUpdate()
        {
            float dt = Time.deltaTime;
            foreach (var pair in _pools)
            {
                var active = pair.Value.ActiveInstances;
                for (int i = active.Count - 1; i >= 0; i--)
                {
                    var inst = active[i];
                    inst.FollowTick();
                    if (inst.IsFinished(dt))
                    {
                        FXCompleteReason reason;
                        if (inst.IsStopping)
                            reason = FXCompleteReason.Stopped;
                        else if (inst.FollowLost)
                            reason = FXCompleteReason.Recycled;
                        else
                            reason = FXCompleteReason.Finished;
                        Recycle(inst, reason);
                    }
                }
            }

            FlushCompletes();
        }

        public FXHandle Play(string id, in FXPlayParams playParams)
        {
            if (!TryGetDefinition(id, out var def))
            {
                NotifyDropped(in playParams);
                return FXHandle.Invalid;
            }
            return Play(def, in playParams);
        }

        public FXHandle Play(FXDefinition definition, in FXPlayParams playParams)
        {
            if (definition == null)
            {
                Warn("Play 传入了空的 FXDefinition。");
                NotifyDropped(in playParams);
                return FXHandle.Invalid;
            }

            if (definition.Prefab == null)
            {
                Warn($"FX '{definition.ResolvedId}' 没有 Prefab。");
                NotifyDropped(in playParams);
                return FXHandle.Invalid;
            }

            var pool = GetOrCreatePool(definition);
            var p = playParams;
            p.Sanitize();

            if (definition.Space == FXSpace.UI)
                p.IsUI = true;

            if (p.IsUI && !PrepareUIPlay(definition, ref p))
            {
                NotifyDropped(in p);
                return FXHandle.Invalid;
            }

            int before = pool.ActiveCount;
            if (!pool.TryRent(out var instance, definition.Overflow, _activeCount, EffectiveGlobalMax()))
            {
                Warn($"FX '{definition.ResolvedId}' 播放被丢弃（池已满）。");
                NotifyDropped(in p);
                return FXHandle.Invalid;
            }

            BindSlot(instance);
            int generation = instance.BeginPlay(in p);
            if (p.Parent != null && !p.Follow)
                instance.Attach(p.Parent, true);

            if (pool.ActiveCount > before)
                _activeCount++;

            var handle = new FXHandle(instance.Slot, generation);
            FlushCompletes();
            return handle;
        }

        public void Stop(in FXHandle handle, bool immediate)
        {
            if (!TryGetInstance(handle, out var instance))
                return;

            if (immediate)
            {
                Recycle(instance, FXCompleteReason.Stopped);
                FlushCompletes();
                return;
            }

            instance.Stop(false);
            FlushCompletes();
        }

        public void StopAll(bool immediate = true)
        {
            foreach (var pair in _pools)
            {
                var active = pair.Value.ActiveInstances;
                if (immediate)
                {
                    pair.Value.ReturnAll();
                }
                else
                {
                    for (int i = 0; i < active.Count; i++)
                        active[i].Stop(false);
                }
            }

            if (immediate)
                _activeCount = 0;

            FlushCompletes();
        }

        public void StopAll(FXSpace space, bool immediate = true)
        {
            foreach (var pair in _pools)
            {
                if (pair.Key == null || pair.Key.Space != space)
                    continue;

                if (immediate)
                    pair.Value.ReturnAll();
                else
                {
                    var active = pair.Value.ActiveInstances;
                    for (int i = 0; i < active.Count; i++)
                        active[i].Stop(false);
                }
            }

            _activeCount = 0;
            foreach (var pair in _pools)
                _activeCount += pair.Value.ActiveCount;

            FlushCompletes();
        }

        /// <summary>
        /// 把 UI 特效挂到项目已有 Canvas 下。建议 Screen Space - Camera。
        /// </summary>
        public void BindCanvas(Canvas canvas)
        {
            if (canvas == null)
            {
                Warn("BindCanvas 传入了空 Canvas。");
                return;
            }

            _uiCanvas = canvas;
            var host = canvas.GetComponent<FXCanvasHost>();
            if (host != null && host.Content != null)
            {
                _uiContent = host.Content;
            }
            else
            {
                var existing = canvas.transform.Find("FXUIRoot") as RectTransform;
                if (existing != null)
                {
                    _uiContent = existing;
                }
                else
                {
                    var contentGo = new GameObject("FXUIRoot", typeof(RectTransform));
                    contentGo.layer = canvas.gameObject.layer;
                    _uiContent = contentGo.GetComponent<RectTransform>();
                    _uiContent.SetParent(canvas.transform, false);
                    FXCanvasUtility.Stretch(_uiContent);
                }

                if (host == null)
                    host = canvas.gameObject.AddComponent<FXCanvasHost>();
                host.Canvas = canvas;
                host.Content = _uiContent;
            }

            FXCanvasUtility.BindCamera(_uiCanvas);
            if (_uiCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
                Warn("绑定的 Canvas 是 Overlay，Particle System 通常不会显示。可用 Animator/Image，或改为 Screen Space - Camera。");
        }

        public void Prewarm(string id)
        {
            if (TryGetDefinition(id, out var def))
                Prewarm(def);
        }

        public void Prewarm(FXDefinition definition)
        {
            if (definition == null)
                return;
            GetOrCreatePool(definition).Prewarm();
        }

        public void PrewarmAll()
        {
            foreach (var pair in _definitions)
                GetOrCreatePool(pair.Value).Prewarm();
        }

        /// <summary>
        /// 运行时登记一条特效（Demo 或动态生成的 Definition）。同 Id 会覆盖查找表。
        /// </summary>
        public void RegisterDefinition(FXDefinition definition, bool prewarm = true)
        {
            if (definition == null)
            {
                Warn("RegisterDefinition 传入了空配置。");
                return;
            }

            if (definition.Prefab == null)
            {
                Warn($"FX '{definition.ResolvedId}' 没有 Prefab，无法登记。");
                return;
            }

            string id = definition.ResolvedId;
            if (_definitions.ContainsKey(id))
                Warn($"Id '{id}' 已存在，登记时将覆盖查找表。");

            _definitions[id] = definition;
            if (prewarm)
                GetOrCreatePool(definition).Prewarm();
        }

        public bool TryGetDefinition(string id, out FXDefinition definition)
        {
            definition = null;
            if (string.IsNullOrEmpty(id))
            {
                Warn("Play 使用了空 Id。");
                return false;
            }

            if (_definitions.TryGetValue(id, out definition))
                return true;

            Warn($"未找到 Id 为 '{id}' 的特效。请检查 Catalog。");
            return false;
        }

        internal void Subscribe(in FXHandle handle, FXCompleteCallback callback)
        {
            if (callback == null)
                return;

            if (TryGetInstance(handle, out var instance))
            {
                instance.AddComplete(callback);
                return;
            }

            callback(handle, FXCompleteReason.Recycled);
        }

        internal void QueueComplete(FXHandle handle, FXCompleteCallback callback, FXCompleteReason reason)
        {
            if (callback == null)
                return;
            _pendingCompletes.Add(new PendingComplete
            {
                Callback = callback,
                Handle = handle,
                Reason = reason
            });
        }

        void FlushCompletes()
        {
            for (int i = 0; i < _pendingCompletes.Count; i++)
            {
                var pending = _pendingCompletes[i];
                if (pending.Callback == null)
                    continue;
                try
                {
                    pending.Callback(pending.Handle, pending.Reason);
                }
                catch (System.Exception exception)
                {
                    Debug.LogException(exception, this);
                }
            }

            _pendingCompletes.Clear();
        }

        static void NotifyDropped(in FXPlayParams playParams)
        {
            var callback = playParams.OnComplete;
            if (callback == null)
                return;

            try
            {
                callback(FXHandle.Invalid, FXCompleteReason.Dropped);
            }
            catch (System.Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        internal void NotifyExternalDestroy(FXInstance instance)
        {
            if (instance == null)
                return;

            foreach (var pair in _pools)
            {
                if (pair.Key == instance.Definition)
                {
                    pair.Value.NotifyDestroyed(instance);
                    break;
                }
            }

            FreeSlot(instance);
            if (_activeCount > 0)
                _activeCount--;
            FlushCompletes();
        }

        void Recycle(FXInstance instance, FXCompleteReason reason)
        {
            if (instance == null || instance.Definition == null)
                return;
            if (!_pools.TryGetValue(instance.Definition, out var pool))
                return;

            instance.RaiseComplete(reason);
            bool wasActive = instance.isActiveAndEnabled;
            pool.Return(instance);
            if (wasActive && _activeCount > 0)
                _activeCount--;
        }

        void BindSlot(FXInstance instance)
        {
            if (instance.Slot >= 0)
            {
                while (_slots.Count <= instance.Slot)
                    _slots.Add(null);
                _slots[instance.Slot] = instance;
                return;
            }

            int slot = _freeSlots.Count > 0 ? _freeSlots.Pop() : _slots.Count;
            if (slot == _slots.Count)
                _slots.Add(instance);
            else
                _slots[slot] = instance;
            instance.Bind(slot);
        }

        void FreeSlot(FXInstance instance)
        {
            int slot = instance.Slot;
            if (slot < 0 || slot >= _slots.Count)
                return;
            if (_slots[slot] == instance)
                _slots[slot] = null;
            _freeSlots.Push(slot);
        }

        bool TryGetInstance(in FXHandle handle, out FXInstance instance)
        {
            instance = null;
            if (!IsHandleValid(handle))
                return false;
            instance = _slots[handle.Slot];
            return true;
        }

        FXPool GetOrCreatePool(FXDefinition definition)
        {
            if (_pools.TryGetValue(definition, out var pool))
                return pool;

            Transform root = transform;
            if (definition.Space == FXSpace.UI)
            {
                EnsureUICanvas();
                if (_uiContent != null)
                    root = _uiContent;
            }

            pool = new FXPool(definition, root);
            _pools.Add(definition, pool);
            return pool;
        }

        bool PrepareUIPlay(FXDefinition definition, ref FXPlayParams playParams)
        {
            EnsureUICanvas();
            if (_uiCanvas == null || _uiContent == null)
            {
                Warn("无法准备 UI Canvas。");
                return false;
            }

            if (_uiCanvas.renderMode == RenderMode.ScreenSpaceOverlay && !_warnedOverlay)
            {
                _warnedOverlay = true;
                Warn("当前 FX Canvas 为 Overlay，Particle System 通常不可见。请改用 Screen Space - Camera，或使用 Image/Animator。");
            }

            playParams.IsUI = true;
            playParams.UIScaleMultiplier = definition.UIScale > 0f ? definition.UIScale : 1f;
            if (definition.Space != FXSpace.UI)
            {
                float lossy = _uiCanvas.transform.lossyScale.x;
                if (lossy > 1e-5f)
                    playParams.UIScaleMultiplier = playParams.UIScaleMultiplier / lossy;
            }

            if (playParams.UseScreenPosition)
            {
                if (!FXCanvasUtility.TryScreenToCanvasWorld(_uiCanvas, playParams.ScreenPosition, out var world))
                {
                    Warn("屏幕坐标无法映射到 Canvas。");
                    return false;
                }

                playParams.Position = world;
            }
            else if (playParams.Parent == null)
            {
                if (!FXCanvasUtility.TryWorldToCanvasWorld(_uiCanvas, playParams.Position, playParams.WorldCamera, out var world))
                {
                    Warn("世界坐标无法映射到 Canvas。");
                    return false;
                }

                playParams.Position = world;
            }

            if (playParams.Parent == null)
                playParams.Parent = _uiContent;

            return true;
        }

        void EnsureUICanvas()
        {
            if (_uiCanvas != null && _uiContent != null)
            {
                FXCanvasUtility.BindCamera(_uiCanvas);
                return;
            }

            var host = FindFirstObjectByType<FXCanvasHost>();
            if (host != null && host.Canvas != null)
            {
                _uiCanvas = host.Canvas;
                _uiContent = host.Content != null ? host.Content : host.transform as RectTransform;
                FXCanvasUtility.BindCamera(_uiCanvas);
                return;
            }

            _uiCanvas = FXCanvasUtility.CreateDefaultCanvas(settings, transform);
            var created = _uiCanvas != null ? _uiCanvas.GetComponent<FXCanvasHost>() : null;
            _uiContent = created != null ? created.Content : null;
        }

        void LoadSettings()
        {
            if (settings == null)
                settings = Resources.Load<FXSettings>(SettingsResourceName);
        }

        void BuildCatalog()
        {
            _definitions.Clear();
            if (settings == null || settings.Catalog == null || settings.Catalog.Definitions == null)
            {
                Warn("未找到 FXSettings / Catalog。请使用菜单 FX/Create Default Settings。");
                return;
            }

            var defs = settings.Catalog.Definitions;
            for (int i = 0; i < defs.Length; i++)
            {
                var def = defs[i];
                if (def == null)
                    continue;
                string id = def.ResolvedId;
                if (_definitions.ContainsKey(id))
                    Warn($"Catalog 中存在重复 Id '{id}'，后写入的会覆盖前者。");
                _definitions[id] = def;
            }
        }

        void OnActiveSceneChanged(Scene previous, Scene next)
        {
            if (settings != null && !settings.ClearOnActiveSceneChange)
                return;

            FXCanvasUtility.BindCamera(_uiCanvas);

            foreach (var pair in _pools)
            {
                if (pair.Key != null && !pair.Key.RecycleOnSceneChange)
                    continue;
                pair.Value.ReturnAll();
            }

            _activeCount = 0;
            foreach (var pair in _pools)
                _activeCount += pair.Value.ActiveCount;

            FlushCompletes();
        }

        int EffectiveGlobalMax()
        {
            return settings != null ? Mathf.Max(1, settings.GlobalMaxActive) : 256;
        }

        void Warn(string message)
        {
            if (settings == null || settings.LogWarnings)
                Debug.LogWarning("[FX] " + message, this);
        }
    }
}
