using UnityEngine;

namespace DialogueSystem
{
    public class DialogueInteractor : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Register(Resolve(other));
        }

        void OnTriggerExit(Collider other)
        {
            Unregister(Resolve(other));
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Register(Resolve(other));
        }

        void OnTriggerExit2D(Collider2D other)
        {
            Unregister(Resolve(other));
        }

        static DialogueTrigger Resolve(Component other)
        {
            return other != null ? other.GetComponentInParent<DialogueTrigger>() : null;
        }

        static void Register(DialogueTrigger trigger)
        {
            if (trigger != null && DialogueRunner.Instance != null)
                DialogueRunner.Instance.DialogueTriggerAdd(trigger);
        }

        static void Unregister(DialogueTrigger trigger)
        {
            if (trigger != null && DialogueRunner.Instance != null)
                DialogueRunner.Instance.DialogueTriggerSub(trigger);
        }
    }
}
