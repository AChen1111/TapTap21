using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DialogueSystem
{
    [DefaultExecutionOrder(-100)]
    public class DialogueRunner : MonoBehaviour
    {
        public static DialogueRunner Instance { get; private set; }

        [SerializeField] bool persistAcrossScenes;
        [HideInInspector] public List<DialogueTrigger> dialogues = new List<DialogueTrigger>();

        public bool isDialoguing => Session.IsActive;
        public DialogueSession Session { get; } = new DialogueSession();
        public IDialogueVariableStore Variables => _variables;
        public DialogueTrigger CurrentTarget => HasSelection ? dialogues[_selection] : null;

        public event Action<DialogueTrigger> OnInteractionListAdd;
        public event Action<DialogueTrigger> OnInteractionListSub;
        public event Action<DialogueTrigger> OnInterectionSelectionChange;
        public event Action<DialogueLine> DialogueContextShow;
        public event Action DialogueContextClose;
        public event Action<bool> DialogueLockChanged;
        public event Action<string, string> DialogueSignalEmitted;

        readonly DialogueVariableStore _variables = new DialogueVariableStore();
        int _selection;

        bool HasSelection => dialogues.Count > 0 && _selection >= 0 && _selection < dialogues.Count;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            if (persistAcrossScenes)
                DontDestroyOnLoad(gameObject);

            Session.LinePresented += HandleLinePresented;
            Session.Ended += HandleEnded;
            Session.SignalEmitted += HandleSignal;
        }

        void OnDestroy()
        {
            if (Instance == this)
                Instance = null;

            Session.LinePresented -= HandleLinePresented;
            Session.Ended -= HandleEnded;
            Session.SignalEmitted -= HandleSignal;
        }

        public void DialogueTriggerAdd(DialogueTrigger dialogue)
        {
            if (dialogue == null || dialogues.Contains(dialogue))
                return;

            if (dialogues.Count == 0)
                _selection = 0;

            dialogues.Add(dialogue);
            OnInteractionListAdd?.Invoke(dialogue);
            NotifySelection();
        }

        public void DialogueTriggerSub(DialogueTrigger dialogue)
        {
            if (dialogue == null)
                return;

            var wasSelected = CurrentTarget == dialogue;
            if (!dialogues.Remove(dialogue))
                return;

            OnInteractionListSub?.Invoke(dialogue);
            if (wasSelected && Session.Source == dialogue)
                EndDialogue();

            if (_selection >= dialogues.Count)
                _selection = dialogues.Count - 1;
            NotifySelection();
        }

        public int GetSelectID()
        {
            return _selection;
        }

        public void ChangeSelectID(int dir)
        {
            if (isDialoguing || dialogues.Count == 0)
                return;

            _selection = ((_selection + dir) % dialogues.Count + dialogues.Count) % dialogues.Count;
            NotifySelection();
        }

        public bool ShowDialogue()
        {
            if (isDialoguing)
            {
                Advance();
                return true;
            }

            if (!HasSelection)
                return false;

            return StartDialogue(CurrentTarget);
        }
        [Button]

        public bool StartDialogue(DialogueTrigger trigger)
        {
            if (trigger == null)
                return false;
            return StartDialogue(trigger.Graph, trigger, trigger.OverrideEntryId);
        }

        public bool StartDialogue(DialogueGraphSO graph, DialogueTrigger source = null, string overrideEntryId = null)
        {
            if (graph == null)
            {
                Debug.LogWarning("Dialogue graph is missing.");
                return false;
            }

            Session.Start(graph, _variables, source, overrideEntryId);
            return Session.IsActive;
        }

        public void Advance()
        {
            if (!isDialoguing)
                return;

            if (Session.HasVisibleChoices)
                return;

            Session.Advance();
        }

        public void SelectChoice(int index)
        {
            Session.SelectChoice(index);
        }

        public void SelectChoice(DialogueChoice choice)
        {
            Session.SelectChoice(choice);
        }

        public void EndDialogue()
        {
            if (isDialoguing)
                Session.Stop();
        }

        public void ResetVariables()
        {
            _variables.ClearAll();
        }

        void HandleLinePresented(DialogueLine line)
        {
            DialogueLockChanged?.Invoke(true);
            DialogueContextShow?.Invoke(line);
        }

        void HandleEnded()
        {
            DialogueContextClose?.Invoke();
            DialogueLockChanged?.Invoke(false);
        }

        void HandleSignal(string key, string value)
        {
            DialogueSignalEmitted?.Invoke(key, value);
        }

        void NotifySelection()
        {
            OnInterectionSelectionChange?.Invoke(CurrentTarget);
        }
    }
}
