#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FXSystem.Editor
{
    [CustomEditor(typeof(FXCatalog))]
    public class FXCatalogEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var catalog = (FXCatalog)target;
            if (catalog.Definitions == null)
                return;

            var seen = new HashSet<string>();
            for (int i = 0; i < catalog.Definitions.Length; i++)
            {
                var def = catalog.Definitions[i];
                if (def == null)
                {
                    EditorGUILayout.HelpBox($"Definitions[{i}] 为空。", MessageType.Warning);
                    continue;
                }

                if (def.Prefab == null)
                    EditorGUILayout.HelpBox($"'{def.ResolvedId}' 未指定 Prefab。", MessageType.Warning);

                if (def.Space == FXSpace.UI)
                    EditorGUILayout.HelpBox($"'{def.ResolvedId}' 为 UI 特效：粒子请使用 Screen Space - Camera Canvas（系统会自动创建 [FXCanvas]）。若只有 Image/Animator，请把 ExtraLifetime 或动画时长设好。", MessageType.Info);

                if (!seen.Add(def.ResolvedId))
                    EditorGUILayout.HelpBox($"重复 Id：{def.ResolvedId}", MessageType.Error);
            }
        }
    }
}
#endif
