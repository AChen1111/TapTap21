using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TapTap21.SaveSystem.dyh
{
    /// <summary>内部注册表，负责保证 key、文件名和数据类型绑定唯一。</summary>
    internal static class SaveDefinitionRegistry
    {
        private static readonly Dictionary<string, object> byKey = new Dictionary<string, object>(StringComparer.Ordinal);
        private static readonly Dictionary<string, object> byFileName = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        public static SaveDefinition<T> Register<T>(string key, string fileName, int version)
        {
            string error;
            if (!ValidateName(key, false, out error)) throw new SaveRegistrationException(SaveStatus.InvalidDefinition, error);
            if (!ValidateName(fileName, true, out error)) throw new SaveRegistrationException(SaveStatus.InvalidDefinition, error);
            if (version < 1) throw new SaveRegistrationException(SaveStatus.InvalidDefinition, "version 必须大于等于 1。");
            if (byKey.ContainsKey(key)) throw new SaveRegistrationException(SaveStatus.DuplicateKey, "重复的存档 key: " + key);
            if (byFileName.ContainsKey(fileName)) throw new SaveRegistrationException(SaveStatus.DuplicateFileName, "重复的存档 fileName: " + fileName);
            if (!IsSupportedDataType(typeof(T))) throw new SaveRegistrationException(SaveStatus.InvalidDefinition, "数据类型必须是纯数据类型且标记 Serializable: " + typeof(T).FullName);

            SaveDefinition<T> definition = new SaveDefinition<T>(key, fileName, version);
            byKey.Add(key, definition);
            byFileName.Add(fileName, definition);
            return definition;
        }

        public static bool IsRegistered<T>(SaveDefinition<T> definition)
        {
            object value;
            return definition != null && byKey.TryGetValue(definition.Key, out value) && ReferenceEquals(value, definition);
        }

        private static bool ValidateName(string value, bool fileName, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(value))
            {
                error = (fileName ? "fileName" : "key") + " 不能为空。";
                return false;
            }
            if (value == "." || value == ".." || Path.IsPathRooted(value) || value.IndexOf('/') >= 0 || value.IndexOf('\\') >= 0)
            {
                error = "名称不能包含路径穿越或目录分隔符: " + value;
                return false;
            }
            foreach (char c in value)
            {
                if (char.IsControl(c) || "<>:\"|?*".IndexOf(c) >= 0)
                {
                    error = "名称包含非法字符: " + value;
                    return false;
                }
            }
            if (fileName && !string.Equals(Path.GetFileName(value), value, StringComparison.Ordinal))
            {
                error = "fileName 必须是单个文件名。";
                return false;
            }
            return true;
        }

        private static bool IsSupportedDataType(Type type)
        {
            if (typeof(UnityEngine.Object).IsAssignableFrom(type)) return false;
            if (type == typeof(object) || type.IsInterface || type.IsPointer) return false;
            if (type == typeof(string) || type.IsPrimitive || type.IsEnum || type == typeof(decimal)) return true;
            if (type.IsArray || type.IsGenericType) return true;
            return Attribute.IsDefined(type, typeof(SerializableAttribute), true);
        }
    }

    /// <summary>注册失败时抛出的异常，同时携带 SaveStatus。</summary>
    public sealed class SaveRegistrationException : Exception
    {
        public SaveStatus Status { get; private set; }
        internal SaveRegistrationException(SaveStatus status, string message) : base(message) { Status = status; }
    }
}
