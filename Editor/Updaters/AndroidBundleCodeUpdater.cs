using UnityEditor;

namespace CarterGames.Assets.BuildVersions.Editor
{
    public class AndroidBundleCodeUpdater : IBuildVersionUpdate
    {
        public void OnBuildVersionIncremented(BuildTarget buildTarget)
        {
            if (buildTarget != BuildTarget.Android) return;
            if (BuildVersionsEditorUtil.BuildOptions.androidUpdateBundleCode == AssetUsageType.Disabled) return;
            
            if (BuildVersionsEditorUtil.BuildOptions.androidUpdateBundleCode == AssetUsageType.PromptMe)
            {
                if (!EditorUtility.DisplayDialog("Build Versions",
                        "Do you want to increment the bundle code for this build?\nYou can disable this prompt in the asset settings.",
                        "Yes", "No")) return;
            }
                        
            PlayerSettings.Android.bundleVersionCode++;
        }
    }
}