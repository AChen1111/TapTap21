using System;
using System.Collections.Generic;

namespace TapTap21.SaveSystem.dyh
{
    /// <summary>Optional, typed migration steps. Register these in the central definitions file.</summary>
    public static class SaveMigration
    {
        private sealed class Step
        {
            public int From;
            public int To;
            public Func<object, object> Migrate;
        }

        private static readonly Dictionary<string, List<Step>> steps = new Dictionary<string, List<Step>>(StringComparer.Ordinal);

        public static void Register<T>(SaveDefinition<T> definition, int fromVersion, int toVersion, Func<T, T> migration)
        {
            if (definition == null) throw new ArgumentNullException("definition");
            if (!SaveDefinitionRegistry.IsRegistered(definition)) throw new ArgumentException("definition 未在注册中心登记。", "definition");
            if (fromVersion < 1 || toVersion <= fromVersion) throw new ArgumentOutOfRangeException("toVersion", "迁移版本必须递增。 ");
            if (migration == null) throw new ArgumentNullException("migration");
            List<Step> list;
            if (!steps.TryGetValue(definition.Key, out list)) steps.Add(definition.Key, list = new List<Step>());
            for (int i = 0; i < list.Count; i++)
                if (list[i].From == fromVersion) throw new InvalidOperationException("已存在相同起始版本的迁移: " + definition.Key + " v" + fromVersion);
            list.Add(new Step { From = fromVersion, To = toVersion, Migrate = value => migration((T)value) });
        }

        internal static bool TryMigrate<T>(SaveDefinition<T> definition, ref T data, int fromVersion, out string error)
        {
            error = null;
            if (fromVersion == definition.Version) return true;
            List<Step> list;
            if (!steps.TryGetValue(definition.Key, out list)) { error = "没有找到从 v" + fromVersion + " 到 v" + definition.Version + " 的迁移。"; return false; }
            int current = fromVersion;
            while (current < definition.Version)
            {
                Step selected = null;
                for (int i = 0; i < list.Count; i++) if (list[i].From == current) { selected = list[i]; break; }
                if (selected == null || selected.To > definition.Version) { error = "迁移链不完整: v" + current; return false; }
                try { data = (T)selected.Migrate(data); }
                catch (Exception ex) { error = ex.Message; return false; }
                current = selected.To;
            }
            return current == definition.Version;
        }
    }
}
