# 本地存档系统使用文档

这是一套 Unity 本地 JSON 存档框架。你只需要做三件事：定义纯 C# 数据、在注册中心登记、调用 `SaveSystem`。LitJSON、文件读写、临时文件、备份、校验和版本处理都由框架完成。

命名空间：

```csharp
using TapTap21.SaveSystem.dyh;
```

## 5 分钟上手

### 1. 定义数据

```csharp
[Serializable]
public class PlayerSaveData
{
    public int level;
    public int coins;
}
```

只能保存普通数据：`int`、`float`、`bool`、`string`、枚举、数组、`List<T>`、`Dictionary<TKey,TValue>` 和嵌套 DTO。不要直接保存 `GameObject`、`MonoBehaviour`、`Transform`、Prefab、`ScriptableObject` 或其他 `UnityEngine.Object`，请转换成 ID、坐标和状态。

### 2. 集中注册

在 `Register/SaveDefinitions.cs` 中注册一次：

```csharp
public static readonly SaveDefinition<PlayerSaveData> Player =
    SaveSystem.Register<PlayerSaveData>("player", "player.json", 1);
```

之后业务代码只使用 `SaveDefinitions.Player`，不要再次填写 key 和文件名。

### 3. 保存和读取

```csharp
PlayerSaveData data = new PlayerSaveData
{
    level = 10,
    coins = 500
};

SaveResult save = SaveSystem.Save(
    SaveDefinitions.Player,
    SaveSlots.Slot0,
    data
);

LoadResult<PlayerSaveData> load = SaveSystem.Load(
    SaveDefinitions.Player,
    SaveSlots.Slot0
);

if (load.Success)
{
    PlayerSaveData player = load.Data;
}
```

## 三个名字必须分清

| 名称 | 含义 | 示例 | 谁负责填写 |
|---|---|---|---|
| `key` | 保存的是什么数据 | `player` | 注册中心 |
| `fileName` | 该数据使用的逻辑文件名 | `player.json` | 注册中心 |
| `slotId` | 哪一份存档 | `slot_0`、`autosave` | `SaveSlots` 或框架生成 |
| `DisplayName` | 玩家看到的名称 | `Boss 战前` | 快照元数据 |

默认路径：`Application.persistentDataPath/Saves/{slotId}/{fileName}`。

## 槽位 ID：固定槽位和玩家命名

### 固定槽位

使用集中定义的强类型字段，不要散落裸字符串：

```csharp
SaveSlots.AutoSave
SaveSlots.Global
SaveSlots.Slot0
SaveSlots.Slot1
SaveSlots.Slot2
```

示例：

```csharp
SaveSystem.Save(SaveDefinitions.Player, SaveSlots.AutoSave, data);
```

### 玩家动态存档

玩家输入的名称不是 `slotId`。程序生成稳定 ID，名称只存进元数据：

```csharp
SaveSlotId id = SaveSystem.CreateSlotId();

SaveSnapshot snapshot = SaveSystem.CreateSnapshot(id)
    .SetMetadata("Boss 战前", "BossScene", 3600)
    .Set(SaveDefinitions.Player, data);

SaveSystem.SaveSlot(snapshot);
```

目录使用类似 `slot_a84f...` 的内部 ID，界面显示 `Boss 战前`。因此玩家可以重命名存档，也不会因中文或特殊字符破坏路径。

已有字符串可显式转换：

```csharp
SaveSlotId id = SaveSlotId.From("slot_0");
SaveSlotId.TryFrom(input, out id);
```

旧代码仍可传 `string slotId`，但新代码推荐 `SaveSlotId`。

## 接口速查

### SaveSystem：注册和生成 ID

```csharp
SaveDefinition<T> Register<T>(string key, string fileName, int version = 1);
SaveSlotId CreateSlotId();
```

`Register` 检查空值、重复 key/fileName、非法路径、版本和 Unity 对象类型。失败抛出 `SaveRegistrationException`。

### SaveSystem：单项数据

```csharp
SaveResult Save<T>(SaveDefinition<T> definition, SaveSlotId slotId, T data);
LoadResult<T> Load<T>(SaveDefinition<T> definition, SaveSlotId slotId);
bool TryLoad<T>(SaveDefinition<T> definition, SaveSlotId slotId, out T data);
bool Exists<T>(SaveDefinition<T> definition, SaveSlotId slotId);
SaveResult Delete<T>(SaveDefinition<T> definition, SaveSlotId slotId);
```

每个方法也保留 `string slotId` 重载。

```csharp
if (SaveSystem.TryLoad(SaveDefinitions.Player, SaveSlots.Slot0, out PlayerSaveData player))
{
    Debug.Log(player.level);
}

SaveSystem.Delete(SaveDefinitions.Player, SaveSlots.Slot0);
```

### SaveSnapshot：批量数据

```csharp
SaveSnapshot CreateSnapshot(SaveSlotId slotId);
SaveSnapshot SetMetadata(string displayName, string sceneName, long playTimeSeconds);
SaveSnapshot Set<T>(SaveDefinition<T> definition, T data);
bool Contains<T>(SaveDefinition<T> definition);
```

快照是“本次整槽位保存的数据清单”。它不会自动扫描场景；调用者必须主动把玩家、背包、任务等 DTO 放进去。

