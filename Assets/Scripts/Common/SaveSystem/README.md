# Local Save System

这是一个与游戏类型无关的本地 JSON 存档框架，适用于 Game Jam 预代码。项目使用 Unity `6000.3.23f1`。检查项目依赖后确认原先没有 LitJSON 引用，因此加入 NuGet LitJSON `0.19.0` 的单独 `Assets/Plugins/LitJSON/LitJSON.dll`（Unlicense，来源 https://github.com/LitJSON/litjson）；业务代码不会引用它。

## 三个名称

* `key` 表示保存的内容，例如 `player`、`inventory`。
* `slotId` 表示哪一份存档，例如 `slot_0`、`autosave`、`global`。
* `fileName` 是定义中登记的逻辑文件名，例如 `player.json`。

默认路径为 `Application.persistentDataPath/Saves/{slotId}/{fileName}`。框架不会把 slotId 当作玩家显示名称；`SaveSlotInfo` 中的 `DisplayName`、场景和游玩时间是独立元数据。

## 注册定义

所有定义集中在 `SaveDefinitions.cs`。普通业务代码只引用这些静态定义，不应再次调用 `Register`：

```csharp
[Serializable]
public class PlayerSaveData
{
    public int level;
    public int coins;
}

public static readonly SaveDefinition<PlayerSaveData> Player =
    SaveSystem.Register<PlayerSaveData>("player", "player.json", 1);
```

`SaveDefinition<T>` 将 key、fileName、数据类型和格式版本绑定为只读对象。注册会检查空值、重复 key/fileName、路径穿越、非法文件名、版本和 Unity 对象类型；失败时抛出 `SaveRegistrationException`。

定义上的 `Version` 是 JSON 格式版本，写入 envelope；DTO 自己的 `version` 字段（如果有）只是业务字段，两者不需要强行同步。

## 单项保存和读取

```csharp
PlayerSaveData value = new PlayerSaveData { level = 10, coins = 500 };
SaveResult saved = SaveSystem.Save(SaveDefinitions.Player, "slot_0", value);

LoadResult<PlayerSaveData> loaded = SaveSystem.Load(SaveDefinitions.Player, "slot_0");
if (loaded.Success)
{
    PlayerSaveData player = loaded.Data;
}

if (SaveSystem.TryLoad(SaveDefinitions.Player, "slot_0", out PlayerSaveData playerAgain))
    Debug.Log(playerAgain.level);
```

可用公开方法还包括 `Exists`、`Delete`、`SaveAsync` 和 `LoadAsync`。所有结果都包含 `Success`、`Status`、`Message` 和 `UsedBackup`；读取备份时状态是 `RecoveredFromBackup`。

## 整个槽位

框架不会扫描场景猜测哪些对象应该保存。调用者先收集纯 C# 快照，再一次性提交：

```csharp
SaveSnapshot snapshot = SaveSystem.CreateSnapshot("slot_0")
    .SetMetadata("第一槽", "SampleScene", 3600);
snapshot.Set(SaveDefinitions.Player, playerData);
snapshot.Set(SaveDefinitions.Inventory, inventoryData);

SaveResult saved = SaveSystem.SaveSlot(snapshot);
LoadSlotResult slot = SaveSystem.LoadSlot("slot_0");
if (slot.Success)
{
    LoadResult<PlayerSaveData> player = slot.Get(SaveDefinitions.Player);
}
```

批量保存先写 `slot_0.tmp` 目录；全部数据、校验和与元数据写成功后，旧目录移动为 `slot_0.bak`，临时目录才会替换正式目录。因此中途序列化或写入失败不会产生半套正式存档。`LoadSlot` 返回摘要，`GetSlotInfo` 和 `GetAllSlotInfos` 可用于存档列表 UI。

## 可靠性、版本和校验

单项文件使用 `file.json`、`file.json.tmp`、`file.json.bak`；批量保存使用临时目录和备份目录。每个文件包含 key、完整类型名、版本、UTC 时间、payload 和 SHA-256 校验和。正式文件损坏或缺失时自动尝试备份，不会让游戏因存档损坏崩溃。

当前版本高于定义版本时返回 `VersionUnsupported`。旧版本可按连续步骤迁移：

```csharp
SaveMigration.Register(SaveDefinitions.Player, 1, 2, oldData =>
{
    // 修改并返回纯 DTO
    return oldData;
});
```

迁移步骤按 `key`、`fromVersion`、`toVersion` 组织，失败时返回 `MigrationFailed`。字段可标记 `[SaveRequired]` 参与通用必填检查；还可以通过 `SaveValidator.Register(definition, data => errorMessage)` 添加金币、等级、物品 ID 等业务校验；返回 `null` 表示通过。

## 数据边界

DTO 必须是 `[Serializable]` 标记的纯 C# 类型，支持基本类型、枚举、数组、`List<T>`、`Dictionary<TKey,TValue>`、嵌套 DTO。不要直接保存 `GameObject`、`MonoBehaviour`、`Transform`、`Sprite`、`Texture`、`Prefab`、`ScriptableObject` 或其它 `UnityEngine.Object`；请转换为稳定 ID、坐标、旋转和普通状态。

LitJSON 只位于 `LitJsonSerializer` 适配层，业务代码不需要接触 `JsonMapper`、`File` 或 `Application.persistentDataPath`。异步 API 只应传入已经收集好的纯 C# 数据；后台线程仅进行序列化和文件操作，不访问 Unity 对象。

## 测试

测试位于 `Assets/Tests/SaveSystem/SaveSystemTests.cs`，覆盖基本类型/枚举/List/Dictionary/嵌套 DTO、多槽位、缺失和非法 slot、损坏文件备份恢复、重复注册、批量槽位、删除、类型/版本错误、迁移和摘要。打开 Unity Test Runner，选择 **EditMode**，运行 `SaveSystemTests` 即可。测试使用唯一 slot，并在 TearDown 中清理。

后续可在此基础上增加自动保存调度器（由业务传入快照工厂）、场景切换后的恢复钩子、云存档和服务器权威数据；框架不会自动扫描场景或把 PlayerPrefs 当作游戏进度。
