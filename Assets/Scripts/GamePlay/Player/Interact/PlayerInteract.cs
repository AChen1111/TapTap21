using AChen.Log;
using GamePlay.Scene;
using UnityEngine;
namespace GamePlay.Player
{
    public class PlayerInteract : MonoBehaviour
    {


        void Awake()
        {
        }



        void OnTriggerEnter2D(Collider2D collision)
        {
            ICanInteract interact;
            if(collision.TryGetComponent(out interact))
            {
                interact.OnEnterInteract();
            }
        }
        void OnTriggerExit2D(Collider2D collision)
        {
            ICanInteract interact;
            if(collision.TryGetComponent(out interact))
            {
                interact.OnExitInteract();
            }
        }


    }

}
