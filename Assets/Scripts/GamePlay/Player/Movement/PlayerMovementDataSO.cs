using System;
using UnityEngine;
namespace GamePlay.Player.Movement
{
    [CreateAssetMenu(fileName = "PlayerMovementDataSO", menuName = "Scriptable Objects/PlayerMovementDataSO")]
    public class PlayerMovementDataSO : ScriptableObject
    {
        [SerializeField]private float _moveSpeed;
        [SerializeField]private float _jumpForce;
        [SerializeField,Range(1,999)]private int _jumpTimes;

        public float MoveSpeed=>_moveSpeed;
        public float JumpForce=>_jumpForce;
        public int JumpTimes=>_jumpTimes;
    }

}
