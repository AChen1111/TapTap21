---
name: unity-pipeline
description: 通过 unity CLI 查询或修改 Unity 编辑器中的场景、游戏对象、组件和资源。
---

# Unity Pipeline

## 规则

- 仅用户明确要求时才编译或运行测试；不在编辑后自动执行，也不例行询问。
- 不通过刷新、重新导入、播放模式或辅助脚本绕过上述限制，不修改自动刷新设置。
- 使用现有 Pipeline 命令完成任务，不附带审计、构建等额外操作。

## 连接与调用

编辑器须已打开本项目并启用 Pipeline 服务；unity CLI 须在 PATH 中（%LOCALAPPDATA%\Unity\bin）。

本机调用必须带 --proxy-disable，避免代理拦截本地请求。

```powershell
$projectPath = 'D:\GameWorkplace\Doing\TapTap21'

# 检查连接
unity pipeline list --proxy-disable

# 查询可用命令和参数
unity command --proxy-disable --project-path "$projectPath"

# 执行命令
unity command --proxy-disable --project-path "$projectPath" <command> [args]
```

连接失败时检查 Pipeline → Start Server。端口文件：Library/Pipeline/.unity-pipeline-port。

## 操作要点

- target / parent 使用 ObjectRef，如层级路径 /Player 或 globalId。
- 场景修改后保存对应场景。
- 场景和物体修改在播放模式下会被阻止，切换播放状态须符合用户任务。
- editor_status 返回 blocked_by_dialog 时停止重试，告知用户。
- 命令超时后先查询状态，避免重复提交仍在执行的操作。

## 命令参考

按需查阅 [Unity Pipeline 文档](../../../../../.doc/unity-pipeline/index.md)；参数以实时命令结构为准。
