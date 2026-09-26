// Generated from ExcelData/weapon.xlsx. Do not edit.
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[Serializable]
public class @weapon
{
    // 编号
    [XmlAttribute("id")] public int @id;
    // 名字
    [XmlAttribute("name")] public string @name;
    // 预制体名
    [XmlAttribute("prefabName")] public string @prefabName;
    // 描述#真好
    [XmlIgnore] public List<string> @desc => @_desc?.item;
    [XmlElement("desc")] public stringArray @_desc;
    // 数量
    [XmlIgnore] public List<int> @nums => @_nums?.item;
    [XmlElement("nums")] public intArray @_nums;

    public static List<@weapon> LoadBytes() => TableBinary.Load("weapon", "weapon|id:int|name:string|prefabName:string|desc:string[]|nums:int[]", ReadRow);
    private static @weapon ReadRow(BinaryReader reader)
    {
        var row = new @weapon();
        row.@id = reader.ReadInt32();
        row.@name = reader.ReadString();
        row.@prefabName = reader.ReadString();
        int count3 = TableBinary.ReadCount(reader);
        row.@_desc = new stringArray { item = new List<string>(count3) };
        for (int i = 0; i < count3; i++) row.@_desc.item.Add(reader.ReadString());
        int count4 = TableBinary.ReadCount(reader);
        row.@_nums = new intArray { item = new List<int>(count4) };
        for (int i = 0; i < count4; i++) row.@_nums.item.Add(reader.ReadInt32());
        return row;
    }
}
[Serializable]
public class @allweapon
{
    public List<@weapon> @weapons;
}
