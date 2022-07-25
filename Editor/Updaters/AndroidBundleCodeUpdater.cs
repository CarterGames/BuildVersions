using UnityEditor;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Updates the android bundle code...
    /// </summary>
    public sealed class AndroidBundleCodeUpdater : IPreBuildDialogue, IBuildUpdater
    {
        //
        //
        //  IPreBuildDialogue Implementation 
        //
        //


        public void OnPreBuildDialogue(BuildTarget target)
        {
            if (target != BuildTarget.Android) return;
            if (BuildVersionsEditorUtil.BuildOptions.androidUpdateBundleCode != AssetUsageType.PromptMe)
            {
                BuildHandler.Register(new HandlerDialogueData(BuildVersionsEditorUtil.GetClassName<AndroidBundleCodeUpdater>(), true));
                return;
            }

            var currentBundleCode = PlayerSettings.Android.bundleVersionCode;

            var choice = EditorUtility.DisplayDialog("Build Versions | Android Bundle Code Updater",
                $"Do you want to increment the bundle code for this build?\n\nThis will update the code from {currentBundleCode} to {currentBundleCode + 1}.\n\nYou can disable this prompt in the asset settings.",
                $"Yes (Set to {currentBundleCode + 1})", $"No (Leave as {currentBundleCode})");
            
            BuildHandler.Register(new HandlerDialogueData(BuildVersionsEditorUtil.GetClassName<AndroidBundleCodeUpdater>(), choice));
        }
        
        
        //
        //
        //  IBuildUpdater Implementation 
        //
        //
        
        public int UpdateOrder => 0;
        
        public void OnBuildVersionIncremented(BuildTarget buildTarget)
        {
            if (buildTarget != BuildTarget.Android) return;
            if (BuildVersionsEditorUtil.BuildOptions.androidUpdateBundleCode == AssetUsageType.Disabled) return;
            if (!BuildHandler.Get<AndroidBundleCodeUpdater>().Choice) return;

            PlayerSettings.Android.bundleVersionCode++;
        }
    }
}