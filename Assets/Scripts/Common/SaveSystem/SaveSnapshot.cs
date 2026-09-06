using System;
using System.Collections.Generic;

namespace TapTap21.SaveSystem.dyh
{
    /// <summary>Explicit collection of DTOs to be committed as one slot.</summary>
    public sealed class SaveSnapshot
    {
        internal sealed class Entry
        {
            public object Data;
            public object DefinitionObject;
            public SaveDefinitionAdapter Definition;
            public Func<object, string> Validate;
        }

        // Adapter keeps the heterogeneous entries private while retaining their strong public API.
        internal sealed class SaveDefinitionAdapter
        {
            public string Key;
            public string FileName;
            public Type DataType;
            public int Version;
            public SaveDefinitionAdapter(string key, string fileName, Type dataType, int version) { Key = key; FileName = fileName; DataType = dataType; Version = version; }
        }

        private readonly Dictionary<string, Entry> entries = new Dictionary<string, Entry>(StringComparer.Ordinal);
        public string SlotId { get; private set; }
        public string DisplayName { get; private set; }
        public string SceneName { get; private set; }
        public long PlayTimeSeconds { get; private set; }
        internal int Count { get { return entries.Count; } }
        internal IEnumerable<Entry> Entries { get { return entries.Values; } }

        internal SaveSnapshot(string slotId)
        {
            SlotId = slotId;
            DisplayName = slotId;
            SceneName = string.Empty;
        }

        public SaveSnapshot SetMetadata(string displayName, string sceneName, long playTimeSeconds)
        {
            if (playTimeSeconds < 0) throw new ArgumentOutOfRangeException("playTimeSeconds");
            DisplayName = displayName ?? string.Empty;
            SceneName = sceneName ?? string.Empty;
            PlayTimeSeconds = playTimeSeconds;
            return this;
        }

        public SaveSnapshot Set<T>(SaveDefinition<T> definition, T data)
        {
            if (definition == null) throw new ArgumentNullException("definition");
            if (!SaveSystem.IsDefinitionRegistered(definition)) throw new ArgumentException("未注册的存档定义。", "definition");
            entries[definition.Key] = new Entry
            {
                Data = data,
                DefinitionObject = definition,
                Definition = new SaveDefinitionAdapter(definition.Key, definition.FileName, definition.DataType, definition.Version),
                Validate = value => SaveValidator.Validate(definition, (T)value)
            };
            return this;
        }

        public bool Contains<T>(SaveDefinition<T> definition) { return definition != null && entries.ContainsKey(definition.Key); }
    }
}
