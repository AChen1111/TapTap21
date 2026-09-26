using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Excel;
using UnityEditor;
using UnityEngine;

/// <summary>Migrated from UnityExcel2BytesCs. Keeps the three-row header and # arrays.</summary>
public static class Excel2CsBytesTool
{
    private static string Root => Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
    public static string ExcelDataPath => Path.Combine(Root, "ExcelData");
    public static string CsClassPath => Path.Combine(Application.dataPath, "Scripts/Common/ExcelTable/Generated");
    public static string BytesDataPath => Path.Combine(Application.dataPath, "Resources/DataTable");
    private static readonly string[] ScalarTypes = { "string", "int", "bool", "float", "double", "long" };
    private static readonly string[] ReaderMethods = { "ReadString", "ReadInt32", "ReadBoolean", "ReadSingle", "ReadDouble", "ReadInt64" };

    private sealed class Sheet
    {
        public string Name;
        public string[] Names, Types, Descriptions;
        public List<object[]> Rows = new List<object[]>();
        public string Schema => Name + "|" + string.Join("|", Names.Select((name, i) => name + ":" + Types[i]));
    }

    [MenuItem("Tools/Excel/Export All (CS + Bytes)")]
    public static void ExportAll() => Export(true, true);
    [MenuItem("Tools/Excel/Generate C#")]
    [MenuItem("SDGSupporter/Excel/Excel2Cs")]
    public static void Excel2Cs() => Export(true, false);
    [MenuItem("Tools/Excel/Generate Bytes")]
    [MenuItem("SDGSupporter/Excel/Excel2Bytes")]
    public static void Excel2Bytes() => Export(false, true);
    [MenuItem("Tools/Excel/Open Source Folder")]
    public static void OpenSourceFolder()
    {
        Directory.CreateDirectory(ExcelDataPath);
        EditorUtility.RevealInFinder(ExcelDataPath);
    }

