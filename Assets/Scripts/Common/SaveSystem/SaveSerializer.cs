using System;
using System.Security.Cryptography;
using System.Text;
using LitJson;

namespace TapTap21.SaveSystem.dyh
{
    internal interface ISaveSerializer
    {
        string Serialize(object value, Type type);
        object Deserialize(string json, Type type);
    }

    /// <summary>LitJSON is deliberately hidden behind this adapter.</summary>
    internal sealed class LitJsonSerializer : ISaveSerializer
    {
        public string Serialize(object value, Type type)
        {
            if (value == null) throw new InvalidOperationException("存档数据不能为 null。");
            // LitJSON's root writer expects an object. Box scalar/enum definitions while keeping the
            // boxing private to this adapter, so the public API still supports SaveDefinition<int>, etc.
            if (type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal))
                return JsonMapper.ToJson(new ScalarEnvelope { value = Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture) });
            return JsonMapper.ToJson(value);
        }

        public object Deserialize(string json, Type type)
        {
            if (string.IsNullOrWhiteSpace(json)) throw new InvalidOperationException("存档 JSON 为空。");
            if (type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal))
            {
                ScalarEnvelope scalar = JsonMapper.ToObject<ScalarEnvelope>(json);
                if (scalar == null || scalar.value == null) throw new InvalidOperationException("标量存档值为空。");
                if (type.IsEnum) return Enum.Parse(type, scalar.value, true);
                return Convert.ChangeType(scalar.value, type, System.Globalization.CultureInfo.InvariantCulture);
            }
            return JsonMapper.ToObject(json, type);
        }

        public static string Checksum(string value)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(value ?? string.Empty));
                var builder = new StringBuilder(bytes.Length * 2);
                for (int i = 0; i < bytes.Length; i++) builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }
    }

    [Serializable]
    internal sealed class SaveFileEnvelope
    {
        public string key;
        public string typeName;
        public int version;
        public string savedAtUtc;
        public string checksum;
        public string dataJson;
    }

    [Serializable]
    internal sealed class ScalarEnvelope
    {
        public string value;
    }

    [Serializable]
    internal sealed class SaveSlotMetadata
    {
        public int schemaVersion = 1;
        public string slotId;
        public string displayName;
        public string sceneName;
        public long playTimeSeconds;
        public string savedAtUtc;
        public int itemCount;
    }
}
