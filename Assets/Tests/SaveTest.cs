using UnityEngine;
using Sirenix.OdinInspector;
using TapTap21.SaveSystem.dyh;
using Unity.VisualScripting;

public class SaveTest : MonoBehaviour
{
    public PlayerSaveData playerSaveData;

    void Awake()
    {
        LoadResult<PlayerSaveData> res = SaveSystem.Load(SaveDefinitions.Player, SaveSlots.Slot0);
        if (!res.Success)
        {
            Debug.LogError("读取失败" + res.Message);
            return;
        }
        playerSaveData = res.Data;
    }

    [Button]
    public void AddSaveData()
    {
        playerSaveData.coins += 1;
        playerSaveData.level += 1;

        Debug.Log("Add成功");
    }
    [Button("保存")]
    public void Save()
    {
        SaveSystem.Save(SaveDefinitions.Player, SaveSlots.Slot0, playerSaveData);
    }
}
