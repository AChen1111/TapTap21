using DialogueSystem;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public class DialogueGraphWindow : EditorWindow
    {
        [SerializeField] DialogueGraphSO _graph;
        DialogueGraphView _graphView;
        ObjectField _graphField;
        TextField _nameField;

        [MenuItem("Tools/Dialogue/Graph Editor")]
        public static void Open()
        {
            GetWindow<DialogueGraphWindow>("Dialogue Graph");
        }

        public static void Open(DialogueGraphSO graph)
        {
            var window = GetWindow<DialogueGraphWindow>("Dialogue Graph");
            window.Load(graph);
        }

        void CreateGUI()
        {
            var toolbar = new Toolbar();
            _graphField = new ObjectField("Graph")
            {
                objectType = typeof(DialogueGraphSO),
                value = _graph
            };
            _graphField.style.minWidth = 280;
            _graphField.RegisterValueChangedCallback(evt => Load(evt.newValue as DialogueGraphSO));
            toolbar.Add(_graphField);

            _nameField = new TextField("Name") { value = _graph != null ? _graph.graphName : string.Empty };
            _nameField.style.minWidth = 180;
            _nameField.RegisterValueChangedCallback(evt =>
            {
                if (_graph == null)
                    return;
                Undo.RecordObject(_graph, "Rename Dialogue Graph");
                _graph.graphName = evt.newValue;
                EditorUtility.SetDirty(_graph);
            });
            toolbar.Add(_nameField);

            toolbar.Add(new ToolbarButton(Save) { text = "Save" });
            rootVisualElement.Add(toolbar);

            _graphView = new DialogueGraphView();
            rootVisualElement.Add(_graphView);
            if (_graph != null)
                _graphView.Load(_graph);
        }

        void Load(DialogueGraphSO graph)
        {
            _graph = graph;
            if (_graphField != null)
                _graphField.SetValueWithoutNotify(graph);
            if (_nameField != null)
                _nameField.SetValueWithoutNotify(graph != null ? graph.graphName : string.Empty);
            _graphView?.Load(graph);
        }

        void Save()
        {
            if (_graph == null)
                return;
            EditorUtility.SetDirty(_graph);
            AssetDatabase.SaveAssets();
        }
    }
}
