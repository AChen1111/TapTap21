using System;
using UnityEngine;

namespace FXSystem
{
    /// <summary>
    /// 挂在特效 Prefab 上（也可运行时自动补）。负责粒子重启、排序和结束检测。
    /// 不使用自身 Update，由 FXManager 统一轮询。
    /// </summary>
    [DisallowMultipleComponent]
    public class FXInstance : MonoBehaviour
    {
        FXDefinition _definition;
        ParticleSystem[] _systems;
        ParticleSystemRenderer[] _renderers;
        ParticleSystem.MinMaxGradient[] _originalStartColors;
        Animator _animator;
        Canvas _overlayCanvas;
        Vector3 _baseScale;
        Transform _follow;
        Vector3 _localFollowOffset;
        bool _hasFollow;
        bool _stopping;
        float _extraTimer;
        int _generation;
        int _playGeneration;
        int _slot = -1;
        FXCompleteCallback _onComplete;

        public FXDefinition Definition => _definition;
        public int Generation => _generation;
        public int Slot => _slot;
        public bool IsLooping => _definition != null && _definition.Looping && !_stopping;
        internal bool IsStopping => _stopping;
        internal bool FollowLost => _hasFollow && _follow == null;

        internal void Bind(int slot)
        {
            _slot = slot;
        }

        internal void EnsureCached()
        {
            if (_systems != null)
                return;

            _systems = GetComponentsInChildren<ParticleSystem>(true);
            _renderers = GetComponentsInChildren<ParticleSystemRenderer>(true);
            _animator = GetComponentInChildren<Animator>(true);
            _baseScale = transform.localScale;
            _originalStartColors = new ParticleSystem.MinMaxGradient[_systems.Length];
            for (int i = 0; i < _systems.Length; i++)
                _originalStartColors[i] = _systems[i].main.startColor;
        }

        internal void Prepare(FXDefinition definition)
        {
            _definition = definition;
            EnsureCached();
        }

        internal int BeginPlay(in FXPlayParams playParams)
        {
            EnsureCached();
            _generation++;
            _stopping = false;
            _extraTimer = 0f;
            _hasFollow = playParams.Follow && playParams.Parent != null;
            _follow = _hasFollow ? playParams.Parent : null;
            _localFollowOffset = _hasFollow
                ? playParams.Parent.InverseTransformPoint(playParams.Position)
                : Vector3.zero;

            ApplyTransform(in playParams);
            ApplySorting(playParams.SortingOrderOffset, playParams.IsUI);
            ApplyColor(in playParams);

            gameObject.SetActive(true);
            RestartParticles(playParams.Speed);
            RestartAnimator(playParams.Speed);

            _onComplete = playParams.OnComplete;
            _playGeneration = _generation;
            return _generation;
        }

        internal void FollowTick()
        {
            if (!_hasFollow)
                return;
            if (_follow == null)
                return;
            transform.position = _follow.TransformPoint(_localFollowOffset);
        }

        /// <summary>
        /// Parent 模式：作为子物体跟随。Follow 模式由 Manager 每帧同步。
        /// </summary>
        internal void Attach(Transform parent, bool asChild)
        {
            if (asChild && parent != null)
            {
                var scale = transform.localScale;
                transform.SetParent(parent, true);
                transform.localScale = scale;
                _hasFollow = false;
                _follow = null;
            }
        }

        internal void DetachTo(Transform poolRoot)
        {
            _hasFollow = false;
            _follow = null;
            transform.SetParent(poolRoot, false);
        }

        internal void Stop(bool immediate)
        {
            if (immediate)
            {
                StopAndClear();
                _stopping = true;
                _extraTimer = 0f;
                return;
            }

            _stopping = true;
            if (_systems != null)
            {
                for (int i = 0; i < _systems.Length; i++)
                {
                    if (_systems[i] != null)
                        _systems[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
                }
            }

            if (_animator != null)
                _animator.speed = 0f;
        }

        internal bool IsFinished(float dt)
        {
            if (IsLooping)
                return false;

            if (_hasFollow && _follow == null)
                return true;

            bool hasParticles = _systems != null && _systems.Length > 0;
            if (hasParticles)
            {
                bool alive = false;
                for (int i = 0; i < _systems.Length; i++)
                {
                    var ps = _systems[i];
                    if (ps != null && ps.IsAlive(true))
                    {
                        alive = true;
                        break;
                    }
                }

                if (alive)
                    return false;

                return WaitExtra(dt);
            }

            if (_animator != null && _animator.enabled && _animator.runtimeAnimatorController != null)
            {
                if (_stopping)
                    return WaitExtra(dt);
                if (_animator.IsInTransition(0))
                    return false;
                var info = _animator.GetCurrentAnimatorStateInfo(0);
                if (info.normalizedTime < 1f)
                    return false;
                return WaitExtra(dt);
            }

            if (_definition != null && _definition.ExtraLifetime > 0f)
                return WaitExtra(dt);

            return true;
        }

        bool WaitExtra(float dt)
        {
            if (_definition != null && _definition.ExtraLifetime > 0f)
            {
                _extraTimer += dt;
                return _extraTimer >= _definition.ExtraLifetime;
            }

            return true;
        }

        internal void Sleep()
        {
            StopAndClear();
            RestoreColors();
            _generation++;
            _hasFollow = false;
            _follow = null;
            _stopping = false;
            _extraTimer = 0f;
            _onComplete = null;
            gameObject.SetActive(false);
        }

        internal void AddComplete(FXCompleteCallback callback)
        {
            if (callback != null)
                _onComplete += callback;
        }

        internal void RaiseComplete(FXCompleteReason reason)
        {
            var callback = _onComplete;
            _onComplete = null;
            if (callback == null)
                return;

            var handle = new FXHandle(_slot, _playGeneration);
            if (FXManager.HasInstance)
            {
                FXManager.Instance.QueueComplete(handle, callback, reason);
                return;
            }

            try
            {
                callback(handle, reason);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception, this);
            }
        }

