using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "DialogueGraph", menuName = "Dialogue/Dialogue Graph")]
    public class DialogueGraphSO : ScriptableObject
    {
        public string graphName;
        public string entryNodeId;
        public List<DialogueNode> nodes = new List<DialogueNode>();

        Dictionary<string, DialogueNode> _index;

        public DialogueNode FindNode(string nodeId)
        {
            if (string.IsNullOrEmpty(nodeId))
                return null;

            EnsureIndex();
            _index.TryGetValue(nodeId, out var node);
            return node;
        }

        public DialogueNode GetEntryNode(string overrideId = null)
        {
            var node = FindNode(string.IsNullOrEmpty(overrideId) ? entryNodeId : overrideId);
            if (node != null)
                return node;
            return nodes != null && nodes.Count > 0 ? nodes[0] : null;
        }

        public DialogueNode CreateNode(Vector2 position)
        {
            var node = new DialogueNode
            {
                id = Guid.NewGuid().ToString("N"),
                title = "Dialogue",
                editorPosition = position,
                choices = new List<DialogueChoice>(),
                enterSignals = new List<DialogueSignal>()
            };

            if (nodes == null)
                nodes = new List<DialogueNode>();

            nodes.Add(node);
            if (string.IsNullOrEmpty(entryNodeId))
                entryNodeId = node.id;

            InvalidateIndex();
            return node;
        }

        public bool RemoveNode(string nodeId)
        {
            if (nodes == null || string.IsNullOrEmpty(nodeId))
                return false;

            var removed = nodes.RemoveAll(n => n != null && n.id == nodeId) > 0;
            if (!removed)
                return false;

            if (entryNodeId == nodeId)
                entryNodeId = nodes.Count > 0 ? nodes[0].id : string.Empty;

            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                if (node == null)
                    continue;
                if (node.nextNodeId == nodeId)
                    node.nextNodeId = string.Empty;
                if (node.choices == null)
                    continue;
                for (int c = 0; c < node.choices.Count; c++)
                {
                    if (node.choices[c] != null && node.choices[c].nextNodeId == nodeId)
                        node.choices[c].nextNodeId = string.Empty;
                }
            }

            InvalidateIndex();
            return true;
        }

        public void EnsureIndex()
        {
            if (_index != null)
                return;

            _index = new Dictionary<string, DialogueNode>();
            if (nodes == null)
                return;

            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                if (node == null || string.IsNullOrEmpty(node.id))
                    continue;
                _index[node.id] = node;
            }
        }

        public void InvalidateIndex()
        {
            _index = null;
        }

        void OnValidate()
        {
            InvalidateIndex();
            if (nodes == null)
                nodes = new List<DialogueNode>();
        }
    }
}
