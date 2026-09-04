using UnityEditor.AssetImporters;
using VoxelLabs.UltimatePreview.Shared;

namespace VoxelLabs.UltimatePreview
{
    public class AssetImporterEditorBase : AssetImporterEditor
    {
        public object[] containers = null;
        
#if UNITY_2022_2_OR_NEWER
        protected override void ResetValues() => DiscardChanges();

        public override void DiscardChanges()
        {
            base.DiscardChanges();
            if (containers == null)
                return;
            foreach (object container in containers)
                ReflectionUtils.ResetValuesMethodInfo?.Invoke(container, null);
        }
#else
        protected override void ResetValues()
        {
            base.ResetValues();
            
            if (containers == null)
                return;
            foreach (object container in containers)
                ReflectionUtils.ResetValuesMethodInfo?.Invoke(container, null);
        }
#endif
    }
}