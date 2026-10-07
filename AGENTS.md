# Repository Guidelines

## 项目结构与模块组织

TapTap21 是 Unity 6 项目，编辑器版本为 `6000.3.23f1`（见 `ProjectSettings/ProjectVersion.txt`）。运行时代码位于 `Assets/Scripts`，按 `Common`、`GamePlay`、`UI`、`Network` 和 `Bootstrap` 组织。场景、预制体、美术、音频和着色器位于 `Assets/`。测试位于 `Assets/Tests/Editor`、`Assets/Tests/PlayMode`、`Assets/Tests/SaveSystem` 及各功能目录。本地包位于 `Packages/`；Excel 源表位于 `ExcelData/`，生成的 C# 和二进制表位于 `Assets/Scripts/Common/ExcelTable/Generated` 与 `Assets/Resources/DataTable`。

## 构建、测试与开发命令

- 安装 Git LFS，执行 `git lfs install` 和 `git lfs pull`，再用 Unity Hub 以 `6000.3.23f1` 打开仓库根目录。
- 使用 Unity 的 Build Profiles 窗口构建目标平台。
- 在 Unity Test Runner 中运行编辑器测试，或使用批处理命令：`Unity -batchmode -quit -projectPath . -runTests -testPlatform editmode -testResults TestResults/editmode.xml`。
- Play Mode 测试使用相同命令，并将平台改为 `-testPlatform playmode`。
- 修改 `.xlsx` 后，在 Unity 中运行 `Tools > Excel > Export All (CS + Bytes)`。生成文件是输出结果，不要手动编辑。

## 编码风格与命名约定

使用 4 个空格缩进，左大括号另起一行，显式声明命名空间，并保持方法职责单一。类、公共方法和属性使用 `PascalCase`；局部变量和参数使用 `camelCase`；私有字段使用 `_camelCase`。测试类以 `Tests` 结尾，测试方法描述行为，例如 `Dispatch_InvokesListenersInRegistrationOrder`。遵循相邻代码风格；每个 Unity 资源都必须保留对应的 `.meta` 文件。

## 测试规范

通过 Unity Test Framework 使用 NUnit。仅编辑器测试放在 `Assets/Tests/Editor`；运行时或依赖场景的测试放在 `Assets/Tests/PlayMode`。测试夹具按被测子系统命名，并用 `[SetUp]`/`[TearDown]` 隔离状态。提交前运行相关测试套件，并在 PR 中附上 XML 结果或测试摘要。

## 提交与 Pull Request 规范

近期提交使用 `[开发]`、`[修改]`、`[完善]`、`[调整]` 等前缀，后接简洁明确的中文摘要。请保持这一约定。PR 应说明行为变化，列出受影响的场景和资源，关联任务，并提供测试证据。UI、场景或视觉改动应附截图或简短录屏。不要提交生成的项目文件、`Library/`、`Logs/` 或本地构建产物。

## 资源与配置安全

不要提交密钥或机器本地配置。大型二进制资源应通过 Git LFS 跟踪；排查插件错误前，先确认克隆得到的 DLL 是真实文件，而不是 LFS 指针文本。修改 `ProjectSettings/`、`Packages/manifest.json` 或 Addressables 数据时，应同时提交相关依赖资源。
