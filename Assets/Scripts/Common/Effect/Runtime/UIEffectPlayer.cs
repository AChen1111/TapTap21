using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Effects
{
    /// <summary>
    /// 把 SpriteEffectProfile 写到 UI Graphic 上。会换成 Effects/UIEffects，遮罩用的模板参数仍由 Unity UI 写入。
    /// 文字请用 TMPEffectPlayer。
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Graphic))]
    public class UIEffectPlayer : MonoBehaviour
    {
        Graphic _graphic;
        Material _previous;
        Material _instance;
        readonly List<EffectPlayback.Active> _playing = new List<EffectPlayback.Active>();
        readonly List<Action> _finished = new List<Action>();

        void OnEnable()
        {
            _graphic = GetComponent<Graphic>();
            if (GetComponent<TMP_Text>() != null)
            {
                Debug.LogWarning("文字请使用 TMPEffectPlayer。", this);
                return;
            }

            Shader shader = Shader.Find("Effects/UIEffects");
            if (shader == null)
            {
                Debug.LogWarning("找不到 Effects/UIEffects。", this);
                return;
            }

            Material source = _graphic.material;
            _previous = source == _graphic.defaultMaterial ? null : source;
            _instance = new Material(shader)
            {
                name = "UIEffects (Instance)",
                hideFlags = HideFlags.HideAndDontSave,
            };
            _graphic.material = _instance;
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
            if (_instance != null && _graphic != null)
                _graphic.material = _previous;
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
            if (_instance == null || _graphic == null)
                return;

            EffectPlayback.Values values = EffectPlayback.Evaluate(_playing);
            EffectPlayback.Write(_instance, values);

            Material current = _graphic.canvasRenderer != null ? _graphic.canvasRenderer.GetMaterial() : null;
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

