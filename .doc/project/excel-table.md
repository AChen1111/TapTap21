# Excel 导表：使用与 API

## 使用

1. 把 `.xlsx` 放到工程根目录 `ExcelData/`，只读取第一张工作表。
2. 前三行分别填写字段名、类型、说明，第 4 行起填写数据：

| 行 | id | name | nums |
| --- | --- | --- | --- |
| 1：字段名 | id | name | nums |
| 2：类型 | int | string | int[] |
| 3：说明 | 编号 | 名称 | 数量列表 |
| 4 起：数据 | 1 | 寒冰剑 | 10#20 |

类型支持 `string/int/bool/float/double/long` 及对应的 `[]`。数组用 `#` 分隔，生成后是 `List<T>`。
文件名和字段名使用字母、数字、下划线，不能以数字开头。文件名转小写成为类名，例如 `weapon.xlsx` → `weapon`。

3. 保存 Excel，点击 `Tools > Excel > Export All (CS + Bytes)`，等待 Unity 编译完成。
4. 游戏中调用生成类的 `LoadBytes()`：

```csharp
var weapons = weapon.LoadBytes();
foreach (var item in weapons)
{
    UnityEngine.Debug.Log($"{item.id}: {item.name}");
    foreach (int num in item.nums)
        UnityEngine.Debug.Log(num);
}
```

生成代码：`Assets/Scripts/Common/ExcelTable/Generated/`；数据：`Assets/Resources/DataTable/`。
只改数据可用 `Generate Bytes`；字段结构变化时用 `Export All`。不要手改生成代码或混用旧工程的 bytes。

## 外部 API

| API | 用途 |
| --- | --- |
| `表名.LoadBytes()` → `List<表名>` | 加载整张表；每次返回新列表，建议业务保存一次 |
| `数据对象.字段名` | 读取普通字段或数组列表，如 `item.id`、`item.nums` |
| `Excel2CsBytesTool.ExportAll()` | 导出所有表的 C# 和 bytes，仅编辑器可用 |
| `Excel2CsBytesTool.Excel2Cs()` | 只生成 C#，仅编辑器可用 |
| `Excel2CsBytesTool.Excel2Bytes()` | 只生成 bytes，仅编辑器可用 |
| `Excel2CsBytesTool.Export(bool generateCode, bool generateBytes)` | 按参数选择导出内容，仅编辑器可用 |
| `Excel2CsBytesTool.OpenSourceFolder()` | 打开 Excel 源目录，仅编辑器可用 |
| `Excel2CsBytesTool.ExcelDataPath / CsClassPath / BytesDataPath` | 获取源表、生成代码、数据目录；只读，仅编辑器可用 |

底层扩展接口（普通业务无需调用）：

| API | 用途 |
| --- | --- |
| `TableBinary.Load<T>(string tableName, string schema, Func<BinaryReader, T> readRow)` → `List<T>` | 自定义行读取；从 `Resources/DataTable/表名` 加载并校验字段签名 |
| `TableBinary.ReadCount(BinaryReader reader)` → `int` | 读取并校验行数或数组长度 |
| `TableBinary.Magic / Version` | 二进制格式常量 |

缺少数据文件或字段签名不匹配时，加载会抛出异常，重新执行 `Export All`。
