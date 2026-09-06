using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace TapTap21.SaveSystem.dyh
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class SaveRequiredAttribute : Attribute { }

    /// <summary>Basic safety checks plus optional business validation callbacks.</summary>
    public static class SaveValidator
    {
        private static readonly Dictionary<string, Delegate> validators = new Dictionary<string, Delegate>(StringComparer.Ordinal);

        public static void Register<T>(SaveDefinition<T> definition, Func<T, string> validator)
        {
            if (definition == null) throw new ArgumentNullException("definition");
            if (validator == null) throw new ArgumentNullException("validator");
            validators[definition.Key] = validator;
        }

        internal static string Validate<T>(SaveDefinition<T> definition, T data)
        {
            if (ReferenceEquals(data, null)) return "存档数据不能为 null。";
            var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
            string error = ValidateValue(data, typeof(T), visited, "data");
            if (error != null) return error;
            Delegate validator;
            if (validators.TryGetValue(definition.Key, out validator))
            {
                try { return ((Func<T, string>)validator)(data); }
                catch (Exception ex) { return "业务校验异常: " + ex.Message; }
            }
            return null;
        }

        private static string ValidateValue(object value, Type type, HashSet<object> visited, string path)
        {
            if (ContainsUnityObjectType(type)) return "不允许保存 Unity 对象: " + path;
            if (value == null) return null;
            if (type == typeof(float) && (float.IsNaN((float)value) || float.IsInfinity((float)value))) return path + " 不是有限数值。";
            if (type == typeof(double) && (double.IsNaN((double)value) || double.IsInfinity((double)value))) return path + " 不是有限数值。";
            if (type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal)) return null;
            if (!type.IsValueType && !visited.Add(value)) return null;
            if (value is IDictionary dictionary)
            {
                foreach (DictionaryEntry item in dictionary)
                {
                    string error = ValidateValue(item.Key, item.Key == null ? typeof(object) : item.Key.GetType(), visited, path + ".key");
                    if (error != null) return error;
                    error = ValidateValue(item.Value, item.Value == null ? typeof(object) : item.Value.GetType(), visited, path + "[value]");
                    if (error != null) return error;
                }
                return null;
            }
            if (value is IEnumerable enumerable && !(value is string))
            {
                int index = 0;
                foreach (object item in enumerable)
                {
                    string error = ValidateValue(item, item == null ? typeof(object) : item.GetType(), visited, path + "[" + index++ + "]");
                    if (error != null) return error;
                }
                return null;
            }
            foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                object fieldValue = field.GetValue(value);
                if (Attribute.IsDefined(field, typeof(SaveRequiredAttribute), true) &&
                    (fieldValue == null || (fieldValue is string && string.IsNullOrWhiteSpace((string)fieldValue))))
                    return path + "." + field.Name + " 是必填字段。";
                string error = ValidateValue(fieldValue, field.FieldType, visited, path + "." + field.Name);
                if (error != null) return error;
            }
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!property.CanRead || property.GetIndexParameters().Length != 0 || !Attribute.IsDefined(property, typeof(SaveRequiredAttribute), true)) continue;
                object propertyValue;
                try { propertyValue = property.GetValue(value, null); }
                catch (Exception ex) { return path + "." + property.Name + " 无法读取: " + ex.Message; }
                if (propertyValue == null || (propertyValue is string && string.IsNullOrWhiteSpace((string)propertyValue))) return path + "." + property.Name + " 是必填字段。";
            }
            return null;
        }

        private static bool ContainsUnityObjectType(Type type)
        {
            if (typeof(UnityEngine.Object).IsAssignableFrom(type)) return true;
            if (type.IsArray) return ContainsUnityObjectType(type.GetElementType());
            if (type.IsGenericType)
            {
                Type[] arguments = type.GetGenericArguments();
                for (int i = 0; i < arguments.Length; i++) if (ContainsUnityObjectType(arguments[i])) return true;
            }
            return false;
        }

        private sealed class ReferenceEqualityComparer : IEqualityComparer<object>
        {
            public static readonly ReferenceEqualityComparer Instance = new ReferenceEqualityComparer();
            public new bool Equals(object x, object y) { return ReferenceEquals(x, y); }
            public int GetHashCode(object obj) { return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj); }
        }
    }
}
