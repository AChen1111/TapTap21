using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueUIController : MonoBehaviour
    {
        [Header("CanvasGroup")]
        public CanvasGroup DialogueContext;
        public CanvasGroup DialogueInteraction;
        public CanvasGroup DialogueChoices;

        [Header("Dialogue UI")]
        public TextMeshProUGUI speakerName;
        public TextMeshProUGUI dialogueContext;

        [Header("Prefabs/ParentObject")]
        public GameObject Interaction_prefab;
        public Transform Interaction_parent;
        public GameObject Choice_prefab;
        public Transform Choice_parent;

        readonly Dictionary<DialogueTrigger, GameObject> _interactions = new Dictionary<DialogueTrigger, GameObject>();
        readonly List<GameObject> _spawnedChoices = new List<GameObject>();
        GameObject _currentInteractionObject;
        bool _bound;

        void Start()
        {
            CloseDialogueContext();
            Bind();
        }

        void OnEnable()
        {
            Bind();
        }

        void OnDisable()
        {
            Unbind();
        }

        void Bind()
        {
            var runner = DialogueRunner.Instance;
            if (runner == null || _bound)
                return;

            runner.DialogueContextShow += ShowDialogueContext;
            runner.DialogueContextClose += CloseDialogueContext;
            runner.OnInteractionListAdd += OnListAdd_Interaction;
            runner.OnInteractionListSub += OnListSub_Interaction;
            runner.OnInterectionSelectionChange += OnSelectChange_Interaction;
            _bound = true;
        }

        void Unbind()
        {
            var runner = DialogueRunner.Instance;
            if (runner == null || !_bound)
            {
                _bound = false;
                return;
            }

            runner.DialogueContextShow -= ShowDialogueContext;
            runner.DialogueContextClose -= CloseDialogueContext;
            runner.OnInteractionListAdd -= OnListAdd_Interaction;
            runner.OnInteractionListSub -= OnListSub_Interaction;
            runner.OnInterectionSelectionChange -= OnSelectChange_Interaction;
            _bound = false;
        }

        public void ShowDialogueContext(DialogueLine line)
        {
            SetGroup(DialogueContext, true);

            var node = line.Node;
            var speaker = node != null ? node.speaker : null;
            var body = node != null ? node.ResolveText() : string.Empty;
            var speakerLabel = speaker != null ? speaker.speakerName : string.Empty;

            if (speakerName != null)
            {
                speakerName.text = speakerLabel;
                speakerName.color = speaker != null ? speaker.nameColor : Color.white;
                if (dialogueContext != null)
                    dialogueContext.text = body;
            }
            else if (dialogueContext != null)
            {
                dialogueContext.text = string.IsNullOrEmpty(speakerLabel) ? body : speakerLabel + "\n" + body;
            }

            if (line.Choices != null && line.Choices.Count > 0)
                ShowDialogueChoice(line);
            else
                CloseDialogueChoice();
        }

        public void CloseDialogueContext()
        {
            SetGroup(DialogueContext, false);
            CloseDialogueChoice();
        }

        public void OnListAdd_Interaction(DialogueTrigger dialogue)
        {
            if (dialogue == null || Interaction_prefab == null || Interaction_parent == null)
                return;

            var obj = Instantiate(Interaction_prefab, Interaction_parent);
            _interactions[dialogue] = obj;
            var view = obj.GetComponent<DialogueInteractUI>();
            if (view != null)
                view.Bind(dialogue.DisplayName, DialogueRunner.Instance != null && DialogueRunner.Instance.CurrentTarget == dialogue);
            SetGroup(DialogueInteraction, true);
        }

        public void OnListSub_Interaction(DialogueTrigger dialogue)
        {
            if (dialogue == null)
                return;

            if (_interactions.TryGetValue(dialogue, out var obj))
            {
                Destroy(obj);
                _interactions.Remove(dialogue);
            }

            if (_currentInteractionObject != null && !_interactions.ContainsValue(_currentInteractionObject))
                _currentInteractionObject = null;

            if (_interactions.Count == 0)
                SetGroup(DialogueInteraction, false);
        }

        public void OnSelectChange_Interaction(DialogueTrigger dialogue)
        {
            if (_currentInteractionObject != null)
            {
                var previous = _currentInteractionObject.GetComponent<DialogueInteractUI>();
                if (previous != null)
                    previous.SetSelected(false);
            }

            if (dialogue != null && _interactions.TryGetValue(dialogue, out _currentInteractionObject))
            {
                var current = _currentInteractionObject.GetComponent<DialogueInteractUI>();
                if (current != null)
                    current.SetSelected(true);
            }
            else
            {
                _currentInteractionObject = null;
            }
        }

        public void ShowDialogueChoice(DialogueLine line)
        {
            SetGroup(DialogueChoices, true);
            ClearChoices();
            if (Choice_prefab == null || Choice_parent == null || line.Choices == null)
                return;

            for (int i = 0; i < line.Choices.Count; i++)
            {
                var choice = line.Choices[i];
                var obj = Instantiate(Choice_prefab, Choice_parent);
                _spawnedChoices.Add(obj);
                var view = obj.GetComponent<DialogueChoiceUI>() ?? obj.AddComponent<DialogueChoiceUI>();
                view.Bind(choice, HandleChoiceClicked);
            }
        }

        public void CloseDialogueChoice()
        {
            SetGroup(DialogueChoices, false);
            ClearChoices();
        }

        void HandleChoiceClicked(DialogueChoice choice)
        {
            if (DialogueRunner.Instance != null)
                DialogueRunner.Instance.SelectChoice(choice);
        }

        void ClearChoices()
        {
            for (int i = 0; i < _spawnedChoices.Count; i++)
            {
                if (_spawnedChoices[i] != null)
                    Destroy(_spawnedChoices[i]);
            }
            _spawnedChoices.Clear();
        }

        static void SetGroup(CanvasGroup group, bool visible)
        {
            if (group == null)
                return;
            group.alpha = visible ? 1f : 0f;
            group.interactable = visible;
            group.blocksRaycasts = visible;
        }
    }
}
