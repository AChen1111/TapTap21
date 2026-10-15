using System;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    /// <summary>
    /// 把 SpriteEffectProfile 写到这个物体的 SpriteRenderer 上。
    /// 材质需要使用 Effects/SpriteEffects。多个效果同时播放时，同名属性以后播的为准。
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteEffectPlayer : MonoBehaviour
    {
        static readonly int MainTexId = Shader.PropertyToID("_MainTex");

        SpriteRenderer _renderer;
        MaterialPropertyBlock _block;
        readonly List<EffectPlayback.Active> _playing = new List<EffectPlayback.Active>();
        readonly List<Action> _finished = new List<Action>();

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _block = new MaterialPropertyBlock();

            Material material = _renderer.sharedMaterial;
            if (material == null || !material.HasProperty("_DissolveAmount"))
                Debug.LogWarning("SpriteEffectPlayer 需要使用 Effects/SpriteEffects 材质。", this);
        }

        void LateUpdate()
        {
            _finished.Clear();
            EffectPlayback.Tick(_playing, Time.deltaTime, _finished);
            Apply();
            EffectPlayback.RemoveFinished(_playing);
            InvokeFinished();
        }

        void OnDisable()
        {
            EffectPlayback.Stop(_playing);
            Apply();
        }

        public void Play(SpriteEffectProfile profile, Action onComplete = null)
        {
            EffectPlayback.Play(_playing, profile, onComplete);
            Apply();
        }

        public void Stop()
        {
            EffectPlayback.Stop(_playing);
            Apply();
        }

        public void Stop(SpriteEffectProfile profile)
        {
            EffectPlayback.Stop(_playing, profile);
            Apply();
        }

        void Apply()
        {
            if (_renderer == null)
                return;

            _renderer.GetPropertyBlock(_block);
            EffectPlayback.Write(_block, EffectPlayback.Evaluate(_playing));

            if (_renderer.sprite != null && _renderer.sprite.texture != null)
                _block.SetTexture(MainTexId, _renderer.sprite.texture);

            _renderer.SetPropertyBlock(_block);
        }

        void InvokeFinished()
        {
            for (int i = 0; i < _finished.Count; i++)
                _finished[i].Invoke();
        }
    }
}

