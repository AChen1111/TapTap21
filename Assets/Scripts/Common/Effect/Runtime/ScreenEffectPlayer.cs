using System;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    /// <summary>
    /// 挂在摄像机上。Blit 效果进入 ScreenEffectFeature 的列表，描边仍由 SimpleOutline 单独绘制。
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class ScreenEffectPlayer : MonoBehaviour
    {
        public readonly struct OutlineRequest
        {
            public readonly LayerMask layerMask;
            public readonly Color color;
            public readonly float width;

            public OutlineRequest(LayerMask layerMask, Color color, float width)
            {
                this.layerMask = layerMask;
                this.color = color;
                this.width = width;
            }
        }

        static readonly int IntensityId = Shader.PropertyToID("_Intensity");

        readonly List<Active> _playing = new List<Active>();
        readonly List<Action> _finished = new List<Action>();

        class Active
        {
            public ScreenEffectProfile profile;
            public float time;
            public Material material;
            public Action onComplete;
        }

        void LateUpdate()
        {
            _finished.Clear();
            Tick(Time.deltaTime);
            for (int i = 0; i < _finished.Count; i++)
                _finished[i].Invoke();
        }

        void OnDestroy()
        {
            Stop();
        }

        public void Play(ScreenEffectProfile profile, Action onComplete = null)
        {
            if (profile == null)
                return;

            if (profile.kind == ScreenEffectKind.Blit && profile.material == null)
            {
                Debug.LogWarning($"屏幕效果 {profile.name} 没有材质。", profile);
                return;
            }

            Stop(profile);

            var active = new Active
            {
                profile = profile,
                time = 0f,
                onComplete = onComplete,
            };

            if (profile.kind == ScreenEffectKind.Blit)
            {
                active.material = new Material(profile.material)
                {
                    name = profile.name,
                    hideFlags = HideFlags.HideAndDontSave,
                };
                ApplyIntensity(active);
            }

            _playing.Add(active);
        }

        public void Stop()
        {
            for (int i = _playing.Count - 1; i >= 0; i--)
                Release(_playing[i]);
            _playing.Clear();
        }

        public void Stop(ScreenEffectProfile profile)
        {
            if (profile == null)
                return;

            for (int i = _playing.Count - 1; i >= 0; i--)
            {
                if (_playing[i].profile != profile)
                    continue;

                Release(_playing[i]);
                _playing.RemoveAt(i);
            }
        }


        public void CopyBlitMaterials(List<Material> destination)
        {
            destination.Clear();
            for (int i = 0; i < _playing.Count; i++)
            {
                Active active = _playing[i];
                if (active.profile == null || active.profile.kind != ScreenEffectKind.Blit)
                    continue;
                if (active.material == null)
                    continue;
                destination.Add(active.material);
            }
        }

        void Tick(float deltaTime)
        {
            for (int i = _playing.Count - 1; i >= 0; i--)
            {
                Active active = _playing[i];
                float duration = active.profile.duration;
                if (duration > 0f)
                {
                    active.time += deltaTime;
                    if (active.time >= duration)
                    {
                        if (active.profile.loop)
                            active.time %= duration;
                        else
                        {
                            active.time = duration;
                            ApplyIntensity(active);
                            if (active.onComplete != null)
                                _finished.Add(active.onComplete);
                            Release(active);
                            _playing.RemoveAt(i);
                            continue;
                        }
                    }
                }

                ApplyIntensity(active);
            }
        }

        static void ApplyIntensity(Active active)
        {
            if (active.material == null)
                return;

            float duration = active.profile.duration;
            float t = duration <= 0f ? 1f : Mathf.Clamp01(active.time / duration);
            active.material.SetFloat(IntensityId, Evaluate(active.profile.intensity, t));
        }

        static void Release(Active active)
        {
            if (active.material == null)
                return;

            if (Application.isPlaying)
                Destroy(active.material);
            else
                DestroyImmediate(active.material);
            active.material = null;
        }

        static float Evaluate(AnimationCurve curve, float time)
        {
            if (curve == null || curve.length == 0)
                return 0f;
            return curve.Evaluate(time);
        }
    }

}

