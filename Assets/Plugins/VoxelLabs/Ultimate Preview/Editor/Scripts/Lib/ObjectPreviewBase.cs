using System.Runtime.CompilerServices;
using UnityEditor;

[assembly: InternalsVisibleTo("VoxelLabs.UltimatePreview.Core")]

namespace VoxelLabs.UltimatePreview
{
    public abstract class ObjectPreviewBase : ObjectPreview
    {
        public ObjectPreviewBase()
        {
            
        }
        
        internal abstract void Dispose();

#if UNITY_2021_1_OR_NEWER
        public override void Cleanup()
        {
            base.Cleanup();
            
            Dispose();
        }
#endif
    }
}