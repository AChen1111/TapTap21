using System;

namespace TapTap21.SaveSystem.dyh
{
    /// <summary>所有存档操作共用的结果状态。</summary>
    public enum SaveStatus
    {
        Success, NotFound, InvalidSlot, InvalidDefinition, DuplicateKey, DuplicateFileName,
        TypeMismatch, SerializeFailed, DeserializeFailed, ReadFailed, WriteFailed,
        ValidationFailed, VersionUnsupported, MigrationFailed, RecoveredFromBackup
    }

    [Serializable]
    public class SaveResult
    {
        /// <summary>操作是否成功。</summary>
        public bool Success { get; private set; }
        /// <summary>具体状态。</summary>
        public SaveStatus Status { get; private set; }
        /// <summary>人类可读的信息。</summary>
        public string Message { get; private set; }
        /// <summary>是否使用了备份文件。</summary>
        public bool UsedBackup { get; private set; }

        internal SaveResult(bool success, SaveStatus status, string message = null, bool usedBackup = false)
        {
            Success = success;
            Status = status;
            Message = message ?? string.Empty;
            UsedBackup = usedBackup;
        }

        public static SaveResult Ok() { return new SaveResult(true, SaveStatus.Success); }
        internal static SaveResult Recovered(string message) { return new SaveResult(true, SaveStatus.RecoveredFromBackup, message, true); }
        internal static SaveResult Fail(SaveStatus status, string message) { return new SaveResult(false, status, message); }

        public override string ToString()
        {
            return Status + (string.IsNullOrEmpty(Message) ? string.Empty : ": " + Message);
        }
    }

    [Serializable]
    public class LoadResult<T> : SaveResult
    {
        /// <summary>读取到的数据；失败时为默认值。</summary>
        public T Data { get; private set; }

        internal LoadResult(bool success, SaveStatus status, T data, string message = null, bool usedBackup = false)
            : base(success, status, message, usedBackup) { Data = data; }

        internal static LoadResult<T> Ok(T data) { return new LoadResult<T>(true, SaveStatus.Success, data); }
        internal static LoadResult<T> Recovered(T data, string message) { return new LoadResult<T>(true, SaveStatus.RecoveredFromBackup, data, message, true); }
        internal static new LoadResult<T> Fail(SaveStatus status, string message) { return new LoadResult<T>(false, status, default(T), message); }
    }

    [Serializable]
    public class LoadSlotResult : SaveResult
    {
        private readonly SaveSlotInfo info;
        internal LoadSlotResult(bool success, SaveStatus status, SaveSlotInfo info, string message = null, bool usedBackup = false)
            : base(success, status, message, usedBackup) { this.info = info; }

        /// <summary>槽位摘要信息。</summary>
        public SaveSlotInfo Info { get { return info; } }

        public LoadResult<T> Get<T>(SaveDefinition<T> definition)
        {
            if (!Success) return LoadResult<T>.Fail(Status, Message);
            return SaveSystem.Load(definition, info.SlotId);
        }

        internal static new LoadSlotResult Fail(SaveStatus status, string message)
        {
            return new LoadSlotResult(false, status, null, message);
        }

        internal static LoadSlotResult Ok(SaveSlotInfo info, bool recovered)
        {
            if (recovered) return new LoadSlotResult(true, SaveStatus.RecoveredFromBackup, info, "存档已从备份恢复。", true);
            return new LoadSlotResult(true, SaveStatus.Success, info);
        }
    }
}
