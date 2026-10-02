// Generated from ExcelData/itemmergetable.xlsx. Do not edit.
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

    [Serializable]
    public class @itemmergetable
    {
        // 编号
        [XmlAttribute("id")] public int @id;
        // 合成物品1类型
        [XmlAttribute("typeIn1")] public string @typeIn1;
        // 合成物品1状态
        [XmlAttribute("stateIn1")] public int @stateIn1;
        // 合成物品2类型
        [XmlAttribute("typeIn2")] public string @typeIn2;
        // 合成物品2状态
        [XmlAttribute("stateIn2")] public int @stateIn2;
        // 操作
        [XmlAttribute("op")] public int @op;
        // 结果物品
        [XmlAttribute("typeOut")] public string @typeOut;
        // 结果物品状态
        [XmlAttribute("stateOut")] public int @stateOut;

        public static List<@itemmergetable> LoadBytes() => TableBinary.Load("itemmergetable", "itemmergetable|id:int|typeIn1:string|stateIn1:int|typeIn2:string|stateIn2:int|op:int|typeOut:string|stateOut:int", ReadRow);
        private static @itemmergetable ReadRow(BinaryReader reader)
        {
            var row = new @itemmergetable();
            row.@id = reader.ReadInt32();
            row.@typeIn1 = reader.ReadString();
            row.@stateIn1 = reader.ReadInt32();
            row.@typeIn2 = reader.ReadString();
            row.@stateIn2 = reader.ReadInt32();
            row.@op = reader.ReadInt32();
            row.@typeOut = reader.ReadString();
            row.@stateOut = reader.ReadInt32();
            return row;
        }
    }
    [Serializable]
    public class @allitemmergetable
    {
        public List<@itemmergetable> @itemmergetables;
    }
