namespace VoxelLabs.UltimatePreview
{
    public static class Utils
    {
        public static int GetEditorVersionIdentifier()
        {
#if UNITY_2022_2_OR_NEWER
            return 2022_2;
#elif UNITY_2021_3_OR_NEWER
                return 2021_3;
#elif UNITY_2021_1_OR_NEWER
                return 2021_1;
#elif UNITY_2018_4_OR_NEWER
                return 2018_4;
#endif
        }

        public static bool GetVerbosity()
        {
#if ULTIMATE_PREVIEW_VERBOSE
            return true;
#else
            return false;
#endif
        }
    }
}