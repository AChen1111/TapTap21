# Unity Pipeline — command cheat sheet

Always prefix with:

```bash
unity command --proxy-disable --project-path <project-root>
```

Full parameter schemas: live `unity command` list, or `.doc/unity-pipeline/commands/*.md`.

## GameObject

| Command | Purpose | Key args |
|---------|---------|----------|
| `create_gameobject` | Empty GO or primitive | `--name`, `--primitive` (`cube`/`sphere`/`capsule`/`cylinder`/`plane`/`quad`), `--parent` |
| `create_gameobjects` | Batch create | `--name`, `--primitive`, `--count`, `--positions`, `--rotations`, `--scales`, `--parent` |
| `find_gameobjects` | Query hierarchy | `--name`, `--tag`, `--type`, `--hierarchy_path`, `--include_inactive` |
| `set_transform` | Local TRS | `--target`, `--position`, `--rotation`, `--scale` (each `[x,y,z]`) |
| `set_parent` | Reparent | `--target`, `--parent`, `--world_position_stays` |
| `set_active` | activeSelf | `--target`, `--active` |
| `set_tag` / `set_layer` | Tag / layer | `--target`, `--tag` or `--layer` |
| `rename_gameobject` | Rename | `--target`, `--name` |
| `delete_gameobject` | Delete (Undo) | `--target` |
| `add_component` | Add component | `--target`, `--type` |
| `remove_component` | Remove component | `--target`, `--type` |
| `set_component_properties` | Set serialized props | `--target`, `--properties`, `--type` |
| `get_serialized_fields` | Read fields | `--target`, `--field`, `--component` |

**Verified example:**

```bash
unity command --proxy-disable --project-path <project> create_gameobject --name CLI_Cube --primitive cube
```

`target` / `parent` accept `ObjectRef` strings: hierarchy path (`/CLI_Cube`), `globalId`, etc.

## Scene

| Command | Purpose | Key args |
|---------|---------|----------|
| `create_scene` | New scene under authoring root | `--path`, `--additive`, `--template` |
| `open_scene` | Open scene | `--path`, `--additive` |
| `save_scene` | Save open scene | (optional path args per live schema) |
| `save_all` | Save all dirty scenes | — |
| `list_open_scenes` | List open scenes | — |
| `set_active_scene` | Set active scene | `--path` |
| `get_scene_hierarchy` | Hierarchy tree | (scene selector per live schema) |
| `add_scene_to_build` / `remove_scene_from_build` | Build Settings | scene path args |

## Editor lifecycle & observability

| Command | Purpose | Key args |
|---------|---------|----------|
| `editor_status` | Editor / Pipeline health | — |
| `set_autotick` | Keep Editor ticking unfocused | `--enable`, `--interval_ms` |
| `editor_play` / `editor_stop` / `editor_pause` | Play mode | — |
| `recompile` | Force script compile | `--focus` |
| `recompile_status` | Poll compile | — |
| `run_tests` | Run tests — sync by default; always `--filter`; use `--async_tests` for full suites | `--mode` (`all`/`editor`/`playmode`), `--filter`, `--async_tests` |
| `test_status` / `cancel_tests` | Poll / abort tests (reachable while a run is in progress) | — |
| `list_tests` | List tests | `--mode` |
| `get_console_logs` | Console buffer | `--severity`, `--limit` |
| `clear_console` | Clear console | — |
| `screenshot` | Scene/Game PNG | `--view`, `--output`, `--width`, `--height` |
| `capture_game_view` | Game view PNG; default path-only when `--save_path` is set | `--width`, `--height`, `--camera`, `--save_path` |
| `run_script` | Compile+run a C# file in memory (no domain reload) | `--file`, `--entry` |
| `batch` | Up to 200 ordered ops in one request | see `.doc/unity-pipeline/commands/batch.md` |
| `audit` / `audit_status` | Project Auditor scan (optional) | `--categories`, `--output` |

## Assets (brief)

| Command | Purpose |
|---------|---------|
| `find_assets` | By type/name/label |
| `create_asset` / `import_asset` / `move_asset` / `copy_asset` / `rename_asset` | Asset ops |
| `delete_asset` | Needs `--confirm true` |
| `read_text_file` / `write_text_file` | Text under authoring root |

## Safety

- Destructive commands often require `--confirm true`; use `--dry_run` when available.
- Scene/GO mutations blocked during Play mode.
- Async flows: trigger once, then poll `*_status` until done. A CLI timeout never cancels the
  Editor-side run — re-issuing the same command stacks a second run and can wedge the Editor.
