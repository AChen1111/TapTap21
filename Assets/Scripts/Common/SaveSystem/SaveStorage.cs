using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

namespace TapTap21.SaveSystem.dyh
{
    internal static class SaveStorage
    {
        public const string MetadataFileName = "_slot.json";
        private static readonly ISaveSerializer serializer = new LitJsonSerializer();
        [ThreadStatic] private static string threadRootPath;

        public static string RootPath { get { return Path.Combine(threadRootPath ?? Application.persistentDataPath, "Saves"); } }

        internal static TResult WithRoot<TResult>(string rootPath, Func<TResult> action)
        {
            string previous = threadRootPath;
            threadRootPath = rootPath;
            try { return action(); }
            finally { threadRootPath = previous; }
        }

        public static SaveResult WriteItem<T>(SaveDefinition<T> definition, string slotId, T data)
        {
            string slotPath = GetSlotPath(slotId);
            string path = Path.Combine(slotPath, definition.FileName);
            string temp = path + ".tmp";
            string backup = path + ".bak";
            try
            {
                Directory.CreateDirectory(slotPath);
                string dataJson = serializer.Serialize(data, typeof(T));
                var envelope = CreateEnvelope(definition, dataJson);
                File.WriteAllText(temp, JsonMapper.ToJson(envelope));
                ReplaceFile(temp, path, backup);
                UpdateMetadata(slotId, slotPath);
                return SaveResult.Ok();
            }
            catch (Exception ex) { return SaveResult.Fail(ClassifyWriteError(ex), ex.Message); }
        }

        public static LoadResult<T> ReadItem<T>(SaveDefinition<T> definition, string slotId)
        {
            string slotPath;
            try { slotPath = GetSlotPath(slotId); } catch (Exception ex) { return LoadResult<T>.Fail(SaveStatus.InvalidSlot, ex.Message); }
            string path = Path.Combine(slotPath, definition.FileName);
            string backup = path + ".bak";
            string error;
            LoadResult<T> result = ReadItemPath(definition, path, false, out error);
            if (result.Success) return result;
            LoadResult<T> backupResult = ReadItemPath(definition, backup, true, out error);
            if (backupResult.Success) return backupResult;
            // A batch save keeps the previous complete directory as slotId.bak.
            LoadResult<T> directoryBackupResult = ReadItemPath(definition, Path.Combine(slotPath + ".bak", definition.FileName), true, out error);
            if (directoryBackupResult.Success) return directoryBackupResult;
            if (!File.Exists(path) && !File.Exists(backup)) return LoadResult<T>.Fail(SaveStatus.NotFound, "存档文件不存在: " + definition.FileName);
            return result.Status == SaveStatus.NotFound ? backupResult : result;
        }

        public static SaveResult DeleteItem<T>(SaveDefinition<T> definition, string slotId)
        {
            try
            {
                string path = Path.Combine(GetSlotPath(slotId), definition.FileName);
                if (!File.Exists(path) && !File.Exists(path + ".bak")) return SaveResult.Fail(SaveStatus.NotFound, "存档文件不存在。 ");
                if (File.Exists(path)) File.Delete(path);
                if (File.Exists(path + ".tmp")) File.Delete(path + ".tmp");
                if (File.Exists(path + ".bak")) File.Delete(path + ".bak");
                UpdateMetadata(slotId, GetSlotPath(slotId));
                return SaveResult.Ok();
            }
            catch (ArgumentException ex) { return SaveResult.Fail(SaveStatus.InvalidSlot, ex.Message); }
            catch (Exception ex) { return SaveResult.Fail(SaveStatus.WriteFailed, ex.Message); }
        }

