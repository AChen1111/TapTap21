using System;
using System.Collections.Generic;

namespace DialogueSystem
{
    [Serializable]
    public class DialogueChoice
    {
        public string choiceText;
        public string nextNodeId;
        public List<DialogueCondition> conditions = new List<DialogueCondition>();

        public bool IsAvailable(IDialogueVariableStore store)
        {
            if (conditions == null || conditions.Count == 0)
                return true;

            for (int i = 0; i < conditions.Count; i++)
            {
                if (conditions[i] != null && !conditions[i].Evaluate(store))
                    return false;
            }

            return true;
        }
    }
}
