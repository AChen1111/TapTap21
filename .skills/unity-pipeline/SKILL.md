---
name: unity-pipeline
description: >-
  Let Codex or Cursor drive this Unity project via the Unity CLI and
  com.unity.pipeline: create/edit
  GameObjects and scenes, recompile scripts, run tests, screenshots, and hot reload.
  Invoke this skill whenever Unity Editor or project operations are needed
  (场景/物体/脚本/编译/测试/截图等). Also use when controlling a live Unity Editor,
  automating scene authoring, running unity command / unity pipeline, or when
  localhost Proxy/Clash blocks Pipeline.
---

# Unity Pipeline (this project)

Control the open Unity Editor through the local Pipeline HTTP API using the `unity` CLI.

## Prerequisites

- Unity Editor has this project open (`TapTap21`, Unity 6.3 LTS)
- Start Editor with `-automated` so modal dialogs don't block Pipeline commands:
  `"D:\UnityEditor\6000.3.23f1\Editor\Unity.exe" -projectPath "<project>" -automated`
- `unity` CLI on PATH (`%LOCALAPPDATA%\Unity\bin`)
- Package embedded at `Packages/com.unity.pipeline` (`file:` dependency in `Packages/manifest.json`)

## Connection checklist (required)

This machine often has `HTTP_PROXY` / `HTTPS_PROXY` pointing at Clash (`127.0.0.1:7897`). That proxy breaks localhost Pipeline calls. **Always pass `--proxy-disable`.**

```bash
# Prefer an explicit project path
set PROJECT=<absolute-path-to-this-repo>

unity pipeline list --proxy-disable
# Expect: Running=true, Pipeline=true, Server Reachable=true

unity command --proxy-disable --project-path "%PROJECT%"
# Lists available commands when connected
```

If `Server Reachable=false`:

1. Confirm Editor is focused on this project and finished compiling
2. Check nothing is hogging the main thread — a long `run_tests` or a modal dialog (Editor not
   started with `-automated`) blocks every command until it clears. See [Running tests](#running-tests-read-before-calling-run_tests).
3. In Editor: **Pipeline → Start Server**
4. Descriptor file: `Library/Pipeline/.unity-pipeline-port` (port range `7800`–`7849`)

## Standard invocation

```bash
unity command --proxy-disable --project-path "%PROJECT%" <command> [args]
unity command --proxy-disable --project-path "%PROJECT%"   # list commands / schemas
```

Prefer `--format json` when parsing results.

## Core workflows

### Scene authoring

```bash
unity command --proxy-disable --project-path "%PROJECT%" create_gameobject --name CLI_Cube --primitive cube
unity command --proxy-disable --project-path "%PROJECT%" find_gameobjects --name CLI_Cube
unity command --proxy-disable --project-path "%PROJECT%" set_transform --target /CLI_Cube --position "[0,1,0]"
unity command --proxy-disable --project-path "%PROJECT%" save_scene
```

Primitives: `cube`, `sphere`, `capsule`, `cylinder`, `plane`, `quad`. Objects are referenced by `ObjectRef` (`hierarchyPath`, `globalId`, etc.). Mutations are Undo-able and blocked in Play mode.

### Edit → recompile → test

Simple, low-risk edits: recompile and check console errors; **do not** run tests.
Run tests only for new features or large / high-risk changes, and then only with `--filter`.

```bash
unity command --proxy-disable --project-path "%PROJECT%" set_autotick --enable true
# Edit C# on disk, then:
unity command --proxy-disable --project-path "%PROJECT%" recompile
unity command --proxy-disable --project-path "%PROJECT%" recompile_status
# Poll until completed / up_to_date. Connection drops during domain reload are expected.
unity command --proxy-disable --project-path "%PROJECT%" run_tests --mode editor --filter MyFixture.MyTest
```

Always enable `set_autotick` before headless compile/test work (unfocused Editor otherwise stalls).

### Running tests (read before calling `run_tests`)

Skip `run_tests` unless the change is a new feature or otherwise large / high-risk.

Command execution is still serialized (one `/api/exec` at a time). HTTP itself is concurrent:
`editor_status`, `test_status`, and progress stay reachable while a long command runs.

`run_tests` still occupies the Editor main thread for the whole run. The CLI's 30 s transport
timeout applies to the sync path, not to the Editor-side budget.

- **Always pass `--filter` in verification loops.** A bare `run_tests --mode editor` runs the whole
  suite (hundreds of tests here) and never returns within the CLI's 30 s transport timeout.
- **A CLI timeout does not cancel the run** — the Editor keeps going. Do *not* re-issue `run_tests`
  after a timeout. Concurrent runs can still wedge `TestRunnerApi`, leaving `Server Reachable=false`
  with only an Editor restart to recover.
- **Full suites go through async mode**, same trigger-then-poll shape as `recompile`:

```bash
unity command --proxy-disable --project-path "%PROJECT%" run_tests --mode editor --async_tests
unity command --proxy-disable --project-path "%PROJECT%" test_status    # poll until done
unity command --proxy-disable --project-path "%PROJECT%" cancel_tests   # abort a stuck run
```

- `run_tests --timeout` is the in-Editor execution budget (default 300 s), **not** the transport
  timeout. Raising it does not stop the CLI from giving up at 30 s.
- `--filter` is a case-insensitive substring match, so `--filter ALog` also pulls in
  `AddressableCatalogTests` (it contains "alog"). Use the full fixture name when the pass/fail
  tally has to be exact.
- PlayMode tests can't run synchronously at all (entering play mode triggers a domain reload that
  drops the request) — `--async_tests` is mandatory there.

### Verify

```bash
unity command --proxy-disable --project-path "%PROJECT%" get_console_logs --severity error --limit 20
unity command --proxy-disable --project-path "%PROJECT%" screenshot --view scene
```

### Hot reload / eval (optional)

Needs Play mode or a development Player. See package skill notes and [hot-reload.md](../../.doc/unity-pipeline/hot-reload.md).

```bash
unity command --proxy-disable --project-path "%PROJECT%" editor_play
unity command --proxy-disable --project-path "%PROJECT%" reload_file --filename Assets/Spinner.cs
unity command --proxy-disable --project-path "%PROJECT%" eval "return 2 + 2;"
```

`eval` default timeout is 60s. Longer or bulk work: `run_script` (in-memory compile, no domain
reload) or `batch` (up to 200 ops). A stuck command may be a modal dialog — `editor_status`
`blocked_by_dialog` means stop retrying and tell the user.

## Known noise (ignore for local Pipeline)

`UnityConnectWebRequestException: Token Exchange failed` — Unity Cloud login/proxy issue. Does **not** block local Pipeline authoring commands.

Package Manager `ECONNRESET` to `download.packages.unity.com` — use embedded `file:` packages under `Packages/` (already done for Pipeline + deps). Do not switch Pipeline back to registry versions on this network without a working proxy path for UPM.

## Progressive docs

| Need | Where |
|------|--------|
| High-frequency command cheat sheet | [commands.md](commands.md) |
| Full command reference | [.doc/unity-pipeline/](../../.doc/unity-pipeline/index.md), [TableOfContents.md](../../.doc/unity-pipeline/TableOfContents.md) |
| Ports, auth, discovery | [connectivity.md](../../.doc/unity-pipeline/connectivity.md) |
| Live parameter schemas | `unity command --proxy-disable --project-path …` (no command name) |

When unsure of flags, list commands from the live Editor or read the matching file under `.doc/unity-pipeline/commands/`.
