using AChen.Log;
using GamePlay.Gravity;
using GamePlay.Player.Input;
using GamePlay.Environment.Platform;
using Unity.Mathematics;
using UnityEngine;
namespace GamePlay.Player.Movement
{
    [RequireComponent(typeof(PlayerInput),typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private Rigidbody2D _rb2d;
        [SerializeField]private int _jumpTimes;
        [SerializeField]private Collider2D _foot;
        [SerializeField]private PlayerMovementDataSO _movementDataSO;
        private Collider2D _platformCollider;
        private float _carriedPlatformVelocityX;
        private float _lastPlatformVelocityX;
        private bool _preserveCarry;
        private bool _wasOnPlatform;
        private bool _leftPlatform;
        private bool _dropRequested;
        /// <summary>实际收到的移动输入，供动画区分主动行走与被平台搬运。</summary>
        public Vector2 MoveInput { get; private set; }
        public float LastJumpTime { get; private set; } = float.NegativeInfinity;
        public float AnimationMoveSpeed => Mathf.Abs(_rb2d.linearVelocity.x -
            (_platformCollider != null && _platformCollider.attachedRigidbody != null
                ? _platformCollider.attachedRigidbody.linearVelocity.x
                : _preserveCarry ? _carriedPlatformVelocityX : 0f));
        void Awake()
        {
            _playerInput=GetComponent<PlayerInput>();
            _rb2d=GetComponent<Rigidbody2D>();
            if (_movementDataSO == null)
            {
                ALog.LogError("没有给PlayerMovement挂载PlayerMovementDataSO!");
                return;
            }
            if (_foot == null)
            {
                ALog.LogError("没有给PlayerMovement指定脚部Collider!");
            }
            if (_foot.isTrigger == false)
            {
                ALog.LogError("Player Foot碰撞箱必须是触发器!");
            }
            _jumpTimes=_movementDataSO.JumpTimes;
        }

        void Update()
        {
        }
        void OnEnable()
        {
            _playerInput.OnInputJump += Jump;  
            _playerInput.OnInputMove += Move;
        }
        void OnDisable()
        {
            MoveInput = Vector2.zero;
            _playerInput.OnInputJump -= Jump;
            _playerInput.OnInputMove -= Move;
        }

        void Jump()
        {
            if(_jumpTimes<=0)return;
            LastJumpTime = Time.time;
            if (TryGetPlatformVelocityX(out float platformX))
            {
                _carriedPlatformVelocityX = platformX;
                _preserveCarry = true;
            }
            else if (_wasOnPlatform)
            {
                _carriedPlatformVelocityX = _lastPlatformVelocityX;
                _preserveCarry = true;
            }
            _rb2d.linearVelocity=new Vector2(_rb2d.linearVelocity.x,0);
            _rb2d.AddForce(Vector2.up*_movementDataSO.JumpForce,ForceMode2D.Impulse);            
            _jumpTimes--;
        }
        void Move(Vector2 moveDir)
        {
            MoveInput = moveDir;
            TryDropThrough(moveDir);
            bool onPlatform = TryGetPlatformVelocityX(out float platformX);
            float extraX;
            if (onPlatform)
            {
                _lastPlatformVelocityX = platformX;
                extraX = platformX;
            }
            else if (_preserveCarry)
            {
                extraX = _carriedPlatformVelocityX;
            }
            else
            {
                extraX = 0f;
            }
            if (_wasOnPlatform && !onPlatform)
            {
                _leftPlatform = true;
            }
            _wasOnPlatform = onPlatform;
            float speedX = moveDir.x*_movementDataSO.MoveSpeed+extraX;
            _rb2d.linearVelocity=new Vector2(speedX,_rb2d.linearVelocity.y);
        }

        void OnTouchGround()
        {
            _jumpTimes=_movementDataSO.JumpTimes;
        }

        private void TryDropThrough(Vector2 moveDir)
        {
            if (moveDir.y > -0.5f)
            {
                _dropRequested = false;
                return;
            }
            if (_dropRequested || _platformCollider == null) return;
            OneWayPlatform2D platform = _platformCollider.GetComponentInParent<OneWayPlatform2D>();
            if (platform == null) return;

            _dropRequested = true;
            platform.DropThrough(gameObject);
            _platformCollider = null;
            _wasOnPlatform = false;
            _preserveCarry = false;
            _jumpTimes = Mathf.Max(0, _movementDataSO.JumpTimes - 1);
        }
        /// <summary>重生时清理旧平台惯性并恢复跳跃次数。</summary>
        public void ResetAfterRespawn()
        {
            _platformCollider = null;
            _carriedPlatformVelocityX = 0f;
            _lastPlatformVelocityX = 0f;
            _preserveCarry = false;
            _wasOnPlatform = false;
            _leftPlatform = false;
            if (_movementDataSO != null) _jumpTimes = _movementDataSO.JumpTimes;
        }
        void OnLeaveGround()
        {
            int walkedOff = _movementDataSO.JumpTimes - 1;
            if (_jumpTimes > walkedOff)
            {
                _jumpTimes = walkedOff;
            }
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            TryLand(other);
            if (IsFootOnPlatform(other))
            {
                _platformCollider = other;
            }
        }
        void OnTriggerStay2D(Collider2D other)
        {
            TryLand(other);
            if (IsFootOnPlatform(other))
            {
                _platformCollider = other;
            }
            if (!IsFootOnGround(other)) return;
            OnTouchGround();
        }
        void OnTriggerExit2D(Collider2D other)
        {
            if (_platformCollider == other && (_foot == null || !_foot.IsTouching(other)))
            {
                _platformCollider = null;
                if (_preserveCarry)
                {
                    _leftPlatform = true;
                }
            }
            if (_foot == null || !other.CompareTag("Ground")) return;
            if (_foot.IsTouching(other)) return;
            OnLeaveGround();
        }

        bool IsFootOnGround(Collider2D other)
        {
            if (_foot == null || !other.CompareTag("Ground") || !_foot.IsTouching(other)) return false;
            OneWayPlatform2D platform = other.GetComponentInParent<OneWayPlatform2D>();
            if (platform == null) return true;
            if (_rb2d.linearVelocity.y > 0.05f) return false;
            return _foot.bounds.min.y >= other.bounds.max.y - 0.08f;
        }

        bool IsFootOnPlatform(Collider2D other)
        {
            if (_foot == null || other == null || other.isTrigger) return false;
            OneWayPlatform2D oneWayPlatform = other.GetComponentInParent<OneWayPlatform2D>();
            if (oneWayPlatform != null) return _foot.IsTouching(other);
            Rigidbody2D body=other.attachedRigidbody;
            if (body == null || body == _rb2d) return false;
            return _foot.IsTouching(other);
        }

        bool TryGetPlatformVelocityX(out float velocityX)
        {
            velocityX = 0f;
            if (_platformCollider == null || _foot == null) return false;
            if (!_foot.IsTouching(_platformCollider))
            {
                _platformCollider = null;
                return false;
            }
            Rigidbody2D body = _platformCollider.attachedRigidbody;
            if (body == null) return false;
            velocityX = body.linearVelocity.x;
            return true;
        }

        void TryLand(Collider2D other)
        {
            if (!_leftPlatform) return;
            if (_foot == null || other == null || other.isTrigger) return;
            if (!_foot.IsTouching(other)) return;
            _leftPlatform = false;
            _dropRequested = false;
            _preserveCarry = false;
            _carriedPlatformVelocityX = 0f;
        }


    }
}

