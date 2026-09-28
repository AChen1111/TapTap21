using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public enum DialogueNodeType
    {
        Line = 0,
        Branch = 1
    }

    [Serializable]
    public class DialogueNode
    {
        public string id;
        public string title;
        public SpeakerSO speaker;
        [TextArea(4, 12)]
        public string text;
        public string textId;
        public string nextNodeId;
        public Vector2 editorPosition;
        public DialogueNodeType nodeType = DialogueNodeType.Line;
        public List<DialogueChoice> choices = new List<DialogueChoice>();
        public List<DialogueSignal> enterSignals = new List<DialogueSignal>();

        public string ResolveText()
        {
            return string.IsNullOrEmpty(text) ? textId : text;
        }

        public bool HasChoices()
        {
            return choices != null && choices.Count > 0;
        }

        public bool IsBranch => nodeType == DialogueNodeType.Branch;
    }
}
