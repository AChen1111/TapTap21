using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UiPrefixBind
{
    public string fieldName;
    public GameObject target;
    public Component component;
}

public static class UiPrefixCollector
{
    static readonly Dictionary<string, Type> PrefixTypes = new Dictionary<string, Type>
    {
        { "Btn", typeof(Button) },
        { "Img", typeof(Image) },
        { "Txt", typeof(TextMeshProUGUI) },
        { "Tog", typeof(Toggle) },
        { "Sld", typeof(Slider) },
        { "Inp", typeof(TMP_InputField) },
        { "Scr", typeof(ScrollRect) },
        { "Raw", typeof(RawImage) },
        { "Drop", typeof(TMP_Dropdown) },
        { "Go", null },
    };

    public static UiPrefixBind[] Collect(Transform root)
    {
        var list = new List<UiPrefixBind>();
        var transforms = root.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < transforms.Length; i++)
        {
            Transform child = transforms[i];
            if (child == root)
            {
                continue;
            }

            string objectName = child.name;
            int split = objectName.IndexOf('_');
            if (split <= 0 || split == objectName.Length - 1)
            {
                continue;
            }

            string prefix = objectName.Substring(0, split);
            if (!PrefixTypes.TryGetValue(prefix, out Type type))
            {
                continue;
            }

            Component component = null;
            if (type != null)
            {
                component = child.GetComponent(type);
                if (component == null)
                {
                    continue;
                }
            }

            string fieldName = "m_" + prefix + objectName.Substring(split + 1);
            var entry = new UiPrefixBind
            {
                fieldName = fieldName,
                target = child.gameObject,
                component = component
            };
            int existing = list.FindIndex(e => e.fieldName == fieldName);
            if (existing >= 0)
            {
                list[existing] = entry;
            }
            else
            {
                list.Add(entry);
            }
        }
        return list.ToArray();
    }

    public static void ApplyRuntime(object host, UiPrefixBind[] binds)
    {
        if (binds == null)
        {
            return;
        }

        for (int i = 0; i < binds.Length; i++)
        {
            UiPrefixBind bind = binds[i];
            object value = bind.component != null ? (object)bind.component : bind.target;
            if (value == null)
            {
                continue;
            }

            var field = FindField(host.GetType(), bind.fieldName);
            if (field != null && field.FieldType.IsInstanceOfType(value))
            {
                field.SetValue(host, value);
            }
        }
    }

    static System.Reflection.FieldInfo FindField(Type type, string name)
    {
        while (type != null)
        {
            var field = type.GetField(name,
                System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.DeclaredOnly);
            if (field != null)
            {
                return field;
            }
            type = type.BaseType;
        }
        return null;
    }

#if UNITY_EDITOR
    public static void WriteBinds(UnityEditor.SerializedObject so, string arrayProperty, UiPrefixBind[] binds)
    {
        var prop = so.FindProperty(arrayProperty);
        prop.arraySize = binds.Length;
        for (int i = 0; i < binds.Length; i++)
        {
            var elem = prop.GetArrayElementAtIndex(i);
            elem.FindPropertyRelative("fieldName").stringValue = binds[i].fieldName;
            elem.FindPropertyRelative("target").objectReferenceValue = binds[i].target;
            elem.FindPropertyRelative("component").objectReferenceValue = binds[i].component;
        }
    }

    public static void ApplySerializedFields(UnityEditor.SerializedObject so, UiPrefixBind[] binds)
    {
        for (int i = 0; i < binds.Length; i++)
        {
            var fieldProp = so.FindProperty(binds[i].fieldName);
            if (fieldProp != null && fieldProp.propertyType == UnityEditor.SerializedPropertyType.ObjectReference)
            {
                fieldProp.objectReferenceValue = binds[i].component != null
                    ? (UnityEngine.Object)binds[i].component
                    : binds[i].target;
            }
        }
    }
#endif
}
