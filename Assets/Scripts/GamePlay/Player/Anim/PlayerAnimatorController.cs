using UnityEngine;
namespace GamePlay.Player.Anim
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimatorController : MonoBehaviour
    {
        private Animator _animator;
        void Awake()
        {
            _animator=GetComponent<Animator>();
        }

    }

}
