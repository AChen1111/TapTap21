using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TapTap21.SaveSystem.dyh
{
    internal static class SaveDefinitionRegistry
    {
        private static readonly Dictionary<string, object> byKey = new Dictionary<string, object>(StringComparer.Ordinal);
        private static readonly Dictionary<string, object> byFileName = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 登记注册存储类型
        /// </summary>
        /// <typeparam name="T">存储数据类型</typeparam>
        /// <param name="key">Key</param>
        /// <param name="fileName">生成json文件名</param>
        /// <param name="version">版本号</param>
        /// <returns></returns>
        /// <exception cref="SaveRegistrationException"></exception> <summary>
        /// 
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="fileName">生成json的文件名，要带.json</param>
        /// <param name="version">版本号</param>
        /// <typeparam name="T">存储数据类型</typeparam>
        /// <returns></returns>
        public static SaveDefinition<T> Register<T>(string key, string fileName, int version)
        {
            string error;
            if (!ValidateName(key, false, out error)) throw new SaveRegistrationException(SaveStatus.InvalidDefinition, error);
            if (!ValidateName(fileName, true, out error)) throw new SaveRegistrationException(SaveStatus.InvalidDefinition, error);
            if (version < 1) throw new SaveRegistrationException(SaveStatus.InvalidDefinition, "version 必须大于等于 1。 ");
            if (byKey.ContainsKey(key)) throw new SaveRegistrationException(SaveStatus.DuplicateKey, "重复的存档 key: " + key);
            if (byFileName.ContainsKey(fileName)) throw new SaveRegistrationException(SaveStatus.DuplicateFileName, "重复的存档 fileName: " + fileName);
            if (!IsSupportedDataType(typeof(T))) throw new SaveRegistrationException(SaveStatus.InvalidDefinition, "数据类型不能是 UnityEngine.Object，且必须是纯数据类型或标记 Serializable: " + typeof(T).FullName);

            var definition = new SaveDefinition<T>(key, fileName, version);
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
            if (string.IsNullOrWhiteSpace(value)) { error = (fileName ? "fileName" : "key") + " 不能为空。"; return false; }
            if (value == "." || value == ".." || Path.IsPathRooted(value) || value.IndexOf('/') >= 0 || value.IndexOf('\\') >= 0)
            { error = "名称不能包含路径穿越或目录分隔符: " + value; return false; }
            foreach (char c in value)
            {
                if (char.IsControl(c) || "<>:\"|?*".IndexOf(c) >= 0)
                { error = "名称包含非法字符: " + value; return false; }
            }
            if (fileName && !string.Equals(Path.GetFileName(value), value, StringComparison.Ordinal))
            { error = "fileName 必须是单一文件名。"; return false; }
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

    public sealed class SaveRegistrationException : Exception
    {
        public SaveStatus Status { get; private set; }
        internal SaveRegistrationException(SaveStatus status, string message) : base(message) { Status = status; }
    }
}
