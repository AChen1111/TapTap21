using AChen.Log;
using GamePlay.Gravity;
using NUnit.Framework;
using GamePlay.Player.Input;
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
            _playerInput.OnInputJump -= Jump;
            _playerInput.OnInputMove -= Move;
        }

        void Jump()
        {
            if(_jumpTimes<=0)return;
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
            return _foot != null && other.CompareTag("Ground") && _foot.IsTouching(other);
        }

        bool IsFootOnPlatform(Collider2D other)
        {
            if (_foot == null || other == null || other.isTrigger) return false;
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
            _preserveCarry = false;
            _carriedPlatformVelocityX = 0f;
        }


    }
}

