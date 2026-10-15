using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Effects
{
    /// <summary>
    /// 把 SpriteEffectProfile 写到 TextMesh Pro 上。会复制当前字体材质，并换成 Effects/TMPEffects。
    /// 字体图集、字色、描边厚度会留下来。
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Text))]
    public class TMPEffectPlayer : MonoBehaviour
    {
        TMP_Text _text;
        Material _previous;
        Material _instance;
        readonly List<EffectPlayback.Active> _playing = new List<EffectPlayback.Active>();
        readonly List<Action> _finished = new List<Action>();

        void OnEnable()
        {
            _text = GetComponent<TMP_Text>();
            Shader shader = Shader.Find("Effects/TMPEffects");
            if (shader == null)
            {
                Debug.LogWarning("找不到 Effects/TMPEffects。", this);
                return;
            }

            _previous = _text.fontSharedMaterial;
            if (_previous == null)
            {
                Debug.LogWarning("TMPEffectPlayer 需要先指定字体材质。", this);
                return;
            }

            _instance = new Material(_previous)
            {
                name = "TMPEffects (Instance)",
                hideFlags = HideFlags.HideAndDontSave,
                shader = shader,
                shaderKeywords = _previous.shaderKeywords,
            };
            _text.fontSharedMaterial = _instance;
        }

        void LateUpdate()
        {
            if (_instance == null)
                return;

            _finished.Clear();
            EffectPlayback.Tick(_playing, Time.deltaTime, _finished);
            Apply();
            EffectPlayback.RemoveFinished(_playing);
            InvokeFinished();
        }

        void OnDisable()
        {
            EffectPlayback.Stop(_playing);
            if (_text != null && _previous != null)
                _text.fontSharedMaterial = _previous;
            DestroyMaterial(_instance);
            _instance = null;
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
            if (_instance == null || _text == null)
                return;

            EffectPlayback.Values values = EffectPlayback.Evaluate(_playing);
            EffectPlayback.Write(_instance, values);

            Material current = _text.materialForRendering;
            if (current != null && current != _instance)
                EffectPlayback.Write(current, values);
        }

        void InvokeFinished()
        {
            for (int i = 0; i < _finished.Count; i++)
                _finished[i].Invoke();
        }

        static void DestroyMaterial(Material material)
        {
            if (material == null)
                return;

            if (Application.isPlaying)
                Destroy(material);
            else
                DestroyImmediate(material);
        }
    }

}

