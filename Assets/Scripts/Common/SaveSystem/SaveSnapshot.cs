using System;
using System.Collections.Generic;

namespace TapTap21.SaveSystem.dyh
{
    /// <summary>由调用者显式收集、并作为一个完整槽位提交的纯 C# 数据快照。</summary>
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
        /// <summary>兼容接口：获取内部槽位字符串。</summary>
        public string SlotId { get; private set; }
        /// <summary>获取强类型内部槽位标识。</summary>
        public SaveSlotId Id { get { return SaveSlotId.From(SlotId); } }
        /// <summary>玩家界面中显示的存档名称。</summary>
        public string DisplayName { get; private set; }
        /// <summary>保存时所在的场景名称。</summary>
        public string SceneName { get; private set; }
        /// <summary>累计游戏时长，单位为秒。</summary>
        public long PlayTimeSeconds { get; private set; }
        internal int Count { get { return entries.Count; } }
        internal IEnumerable<Entry> Entries { get { return entries.Values; } }

        internal SaveSnapshot(string slotId)
        {
            SlotId = slotId;
            DisplayName = slotId;
            SceneName = string.Empty;
        }

        /// <summary>设置玩家可见的槽位摘要信息。</summary>
        /// <param name="displayName">玩家看到的存档名称，可以包含中文和空格。</param>
        /// <param name="sceneName">当前场景名称。</param>
        /// <param name="playTimeSeconds">累计游戏秒数，不能小于 0。</param>
        /// <returns>当前快照，便于链式调用。</returns>
        public SaveSnapshot SetMetadata(string displayName, string sceneName, long playTimeSeconds)
        {
            if (playTimeSeconds < 0) throw new ArgumentOutOfRangeException("playTimeSeconds");
            DisplayName = displayName ?? string.Empty;
            SceneName = sceneName ?? string.Empty;
            PlayTimeSeconds = playTimeSeconds;
            return this;
        }

        /// <summary>将一项强类型数据加入快照；相同 key 会覆盖快照内的旧值。</summary>
        /// <typeparam name="T">存档数据类型。</typeparam>
        /// <param name="definition">已注册的强类型定义。</param>
        /// <param name="data">需要保存的纯 C# 数据。</param>
        /// <returns>当前快照，便于继续添加数据。</returns>
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

        /// <summary>判断快照是否已经包含指定定义的数据。</summary>
        /// <typeparam name="T">存档数据类型。</typeparam>
        /// <param name="definition">需要查询的定义。</param>
        /// <returns>包含该定义时返回 true。</returns>
        public bool Contains<T>(SaveDefinition<T> definition) { return definition != null && entries.ContainsKey(definition.Key); }
    }
}
