using System;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public static class EffectPlayback
    {
        public class Active
        {
            public SpriteEffectProfile profile;
            public float time;
            public Action onComplete;
            public bool finished;
        }

        public struct Values
        {
            public float dissolve;
            public float flash;
            public float distort;
            public float edgeWidth;
            public float noiseScale;
            public float noiseFromTexture;
            public Color flashColor;
            public Color edgeColor;
            public Texture noise;
        }

        static readonly int DissolveAmountId = Shader.PropertyToID("_DissolveAmount");
        static readonly int DissolveEdgeWidthId = Shader.PropertyToID("_DissolveEdgeWidth");
        static readonly int DissolveEdgeColorId = Shader.PropertyToID("_DissolveEdgeColor");
        static readonly int FlashColorId = Shader.PropertyToID("_FlashColor");
        static readonly int FlashAmountId = Shader.PropertyToID("_FlashAmount");
        static readonly int DistortAmountId = Shader.PropertyToID("_DistortAmount");
        static readonly int NoiseTexId = Shader.PropertyToID("_NoiseTex");
        static readonly int NoiseScaleId = Shader.PropertyToID("_NoiseScale");
        static readonly int NoiseFromTextureId = Shader.PropertyToID("_NoiseFromTexture");

        public static void Play(List<Active> playing, SpriteEffectProfile profile, Action onComplete = null)
        {
            if (profile == null)
                return;
            
            Stop(playing, profile);
            playing.Add(new Active
            {
                profile = profile,
                time = 0f,
                onComplete = onComplete,
            });
        }

        public static void Stop(List<Active> playing)
        {
            playing.Clear();
        }

        public static void Stop(List<Active> playing, SpriteEffectProfile profile)
        {
            if (profile == null)
                return;

            for (int i = playing.Count - 1; i >= 0; i--)
            {
                if (playing[i].profile == profile)
                    playing.RemoveAt(i);
            }
        }

        public static void Tick(List<Active> playing, float deltaTime, List<Action> finished)
        {
            for (int i = playing.Count - 1; i >= 0; i--)
            {
                Active active = playing[i];
                if (active.finished)
                    continue;

                float duration = active.profile.duration;
                if (duration <= 0f)
                    continue;

                active.time += deltaTime;
                if (active.time < duration)
                    continue;

                if (active.profile.loop)
                {
                    active.time %= duration;
                    continue;
                }

                active.time = duration;
                active.finished = true;
                if (active.onComplete != null)
                    finished.Add(active.onComplete);
            }
        }

        public static void RemoveFinished(List<Active> playing)
        {
            for (int i = playing.Count - 1; i >= 0; i--)
            {
                if (playing[i].finished)
                    playing.RemoveAt(i);
            }
        }

        public static Values Evaluate(List<Active> playing)
        {
            var values = new Values
            {
                edgeWidth = 0.08f,
                noiseScale = 4f,
                flashColor = Color.white,
                edgeColor = new Color(1f, 0.45f, 0.1f, 1f),
            };

            for (int i = 0; i < playing.Count; i++)
            {
                SpriteEffectProfile profile = playing[i].profile;
                float t = NormalizedTime(playing[i]);
                values.dissolve = Evaluate(profile.dissolve, t);
                values.flash = Evaluate(profile.flash, t);
                values.distort = Evaluate(profile.distort, t);
                values.flashColor = profile.flashColor;
                values.edgeColor = profile.dissolveEdgeColor;
                values.edgeWidth = profile.dissolveEdgeWidth;
                values.noiseScale = profile.noiseScale;
                if (profile.noise != null)
                {
                    values.noise = profile.noise;
                    values.noiseFromTexture = 1f;
                }
                else
                {
                    values.noise = null;
                    values.noiseFromTexture = 0f;
                }
            }

            return values;
        }

        public static void Write(MaterialPropertyBlock block, Values values)
        {
            block.SetFloat(DissolveAmountId, values.dissolve);
            block.SetFloat(DissolveEdgeWidthId, values.edgeWidth);
            block.SetColor(DissolveEdgeColorId, values.edgeColor);
            block.SetColor(FlashColorId, values.flashColor);
            block.SetFloat(FlashAmountId, values.flash);
            block.SetFloat(DistortAmountId, values.distort);
            block.SetFloat(NoiseScaleId, values.noiseScale);
            block.SetFloat(NoiseFromTextureId, values.noiseFromTexture);
            if (values.noise != null)
                block.SetTexture(NoiseTexId, values.noise);
        }

        public static void Write(Material material, Values values)
        {
            if (material == null)
                return;

            material.SetFloat(DissolveAmountId, values.dissolve);
            material.SetFloat(DissolveEdgeWidthId, values.edgeWidth);
            material.SetColor(DissolveEdgeColorId, values.edgeColor);
            material.SetColor(FlashColorId, values.flashColor);
            material.SetFloat(FlashAmountId, values.flash);
            material.SetFloat(DistortAmountId, values.distort);
            material.SetFloat(NoiseScaleId, values.noiseScale);
            material.SetFloat(NoiseFromTextureId, values.noiseFromTexture);
            if (values.noise != null)
                material.SetTexture(NoiseTexId, values.noise);
        }

        static float NormalizedTime(Active active)
        {
            float duration = active.profile.duration;
            if (duration <= 0f)
                return 1f;
            return Mathf.Clamp01(active.time / duration);
        }

        static float Evaluate(AnimationCurve curve, float time)
        {
            if (curve == null || curve.length == 0)
                return 0f;
            return curve.Evaluate(time);
        }
    }
}



    