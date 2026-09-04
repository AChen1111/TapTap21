using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Unity.CodeEditor;
using UnityEditor;
using UnityEngine;

namespace Microsoft.Unity.VisualStudio.Editor
{
	[InitializeOnLoad]
	internal static class CursorExternalScriptWatcher
	{
		private static readonly long DebounceTicks = TimeSpan.FromMilliseconds(500).Ticks;
		private static readonly ConcurrentQueue<string> ChangedPaths = new ConcurrentQueue<string>();

		private static FileSystemWatcher _watcher;
		private static int _refreshPending;
		private static long _lastChangeTicks;

		static CursorExternalScriptWatcher()
		{
			EditorApplication.update += OnEditorUpdate;
			AssemblyReloadEvents.beforeAssemblyReload += Dispose;
			Initialize();
		}

		private static void Initialize()
		{
			if (_watcher != null || !Directory.Exists(Application.dataPath))
				return;

			try
			{
				_watcher = new FileSystemWatcher(Application.dataPath, "*.cs")
				{
					IncludeSubdirectories = true,
					NotifyFilter = NotifyFilters.FileName,
					InternalBufferSize = 32 * 1024,
					EnableRaisingEvents = true
				};
				_watcher.Created += OnPathChanged;
				_watcher.Deleted += OnPathChanged;
				_watcher.Renamed += OnPathRenamed;
				_watcher.Error += OnWatcherError;
			}
			catch (Exception ex)
			{
				Debug.LogWarning($"[Cursor] Failed to watch external C# file changes: {ex.Message}");
				Dispose();
			}
		}

		private static void OnPathChanged(object sender, FileSystemEventArgs args)
		{
			QueueRefresh(args.FullPath);
		}

		private static void OnPathRenamed(object sender, RenamedEventArgs args)
		{
			QueueRefresh(args.OldFullPath);
			QueueRefresh(args.FullPath);
		}

		private static void OnWatcherError(object sender, ErrorEventArgs args)
		{
			QueueRefresh(null);
		}

		private static void QueueRefresh(string fullPath)
		{
			if (!string.IsNullOrEmpty(fullPath))
				ChangedPaths.Enqueue(fullPath);

			Interlocked.Exchange(ref _lastChangeTicks, DateTime.UtcNow.Ticks);
			Interlocked.Exchange(ref _refreshPending, 1);
		}

		private static void OnEditorUpdate()
		{
			if (Volatile.Read(ref _refreshPending) == 0)
				return;

			var lastChangeTicks = Interlocked.Read(ref _lastChangeTicks);
			if (DateTime.UtcNow.Ticks - lastChangeTicks < DebounceTicks)
				return;

			if (Interlocked.Exchange(ref _refreshPending, 0) == 0)
				return;

			var paths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			while (ChangedPaths.TryDequeue(out var path))
				paths.Add(path);

			try
			{
				// 先定向导入仍存在的脚本, 即使 Hot Reload 暂停全局 Refresh 也能更新新文件.
				foreach (var path in paths)
				{
					if (!File.Exists(path))
						continue;

					var assetPath = ToAssetPath(path);
					if (assetPath != null)
						AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
				}

				AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
				CodeEditor.CurrentEditor?.SyncAll();
			}
			catch (Exception ex)
			{
				Debug.LogWarning($"[Cursor] Failed to synchronize external C# file changes: {ex.Message}");
			}
		}

		private static string ToAssetPath(string fullPath)
		{
			var assetsRoot = Application.dataPath.TrimEnd('/', '\\');
			if (!fullPath.StartsWith(assetsRoot, StringComparison.OrdinalIgnoreCase))
				return null;

			return "Assets" + fullPath.Substring(assetsRoot.Length).Replace('\\', '/');
		}

		private static void Dispose()
		{
			if (_watcher == null)
				return;

			_watcher.EnableRaisingEvents = false;
			_watcher.Dispose();
			_watcher = null;
		}
	}
}
