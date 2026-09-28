using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueChoiceUI : MonoBehaviour
    {
        public Button button;
        public TextMeshProUGUI label;

        DialogueChoice _choice;
        System.Action<DialogueChoice> _onClicked;

        void Awake()
        {
            if (button == null)
                button = GetComponentInChildren<Button>(true);
            if (label == null)
                label = GetComponentInChildren<TextMeshProUGUI>(true);
        }

        public void Bind(DialogueChoice choice, System.Action<DialogueChoice> onClicked)
        {
            _choice = choice;
            _onClicked = onClicked;
            if (label != null)
                label.text = choice != null ? choice.choiceText : string.Empty;
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(HandleClick);
            }
        }

        void HandleClick()
        {
            _onClicked?.Invoke(_choice);
        }
    }
}