        void OnDestroy()
        {
            RaiseComplete(FXCompleteReason.Destroyed);
            _generation++;
            if (FXManager.HasInstance)
                FXManager.Instance.NotifyExternalDestroy(this);
            _slot = -1;
        }

        void ApplyTransform(in FXPlayParams playParams)
        {
            var scale = playParams.Scale;
            if (scale == Vector3.zero)
                scale = _baseScale;
            else
                scale = Vector3.Scale(_baseScale, scale);

            if (playParams.FlipX)
                scale.x = -Mathf.Abs(scale.x);
            else
                scale.x = Mathf.Abs(scale.x);

            if (playParams.IsUI && playParams.UIScaleMultiplier > 0f)
                scale *= playParams.UIScaleMultiplier;

            transform.localScale = scale;
            transform.SetPositionAndRotation(playParams.Position, playParams.Rotation);
        }

        void ApplySorting(int orderOffset, bool isUI)
        {
            if (_definition == null)
                return;

            int order = _definition.SortingOrder + orderOffset;

            if (isUI && _definition.OverrideCanvasSorting)
            {
                if (_overlayCanvas == null && !TryGetComponent(out _overlayCanvas))
                    _overlayCanvas = gameObject.AddComponent<Canvas>();
                _overlayCanvas.enabled = true;
                _overlayCanvas.overrideSorting = true;
                _overlayCanvas.sortingOrder = order;
            }
            else if (_overlayCanvas != null)
            {
                _overlayCanvas.overrideSorting = false;
                _overlayCanvas.enabled = false;
            }

            if (_renderers == null)
                return;

            string layerName = ResolveSortingLayerName(_definition.SortingLayer);
            for (int i = 0; i < _renderers.Length; i++)
            {
                var renderer = _renderers[i];
                if (renderer == null)
                    continue;
                if (!string.IsNullOrEmpty(layerName))
                    renderer.sortingLayerName = layerName;
                renderer.sortingOrder = order;
            }
        }

        static string ResolveSortingLayerName(string requested)
        {
            if (string.IsNullOrEmpty(requested))
                return "Default";

            if (requested == "Default" || SortingLayer.NameToID(requested) != 0)
                return requested;

            var layers = SortingLayer.layers;
            string compactRequested = CompactLayerName(requested);
            for (int i = 0; i < layers.Length; i++)
            {
                if (string.Equals(CompactLayerName(layers[i].name), compactRequested, System.StringComparison.OrdinalIgnoreCase))
                    return layers[i].name;
            }

            Debug.LogWarning("[FX] Sorting Layer '" + requested + "' 在项目里不存在，已回退到 Default。Tag Manager 中的层：" + string.Join(", ", System.Array.ConvertAll(layers, l => l.name)));
            return "Default";
        }

        static string CompactLayerName(string name)
        {
            return string.IsNullOrEmpty(name) ? "" : name.Replace(" ", "");
        }

        void ApplyColor(in FXPlayParams playParams)
        {
            if (_systems == null)
                return;

            for (int i = 0; i < _systems.Length; i++)
            {
                var ps = _systems[i];
                if (ps == null)
                    continue;
                var main = ps.main;
                main.startColor = playParams.UseColor ? playParams.Color : _originalStartColors[i];
            }
        }

        void RestoreColors()
        {
            if (_systems == null || _originalStartColors == null)
                return;
            for (int i = 0; i < _systems.Length; i++)
            {
                if (_systems[i] == null)
                    continue;
                var main = _systems[i].main;
                main.startColor = _originalStartColors[i];
            }
        }

        void RestartParticles(float speed)
        {
            if (_systems == null)
                return;
            for (int i = 0; i < _systems.Length; i++)
            {
                var ps = _systems[i];
                if (ps == null)
                    continue;
                var main = ps.main;
                main.simulationSpeed = speed;
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                ps.Play(true);
            }
        }

        void RestartAnimator(float speed)
        {
            if (_animator == null || _animator.runtimeAnimatorController == null)
                return;
            _animator.enabled = true;
            _animator.speed = speed;
            _animator.Play(0, -1, 0f);
            _animator.Update(0f);
        }

        void StopAndClear()
        {
            if (_systems == null)
                return;
            for (int i = 0; i < _systems.Length; i++)
            {
                if (_systems[i] != null)
                    _systems[i].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
    }
}
