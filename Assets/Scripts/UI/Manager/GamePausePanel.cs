using AChen.Events;
using AChen.UI;
using TapTap21.AudioSystem.dyh;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.Manager
{
    /// <summary>由 UIManager 注册到 UIFrame 的游戏工具栏、暂停、设置与退出确认。</summary>
    public sealed class GamePausePanel : APanelController
    {
        private GameObject _shade;
        private RectTransform _content;
        private Font _font;
        private float _previousTimeScale;
        private bool _paused;
        private global::GamePlay.Player.Input.PlayerInput _input;
        private bool _inputWasEnabled;
        public bool IsPaused => _paused;

        protected override void Awake()
        {
            base.Awake();
            _font = Font.CreateDynamicFontFromOSFont(new[] { "Microsoft YaHei", "Arial" }, 24);
            var root = (RectTransform)transform;
            root.anchorMin = Vector2.zero;
            root.anchorMax = Vector2.one;
            root.offsetMin = root.offsetMax = Vector2.zero;
            var tools = Rect("GameTools", transform, new Vector2(1, 1), new Vector2(-180, -35), new Vector2(320, 50));
            Button(tools, "设置", new Vector2(-80, 0), OpenSettings);
            Button(tools, "退出", new Vector2(80, 0), OpenExit);
            _shade = new GameObject("PauseOverlay", typeof(RectTransform), typeof(Image));
            _shade.transform.SetParent(transform, false);
            var shadeRect = (RectTransform)_shade.transform;
            shadeRect.anchorMin = Vector2.zero;
            shadeRect.anchorMax = Vector2.one;
            shadeRect.offsetMin = shadeRect.offsetMax = Vector2.zero;
            _shade.GetComponent<Image>().color = new Color(0, 0, 0, 0.65f);
            _content = Rect("Dialog", _shade.transform, new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(520, 650));
            _content.gameObject.AddComponent<Image>().color = new Color(0.12f, 0.16f, 0.16f, 1);
            _shade.SetActive(false);
        }

        private void Update()
        {
            if (Keyboard.current == null || !Keyboard.current.escapeKey.wasPressedThisFrame) return;
            var sceneManager = FindFirstObjectByType<GameSceneManager>();
            if (sceneManager != null && sceneManager.IsLoading) return;
            if (_paused) Resume(); else OpenPause();
        }

        public void OpenPause()
        {
            if (!BeginDialog("暂停")) return;
            Button(_content, "继续游戏", new Vector2(0, 100), Resume);
            Button(_content, "设置", new Vector2(0, 20), OpenSettings);
            Button(_content, "退出游戏", new Vector2(0, -60), OpenExit);
        }

        public void OpenSettings()
        {
            if (!BeginDialog("设置")) return;
            var service = AudioSettingsService.Instance;
            if (service != null)
            {
                AudioVolumeType[] types = { AudioVolumeType.Master, AudioVolumeType.Music, AudioVolumeType.Sfx, AudioVolumeType.Voice, AudioVolumeType.Ambience, AudioVolumeType.UI };
                string[] names = { "总音量", "音乐", "音效", "语音", "环境", "界面" };
                for (int i = 0; i < types.Length; i++) VolumeRow(types[i], names[i], 185 - i * 56, service);
            }
            else Label(_content, "音量服务尚未加载", new Vector2(0, 140), new Vector2(400, 40));
            var toggleRect = Rect("Fullscreen", _content, new Vector2(0.5f, 0.5f), new Vector2(0, -175), new Vector2(370, 40));
            var toggle = toggleRect.gameObject.AddComponent<Toggle>();
            var box = Rect("Box", toggleRect, new Vector2(0, 0.5f), new Vector2(18, 0), new Vector2(30, 30)).gameObject.AddComponent<Image>();
            box.color = new Color(0.25f, 0.3f, 0.3f);
            var mark = Rect("Check", box.transform, new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(20, 20)).gameObject.AddComponent<Image>();
            mark.color = new Color(0.4f, 0.8f, 0.6f);
            toggle.targetGraphic = box;
            toggle.graphic = mark;
            toggle.SetIsOnWithoutNotify(Screen.fullScreen);
            toggle.onValueChanged.AddListener(value => Screen.fullScreen = value);
            Label(toggleRect, "全屏", new Vector2(20, 0), new Vector2(260, 40));
            Button(_content, "返回", new Vector2(0, -255), OpenPause);
        }

        public void OpenExit()
        {
            if (!BeginDialog("退出游戏？")) return;
            Button(_content, "取消", new Vector2(0, 30), OpenPause);
            Button(_content, "退出游戏", new Vector2(0, -50), () =>
            {
                Resume();
                EventCenter.Dispatch(GameEvent.OnGameExitBtnClicked);
            });
        }

        private bool BeginDialog(string title)
        {
            var manager = FindFirstObjectByType<GameSceneManager>();
            if (manager != null && manager.IsLoading) return false;
            if (!_paused)
            {
                _previousTimeScale = Time.timeScale;
                _input = manager != null && manager.Player != null
                    ? manager.Player.GetComponent<global::GamePlay.Player.Input.PlayerInput>() : null;
                _inputWasEnabled = _input != null && _input.enabled;
                if (_input != null) _input.enabled = false;
                Time.timeScale = 0;
                _paused = true;
            }
            foreach (Transform child in _content)
            {
                child.gameObject.SetActive(false);
                Destroy(child.gameObject);
            }
            _shade.SetActive(true);
            transform.SetAsLastSibling();
            Label(_content, title, new Vector2(0, 270), new Vector2(460, 60), 30);
            return true;
        }

        public void Resume()
        {
            if (!_paused) return;
            Time.timeScale = _previousTimeScale;
            if (_input != null) _input.enabled = _inputWasEnabled;
            _paused = false;
            if (_shade != null) _shade.SetActive(false);
        }

        private void OnDisable() => Resume();
        protected override void OnDestroy()
        {
            Resume();
            if (_font != null) Destroy(_font);
            base.OnDestroy();
        }

        private void VolumeRow(AudioVolumeType type, string name, float y, AudioSettingsService service)
        {
            Label(_content, name, new Vector2(-140, y), new Vector2(110, 35), 22);
            var rect = Rect(name + "Slider", _content, new Vector2(0.5f, 0.5f), new Vector2(45, y), new Vector2(230, 30));
            var slider = rect.gameObject.AddComponent<Slider>();
            var track = Rect("Track", rect, new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(230, 8)).gameObject.AddComponent<Image>();
            track.color = new Color(0.25f, 0.3f, 0.3f);
            var fillArea = Rect("FillArea", rect, new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(210, 8));
            var fill = Rect("Fill", fillArea, new Vector2(0.5f, 0.5f), Vector2.zero, Vector2.zero);
            fill.gameObject.AddComponent<Image>().color = new Color(0.4f, 0.8f, 0.6f);
            var handleArea = Rect("HandleArea", rect, new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(210, 30));
            var handle = Rect("Handle", handleArea, new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(18, 26));
            var handleImage = handle.gameObject.AddComponent<Image>();
            slider.fillRect = fill;
            slider.handleRect = handle;
            slider.targetGraphic = handleImage;
            slider.direction = Slider.Direction.LeftToRight;
            slider.SetValueWithoutNotify(service.GetVolume(type));
            slider.onValueChanged.AddListener(value => service.SetVolume(type, value));
        }

        private RectTransform Rect(string name, Transform parent, Vector2 anchor, Vector2 position, Vector2 size)
        {
            var rect = (RectTransform)new GameObject(name, typeof(RectTransform)).transform;
            rect.SetParent(parent, false);
            rect.anchorMin = rect.anchorMax = anchor;
            rect.anchoredPosition = position;
            rect.sizeDelta = size;
            return rect;
        }

        private void Button(Transform parent, string text, Vector2 position, UnityEngine.Events.UnityAction action)
        {
            var rect = Rect(text, parent, new Vector2(0.5f, 0.5f), position, new Vector2(145, 48));
            var image = rect.gameObject.AddComponent<Image>();
            image.color = new Color(0.24f, 0.36f, 0.32f);
            var button = rect.gameObject.AddComponent<Button>();
            button.targetGraphic = image;
            button.onClick.AddListener(action);
            Label(rect, text, Vector2.zero, new Vector2(145, 48), 22);
        }

        private Text Label(Transform parent, string text, Vector2 position, Vector2 size, int fontSize = 24)
        {
            var label = Rect(text + "Label", parent, new Vector2(0.5f, 0.5f), position, size).gameObject.AddComponent<Text>();
            label.font = _font;
            label.fontSize = fontSize;
            label.text = text;
            label.alignment = TextAnchor.MiddleCenter;
            label.color = Color.white;
            label.raycastTarget = false;
            return label;
        }
    }
}
