using GamePlay.Player.Health;
using GamePlay.Player.Movement;
using GamePlay.Wind;
using UnityEngine;
namespace GamePlay.Player.Anim
{
    /// <summary>根据接地、移动输入、竖直速度和受风情况切换已制作的 Sprite 动画。</summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class PlayerAnimatorController : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private string _idleState = "Idle";
        [SerializeField] private string _runState = "Run";
        [SerializeField] private string _jumpState = "Frame";
        [SerializeField] private string _fallState = "Fall";
        [SerializeField] private string _flyState = "Fly";
        [SerializeField] private bool _artFacesRight = true;
        [SerializeField, Min(0f)] private float _verticalThreshold = 0.1f;
        [SerializeField, Min(0.01f)] private float _runReferenceSpeed = 2.5f;
        [SerializeField, Min(0f)] private float _groundGraceTime = 0.06f;
        [SerializeField, Min(0f)] private float _apexHoldTime = 0.06f;
        private float _lastSupportedTime = float.NegativeInfinity;
        private float _fallStartedTime = float.NegativeInfinity;
        private float _observedJumpTime = float.NegativeInfinity;
        private float _jumpPlaybackSpeed = 1f;
        private float _jumpClipLength = 0.4f;
        private readonly ContactPoint2D[] _contacts = new ContactPoint2D[32];
        private Rigidbody2D _body;
        private PlayerMovement _movement;
        private PlayerHealth _health;
        private WindBody2D _windBody;
        private int _currentState;
        private bool _hasState;
        private bool _flyingOverride;

        public string CurrentState { get; private set; }
        public bool IsGrounded { get; private set; }
        public bool IsRunning => _hasState && CurrentState == _runState;

        void Awake()
        {
            _animator=GetComponent<Animator>();
            _body = GetComponent<Rigidbody2D>();
            _movement = GetComponent<PlayerMovement>();
            _health = GetComponent<PlayerHealth>();
            _windBody = GetComponent<WindBody2D>();
            if (_spriteRenderer == null) _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (_animator.runtimeAnimatorController != null)
            {
                foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
                {
                    if (clip.name == _jumpState) _jumpClipLength = clip.length;
                }
            }
        }

        private void OnEnable()
        {
            _hasState = false;
        }

        private void LateUpdate()
        {
            if (_animator.runtimeAnimatorController == null) return;
            // 暂无死亡动画：死亡期间冻结当前画面，重生后恢复。
            bool dead = _health != null && _health.IsDead;
            _animator.speed = dead ? 0f : 1f;
            if (dead) return;

            Vector2 input = _movement != null && _movement.isActiveAndEnabled
                ? _movement.MoveInput : Vector2.zero;
            if (_spriteRenderer != null && Mathf.Abs(input.x) > 0.01f)
            {
                _spriteRenderer.flipX = (input.x < 0f) == _artFacesRight;
            }

            bool jumped = _movement != null && _movement.LastJumpTime != _observedJumpTime;
            if (jumped)
            {
                _observedJumpTime = _movement.LastJumpTime;
                _lastSupportedTime = float.NegativeInfinity;
                _hasState = false;
                float acceleration = Mathf.Abs(Physics2D.gravity.y * _body.gravityScale);
                float riseTime = acceleration > 0.01f ? Mathf.Max(0.1f, _body.linearVelocity.y / acceleration) : _jumpClipLength;
                _jumpPlaybackSpeed = Mathf.Clamp(_jumpClipLength / riseTime, 0.5f, 3f);
            }
            if (HasSupportingContact()) _lastSupportedTime = Time.time;
            IsGrounded = Time.time - _lastSupportedTime <= _groundGraceTime &&
                (_movement == null || Time.time - _movement.LastJumpTime > 0.08f);
            bool flying = _flyingOverride || (_windBody != null && _windBody.isActiveAndEnabled &&
                _windBody.GetForce().sqrMagnitude > 0.01f);
            float moveSpeed = _movement != null ? _movement.AnimationMoveSpeed : Mathf.Abs(_body.linearVelocity.x);
            bool running = Mathf.Abs(input.x) > 0.01f && moveSpeed > 0.05f;
            bool rising = _body.linearVelocity.y > _verticalThreshold;
            if (rising || IsGrounded) _fallStartedTime = float.NegativeInfinity;
            else if (float.IsNegativeInfinity(_fallStartedTime)) _fallStartedTime = Time.time;
            bool holdJump = CurrentState == _jumpState && Time.time - _fallStartedTime < _apexHoldTime;
            string state = IsGrounded
                ? (running ? _runState : _idleState)
                : flying ? _flyState
                : rising || holdJump ? _jumpState : _fallState;
            PlayState(state);
            _animator.speed = state == _runState ? Mathf.Clamp(moveSpeed / _runReferenceSpeed, 0.15f, 2.5f)
                : state == _jumpState ? _jumpPlaybackSpeed : 1f;
        }

        private bool HasSupportingContact()
        {
            if (!_body.simulated || _body.linearVelocity.y > _verticalThreshold) return false;
            int count = _body.GetContacts(_contacts);
            for (int i = 0; i < count; i++)
            {
                if (Vector2.Dot(_contacts[i].normal, Vector2.up) > 0.5f) return true;
            }
            return false;
        }

        private void PlayState(string state)
        {
            int hash = Animator.StringToHash("Base Layer." + state);
            if (_hasState && hash == _currentState) return;
            if (!_animator.HasState(0, hash)) return;
            _animator.Play(hash, 0, 0f);
            _currentState = hash;
            _hasState = true;
            CurrentState = state;
        }

        /// <summary>供后续飞行机关调用；关闭后继续使用移动和受风自动判断。</summary>
        public void SetFlying(bool flying) => _flyingOverride = flying;
    }

}
