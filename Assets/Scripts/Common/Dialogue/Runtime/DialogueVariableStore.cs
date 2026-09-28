using System.Collections.Generic;

namespace DialogueSystem
{
    public class DialogueVariableStore : IDialogueVariableStore
    {
        readonly Dictionary<string, string> _values = new Dictionary<string, string>();

        public bool Has(string key)
        {
            return !string.IsNullOrEmpty(key) && _values.ContainsKey(key);
        }

        public string Get(string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;
            return _values.TryGetValue(key, out var value) ? value : string.Empty;
        }

        public void Set(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                return;
            _values[key] = value ?? string.Empty;
        }

        public void Clear(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;
            _values.Remove(key);
        }

        public void ClearAll()
        {
            _values.Clear();
        }
    }
}
