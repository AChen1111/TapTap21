# 目录约定

空目录用 `.gitkeep` 占位，方便 Git 跟踪。Unity 下次导入会为新文件夹生成 `.meta`。

```
TapTap21/
├── .doc/                      项目文档、Pipeline 文档
├── .skills/                   Agent skills
├── Packages/                  UPM / 嵌入包
└── Assets/
    ├── Plugins/               Odin、SuperScrollView、vHierarchy、Ultimate Preview
    ├── Scripts/               游戏代码
    │   ├── Bootstrap/         启动、场景入口
    │   ├── Common/            单例、事件、工具
    │   ├── Network/           HTTP、DTO
    │   ├── Player/            会话、登录流程
    │   ├── UI/                界面逻辑（Window / Widget / Core）
    │   └── Addressable/       地址键、打包工具
    ├── UI/                    UI 资源（不含脚本）
    │   ├── Prefab/            界面预制体（BaseUI / LogIn / Hall …）
    │   ├── Sprite/            切图
    │   ├── Fonts/             字体 / TMP
    │   ├── Shader/            UI Shader
    │   └── Atlas/             图集（有再用）
    ├── Art/                   非 UI 美术（角色、特效、场景模型）
    ├── Shaders/               非 UI Shader / Shader Graph
    ├── Prefabs/               非 UI 预制体（角色、特效、场景物件）
    ├── Scenes/                场景（PreInit、Login、Hall …）
    ├── Settings/              URP、Volume、Input Actions
    ├── AddressableAssetsData/ Addressables 配置（有再用）
    ├── Audio/                 音效、BGM
    ├── Config/                ScriptableObject 配置表
    ├── Tests/                 测试代码
    └── StreamingAssets/       运行时原始文件（慎放密钥）
```

## 仓库根

| 路径 | 放什么 | 不放什么 |
| --- | --- | --- |
| `.doc/` | 给人看的说明、Pipeline 文档 | 游戏资源、脚本 |
| `.skills/` | Agent 技能（如 `unity-pipeline`） | 业务代码 |
| `Packages/` | UPM 依赖与嵌入包（UniTask、Hot Reload、Cursor Editor、Pipeline） | Asset Store 插件 |
| `Assets/` | 工程资源与游戏代码 | 文档、技能 |

`ProjectSettings/`、`Packages/manifest.json` 由 Unity 管理，不要手改 GUID。

## `Assets/` 规则

**脚本只进 `Scripts/`，预制体按用途分家：UI 进 `UI/Prefab`，其它进 `Prefabs/`。**

| 路径 | 职责 |
| --- | --- |
| `Plugins/` | Asset Store / 第三方插件源码与 DLL。现有：Sirenix（Odin）、SuperScrollView、vHierarchy、VoxelLabs（Ultimate Preview） |
| `Scripts/` | 全部游戏 C#。按模块分子目录，见下表 |
| `UI/` | UI 美术与界面预制体，**不含** `.cs` |
| `Art/` | 角色、特效、场景模型等非 UI 美术 |
| `Shaders/` | 非 UI Shader / Shader Graph |
| `Prefabs/` | 角色、特效、场景物件等非 UI 预制体 |
| `Scenes/` | 场景。后续入口建议 `PreInit` → `Login` → `Hall`；当前仍是模板 `SampleScene` |
| `Settings/` | URP、Renderer、Volume、Input System Actions |
| `AddressableAssetsData/` | Addressables 组与构建设置。接入 Addressables 包后再用 |
| `Audio/` | 音效、BGM |
| `Config/` | ScriptableObject 配置表 |
| `Tests/` | 编辑器 / PlayMode 测试 |
| `StreamingAssets/` | 运行时按原文件读取的内容。**不要放密钥、token、证书私钥** |

不要在 `Assets/` 根再堆业务目录。模板自带的 `TutorialInfo`、`Readme.asset` 已删；`InputSystem_Actions` 已挪到 `Settings/`。

## `Scripts/`

| 路径 | 职责 |
| --- | --- |
| `Bootstrap/` | 启动、场景入口、跨场景流程 |
| `Common/` | 单例、事件、扩展、通用工具 |
| `Network/` | HTTP 客户端、DTO、令牌存储；不含登录业务流程 |
| `Player/` | 会话、登录/注册流程、玩家数据 |
| `UI/` | 界面逻辑。可再分 `Core`（框架）、`Window`、`Widget` |
| `Addressable/` | 地址键常量、加载封装、打包相关编辑器工具 |

界面脚本在 `Scripts/UI`，对应预制体在 `UI/Prefab`，不要混放。

## `UI/`（资源）

| 路径 | 内容 |
| --- | --- |
| `Prefab/` | 界面预制体（BaseUI、LogIn、Hall …） |
| `Sprite/` | UI 切图，按模块分子目录 |
| `Fonts/` | 字体、TMP SDF |
| `Shader/` | 只服务 UI 的 Shader |
| `Atlas/` | 图集。真正打图集再用，空着即可 |

## 插件放哪

| 类型 | 位置 | 例子 |
| --- | --- | --- |
| Asset Store / 解压进工程 | `Assets/Plugins/` | Odin、SuperScrollView、vHierarchy、Ultimate Preview |
| UPM / 嵌入包 | `Packages/` | UniTask、Hot Reload、Cursor Editor、Unity Pipeline |

不要把插件拆进 `Scripts/` 或 `UI/`。

## Git 与空目录

- 空文件夹用 `.gitkeep` 占位。
- 新文件夹的 `.meta` 由 Unity 导入生成，提交时带上 `.meta`。
- Addressables 构建产物（如 `StreamingAssets/aa*`）已在 `.gitignore`，不要提交。
