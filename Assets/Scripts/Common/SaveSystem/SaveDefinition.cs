using System;

namespace TapTap21.SaveSystem.dyh
{
    /// <summary>Immutable binding between a stable key, file name and DTO type.</summary>
    public sealed class SaveDefinition<T>
    {
        public string Key { get; private set; }
        public string FileName { get; private set; }
        public Type DataType { get { return typeof(T); } }
        public int Version { get; private set; }

        internal SaveDefinition(string key, string fileName, int version)
        {
            Key = key;
            FileName = fileName;
            Version = version;
        }

        public override string ToString() { return Key + " (" + DataType.FullName + ", v" + Version + ")"; }
    }
}
