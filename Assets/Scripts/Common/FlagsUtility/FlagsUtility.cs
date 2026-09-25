using System;
namespace Common.FlagsUtility
{
    public static class FlagsUtility
    {
        /// <summary>
        /// 添加 Flag
        /// </summary>
        public static T Add<T>(this T value, T flag) where T : Enum
        {
            return ToEnum<T>(Convert.ToInt64(value) | Convert.ToInt64(flag));
        }

        /// <summary>
        /// 检查是否包含 Flag
        /// </summary>
        public static bool Has<T>(this T value, T flag) where T : Enum
        {
            return (Convert.ToInt64(value) & Convert.ToInt64(flag)) == Convert.ToInt64(flag);
        }

        /// <summary>
        /// 删除 Flag
        /// </summary>
        public static T Remove<T>(this T value, T flag) where T : Enum
        {
            return ToEnum<T>(Convert.ToInt64(value) & ~Convert.ToInt64(flag));
        }

        public static T Clear<T>(this T value) where T : Enum
        {
            return ToEnum<T>(0);
        }

        static T ToEnum<T>(long bits) where T : Enum
        {
            return (T)Enum.ToObject(typeof(T), bits);
        }
    }
}
