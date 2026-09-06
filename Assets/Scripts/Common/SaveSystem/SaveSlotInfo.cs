using System;

namespace TapTap21.SaveSystem.dyh
{
    [Serializable]
    public sealed class SaveSlotInfo
    {
        public string SlotId { get; private set; }
        public string DisplayName { get; private set; }
        public string SceneName { get; private set; }
        public long PlayTimeSeconds { get; private set; }
        public DateTime SavedAtUtc { get; private set; }
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

        public override string ToString()
        {
            return string.Format("{0} ({1}, {2:O})", SlotId, SceneName, SavedAtUtc);
        }
    }
}
