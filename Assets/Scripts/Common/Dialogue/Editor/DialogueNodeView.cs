using System;
using System.Collections.Generic;
using DialogueSystem;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public class DialogueNodeView : Node
    {
        public DialogueNode Data { get; }
        public Port Input { get; private set; }
        public Port DefaultOutput { get; private set; }

        readonly DialogueGraphSO _graph;
        readonly Action _onStructureChanged;
        readonly Action _onChanged;
        readonly VisualElement _choiceRoot;
        readonly ObjectField _speakerField;
        readonly TextField _textField;

        public DialogueNodeView(DialogueGraphSO graph, DialogueNode data, Action onChanged, Action onStructureChanged)
        {
            _graph = graph;
            Data = data;
            _onChanged = onChanged;
            _onStructureChanged = onStructureChanged;
            viewDataKey = data.id;
            SetPosition(new Rect(data.editorPosition, new Vector2(320f, 180f)));
            RefreshTitle(graph != null && graph.entryNodeId == data.id);

            Input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
            Input.portName = "In";
            inputContainer.Add(Input);

            DefaultOutput = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            DefaultOutput.portName = data.IsBranch ? "Fallback" : "Next";
            DefaultOutput.userData = -1;
            outputContainer.Add(DefaultOutput);

            var titleField = new TextField("Title") { value = data.title ?? string.Empty };
            titleField.RegisterValueChangedCallback(evt =>
            {
                Change(() => data.title = evt.newValue);
                RefreshTitle(_graph != null && _graph.entryNodeId == data.id);
            });
            extensionContainer.Add(titleField);

            var typeField = new EnumField("Type", data.nodeType);
            typeField.RegisterValueChangedCallback(evt =>
            {
                Change(() => data.nodeType = (DialogueNodeType)evt.newValue);
                DefaultOutput.portName = data.IsBranch ? "Fallback" : "Next";
                RefreshEditorVisibility();
                RefreshTitle(_graph != null && _graph.entryNodeId == data.id);
            });
            extensionContainer.Add(typeField);

            _speakerField = new ObjectField("Speaker")
            {
                objectType = typeof(SpeakerSO),
                value = data.speaker
            };
            _speakerField.RegisterValueChangedCallback(evt =>
            {
                Change(() => data.speaker = evt.newValue as SpeakerSO);
            });
            extensionContainer.Add(_speakerField);

            _textField = new TextField("Text") { value = data.text ?? string.Empty, multiline = true };
            _textField.style.minHeight = 72;
            _textField.RegisterValueChangedCallback(evt =>
            {
                Change(() => data.text = evt.newValue);
            });
            extensionContainer.Add(_textField);

            extensionContainer.Add(BuildSignals());
            extensionContainer.Add(new Button(AddChoice) { text = "Add Choice" });
            _choiceRoot = new VisualElement();
            extensionContainer.Add(_choiceRoot);
            RebuildChoices();
            RefreshEditorVisibility();
            RefreshExpandedState();
            RefreshPorts();
        }

        public void RefreshTitle(bool isEntry)
        {
            var label = string.IsNullOrEmpty(Data.title) ? (Data.IsBranch ? "Branch" : "Dialogue") : Data.title;
            if (isEntry)
                label += " [Entry]";
            if (Data.IsBranch)
                label += " [Branch]";
            title = label;
        }

        public Port GetChoicePort(int index)
        {
            foreach (var element in outputContainer.Children())
            {
                if (element is Port port && port.userData is int portIndex && portIndex == index)
                    return port;
            }
            return null;
        }

        VisualElement BuildSignals()
        {
            var foldout = new Foldout { text = "On Enter", value = Data.enterSignals != null && Data.enterSignals.Count > 0 };
            var root = new VisualElement();
            foldout.Add(root);
            RebuildSignals(root);
            foldout.Add(new Button(() =>
            {
                if (Data.enterSignals == null)
                    Data.enterSignals = new List<DialogueSignal>();
                Change(() => Data.enterSignals.Add(new DialogueSignal()));
                RebuildSignals(root);
            }) { text = "Add Signal" });
            return foldout;
        }

        void RebuildSignals(VisualElement root)
        {
            root.Clear();
            if (Data.enterSignals == null)
                return;

            for (int i = 0; i < Data.enterSignals.Count; i++)
            {
                var signal = Data.enterSignals[i];
                var index = i;
                var row = new VisualElement { style = { flexDirection = FlexDirection.Row } };
                var typeField = new EnumField(signal.type) { style = { width = 90 } };
                typeField.RegisterValueChangedCallback(evt =>
                {
                    Change(() => signal.type = (DialogueSignalType)evt.newValue);
                });
                var keyField = new TextField { value = signal.key ?? string.Empty };
                keyField.style.flexGrow = 1;
                keyField.RegisterValueChangedCallback(evt => Change(() => signal.key = evt.newValue));
                var valueField = new TextField { value = signal.value ?? string.Empty };
                valueField.style.width = 70;
                valueField.RegisterValueChangedCallback(evt => Change(() => signal.value = evt.newValue));
                row.Add(typeField);
                row.Add(keyField);
                row.Add(valueField);
                row.Add(new Button(() =>
                {
                    Change(() => Data.enterSignals.RemoveAt(index));
                    RebuildSignals(root);
                }) { text = "X" });
                root.Add(row);
            }
        }

        void AddChoice()
        {
            if (Data.choices == null)
                Data.choices = new List<DialogueChoice>();
            Change(() => Data.choices.Add(new DialogueChoice { choiceText = "Choice" }));
            _onStructureChanged?.Invoke();
        }

        void RebuildChoices()
        {
            _choiceRoot.Clear();
            var stale = new List<VisualElement>();
            foreach (var child in outputContainer.Children())
            {
                if (child is Port port && port.userData is int && (int)port.userData >= 0)
                    stale.Add(port);
            }
            for (int i = 0; i < stale.Count; i++)
                outputContainer.Remove(stale[i]);

            if (Data.choices == null)
                return;

            for (int i = 0; i < Data.choices.Count; i++)
            {
                var choice = Data.choices[i];
                var index = i;
                var box = new VisualElement();
                box.style.marginTop = 4;
                box.style.paddingTop = 4;
                box.style.borderTopWidth = 1;
                box.style.borderTopColor = new Color(0.3f, 0.3f, 0.3f);

                var row = new VisualElement { style = { flexDirection = FlexDirection.Row } };
                var field = new TextField { value = choice.choiceText ?? string.Empty };
                field.style.flexGrow = 1;
                field.RegisterValueChangedCallback(evt => Change(() => choice.choiceText = evt.newValue));
                row.Add(field);
                row.Add(new Button(() => RemoveChoice(index)) { text = "X" });
                box.Add(row);
                box.Add(BuildConditions(choice));
                _choiceRoot.Add(box);

                var port = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                port.portName = "Choice " + (index + 1);
                port.userData = index;
                outputContainer.Add(port);
            }

            RefreshPorts();
        }

        void RefreshEditorVisibility()
        {
            var isBranch = Data.IsBranch;
            _speakerField.style.display = isBranch ? DisplayStyle.None : DisplayStyle.Flex;
            _textField.style.display = isBranch ? DisplayStyle.None : DisplayStyle.Flex;

            // A Line advances through its choices once choices are configured.
            // Branch nodes keep their fallback output even when they use choices.
            var showDefaultOutput = isBranch || !Data.HasChoices();
            DefaultOutput.style.display = showDefaultOutput ? DisplayStyle.Flex : DisplayStyle.None;
            RefreshPorts();
        }

        VisualElement BuildConditions(DialogueChoice choice)
        {
            if (choice.conditions == null)
                choice.conditions = new List<DialogueCondition>();

            var foldout = new Foldout { text = "Conditions", value = choice.conditions.Count > 0 };
            var root = new VisualElement();
            foldout.Add(root);
            RebuildConditions(choice, root);
            foldout.Add(new Button(() =>
            {
                Change(() => choice.conditions.Add(new DialogueCondition()));
                RebuildConditions(choice, root);
            }) { text = "Add Condition" });
            return foldout;
        }

        void RebuildConditions(DialogueChoice choice, VisualElement root)
        {
            root.Clear();
            if (choice.conditions == null)
                return;

            for (int i = 0; i < choice.conditions.Count; i++)
            {
                var condition = choice.conditions[i];
                var index = i;
                var row = new VisualElement { style = { flexDirection = FlexDirection.Row } };
                var typeField = new EnumField(condition.type) { style = { width = 90 } };
                typeField.RegisterValueChangedCallback(evt => Change(() => condition.type = (DialogueConditionType)evt.newValue));
                var keyField = new TextField { value = condition.key ?? string.Empty };
                keyField.style.flexGrow = 1;
                keyField.RegisterValueChangedCallback(evt => Change(() => condition.key = evt.newValue));
                var valueField = new TextField { value = condition.value ?? string.Empty };
                valueField.style.width = 70;
                valueField.RegisterValueChangedCallback(evt => Change(() => condition.value = evt.newValue));
                row.Add(typeField);
                row.Add(keyField);
                row.Add(valueField);
                row.Add(new Button(() =>
                {
                    Change(() => choice.conditions.RemoveAt(index));
                    RebuildConditions(choice, root);
                }) { text = "X" });
                root.Add(row);
            }
        }

        void RemoveChoice(int index)
        {
            if (Data.choices == null || index < 0 || index >= Data.choices.Count)
                return;
            Change(() => Data.choices.RemoveAt(index));
            _onStructureChanged?.Invoke();
        }

        void Change(Action apply)
        {
            if (_graph != null)
                Undo.RecordObject(_graph, "Edit Dialogue Node");
            apply();
            Notify();
        }

        void Notify()
        {
            _graph?.InvalidateIndex();
            _onChanged?.Invoke();
        }
    }
}
