using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TapTap21.SaveSystem.dyh
{
    /// <summary>Public facade for the reusable local save framework.</summary>
    public static class SaveSystem
    {
        public static SaveDefinition<T> Register<T>(string key, string fileName, int version = 1)
        {
            return SaveDefinitionRegistry.Register<T>(key, fileName, version);
        }

        public static SaveResult Save<T>(SaveDefinition<T> definition, string slotId, T data)
        {
            SaveResult validation = ValidateDefinition(definition, slotId);
            if (!validation.Success) return validation;
            string error = SaveValidator.Validate(definition, data);
            if (error != null) return SaveResult.Fail(SaveStatus.ValidationFailed, error);
            return SaveStorage.WriteItem(definition, slotId, data);
        }

        public static LoadResult<T> Load<T>(SaveDefinition<T> definition, string slotId)
        {
            SaveResult validation = ValidateDefinition(definition, slotId);
            if (!validation.Success) return LoadResult<T>.Fail(validation.Status, validation.Message);
            return SaveStorage.ReadItem(definition, slotId);
        }

        public static bool TryLoad<T>(SaveDefinition<T> definition, string slotId, out T data)
        {
            LoadResult<T> result = Load(definition, slotId);
            data = result.Data;
            return result.Success;
        }

        public static bool Exists<T>(SaveDefinition<T> definition, string slotId)
        {
            if (definition == null || !SaveDefinitionRegistry.IsRegistered(definition)) return false;
            try { return SaveStorage.SlotExists(slotId) && System.IO.File.Exists(System.IO.Path.Combine(SaveStorage.GetSlotPath(slotId), definition.FileName)); } catch { return false; }
        }

        public static SaveResult Delete<T>(SaveDefinition<T> definition, string slotId)
        {
            SaveResult validation = ValidateDefinition(definition, slotId);
            return validation.Success ? SaveStorage.DeleteItem(definition, slotId) : validation;
        }

        public static SaveSnapshot CreateSnapshot(string slotId)
        {
            // Validation is performed here so an invalid slot is reported before any data is collected.
            SaveStorage.GetSlotPath(slotId);
            return new SaveSnapshot(slotId);
        }

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

        public static LoadSlotResult LoadSlot(string slotId) { return SaveStorage.ReadSlot(slotId); }
        public static bool ExistsSlot(string slotId) { return SaveStorage.SlotExists(slotId); }
        public static SaveResult DeleteSlot(string slotId) { return SaveStorage.DeleteSlot(slotId); }
        public static SaveSlotInfo GetSlotInfo(string slotId) { return SaveStorage.GetInfo(slotId); }
        public static IReadOnlyList<SaveSlotInfo> GetAllSlotInfos() { return SaveStorage.GetAllInfos().AsReadOnly(); }

        public static Task<SaveResult> SaveAsync<T>(SaveDefinition<T> definition, string slotId, T data)
        {
            string rootPath = SaveStorage.RootPath;
            return Task.Run(() => SaveStorage.WithRoot(rootPath, () => Save(definition, slotId, data)));
        }

        public static Task<LoadResult<T>> LoadAsync<T>(SaveDefinition<T> definition, string slotId)
        {
            string rootPath = SaveStorage.RootPath;
            return Task.Run(() => SaveStorage.WithRoot(rootPath, () => Load(definition, slotId)));
        }

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
