using System.Collections.Generic;

namespace DialogueSystem
{
    public readonly struct DialogueLine
    {
        public readonly DialogueNode Node;
        public readonly IReadOnlyList<DialogueChoice> Choices;

        public DialogueLine(DialogueNode node, IReadOnlyList<DialogueChoice> choices)
        {
            Node = node;
            Choices = choices;
        }
    }
}
