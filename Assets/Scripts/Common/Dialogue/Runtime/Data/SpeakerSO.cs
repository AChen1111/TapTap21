using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "SpeakerSO", menuName = "Dialogue/Speaker")]
    public class SpeakerSO : ScriptableObject
    {
        public string speakerName;
        public Color nameColor = Color.white;
    }
}
