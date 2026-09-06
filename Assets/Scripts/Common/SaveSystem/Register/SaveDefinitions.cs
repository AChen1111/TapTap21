using System;
using System.Collections.Generic;

namespace TapTap21.SaveSystem.dyh
{
    public static class SaveDefinitions
    {
        public static readonly SaveDefinition<SaveData> Main 
                = SaveSystem.Register<SaveData>("main", "main.json", 1);
        public static readonly SaveDefinition<PlayerSaveData> Player 
                = SaveSystem.Register<PlayerSaveData>("player", "player.json", 1);
        public static readonly SaveDefinition<InventorySaveData> Inventory 
                = SaveSystem.Register<InventorySaveData>("inventory", "inventory.json", 1);
        public static readonly SaveDefinition<SettingsSaveData> Settings 
                = SaveSystem.Register<SettingsSaveData>("settings", "settings.json", 1);
    }

    [Serializable]
    public class SaveData
    {
        public int version = 1;
    }

    [Serializable]
    public class PlayerSaveData
    {
        public int level;
        public int coins;
    }

    public enum InventoryItemKind { None, Consumable, Equipment }
    [Serializable]
    public class InventoryItemSaveData
    {
        public string id;
        public int quantity;
        public InventoryItemKind kind;
    }

    [Serializable]
    public class InventorySaveData
    {
        public List<InventoryItemSaveData> items = new List<InventoryItemSaveData>();
        public Dictionary<string, int> counters = new Dictionary<string, int>();
    }

    [Serializable]
    public class SettingsSaveData
    {
        public string language = "zh-CN";
        public float masterVolume = 1f;
    }

}
