using GamePlay.Player.Health;
using UnityEngine;
using UnityEngine.UI;
using AChen.UI;
using UnityEngine.SceneManagement;

namespace UI.GamePlay.HUD.Health
{
    /// <summary>现有 UIFrame 管理的三个心 HUD，订阅玩家生命变化。</summary>
    [RequireComponent(typeof(RectTransform))]
    public sealed class HealthBarView : APanelController
    {
        [SerializeField] private Image[] _hearts = new Image[3];
        [SerializeField] private Sprite[] _heartSprites = new Sprite[3];
        [SerializeField] private Color _fullColor = Color.white;
        [SerializeField] private Color _emptyColor = new Color(1f, 1f, 1f, 0.25f);
        private PlayerHealth _playerHealth;

        protected override void Awake()
        {
            base.Awake();
            EnsureHeartSlots();
        }

        protected override void OnOpen() => Bind(FindFirstObjectByType<PlayerHealth>());
        protected override void OnResume() => Bind(FindFirstObjectByType<PlayerHealth>());

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            Bind(FindFirstObjectByType<PlayerHealth>());
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Unbind();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => Bind(FindFirstObjectByType<PlayerHealth>());

        private void Update()
        {
            if (_playerHealth == null)
            {
                Bind(FindFirstObjectByType<PlayerHealth>());
            }
        }

        protected override void OnDestroy()
        {
            Unbind();
            base.OnDestroy();
        }

        public void Bind(PlayerHealth playerHealth)
        {
            if (_playerHealth == playerHealth) { Refresh(); return; }
            Unbind();
            _playerHealth = playerHealth;
            if (_playerHealth == null) { Refresh(0, 3); return; }
            _playerHealth.HealthChanged += OnHealthChanged;
            Refresh();
        }

        private void Unbind()
        {
            if (_playerHealth == null) return;
            _playerHealth.HealthChanged -= OnHealthChanged;
            _playerHealth = null;
        }

        private void OnHealthChanged(int current, int max) => Refresh(current, max);
        private void Refresh() => Refresh(_playerHealth == null ? 0 : _playerHealth.CurrentHealth, _playerHealth == null ? 3 : _playerHealth.MaxHealth);

        private void Refresh(int current, int max)
        {
            for (int i = 0; i < _hearts.Length; i++)
            {
                if (_hearts[i] == null) continue;
                _hearts[i].enabled = i < max;
                _hearts[i].color = i < current ? _fullColor : _emptyColor;
            }
        }

        private void EnsureHeartSlots()
        {
            if (_hearts == null || _hearts.Length != 3) _hearts = new Image[3];
            RectTransform root = transform as RectTransform;
            for (int i = 0; i < 3; i++)
            {
                if (_hearts[i] == null)
                {
                    Transform child = root.Find($"Heart_{i + 1}");
                    bool created = false;
                    if (child == null)
                    {
                        GameObject heart = new GameObject($"Heart_{i + 1}", typeof(RectTransform), typeof(Image));
                        child = heart.transform;
                        child.SetParent(root, false);
                        created = true;
                    }
                    _hearts[i] = child.GetComponent<Image>();
                    if (created)
                    {
                        RectTransform createdRect = _hearts[i].rectTransform;
                        createdRect.anchorMin = new Vector2(0f, 1f);
                        createdRect.anchorMax = new Vector2(0f, 1f);
                        createdRect.pivot = new Vector2(0f, 1f);
                        createdRect.sizeDelta = new Vector2(48f, 48f);
                        createdRect.anchoredPosition = new Vector2(24f + i * 56f, -24f);
                    }
                }
                RectTransform rect = _hearts[i].rectTransform;
                _hearts[i].raycastTarget = false;
                if (_heartSprites != null && i < _heartSprites.Length && _heartSprites[i] != null) _hearts[i].sprite = _heartSprites[i];
            }
        }
    }
}
