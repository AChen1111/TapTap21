using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueNodeSO : ScriptableObject
    {
        public string dialogueName;
        public string id;
        public SpeakerSO speakerSO;
        [TextArea(5, 10)]
        public string text;
        public string nextNodeId;
        public Vector2 editorPosition;
        public string textId;
        public List<DialogueChoice> choices = new List<DialogueChoice>();
    }
}
