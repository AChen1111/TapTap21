namespace DialogueSystem
{
    public interface IDialogueVariableStore
    {
        bool Has(string key);
        string Get(string key);
        void Set(string key, string value);
        void Clear(string key);
        void ClearAll();
    }
}