    // Public entry point also supports editor automation. Validation finishes before any output is written.
    public static void Export(bool generateCode, bool generateBytes)
    {
        if (!Directory.Exists(ExcelDataPath))
            throw new DirectoryNotFoundException("Create source folder: " + ExcelDataPath);
        string[] files = Directory.GetFiles(ExcelDataPath, "*.xlsx")
            .Where(path => !Path.GetFileName(path).StartsWith("~$", StringComparison.Ordinal))
            .OrderBy(path => path, StringComparer.Ordinal).ToArray();
        if (files.Length == 0) throw new InvalidDataException("No .xlsx files in " + ExcelDataPath);
        var sheets = files.Select(ReadSheet).ToArray();
        var generatedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "TableBinary", "stringArray", "intArray", "boolArray", "floatArray", "doubleArray", "longArray" };
        foreach (var sheet in sheets)
            if (!generatedNames.Add(sheet.Name) || !generatedNames.Add("all" + sheet.Name))
                throw new InvalidDataException("Duplicate/reserved table class name: " + sheet.Name);
        var code = sheets.Select(GenerateCode).ToArray();
        var bytes = sheets.Select(GenerateBytes).ToArray();
        Directory.CreateDirectory(CsClassPath);
        Directory.CreateDirectory(BytesDataPath);
        for (int i = 0; i < sheets.Length; i++)
        {
            if (generateCode) WriteIfChanged(Path.Combine(CsClassPath, sheets[i].Name + ".cs"), Encoding.UTF8.GetBytes(code[i]));
            if (generateBytes) WriteIfChanged(Path.Combine(BytesDataPath, sheets[i].Name + ".bytes"), bytes[i]);
            Debug.Log($"Excel table {sheets[i].Name}: {sheets[i].Rows.Count} rows; CS={generateCode}, bytes={generateBytes}");
        }
        AssetDatabase.Refresh();
    }

    private static void WriteIfChanged(string path, byte[] content)
    {
        if (File.Exists(path) && File.ReadAllBytes(path).SequenceEqual(content)) return;
        File.WriteAllBytes(path, content);
    }

    private static Sheet ReadSheet(string path)
    {
        try
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            using (DataSet data = reader.AsDataSet())
            {
                if (data == null || data.Tables.Count == 0 || data.Tables[0].Rows.Count < 3)
                    throw new InvalidDataException("First sheet requires field names, types, descriptions in rows 1-3.");
                DataTable table = data.Tables[0];
                int columns = table.Columns.Count;
                while (columns > 0 && string.IsNullOrWhiteSpace(Text(table.Rows[0][columns - 1]))) columns--;
                if (columns == 0) throw new InvalidDataException("No field names in row 1.");
                for (int c = columns; c < table.Columns.Count; c++)
                    if (table.Rows.Cast<DataRow>().Any(row => !string.IsNullOrWhiteSpace(Text(row[c]))))
                        throw new InvalidDataException($"Column {c + 1} has content but no field name in row 1.");
                var sheet = new Sheet { Name = Path.GetFileNameWithoutExtension(path).ToLowerInvariant(),
                    Names = new string[columns], Types = new string[columns], Descriptions = new string[columns] };
                RequireIdentifier(sheet.Name);
                var memberNames = new HashSet<string>(StringComparer.Ordinal) { sheet.Name, "LoadBytes", "ReadRow" };
                for (int c = 0; c < columns; c++)
                {
                    string name = Text(table.Rows[0][c]).Trim();
                    string type = Text(table.Rows[1][c]).Trim();
                    RequireIdentifier(name);
                    if (!memberNames.Add(name)) throw new InvalidDataException("Duplicate/reserved field: " + name);
                    if (!ScalarTypes.Contains(ElementType(type))) throw new InvalidDataException("Unsupported type: " + type);
                    sheet.Names[c] = name;
                    sheet.Types[c] = type;
                    sheet.Descriptions[c] = Text(table.Rows[2][c]);
                }
                for (int c = 0; c < columns; c++)
                    if (sheet.Types[c].EndsWith("[]", StringComparison.Ordinal) && !memberNames.Add("_" + sheet.Names[c]))
                        throw new InvalidDataException("Array backing field collision: _" + sheet.Names[c]);
                for (int r = 3; r < table.Rows.Count; r++)
                {
                    if (Enumerable.Range(0, table.Columns.Count).All(c => string.IsNullOrWhiteSpace(Text(table.Rows[r][c])))) continue;
                    var row = new object[columns];
                    for (int c = 0; c < columns; c++)
                    {
                        try { row[c] = ParseValue(Text(table.Rows[r][c]), sheet.Types[c]); }
                        catch (Exception ex) { throw new InvalidDataException($"Row {r + 1}, column {c + 1} ({sheet.Names[c]}, {sheet.Types[c]}): {ex.Message}", ex); }
                    }
                    sheet.Rows.Add(row);
                }
                if (sheet.Rows.Count > 1000000) throw new InvalidDataException("Maximum row count: 1000000.");
                return sheet;
            }
        }
        catch (Exception ex) { throw new InvalidDataException(Path.GetFileName(path) + ": " + ex.Message, ex); }
    }

    private static string Text(object value) => Convert.ToString(value, CultureInfo.InvariantCulture) ?? "";
    private static string ElementType(string type) => type.EndsWith("[]", StringComparison.Ordinal) ? type.Substring(0, type.Length - 2) : type;
    private static void RequireIdentifier(string name)
    {
        if (!Regex.IsMatch(name, "^[A-Za-z_][A-Za-z0-9_]*$"))
            throw new InvalidDataException("Use an ASCII C# identifier (letters, digits, underscore): " + name);
    }
    private static object ParseValue(string value, string type)
    {
        if (type.EndsWith("[]", StringComparison.Ordinal))
        {
            var parts = string.IsNullOrEmpty(value) ? Array.Empty<string>() : value.Split('#');
            if (parts.Length > 1000000) throw new InvalidDataException("Maximum array length: 1000000.");
            return parts.Select(part => ParseValue(part, ElementType(type))).ToArray();
        }
        if (type == "string") return value;
        if (string.IsNullOrWhiteSpace(value)) value = type == "bool" ? "false" : "0";
        switch (type)
        {
            case "int": return int.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture);
            case "long": return long.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture);
            case "float": return float.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);
            case "double": return double.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);
            case "bool":
                if (value.Trim() == "1") return true;
                if (value.Trim() == "0") return false;
                return bool.Parse(value.Trim());
            default: throw new InvalidDataException("Unsupported type: " + type);
        }
    }

    private static byte[] GenerateBytes(Sheet sheet)
    {
        using (var stream = new MemoryStream())
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                writer.Write(TableBinary.Magic);
                writer.Write(TableBinary.Version);
                writer.Write(sheet.Schema);
                writer.Write(sheet.Rows.Count);
                foreach (var row in sheet.Rows)
                    for (int c = 0; c < sheet.Types.Length; c++)
                    {
                        if (row[c] is object[] array)
                        {
                            writer.Write(array.Length);
                            foreach (var value in array) WriteScalar(writer, value);
                        }
                        else WriteScalar(writer, row[c]);
                    }
            }
            return stream.ToArray();
        }
    }
    private static void WriteScalar(BinaryWriter writer, object value)
    {
        switch (value)
        {
            case string text: writer.Write(text); break;
            case int number: writer.Write(number); break;
            case long number: writer.Write(number); break;
            case bool flag: writer.Write(flag); break;
            case float number: writer.Write(number); break;
            case double number: writer.Write(number); break;
            default: throw new InvalidDataException("Unsupported cell value.");
        }
    }

    private static string GenerateCode(Sheet sheet)
    {
        var code = new StringBuilder();
        code.AppendLine("// Generated from ExcelData/" + sheet.Name + ".xlsx. Do not edit.");
        code.AppendLine("using System;\nusing System.Collections.Generic;\nusing System.IO;\nusing System.Xml.Serialization;\n");
        code.AppendLine("    [Serializable]\n    public class @" + sheet.Name + "\n    {");
        for (int c = 0; c < sheet.Names.Length; c++)
        {
            string name = sheet.Names[c], type = sheet.Types[c];
            code.AppendLine("        // " + sheet.Descriptions[c].Replace("\r", " ").Replace("\n", " "));
            if (type.EndsWith("[]", StringComparison.Ordinal))
            {
                string element = ElementType(type);
                code.AppendLine($"        [XmlIgnore] public List<{element}> @{name} => @_{name}?.item;");
                code.AppendLine($"        [XmlElement(\"{name}\")] public {element}Array @_{name};");
            }
            else code.AppendLine($"        [XmlAttribute(\"{name}\")] public {type} @{name};");
        }
        code.AppendLine($"\n        public static List<@{sheet.Name}> LoadBytes() => TableBinary.Load(\"{sheet.Name}\", \"{sheet.Schema}\", ReadRow);");
        code.AppendLine($"        private static @{sheet.Name} ReadRow(BinaryReader reader)\n        {{\n            var row = new @{sheet.Name}();");
        for (int c = 0; c < sheet.Names.Length; c++)
        {
            string name = sheet.Names[c], type = sheet.Types[c], element = ElementType(type);
            string method = ReaderMethods[Array.IndexOf(ScalarTypes, element)];
            if (type.EndsWith("[]", StringComparison.Ordinal))
            {
                code.AppendLine($"            int count{c} = TableBinary.ReadCount(reader);");
                code.AppendLine($"            row.@_{name} = new {element}Array {{ item = new List<{element}>(count{c}) }};");
                code.AppendLine($"            for (int i = 0; i < count{c}; i++) row.@_{name}.item.Add(reader.{method}());");
            }
            else code.AppendLine($"            row.@{name} = reader.{method}();");
        }
        code.AppendLine("            return row;\n        }\n    }");
        code.AppendLine($"    [Serializable]\n    public class @all{sheet.Name}\n    {{\n        public List<@{sheet.Name}> @{sheet.Name}s;\n    }}");
        return code.ToString();
    }
}
