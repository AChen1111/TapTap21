using System;
using System.Collections.Generic;
using UnityEngine;

namespace TapTap21.AudioSystem.dyh
{
    /// <summary>音频配置注册表，负责建立音频 ID 到 AudioConfig 的索引。</summary>
    [CreateAssetMenu(menuName = "Audio/Audio Database")]
    public class AudioDatabase : ScriptableObject
    {
        /// <summary>所有可用的 AudioConfig 配置。</summary>
        [Header("所有可用的 AudioConfig 配置")]
        public AudioConfig[] entries;

        [NonSerialized]
        private Dictionary<string, AudioConfig> lookup;

        private void OnEnable()
        {
            lookup = null;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            var guids = UnityEditor.AssetDatabase.FindAssets("t:AudioConfig");
            var configs = new List<AudioConfig>();

            foreach (var guid in guids)
            {
                var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                var config = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioConfig>(path);
                if (config != null)
                    configs.Add(config);
            }

            configs.Sort((left, right) => string.Compare(left.GetId(), right.GetId(), StringComparison.Ordinal));
            entries = configs.ToArray();
            lookup = null;
        }
#endif

        /// <summary>按 ID 查找音频配置。</summary>
        public bool TryGet(string id, out AudioConfig config)
        {
            if (lookup == null)
            {
                lookup = new Dictionary<string, AudioConfig>(StringComparer.Ordinal);

                if (entries != null)
                {
                    foreach (var entry in entries)
                    {
                        if (entry == null || string.IsNullOrEmpty(entry.GetId()))
                            continue;

                        var entryId = entry.GetId();
                        if (lookup.ContainsKey(entryId))
                        {
                            Debug.LogWarning(
                                $"音频 ID 重复，后面的配置将覆盖前面的配置：{entryId}",
                                this);
                        }

                        lookup[entryId] = entry;
                    }
                }
            }

            var found = lookup.TryGetValue(id, out config);
            if (found)
                Debug.Log($"[AudioDatabase] 查找成功，id={id}，config={config.name}。", this);
            else
                Debug.LogWarning($"[AudioDatabase] 查找失败，id={id}。", this);

            return found;
        }
    }
}
