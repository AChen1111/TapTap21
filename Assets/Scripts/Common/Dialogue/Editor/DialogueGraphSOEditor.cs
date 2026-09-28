using DialogueSystem;
using UnityEditor;
using UnityEngine;

namespace DialogueSystem.Editor
{
    [CustomEditor(typeof(DialogueGraphSO))]
    public class DialogueGraphSOEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Open Graph Editor"))
                DialogueGraphWindow.Open(target as DialogueGraphSO);
        }
    }
}
