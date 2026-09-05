# TapTap21

Unity 工程。编辑器版本以 `ProjectSettings/ProjectVersion.txt` 为准，当前是 **6000.3.23f1**（6.3 LTS）。

## 打开工程

1. 安装 [Git LFS](https://git-lfs.com)，在本机执行一次 `git lfs install`。
2. 克隆本仓库（不要下 GitHub ZIP，ZIP 不含 LFS 资源）：

```bash
git clone https://github.com/AChen1111/TapTap21.git
cd TapTap21
git lfs pull
```

3. 用 Unity Hub 安装 **6000.3.23f1**。Hub 默认列表往往没有这个补丁号，请用下面任一方式：
   - 打开 [6000.3.23f1 发布页](https://unity.com/releases/editor/whats-new/6000.3.23f1) 点 Open in Hub
   - Hub 深链：`unityhub://6000.3.23f1`
   - Hub → Installs → Install Editor → Archive
4. Hub → Add → 选本仓库根目录（含 `Assets/`、`Packages/`、`ProjectSettings/` 的那一层）。

## 克隆后编译报缺 Odin / UltimatePreview.Shared

插件 DLL 已作为普通 Git 文件提交。若你拉到的还是旧提交，或 PNG 等资源显示异常，执行 `git lfs pull`。

快速自检：`Assets/Plugins/Sirenix/Assemblies/Sirenix.OdinInspector.Editor.dll` 大约 3.5MB。若只有一两百字节，说明仍是 LFS 指针，不是真 DLL。