        public static SaveResult WriteSlot(SaveSnapshot snapshot)
        {
            string slotPath;
            try { slotPath = GetSlotPath(snapshot.SlotId); } catch (Exception ex) { return SaveResult.Fail(SaveStatus.InvalidSlot, ex.Message); }
            string tempPath = slotPath + ".tmp";
            string backupPath = slotPath + ".bak";
            try
            {
                DeleteDirectory(tempPath);
                Directory.CreateDirectory(tempPath);
                var metadata = new SaveSlotMetadata
                {
                    schemaVersion = 1,
                    slotId = snapshot.SlotId,
                    displayName = snapshot.DisplayName,
                    sceneName = snapshot.SceneName,
                    playTimeSeconds = snapshot.PlayTimeSeconds,
                    savedAtUtc = DateTime.UtcNow.ToString("O"),
                    itemCount = snapshot.Count
                };
                foreach (SaveSnapshot.Entry entry in snapshot.Entries)
                {
                    string dataJson = serializer.Serialize(entry.Data, entry.Definition.DataType);
                    SaveFileEnvelope envelope = CreateEnvelope(entry.Definition.Key, entry.Definition.DataType, entry.Definition.Version, dataJson);
                    File.WriteAllText(Path.Combine(tempPath, entry.Definition.FileName), JsonMapper.ToJson(envelope));
                }
                File.WriteAllText(Path.Combine(tempPath, MetadataFileName), JsonMapper.ToJson(metadata));

                DeleteDirectory(backupPath);
                if (Directory.Exists(slotPath)) Directory.Move(slotPath, backupPath);
                try { Directory.Move(tempPath, slotPath); }
                catch
                {
                    if (!Directory.Exists(slotPath) && Directory.Exists(backupPath)) Directory.Move(backupPath, slotPath);
                    throw;
                }
                return SaveResult.Ok();
            }
            catch (Exception ex)
            {
                DeleteDirectory(tempPath);
                return SaveResult.Fail(ClassifyWriteError(ex), ex.Message);
            }
        }

        public static LoadSlotResult ReadSlot(string slotId)
        {
            string slotPath;
            try { slotPath = GetSlotPath(slotId); } catch (Exception ex) { return LoadSlotResult.Fail(SaveStatus.InvalidSlot, ex.Message); }
            bool recovered = false;
            string metadataPath = Path.Combine(slotPath, MetadataFileName);
            if (!File.Exists(metadataPath) && Directory.Exists(slotPath + ".bak")) { slotPath += ".bak"; metadataPath = Path.Combine(slotPath, MetadataFileName); recovered = true; }
            if (!File.Exists(metadataPath)) return LoadSlotResult.Fail(SaveStatus.NotFound, "存档槽位不存在: " + slotId);
            try
            {
                SaveSlotInfo info = ParseMetadata(metadataPath, slotId);
                return LoadSlotResult.Ok(info, recovered);
            }
            catch (Exception ex)
            {
                if (!recovered && File.Exists(metadataPath + ".bak"))
                {
                    try { return LoadSlotResult.Ok(ParseMetadata(metadataPath + ".bak", slotId), true); }
                    catch { }
                }
                if (!recovered && Directory.Exists(GetSlotPath(slotId) + ".bak"))
                {
                    try
                    {
                        return LoadSlotResult.Ok(ParseMetadata(Path.Combine(GetSlotPath(slotId) + ".bak", MetadataFileName), slotId), true);
                    }
                    catch { }
                }
                return LoadSlotResult.Fail(SaveStatus.DeserializeFailed, ex.Message);
            }
        }

        private static SaveSlotInfo ParseMetadata(string path, string fallbackSlotId)
        {
            SaveSlotMetadata metadata = JsonMapper.ToObject<SaveSlotMetadata>(File.ReadAllText(path));
            if (metadata == null || string.IsNullOrEmpty(metadata.savedAtUtc)) throw new InvalidDataException("槽位元数据无效。");
            DateTime savedAt;
            if (!DateTime.TryParse(metadata.savedAtUtc, null, System.Globalization.DateTimeStyles.RoundtripKind, out savedAt)) savedAt = DateTime.MinValue;
            return new SaveSlotInfo(metadata.slotId ?? fallbackSlotId, metadata.displayName, metadata.sceneName, metadata.playTimeSeconds, savedAt, metadata.itemCount);
        }

