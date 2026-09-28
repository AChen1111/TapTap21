using System;
using System.Collections.Generic;
using System.Linq;
using DialogueSystem;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public class DialogueGraphView : GraphView
    {
        DialogueGraphSO _graph;
        readonly Dictionary<string, DialogueNodeView> _views = new Dictionary<string, DialogueNodeView>();
        bool _isLoading;

        public DialogueGraphView()
        {
            style.flexGrow = 1;
            this.StretchToParentSize();
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();

            var miniMap = new MiniMap { anchored = true };
            miniMap.SetPosition(new Rect(12f, 12f, 200f, 120f));
            Add(miniMap);

            graphViewChanged = OnGraphViewChanged;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(port =>
                port != startPort &&
                port.node != startPort.node &&
                port.direction != startPort.direction).ToList();
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            var local = contentViewContainer.WorldToLocal(evt.mousePosition);
            evt.menu.AppendAction("Create Dialogue Node", _ => CreateNode(local, DialogueNodeType.Line));
            evt.menu.AppendAction("Create Branch Node", _ => CreateNode(local, DialogueNodeType.Branch));
            evt.menu.AppendAction("Set As Entry", _ => SetSelectedAsEntry(), _ => selection.OfType<DialogueNodeView>().Any() ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);
            base.BuildContextualMenu(evt);
        }

        public void Load(DialogueGraphSO graph)
        {
            _isLoading = true;
            _graph = graph;
            _views.Clear();
            DeleteElements(graphElements.Where(e => e is Node || e is Edge).ToList());

            if (_graph == null)
            {
                _isLoading = false;
                return;
            }

            if (_graph.nodes == null)
                _graph.nodes = new List<DialogueNode>();

            for (int i = 0; i < _graph.nodes.Count; i++)
            {
                var node = _graph.nodes[i];
                if (node == null)
                    continue;
                if (string.IsNullOrEmpty(node.id))
                    node.id = Guid.NewGuid().ToString("N");
                AddNodeView(node);
            }

            RebuildEdges();
            RefreshEntryTitles();
            _isLoading = false;
        }

        public void Reload()
        {
            Load(_graph);
        }

        DialogueNodeView CreateNode(Vector2 position, DialogueNodeType type)
        {
            if (_graph == null)
                return null;

            Undo.RecordObject(_graph, "Create Dialogue Node");
            var data = _graph.CreateNode(position);
            data.nodeType = type;
            data.title = type == DialogueNodeType.Branch ? "Branch" : "Dialogue";
            EditorUtility.SetDirty(_graph);
            var view = AddNodeView(data);
            RefreshEntryTitles();
            return view;
        }

        DialogueNodeView AddNodeView(DialogueNode data)
        {
            var view = new DialogueNodeView(_graph, data, MarkDirty, Reload);
            _views[data.id] = view;
            AddElement(view);
            return view;
        }

        DialogueNodeView FindView(string nodeId)
        {
            if (string.IsNullOrEmpty(nodeId))
                return null;
            _views.TryGetValue(nodeId, out var view);
            return view;
        }

        void RebuildEdges()
        {
            DeleteElements(graphElements.OfType<Edge>().ToList());
            if (_graph == null || _graph.nodes == null)
                return;

            for (int i = 0; i < _graph.nodes.Count; i++)
            {
                var node = _graph.nodes[i];
                if (node == null || !_views.TryGetValue(node.id, out var view))
                    continue;

                Connect(view.DefaultOutput, FindView(node.nextNodeId));
                if (node.choices == null)
                    continue;

                for (int c = 0; c < node.choices.Count; c++)
                {
                    if (node.choices[c] == null)
                        continue;
                    Connect(view.GetChoicePort(c), FindView(node.choices[c].nextNodeId));
                }
            }
        }

        void Connect(Port output, DialogueNodeView target)
        {
            if (output == null || target == null)
                return;
            AddElement(output.ConnectTo(target.Input));
        }

        GraphViewChange OnGraphViewChanged(GraphViewChange change)
        {
            if (_isLoading || _graph == null)
                return change;

            Undo.RecordObject(_graph, "Edit Dialogue Graph");

            if (change.movedElements != null)
            {
                foreach (var element in change.movedElements)
                {
                    if (element is DialogueNodeView view)
                        view.Data.editorPosition = view.GetPosition().position;
                }
            }

            if (change.elementsToRemove != null)
            {
                foreach (var element in change.elementsToRemove)
                {
                    if (element is DialogueNodeView view)
                    {
                        _views.Remove(view.Data.id);
                        _graph.RemoveNode(view.Data.id);
                    }
                    else if (element is Edge edge)
                    {
                        ApplyEdge(edge, false);
                    }
                }
            }

            if (change.edgesToCreate != null)
            {
                foreach (var edge in change.edgesToCreate)
                    ApplyEdge(edge, true);
            }

            RefreshEntryTitles();
            MarkDirty();
            return change;
        }

        void ApplyEdge(Edge edge, bool connected)
        {
            if (edge.output?.node is not DialogueNodeView source)
                return;

            var nextId = string.Empty;
            if (connected && edge.input?.node is DialogueNodeView target)
                nextId = target.Data.id;

            if (edge.output.userData is int index && index >= 0)
            {
                if (source.Data.choices != null && index < source.Data.choices.Count)
                    source.Data.choices[index].nextNodeId = nextId;
                return;
            }

            source.Data.nextNodeId = nextId;
        }

        void SetSelectedAsEntry()
        {
            var view = selection.OfType<DialogueNodeView>().FirstOrDefault();
            if (view == null || _graph == null)
                return;

            Undo.RecordObject(_graph, "Set Dialogue Entry");
            _graph.entryNodeId = view.Data.id;
            RefreshEntryTitles();
            MarkDirty();
        }

        void RefreshEntryTitles()
        {
            foreach (var pair in _views)
                pair.Value.RefreshTitle(_graph != null && _graph.entryNodeId == pair.Key);
        }

        void MarkDirty()
        {
            if (_graph != null)
                EditorUtility.SetDirty(_graph);
        }
    }
}
