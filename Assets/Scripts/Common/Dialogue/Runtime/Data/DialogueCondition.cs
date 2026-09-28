using System;

namespace DialogueSystem
{
    public enum DialogueConditionType
    {
        Always = 0,
        FlagSet = 1,
        FlagNotSet = 2,
        Equals = 3,
        NotEquals = 4
    }

    [Serializable]
    public class DialogueCondition
    {
        public DialogueConditionType type = DialogueConditionType.Always;
        public string key;
        public string value;

        public bool Evaluate(IDialogueVariableStore store)
        {
            if (store == null)
                return type == DialogueConditionType.Always || type == DialogueConditionType.FlagNotSet;

            switch (type)
            {
                case DialogueConditionType.FlagSet:
                    return store.Has(key);
                case DialogueConditionType.FlagNotSet:
                    return !store.Has(key);
                case DialogueConditionType.Equals:
                    return string.Equals(store.Get(key), value ?? string.Empty, StringComparison.Ordinal);
                case DialogueConditionType.NotEquals:
                    return !string.Equals(store.Get(key), value ?? string.Empty, StringComparison.Ordinal);
                default:
                    return true;
            }
        }
    }
}
