using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueInteractUI : MonoBehaviour
    {
        public TextMeshProUGUI textMeshPro;
        public Image image;

        public void Bind(string label, bool selected)
        {
            if (textMeshPro != null)
                textMeshPro.text = label;
            SetSelected(selected);
        }

        public void SetSelected(bool selected)
        {
            if (image != null)
                image.color = selected ? Color.red : Color.white;
        }
    }
}
