using UnityEngine;

namespace DialogueSystem
{
    public class DialogueTrigger : MonoBehaviour
    {
        [HideInInspector] public DialogueNodeSO dialogueConfigSO;
        [SerializeField] DialogueGraphSO graph;
        [SerializeField] string overrideEntryId;
        [SerializeField] string displayName;

        public DialogueGraphSO Graph => graph;
        public string OverrideEntryId => overrideEntryId;
        public string DisplayName
        {
            get
            {
                if (!string.IsNullOrEmpty(displayName))
                    return displayName;
                if (graph != null && !string.IsNullOrEmpty(graph.graphName))
                    return graph.graphName;
                return name;
            }
        }

        public void SetGraph(DialogueGraphSO value)
        {
            graph = value;
        }

        void OnValidate()
        {
            var col = GetComponent<Collider>();
            if (col != null && !col.isTrigger)
                Debug.LogWarning("DialogueTrigger on " + name + " has a Collider that is not a trigger.", this);

            var col2 = GetComponent<Collider2D>();
            if (col2 != null && !col2.isTrigger)
                Debug.LogWarning("DialogueTrigger on " + name + " has a Collider2D that is not a trigger.", this);
        }
    }
}
