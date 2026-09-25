using System;
using AChen.Log;
using Common.FlagsUtility;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Player.Input
{
    public class PlayerInput : MonoBehaviour
    {
        private GameInput _input;
        [SerializeField]private bool _isAutoEnable=true;

        [SerializeField,ReadOnly]private EInputState _inputState;


        public event Action<Vector2> OnInputMove;
        public event Action OnInputJump;


        void Awake()
        {
            _input=new();
            if (_isAutoEnable){EnableAllInput();}
            else{DisableAllInput();}
        }
        void Update()
        {
            MovementInput();
        }

        void OnEnable()
        {
            _input.Player.Jump.started+=OnJump;
        }

        void OnDisable()
        {
            _input.Player.Jump.started-=OnJump;
        }
        void OnDestroy()
        {
            if (_input != null)
            {
                _input.Player.Disable();
                _input.Dispose();
                _input = null;
            }
        }
        [Button]
        public void EnableAllInput()
        {
            _inputState=EInputState.ALL;
            _input.Enable();
        }
        [Button]
        public void DisableAllInput()
        {
            _inputState=_inputState.Clear();
            _input.Disable();
        }
        [Button]
        public void EnableInput(EInputState state)
        {
            if (state == EInputState.None)
            {
                ALog.LogWarning("不能添加 None State");
                return;
            }
            _inputState=_inputState.Add(state);
            _input.Enable();
        }
        [Button]
        public void DisableInput(EInputState state)
        {
            _inputState=_inputState.Remove(state);
        }

        void MovementInput()
        {
            if(!_inputState.Has(EInputState.Move))return;
            var inputVec=_input.Player.Move.ReadValue<Vector2>();
            OnInputMove?.Invoke(inputVec);
        }
        void OnJump(InputAction.CallbackContext context)
        {
            if(!_inputState.Has(EInputState.Jump))return;
            OnInputJump?.Invoke();
        }

        [Button]
        void DebugShowInputStates()
        {
            ALog.Log(_inputState.ToString());
        }
    }
}

