# 导入记录

源目录：`C:/Users/ldc20/OneDrive/Desktop/存档/UnityExcel2BytesCs/UnityExcel2BytesCs`。

原 `Excel.dll` 和 `ICSharpCode.SharpZipLib.dll` 原样导入 `Editor/`，并将 PluginImporter
设为仅 Editor 使用。存档中没有附带该项目或 DLL 的许可证文件，本次未为其添加或推断许可证。

工具代码在 `Assets/Scripts/Common/ExcelTable/Editor/Excel2CsBytesTool.cs`，基于原工具迁移：
保留前三行表头、第一张工作表、# 数组、LoadBytes 调用和旧菜单入口；按工程要求移除命名空间；
替换绝对路径、Xml 中转、Assembly.LoadFile、BinaryFormatter，使用版本化 BinaryReader/Writer 格式；
增加校验、空值处理和错误定位。BaseTable 数组包装类型保留并扩展。

`ExcelData/weapon.xlsx` 是原样复制的源表；配套 C# 和 bytes 按新格式生成。
`Examples/readBytesTest.cs` 保留原示例用途并修正重复打印 desc 而未打印 nums 的问题。
不导入原项目的 Library、Temp、ProjectSettings、场景及无关 BillingMode.json。

使用文档：项目根 `.doc/project/excel-table.md`。
