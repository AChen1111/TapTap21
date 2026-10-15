using System;
using System.Collections;
using GamePlay.Player.Movement;
using TapTap21.SaveSystem.dyh;
using UnityEngine;

namespace GamePlay.Player.Health
{
    /// <summary>挂在玩家根节点：维护生命、JSON 留存和同场景死亡重生。</summary>
    [DisallowMultipleComponent]
    public sealed class PlayerHealth : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _maxHealth = 3;
        [SerializeField, Min(0)] private int _currentHealth = 3;
        [SerializeField] private Transform _respawnPoint;
        [SerializeField] private bool _loadSaveOnAwake = true;
        [SerializeField] private bool _saveOnChange = true;
        [SerializeField] private bool _autoRespawn = true;
        [SerializeField, Min(0f)] private float _respawnDelay = 0.5f;

        private Rigidbody2D _rigidbody;
        private Vector3 _defaultRespawnPosition;
        private Quaternion _defaultRespawnRotation;
        private bool _isDead;
        private Coroutine _respawnRoutine;
        private PlayerMovement _movement;
        private bool _movementWasEnabled;
        private bool _wasSimulated;
        private bool _restoreAtRespawn;

        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;
        public bool IsDead => _isDead;
        public event Action<int, int> HealthChanged;
        public event Action Died;
        public event Action Respawned;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _movement = GetComponent<PlayerMovement>();
            _maxHealth = Mathf.Max(1, _maxHealth);
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            _defaultRespawnPosition = transform.position;
            _defaultRespawnRotation = transform.rotation;
            if (_loadSaveOnAwake && SaveSystem.TryLoad(SaveDefinitions.Player, SaveSlots.Slot0, out PlayerSaveData data))
            {
                _currentHealth = Mathf.Clamp(data.health, 0, _maxHealth);
                _restoreAtRespawn = data.respawnPending || _currentHealth <= 0;
                if (_restoreAtRespawn) _currentHealth = _maxHealth;
            }
        }

        private void Start()
        {
            if (_restoreAtRespawn)
            {
                Respawn();
                return;
            }
            NotifyHealthChanged();
        }

        private void OnDisable()
        {
            if (_respawnRoutine != null)
            {
                StopCoroutine(_respawnRoutine);
                _respawnRoutine = null;
            }
        }

        private void OnEnable()
        {
            if (_isDead && _autoRespawn)
            {
                _respawnRoutine = StartCoroutine(RespawnAfterDelay());
            }
        }

        /// <summary>一次有效攻击扣血；大嘴花调用 TakeDamage(1)。</summary>
        public void TakeDamage(int amount)
        {
            if (_isDead || amount <= 0) return;
            SetHealth(_currentHealth - amount);
        }

        public void Heal(int amount)
        {
            if (_isDead || amount <= 0) return;
            SetHealth(_currentHealth + amount);
        }

        /// <summary>直接死亡；水体调用此方法，重复调用不会重复触发死亡。</summary>
        public void Kill()
        {
            if (_isDead) return;
            _isDead = true;
            _currentHealth = 0;
            _movementWasEnabled = _movement != null && _movement.enabled;
            if (_movement != null) _movement.enabled = false;
            if (_rigidbody != null)
            {
                _wasSimulated = _rigidbody.simulated;
                _rigidbody.linearVelocity = Vector2.zero;
                _rigidbody.angularVelocity = 0f;
                _rigidbody.simulated = false;
            }
            NotifyHealthChanged();
            SaveHealth();
            Died?.Invoke();
            if (_autoRespawn && _isDead && isActiveAndEnabled)
            {
                _respawnRoutine = StartCoroutine(RespawnAfterDelay());
            }
        }

        /// <summary>回到配置点并恢复满血、速度和移动状态。</summary>
        public void Respawn()
        {
            if (_respawnRoutine != null)
            {
                StopCoroutine(_respawnRoutine);
                _respawnRoutine = null;
            }
            Vector3 position = _respawnPoint != null ? _respawnPoint.position : _defaultRespawnPosition;
            Quaternion rotation = _respawnPoint != null ? _respawnPoint.rotation : _defaultRespawnRotation;
            if (_rigidbody != null)
            {
                _rigidbody.linearVelocity = Vector2.zero;
                _rigidbody.angularVelocity = 0f;
                _rigidbody.position = position;
                _rigidbody.rotation = rotation.eulerAngles.z;
                transform.position = position;
                if (_isDead) _rigidbody.simulated = _wasSimulated;
            }
            else transform.SetPositionAndRotation(position, rotation);
            if (_movement != null)
            {
                _movement.ResetAfterRespawn();
                if (_isDead) _movement.enabled = _movementWasEnabled;
            }
            _isDead = false;
            _currentHealth = _maxHealth;
            NotifyHealthChanged();
            SaveHealth();
            Respawned?.Invoke();
        }

        public void SetRespawnPoint(Transform point) => _respawnPoint = point;

        public void SetHealth(int value)
        {
            if (_isDead) return;
            int next = Mathf.Clamp(value, 0, _maxHealth);
            if (next == 0)
            {
                Kill();
                return;
            }
            if (next == _currentHealth) return;
            _currentHealth = next;
            NotifyHealthChanged();
            SaveHealth();
        }

        private void NotifyHealthChanged() => HealthChanged?.Invoke(_currentHealth, _maxHealth);

        private void SaveHealth()
        {
            if (!_saveOnChange) return;
            var loaded = SaveSystem.Load(SaveDefinitions.Player, SaveSlots.Slot0);
            if (!loaded.Success && loaded.Status != SaveStatus.NotFound)
            {
                Debug.LogError("[PlayerHealth] 无法读取原玩家存档，保留文件：" + loaded.Message, this);
                return;
            }
            PlayerSaveData data = loaded.Success ? loaded.Data : new PlayerSaveData();
            data.health = _isDead ? 0 : _currentHealth;
            data.respawnPending = _isDead;
            var saved = SaveSystem.Save(SaveDefinitions.Player, SaveSlots.Slot0, data);
            if (!saved.Success) Debug.LogError("[PlayerHealth] 保存失败：" + saved.Message, this);
        }

        private IEnumerator RespawnAfterDelay()
        {
            yield return new WaitForSecondsRealtime(_respawnDelay);
            _respawnRoutine = null;
            Respawn();
        }
    }
}
