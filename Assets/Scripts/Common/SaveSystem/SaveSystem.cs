using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TapTap21.SaveSystem.dyh
{
    /// <summary>存档框架的统一公开入口。业务代码无需直接接触 LitJSON 或文件 API。</summary>
    public static class SaveSystem
    {
        /// <summary>注册一个强类型存档定义。项目中的定义应集中写在 SaveDefinitions。</summary>
        /// <typeparam name="T">需要保存的纯 C# 数据类型。</typeparam>
        /// <param name="key">数据含义的稳定键，例如 player。</param>
        /// <param name="fileName">该数据在槽位内使用的文件名，例如 player.json。</param>
        /// <param name="version">当前数据格式版本，必须大于等于 1。</param>
        /// <returns>不可变的强类型存档定义。</returns>
        public static SaveDefinition<T> Register<T>(string key, string fileName, int version = 1)
        {
            return SaveDefinitionRegistry.Register<T>(key, fileName, version);
        }

        /// <summary>生成一个适合玩家动态存档的新槽位 ID。</summary>
        /// <returns>全新的稳定槽位标识。玩家名称应另存到快照元数据中。</returns>
        public static SaveSlotId CreateSlotId()
        {
            return SaveSlotId.From("slot_" + Guid.NewGuid().ToString("N"));
        }

        /// <summary>保存单项数据到指定槽位。</summary>
        /// <typeparam name="T">存档数据类型。</typeparam>
        /// <param name="definition">已注册的强类型存档定义。</param>
        /// <param name="slotId">程序内部稳定槽位标识。</param>
        /// <param name="data">已收集好的纯 C# 数据。</param>
        /// <returns>包含成功状态或错误原因的保存结果。</returns>
        public static SaveResult Save<T>(SaveDefinition<T> definition, SaveSlotId slotId, T data)
        {
            return Save(definition, slotId.Value, data);
        }

        /// <summary>使用兼容字符串槽位 ID 保存单项数据；新代码优先使用 SaveSlotId。</summary>
        /// <typeparam name="T">存档数据类型。</typeparam>
        /// <param name="definition">已注册的强类型存档定义。</param>
        /// <param name="slotId">内部槽位字符串，不是玩家显示名称。</param>
        /// <param name="data">已收集好的纯 C# 数据。</param>
        /// <returns>包含成功状态或错误原因的保存结果。</returns>
        public static SaveResult Save<T>(SaveDefinition<T> definition, string slotId, T data)
        {
            SaveResult validation = ValidateDefinition(definition, slotId);
            if (!validation.Success) return validation;
            string error = SaveValidator.Validate(definition, data);
            if (error != null) return SaveResult.Fail(SaveStatus.ValidationFailed, error);
            return SaveStorage.WriteItem(definition, slotId, data);
        }

        /// <summary>从指定槽位读取单项数据。</summary>
        /// <typeparam name="T">存档数据类型。</typeparam>
        /// <param name="definition">已注册的强类型存档定义。</param>
        /// <param name="slotId">程序内部稳定槽位标识。</param>
        /// <returns>包含数据、状态、消息及备份恢复标记的读取结果。</returns>
        public static LoadResult<T> Load<T>(SaveDefinition<T> definition, SaveSlotId slotId)
        {
            return Load(definition, slotId.Value);
        }

        /// <summary>使用兼容字符串槽位 ID 读取单项数据。</summary>
        /// <typeparam name="T">存档数据类型。</typeparam>
        /// <param name="definition">已注册的强类型存档定义。</param>
        /// <param name="slotId">内部槽位字符串。</param>
        /// <returns>包含数据、状态、消息及备份恢复标记的读取结果。</returns>
        public static LoadResult<T> Load<T>(SaveDefinition<T> definition, string slotId)
        {
            SaveResult validation = ValidateDefinition(definition, slotId);
            if (!validation.Success) return LoadResult<T>.Fail(validation.Status, validation.Message);
            return SaveStorage.ReadItem(definition, slotId);
        }

        /// <summary>尝试读取数据，成功时通过 out 参数返回数据。</summary>
        /// <typeparam name="T">存档数据类型。</typeparam>
        /// <param name="definition">已注册的强类型存档定义。</param>
        /// <param name="slotId">程序内部稳定槽位标识。</param>
        /// <param name="data">成功时得到数据，失败时为默认值。</param>
        /// <returns>读取成功返回 true。</returns>
        public static bool TryLoad<T>(SaveDefinition<T> definition, SaveSlotId slotId, out T data)
        {
            LoadResult<T> result = Load(definition, slotId);
            data = result.Data;
            return result.Success;
        }

        /// <summary>使用兼容字符串槽位 ID 尝试读取数据。</summary>
        /// <typeparam name="T">存档数据类型。</typeparam>
        /// <param name="definition">已注册的强类型存档定义。</param>
        /// <param name="slotId">内部槽位字符串。</param>
        /// <param name="data">成功时得到数据，失败时为默认值。</param>
        /// <returns>读取成功返回 true。</returns>
        public static bool TryLoad<T>(SaveDefinition<T> definition, string slotId, out T data)
        {
            LoadResult<T> result = Load(definition, slotId);
            data = result.Data;
            return result.Success;
        }

        /// <summary>判断指定槽位中是否存在该单项存档。</summary>
        /// <typeparam name="T">存档数据类型。</typeparam>
        /// <param name="definition">已注册的强类型存档定义。</param>
        /// <param name="slotId">程序内部稳定槽位标识。</param>
        /// <returns>正式存档文件存在时返回 true。</returns>
        public static bool Exists<T>(SaveDefinition<T> definition, SaveSlotId slotId) { return Exists(definition, slotId.Value); }

        /// <summary>使用兼容字符串槽位 ID 判断单项存档是否存在。</summary>
        /// <typeparam name="T">存档数据类型。</typeparam>
        /// <param name="definition">已注册的强类型存档定义。</param>
        /// <param name="slotId">内部槽位字符串。</param>
        /// <returns>正式存档文件存在时返回 true。</returns>
        public static bool Exists<T>(SaveDefinition<T> definition, string slotId)
        {
            if (definition == null || !SaveDefinitionRegistry.IsRegistered(definition)) return false;
            try
            {
                string path = Path.Combine(SaveStorage.GetSlotPath(slotId), definition.FileName);
                return SaveStorage.SlotExists(slotId) && File.Exists(path);
            }
            catch { return false; }
        }

        /// <summary>删除指定槽位中的单项数据。</summary>
        /// <typeparam name="T">存档数据类型。</typeparam>
        /// <param name="definition">已注册的强类型存档定义。</param>
        /// <param name="slotId">程序内部稳定槽位标识。</param>
        /// <returns>删除结果。</returns>
        public static SaveResult Delete<T>(SaveDefinition<T> definition, SaveSlotId slotId) { return Delete(definition, slotId.Value); }

        /// <summary>使用兼容字符串槽位 ID 删除单项数据。</summary>
        /// <typeparam name="T">存档数据类型。</typeparam>
        /// <param name="definition">已注册的强类型存档定义。</param>
        /// <param name="slotId">内部槽位字符串。</param>
        /// <returns>删除结果。</returns>
        public static SaveResult Delete<T>(SaveDefinition<T> definition, string slotId)
        {
            SaveResult validation = ValidateDefinition(definition, slotId);
            return validation.Success ? SaveStorage.DeleteItem(definition, slotId) : validation;
        }

        /// <summary>为整槽位批量保存创建快照。</summary>
        /// <param name="slotId">程序内部稳定槽位标识。</param>
        /// <returns>可继续设置元数据和各项 DTO 的快照。</returns>
        public static SaveSnapshot CreateSnapshot(SaveSlotId slotId) { return CreateSnapshot(slotId.Value); }

        /// <summary>使用兼容字符串槽位 ID 创建快照。</summary>
        /// <param name="slotId">内部槽位字符串。</param>
        /// <returns>可继续设置元数据和各项 DTO 的快照。</returns>
        public static SaveSnapshot CreateSnapshot(string slotId)
        {
            SaveStorage.GetSlotPath(slotId);
            return new SaveSnapshot(slotId);
        }

        /// <summary>将快照中的全部数据以临时目录和备份目录方式一致性保存。</summary>
        /// <param name="snapshot">调用者已经收集完成的数据快照。</param>
        /// <returns>整个槽位的保存结果；任一项失败时不会提交半套正式存档。</returns>
        public static SaveResult SaveSlot(SaveSnapshot snapshot)
        {
            if (snapshot == null) return SaveResult.Fail(SaveStatus.ValidationFailed, "snapshot 不能为 null。");
            if (snapshot.Count == 0) return SaveResult.Fail(SaveStatus.ValidationFailed, "snapshot 至少需要一项数据。");
            foreach (SaveSnapshot.Entry entry in snapshot.Entries)
            {
                if (entry.DefinitionObject == null) return SaveResult.Fail(SaveStatus.InvalidDefinition, "snapshot 包含无效定义。");
                string error = entry.Validate == null ? "snapshot 缺少数据校验器。" : entry.Validate(entry.Data);
                if (error != null) return SaveResult.Fail(SaveStatus.ValidationFailed, error);
            }
            return SaveStorage.WriteSlot(snapshot);
        }

        /// <summary>读取整个槽位的摘要，并允许通过结果继续读取各项数据。</summary>
        /// <param name="slotId">程序内部稳定槽位标识。</param>
        /// <returns>槽位读取结果。</returns>
        public static LoadSlotResult LoadSlot(SaveSlotId slotId) { return LoadSlot(slotId.Value); }

        /// <summary>使用兼容字符串槽位 ID 读取整个槽位。</summary>
        /// <param name="slotId">内部槽位字符串。</param>
        /// <returns>槽位读取结果。</returns>
        public static LoadSlotResult LoadSlot(string slotId) { return SaveStorage.ReadSlot(slotId); }

        /// <summary>判断槽位是否存在。</summary>
        /// <param name="slotId">程序内部稳定槽位标识。</param>
        /// <returns>正式槽位目录存在时返回 true。</returns>
        public static bool ExistsSlot(SaveSlotId slotId) { return ExistsSlot(slotId.Value); }

        /// <summary>使用兼容字符串槽位 ID 判断槽位是否存在。</summary>
        public static bool ExistsSlot(string slotId) { return SaveStorage.SlotExists(slotId); }

        /// <summary>删除整个槽位及其备份。</summary>
        public static SaveResult DeleteSlot(SaveSlotId slotId) { return DeleteSlot(slotId.Value); }

        /// <summary>使用兼容字符串槽位 ID 删除整个槽位及其备份。</summary>
        public static SaveResult DeleteSlot(string slotId) { return SaveStorage.DeleteSlot(slotId); }

        /// <summary>获取指定槽位的摘要；槽位不存在或损坏时返回 null。</summary>
        public static SaveSlotInfo GetSlotInfo(SaveSlotId slotId) { return GetSlotInfo(slotId.Value); }

        /// <summary>使用兼容字符串槽位 ID 获取摘要。</summary>
        public static SaveSlotInfo GetSlotInfo(string slotId) { return SaveStorage.GetInfo(slotId); }

        /// <summary>获取所有可读取的正式槽位摘要。</summary>
        /// <returns>用于存档列表 UI 的只读摘要集合。</returns>
        public static IReadOnlyList<SaveSlotInfo> GetAllSlotInfos() { return SaveStorage.GetAllInfos().AsReadOnly(); }

        /// <summary>异步保存单项纯 C# 数据。</summary>
        public static Task<SaveResult> SaveAsync<T>(SaveDefinition<T> definition, SaveSlotId slotId, T data) { return SaveAsync(definition, slotId.Value, data); }

        /// <summary>使用兼容字符串槽位 ID 异步保存；后台线程不得接触 Unity 对象。</summary>
        public static Task<SaveResult> SaveAsync<T>(SaveDefinition<T> definition, string slotId, T data)
        {
            string rootPath = SaveStorage.RootPath;
            return Task.Run(() => SaveStorage.WithRoot(rootPath, () => Save(definition, slotId, data)));
        }

        /// <summary>异步读取单项纯 C# 数据。</summary>
        public static Task<LoadResult<T>> LoadAsync<T>(SaveDefinition<T> definition, SaveSlotId slotId) { return LoadAsync(definition, slotId.Value); }

        /// <summary>使用兼容字符串槽位 ID 异步读取。</summary>
        public static Task<LoadResult<T>> LoadAsync<T>(SaveDefinition<T> definition, string slotId)
        {
            string rootPath = SaveStorage.RootPath;
            return Task.Run(() => SaveStorage.WithRoot(rootPath, () => Load(definition, slotId)));
        }

        /// <summary>异步一致性保存整个快照。</summary>
        /// <param name="snapshot">已收集完成的纯 C# 数据快照。</param>
        /// <returns>可等待的槽位保存结果。</returns>
        public static Task<SaveResult> SaveSlotAsync(SaveSnapshot snapshot)
        {
            string rootPath = SaveStorage.RootPath;
            return Task.Run(() => SaveStorage.WithRoot(rootPath, () => SaveSlot(snapshot)));
        }

        internal static bool IsDefinitionRegistered<T>(SaveDefinition<T> definition) { return SaveDefinitionRegistry.IsRegistered(definition); }

        private static SaveResult ValidateDefinition<T>(SaveDefinition<T> definition, string slotId)
        {
            if (definition == null) return SaveResult.Fail(SaveStatus.InvalidDefinition, "definition 不能为 null。");
            if (!SaveDefinitionRegistry.IsRegistered(definition)) return SaveResult.Fail(SaveStatus.InvalidDefinition, "definition 未在注册中心登记。");
            try { SaveStorage.GetSlotPath(slotId); }
            catch (Exception ex) { return SaveResult.Fail(SaveStatus.InvalidSlot, ex.Message); }
            return SaveResult.Ok();
        }
    }
}
