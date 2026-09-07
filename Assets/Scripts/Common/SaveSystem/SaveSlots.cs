namespace TapTap21.SaveSystem.dyh
{
    /// <summary>项目预置的固定槽位；动态槽位请使用 SaveSystem.CreateSlotId 创建。</summary>
    public static class SaveSlots
    {
        /// <summary>自动存档槽位。</summary>
        public static readonly SaveSlotId AutoSave = SaveSlotId.From("autosave");
        /// <summary>跨周目或角色共享的全局槽位。</summary>
        public static readonly SaveSlotId Global = SaveSlotId.From("global");
        /// <summary>第一个固定手动存档槽位。</summary>
        public static readonly SaveSlotId Slot0 = SaveSlotId.From("slot_0");
        /// <summary>第二个固定手动存档槽位。</summary>
        public static readonly SaveSlotId Slot1 = SaveSlotId.From("slot_1");
        /// <summary>第三个固定手动存档槽位。</summary>
        public static readonly SaveSlotId Slot2 = SaveSlotId.From("slot_2");
    }
}
