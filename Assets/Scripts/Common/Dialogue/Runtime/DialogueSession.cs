using System;
using System.Collections.Generic;

namespace DialogueSystem
{
    public class DialogueSession
    {
        const int MaxBranchHops = 32;

        public DialogueGraphSO Graph { get; private set; }
        public DialogueNode Current { get; private set; }
        public DialogueTrigger Source { get; private set; }
        public bool IsActive => Current != null;
        public bool HasVisibleChoices => _visibleChoices.Count > 0;
        public IReadOnlyList<DialogueChoice> VisibleChoices => _visibleChoices;

        public event Action<DialogueLine> LinePresented;
        public event Action Ended;
        public event Action<string, string> SignalEmitted;

        IDialogueVariableStore _store;
        readonly List<DialogueChoice> _visibleChoices = new List<DialogueChoice>();

        public void Start(DialogueGraphSO graph, IDialogueVariableStore store, DialogueTrigger source = null, string overrideEntryId = null)
        {
            Stop();
            Graph = graph;
            _store = store;
            Source = source;
            if (Graph != null)
                Graph.InvalidateIndex();
            Present(Graph != null ? Graph.GetEntryNode(overrideEntryId) : null);
        }

        public void Advance()
        {
            if (!IsActive)
                return;
            if (HasVisibleChoices)
                return;
            Present(Graph != null ? Graph.FindNode(Current.nextNodeId) : null);
        }

        public bool SelectChoice(int index)
        {
            if (!IsActive || index < 0 || index >= _visibleChoices.Count)
                return false;

            Present(Graph != null ? Graph.FindNode(_visibleChoices[index].nextNodeId) : null);
            return true;
        }

        public bool SelectChoice(DialogueChoice choice)
        {
            if (choice == null)
                return false;
            return SelectChoice(_visibleChoices.IndexOf(choice));
        }

        public void Stop()
        {
            var wasActive = IsActive;
            Graph = null;
            Current = null;
            Source = null;
            _visibleChoices.Clear();
            if (wasActive)
                Ended?.Invoke();
        }

        void Present(DialogueNode node)
        {
            var hops = 0;
            while (node != null && node.IsBranch && hops++ < MaxBranchHops)
            {
                ApplySignals(node);
                CollectChoices(node);
                if (_visibleChoices.Count > 0)
                    node = Graph != null ? Graph.FindNode(_visibleChoices[0].nextNodeId) : null;
                else
                    node = Graph != null ? Graph.FindNode(node.nextNodeId) : null;
            }

            _visibleChoices.Clear();
            Current = node;

            if (node == null || hops >= MaxBranchHops)
            {
                Current = null;
                Graph = null;
                Source = null;
                Ended?.Invoke();
                return;
            }

            ApplySignals(node);
            CollectChoices(node);
            LinePresented?.Invoke(new DialogueLine(node, _visibleChoices));
        }

        void CollectChoices(DialogueNode node)
        {
            _visibleChoices.Clear();
            if (node == null || node.choices == null)
                return;

            for (int i = 0; i < node.choices.Count; i++)
            {
                var choice = node.choices[i];
                if (choice != null && choice.IsAvailable(_store))
                    _visibleChoices.Add(choice);
            }
        }

        void ApplySignals(DialogueNode node)
        {
            if (node == null || node.enterSignals == null)
                return;

            for (int i = 0; i < node.enterSignals.Count; i++)
            {
                var signal = node.enterSignals[i];
                if (signal == null || string.IsNullOrEmpty(signal.key))
                    continue;

                switch (signal.type)
                {
                    case DialogueSignalType.SetFlag:
                        _store?.Set(signal.key, string.IsNullOrEmpty(signal.value) ? "1" : signal.value);
                        break;
                    case DialogueSignalType.ClearFlag:
                        _store?.Clear(signal.key);
                        break;
                }

                SignalEmitted?.Invoke(signal.key, signal.value);
            }
        }
    }
}
