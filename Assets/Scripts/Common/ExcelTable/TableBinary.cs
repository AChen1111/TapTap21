using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>Versioned table data loaded from Resources; no editor paths or reflection.</summary>
public static class TableBinary
{
    public const int Magic = 0x314C4254; // TBL1
    public const int Version = 1;

    public static List<T> Load<T>(string tableName, string schema, Func<BinaryReader, T> readRow)
    {
        var asset = Resources.Load<TextAsset>("DataTable/" + tableName);
        if (asset == null)
            throw new FileNotFoundException("Missing table DataTable/" + tableName + ". Run Tools/Excel/Export All.");
        using (var stream = new MemoryStream(asset.bytes, false))
        using (var reader = new BinaryReader(stream))
        {
            if (reader.ReadInt32() != Magic || reader.ReadInt32() != Version || reader.ReadString() != schema)
                throw new InvalidDataException("Table format/schema mismatch: " + tableName + ". Export code and bytes together.");
            int count = ReadCount(reader);
            var rows = new List<T>(count);
            for (int i = 0; i < count; i++) rows.Add(readRow(reader));
            if (stream.Position != stream.Length)
                throw new InvalidDataException("Unexpected trailing table data: " + tableName);
            return rows;
        }
    }

    public static int ReadCount(BinaryReader reader)
    {
        int count = reader.ReadInt32();
        if (count < 0 || count > 1000000)
            throw new InvalidDataException("Invalid table/array length: " + count);
        return count;
    }
}
