using System;

namespace DialogueSystem
{
    public enum DialogueSignalType
    {
        SetFlag = 0,
        ClearFlag = 1,
        Emit = 2
    }

    [Serializable]
    public class DialogueSignal
    {
        public DialogueSignalType type = DialogueSignalType.Emit;
        public string key;
        public string value;
    }
}
