using UnityEditor;

namespace DialogueSystem.Editor
{
    public static class DialoguePackageExporter
    {
        const string PackagePath = "Assets/DialogueSystem";
        const string OutputPath = "DialogueSystem.unitypackage";

        [MenuItem("Tools/Dialogue/Export Package")]
        public static void Export()
        {
            AssetDatabase.ExportPackage(PackagePath, OutputPath, ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
            EditorUtility.DisplayDialog("Dialogue System", "Exported " + OutputPath + " at the project root.", "OK");
        }
    }
}