        public static SaveSlotInfo GetInfo(string slotId)
        {
            LoadSlotResult result = ReadSlot(slotId);
            return result.Success ? result.Info : null;
        }

        public static List<SaveSlotInfo> GetAllInfos()
        {
            var result = new List<SaveSlotInfo>();
            if (!Directory.Exists(RootPath)) return result;
            foreach (string directory in Directory.GetDirectories(RootPath))
            {
                string name = Path.GetFileName(directory);
                if (name.EndsWith(".tmp", StringComparison.OrdinalIgnoreCase) || name.EndsWith(".bak", StringComparison.OrdinalIgnoreCase)) continue;
                SaveSlotInfo info = GetInfo(name);
                if (info != null) result.Add(info);
            }
            return result;
        }

        public static bool SlotExists(string slotId)
        {
            try { return Directory.Exists(GetSlotPath(slotId)); } catch { return false; }
        }

        public static SaveResult DeleteSlot(string slotId)
        {
            try
            {
                string path = GetSlotPath(slotId);
                string backup = path + ".bak";
                if (!Directory.Exists(path) && !Directory.Exists(backup)) return SaveResult.Fail(SaveStatus.NotFound, "存档槽位不存在。 ");
                DeleteDirectory(path);
                DeleteDirectory(path + ".tmp");
                DeleteDirectory(backup);
                return SaveResult.Ok();
            }
            catch (ArgumentException ex) { return SaveResult.Fail(SaveStatus.InvalidSlot, ex.Message); }
            catch (Exception ex) { return SaveResult.Fail(SaveStatus.WriteFailed, ex.Message); }
        }

        public static string GetSlotPath(string slotId)
        {
            string error;
            if (!ValidateSlotId(slotId, out error)) throw new ArgumentException(error, "slotId");
            return Path.Combine(RootPath, slotId);
        }

        private static LoadResult<T> ReadItemPath<T>(SaveDefinition<T> definition, string path, bool backup, out string error)
        {
            error = null;
            if (!File.Exists(path)) return LoadResult<T>.Fail(SaveStatus.NotFound, "文件不存在: " + path);
            try
            {
                SaveFileEnvelope envelope = JsonMapper.ToObject<SaveFileEnvelope>(File.ReadAllText(path));
                if (envelope == null || string.IsNullOrEmpty(envelope.dataJson)) return LoadResult<T>.Fail(SaveStatus.DeserializeFailed, "存档 envelope 无效。");
                if (!string.Equals(envelope.key, definition.Key, StringComparison.Ordinal)) return LoadResult<T>.Fail(SaveStatus.TypeMismatch, "存档 key 不匹配。");
                if (!string.Equals(envelope.typeName, definition.DataType.FullName, StringComparison.Ordinal)) return LoadResult<T>.Fail(SaveStatus.TypeMismatch, "存档数据类型不匹配。");
                if (!string.Equals(envelope.checksum, LitJsonSerializer.Checksum(envelope.dataJson), StringComparison.OrdinalIgnoreCase)) return LoadResult<T>.Fail(SaveStatus.DeserializeFailed, "存档校验和不匹配。");
                if (envelope.version > definition.Version) return LoadResult<T>.Fail(SaveStatus.VersionUnsupported, "存档版本高于当前版本。");
                T data = (T)serializer.Deserialize(envelope.dataJson, typeof(T));
                string migrationError;
                if (!SaveMigration.TryMigrate(definition, ref data, envelope.version, out migrationError)) return LoadResult<T>.Fail(SaveStatus.MigrationFailed, migrationError);
                string validationError = SaveValidator.Validate(definition, data);
                if (validationError != null) return LoadResult<T>.Fail(SaveStatus.ValidationFailed, validationError);
                return backup ? LoadResult<T>.Recovered(data, "正式文件损坏，已从备份恢复。") : LoadResult<T>.Ok(data);
            }
            catch (IOException ex) { error = ex.Message; return LoadResult<T>.Fail(SaveStatus.ReadFailed, ex.Message); }
            catch (Exception ex) { error = ex.Message; return LoadResult<T>.Fail(SaveStatus.DeserializeFailed, ex.Message); }
        }

