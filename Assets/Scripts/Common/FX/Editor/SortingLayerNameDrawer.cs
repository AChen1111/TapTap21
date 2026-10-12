#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace FXSystem.Editor
{
    [CustomPropertyDrawer(typeof(SortingLayerNameAttribute))]
    public class SortingLayerNameDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            var layers = SortingLayer.layers;
            var names = new string[layers.Length];
            int index = 0;
            int compactMatch = 0;
            bool exact = false;
            string compactValue = (property.stringValue ?? "").Replace(" ", "");
            for (int i = 0; i < layers.Length; i++)
            {
                names[i] = layers[i].name;
                if (layers[i].name == property.stringValue)
                {
                    index = i;
                    exact = true;
                }
                else if (layers[i].name.Replace(" ", "") == compactValue)
                    compactMatch = i;
            }

            if (!exact)
                index = compactMatch;

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            int next = EditorGUI.Popup(position, label.text, index, names);
            if (EditorGUI.EndChangeCheck() && next >= 0 && next < names.Length)
                property.stringValue = names[next];
            EditorGUI.EndProperty();
        }
    }
}
#endif