```csharp
SaveSnapshot snapshot = SaveSystem.CreateSnapshot(SaveSlots.Slot0)
    .SetMetadata("第一槽位", "SampleScene", 1200)
    .Set(SaveDefinitions.Player, playerData)
    .Set(SaveDefinitions.Inventory, inventoryData);
```

### SaveSystem：整槽位

```csharp
SaveResult SaveSlot(SaveSnapshot snapshot);
LoadSlotResult LoadSlot(SaveSlotId slotId);
bool ExistsSlot(SaveSlotId slotId);
SaveResult DeleteSlot(SaveSlotId slotId);
SaveSlotInfo GetSlotInfo(SaveSlotId slotId);
IReadOnlyList<SaveSlotInfo> GetAllSlotInfos();
```

示例：

```csharp
SaveResult saved = SaveSystem.SaveSlot(snapshot);

LoadSlotResult loaded = SaveSystem.LoadSlot(SaveSlots.Slot0);
if (loaded.Success)
{
    LoadResult<PlayerSaveData> player =
        loaded.Get(SaveDefinitions.Player);
}
```

批量保存先写临时目录，所有文件成功后才替换正式目录，旧目录保留为备份，不会留下半套正式存档。

### SaveSlotInfo：存档列表

```csharp
IReadOnlyList<SaveSlotInfo> list = SaveSystem.GetAllSlotInfos();
foreach (SaveSlotInfo info in list)
{
    Debug.Log(info.DisplayName);
    LoadSlotResult result = SaveSystem.LoadSlot(info.Id);
}
```

常用属性：`Id`、`SlotId`、`DisplayName`、`SceneName`、`PlayTimeSeconds`、`SavedAtUtc`、`ItemCount`。

### 异步接口

```csharp
Task<SaveResult> SaveAsync<T>(SaveDefinition<T> definition, SaveSlotId slotId, T data);
Task<LoadResult<T>> LoadAsync<T>(SaveDefinition<T> definition, SaveSlotId slotId);
Task<SaveResult> SaveSlotAsync(SaveSnapshot snapshot);
```

适用于数据量较大或自动保存，避免序列化和文件写入卡住主线程：

```csharp
PlayerSaveData data = CollectDataOnMainThread();
SaveResult result = await SaveSystem.SaveAsync(
    SaveDefinitions.Player,
    SaveSlots.AutoSave,
    data
);
```

异步前必须先在主线程把 Unity 对象转换成 DTO，后台线程不能访问 `GameObject`、`Transform` 等 Unity 对象。

## 结果和错误处理

所有操作都返回结果对象（`TryLoad`/`Exists` 除外）：

```csharp
if (!result.Success)
{
    Debug.LogError(result.Status + ": " + result.Message);
}
```

`SaveStatus` 包括：`Success`、`NotFound`、`InvalidSlot`、`InvalidDefinition`、`DuplicateKey`、`DuplicateFileName`、`TypeMismatch`、`SerializeFailed`、`DeserializeFailed`、`ReadFailed`、`WriteFailed`、`ValidationFailed`、`VersionUnsupported`、`MigrationFailed`、`RecoveredFromBackup`。

读取正式文件失败但备份可用时，结果为成功状态 `RecoveredFromBackup`，并且 `UsedBackup == true`。

## 校验

### 必填字段

```csharp
[SaveRequired]
public string playerId;
```

### 业务规则

```csharp
SaveValidator.Register(SaveDefinitions.Player, data =>
    data.coins < 0 ? "金币不能小于 0。" : null);
```

返回 `null` 表示通过，否则保存和读取都会返回 `ValidationFailed`。

## 版本迁移

当 DTO 增加字段或改变格式时，在注册中心把版本号提高（下面示例表示修改注册行的最后一个参数）：

```csharp
public static readonly SaveDefinition<PlayerSaveData> Player =
    SaveSystem.Register<PlayerSaveData>("player", "player.json", 2);
```

注册旧版本到新版本的转换：

```csharp
SaveMigration.Register(
    SaveDefinitions.Player,
    1,
    2,
    oldData =>
    {
        oldData.experience = 0;
        return oldData;
    }
);
```

读取 v1 存档时框架会自动执行 `v1 → v2`，迁移后再校验。多个版本应连续注册：`1 → 2`、`2 → 3`。存档版本高于当前版本返回 `VersionUnsupported`，迁移链不完整或抛异常返回 `MigrationFailed`。

## 安全和文件可靠性

- 单项文件使用 `.tmp` 和 `.bak` 替换。
- 整槽位使用临时目录和备份目录原子替换。
- 读取正式文件失败会尝试备份。
- `slotId`、`key`、`fileName` 禁止绝对路径、目录分隔符、路径穿越和非法文件名。
- SHA-256 校验和用于发现 JSON 内容被篡改或损坏。

## LitJSON 分层

LitJSON 只存在于内部 `LitJsonSerializer` 适配层，位于 `Assets/Plugins/LitJSON/LitJSON.dll`。业务代码不需要调用 `JsonMapper`、`File.ReadAllText` 或 `Application.persistentDataPath`。

## 测试

测试文件：`Assets/Tests/SaveSystem/SaveSystemTests.cs`。打开 Unity 的 `Window → General → Test Runner`，选择 `EditMode`，运行 `SaveSystemTests`。
