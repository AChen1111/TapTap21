using System;

namespace TapTap21.SaveSystem.dyh
{
    [Serializable]
    public sealed class SaveSlotInfo
    {
        /// <summary>兼容接口：内部稳定槽位字符串。</summary>
        public string SlotId { get; private set; }
        /// <summary>推荐接口：强类型内部槽位标识，可直接传给 SaveSystem。</summary>
        public SaveSlotId Id { get { return SaveSlotId.From(SlotId); } }
        /// <summary>玩家界面中显示的存档名称。</summary>
        public string DisplayName { get; private set; }
        /// <summary>保存时所在场景。</summary>
        public string SceneName { get; private set; }
        /// <summary>累计游戏秒数。</summary>
        public long PlayTimeSeconds { get; private set; }
        /// <summary>本次保存的 UTC 时间。</summary>
        public DateTime SavedAtUtc { get; private set; }
        /// <summary>槽位中包含的数据项数量。</summary>
        public int ItemCount { get; private set; }

        internal SaveSlotInfo(string slotId, string displayName, string sceneName, long playTimeSeconds, DateTime savedAtUtc, int itemCount)
        {
            SlotId = slotId;
            DisplayName = displayName ?? string.Empty;
            SceneName = sceneName ?? string.Empty;
            PlayTimeSeconds = playTimeSeconds;
            SavedAtUtc = savedAtUtc;
            ItemCount = itemCount;
        }

        /// <summary>返回便于日志阅读的槽位描述。</summary>
        /// <returns>包含槽位 ID、场景和保存时间的字符串。</returns>
        public override string ToString()
        {
            return string.Format("{0} ({1}, {2:O})", SlotId, SceneName, SavedAtUtc);
        }
    }
}
