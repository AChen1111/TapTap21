using System;

namespace TapTap21.SaveSystem.dyh
{
    /// <summary>表示程序内部使用的稳定存档槽位标识，与玩家看到的显示名称不同。</summary>
    public readonly struct SaveSlotId : IEquatable<SaveSlotId>
    {
        private readonly string value;

        private SaveSlotId(string value) { this.value = value; }

        /// <summary>获取用于目录名和持久化的原始标识。</summary>
        public string Value { get { return value ?? string.Empty; } }

        /// <summary>指示当前值是否是可用的槽位标识。</summary>
        public bool IsValid
        {
            get
            {
                string error;
                return SaveStorage.ValidateSlotId(Value, out error);
            }
        }

        /// <summary>将已有的稳定字符串转换为强类型槽位标识。</summary>
        /// <param name="value">已有的内部槽位标识，不是玩家显示名称。</param>
        /// <returns>经过格式校验的强类型槽位标识。</returns>
        /// <exception cref="ArgumentException">字符串为空、包含非法字符或存在路径穿越时抛出。</exception>
        public static SaveSlotId From(string value)
        {
            string error;
            if (!SaveStorage.ValidateSlotId(value, out error)) throw new ArgumentException(error, "value");
            return new SaveSlotId(value);
        }

        /// <summary>尝试将字符串转换为强类型槽位标识。</summary>
        /// <param name="value">待转换的内部槽位标识。</param>
        /// <param name="slotId">成功时返回转换后的槽位标识。</param>
        /// <returns>格式合法时返回 true，否则返回 false。</returns>
        public static bool TryFrom(string value, out SaveSlotId slotId)
        {
            string error;
            if (!SaveStorage.ValidateSlotId(value, out error))
            {
                slotId = default(SaveSlotId);
                return false;
            }
            slotId = new SaveSlotId(value);
            return true;
        }

        /// <summary>比较两个槽位标识是否相同。</summary>
        public bool Equals(SaveSlotId other) { return string.Equals(Value, other.Value, StringComparison.Ordinal); }

        /// <summary>比较当前槽位标识与其他对象是否相同。</summary>
        public override bool Equals(object obj) { return obj is SaveSlotId && Equals((SaveSlotId)obj); }

        /// <summary>返回槽位标识的哈希值。</summary>
        public override int GetHashCode() { return StringComparer.Ordinal.GetHashCode(Value); }

        /// <summary>返回内部稳定标识字符串。</summary>
        public override string ToString() { return Value; }

        public static bool operator ==(SaveSlotId left, SaveSlotId right) { return left.Equals(right); }
        public static bool operator !=(SaveSlotId left, SaveSlotId right) { return !left.Equals(right); }
    }
}