        private static SaveFileEnvelope CreateEnvelope<T>(SaveDefinition<T> definition, string dataJson)
        {
            return CreateEnvelope(definition.Key, definition.DataType, definition.Version, dataJson);
        }

        private static SaveFileEnvelope CreateEnvelope(string key, Type dataType, int version, string dataJson)
        {
            return new SaveFileEnvelope { key = key, typeName = dataType.FullName, version = version, savedAtUtc = DateTime.UtcNow.ToString("O"), checksum = LitJsonSerializer.Checksum(dataJson), dataJson = dataJson };
        }

        private static void ReplaceFile(string temp, string path, string backup)
        {
            if (File.Exists(backup)) File.Delete(backup);
            if (File.Exists(path)) File.Move(path, backup);
            try { File.Move(temp, path); }
            catch { if (!File.Exists(path) && File.Exists(backup)) File.Move(backup, path); throw; }
        }

        private static void UpdateMetadata(string slotId, string slotPath)
        {
            var metadata = new SaveSlotMetadata { schemaVersion = 1, slotId = slotId, displayName = slotId, sceneName = string.Empty, playTimeSeconds = 0, savedAtUtc = DateTime.UtcNow.ToString("O"), itemCount = 0 };
            if (File.Exists(Path.Combine(slotPath, MetadataFileName)))
            {
                try
                {
                    SaveSlotMetadata existing = JsonMapper.ToObject<SaveSlotMetadata>(File.ReadAllText(Path.Combine(slotPath, MetadataFileName)));
                    if (existing != null) metadata = existing;
                }
                catch { }
                metadata.savedAtUtc = DateTime.UtcNow.ToString("O");
            }
            string[] files = Directory.Exists(slotPath) ? Directory.GetFiles(slotPath, "*.json") : new string[0];
            int count = 0; for (int i = 0; i < files.Length; i++) if (!Path.GetFileName(files[i]).Equals(MetadataFileName, StringComparison.OrdinalIgnoreCase)) count++;
            metadata.itemCount = count;
            string metadataPath = Path.Combine(slotPath, MetadataFileName);
            string metadataTemp = metadataPath + ".tmp";
            string metadataBackup = metadataPath + ".bak";
            File.WriteAllText(metadataTemp, JsonMapper.ToJson(metadata));
            ReplaceFile(metadataTemp, metadataPath, metadataBackup);
        }

        internal static bool ValidateSlotId(string value, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(value)) { error = "slotId 不能为空。"; return false; }
            if (value == "." || value == ".." || Path.IsPathRooted(value) || value.IndexOf('/') >= 0 || value.IndexOf('\\') >= 0) { error = "slotId 不能包含路径穿越或目录分隔符。"; return false; }
            foreach (char c in value) if (char.IsControl(c) || "<>:\"|?*".IndexOf(c) >= 0) { error = "slotId 包含非法字符。"; return false; }
            return true;
        }

        private static SaveStatus ClassifyWriteError(Exception ex)
        {
            string typeName = ex.GetType().FullName ?? string.Empty;
            return ex is InvalidOperationException || typeName.StartsWith("LitJson.", StringComparison.Ordinal)
                ? SaveStatus.SerializeFailed
                : SaveStatus.WriteFailed;
        }

        private static void DeleteDirectory(string path)
        {
            if (Directory.Exists(path)) Directory.Delete(path, true);
        }
    }
}
