using AChen.Log;
using GamePlay.Gravity;
using NUnit.Framework;
using Player.Input;
using Unity.Mathematics;
using UnityEngine;
namespace Player.Movement
{
    [RequireComponent(typeof(PlayerInput),typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private Rigidbody2D _rb2d;
        [SerializeField]private int _jumpTimes;
        [SerializeField]private Collider2D _foot;
        [SerializeField]private PlayerMovementDataSO _movementDataSO;
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
            int gDir=(int)GravityService.Instance.Direction;
            _rb2d.linearVelocity=new Vector2(_rb2d.linearVelocity.x,0);
            _rb2d.AddForce(Vector2.up*_movementDataSO.JumpForce*gDir,ForceMode2D.Impulse);            
            _jumpTimes--;
        }
        void Move(Vector2 moveDir)
        {
            _rb2d.linearVelocity=new Vector2( moveDir.x*_movementDataSO.MoveSpeed,_rb2d.linearVelocity.y);
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
        void OnTriggerStay2D(Collider2D other)
        {
            if (!IsFootOnGround(other)) return;
            OnTouchGround();
        }
        void OnTriggerExit2D(Collider2D other)
        {
            if (_foot == null || !other.CompareTag("Ground")) return;
            if (_foot.IsTouching(other)) return;
            OnLeaveGround();
        }

        bool IsFootOnGround(Collider2D other)
        {
            return _foot != null && other.CompareTag("Ground") && _foot.IsTouching(other);
        }


    }
}

