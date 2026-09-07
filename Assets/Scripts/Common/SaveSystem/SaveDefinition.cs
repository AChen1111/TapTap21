using System;

namespace TapTap21.SaveSystem.dyh
{
    /// <summary>不可变的存档定义：将 key、文件名、数据类型和版本绑定在一起。</summary>
    public sealed class SaveDefinition<T>
    {
        /// <summary>数据键，例如 player、inventory。</summary>
        public string Key { get; private set; }
        /// <summary>逻辑文件名，例如 player.json。</summary>
        public string FileName { get; private set; }
        /// <summary>绑定的 DTO 类型。</summary>
        public Type DataType { get { return typeof(T); } }
        /// <summary>当前数据格式版本。</summary>
        public int Version { get; private set; }

        internal SaveDefinition(string key, string fileName, int version)
        {
            Key = key;
            FileName = fileName;
            Version = version;
        }

        public override string ToString()
        {
            return Key + " (" + DataType.FullName + ", v" + Version + ")";
        }
    }
}
