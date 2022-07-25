using UnityEditor;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// A class to draw all the menu options for the navigation menu options in this asset...
    /// </summary>
    public static class BuildVersionsMenuOptions
    {
        [MenuItem("Carter Games/Build Versions/Edit Info", priority = 0)]
        public static void EditInfo()
        {
            Selection.activeObject = BuildVersionsEditorUtil.BuildInformation;
        }
        
        [MenuItem("Carter Games/Build Versions/Edit Settings", priority = 1)]
        public static void EditSettings()
        {
            Selection.activeObject = BuildVersionsEditorUtil.BuildOptions;
        }
        
        [MenuItem("Carter Games/Build Versions/Type/Set To Prototype")]
        public static void SetToPrototype() => BuildVersionsManager.SetBuildType("Prototype");
        
        [MenuItem("Carter Games/Build Versions/Type/Set To Development")]
        public static void SetToDevelopment() => BuildVersionsManager.SetBuildType("Development");
        
        [MenuItem("Carter Games/Build Versions/Type/Set To Test")]
        public static void SetToTest() => BuildVersionsManager.SetBuildType("Test");
        
        [MenuItem("Carter Games/Build Versions/Type/Alpha/Set To Pre-Alpha")]
        public static void SetToPreAlpha() => BuildVersionsManager.SetBuildType("Pre-Alpha");
        
        [MenuItem("Carter Games/Build Versions/Type/Alpha/Set To Closed Alpha")]
        public static void SetToClosedAlpha() => BuildVersionsManager.SetBuildType("Closed Alpha");
        
        [MenuItem("Carter Games/Build Versions/Type/Alpha/Set To Alpha")]
        public static void SetToAlpha() => BuildVersionsManager.SetBuildType("Alpha");
        
        [MenuItem("Carter Games/Build Versions/Type/Alpha/Set To Open Alpha")]
        public static void SetToOpenAlpha() => BuildVersionsManager.SetBuildType("Open Alpha");
        
        [MenuItem("Carter Games/Build Versions/Type/Beta/Set To Closed Beta")]
        public static void SetToClosedBeta() => BuildVersionsManager.SetBuildType("Closed Beta");

        [MenuItem("Carter Games/Build Versions/Type/Beta/Set To Beta")]
        public static void SetToBeta() => BuildVersionsManager.SetBuildType("Beta");
        
        [MenuItem("Carter Games/Build Versions/Type/Beta/Set To Open Beta")]
        public static void SetToOpenBeta() => BuildVersionsManager.SetBuildType("Open Beta");
        
        [MenuItem("Carter Games/Build Versions/Type/Release/Set To Release Candidate")]
        public static void SetToReleaseCandidate() => BuildVersionsManager.SetBuildType("Release Candidate");
        
        [MenuItem("Carter Games/Build Versions/Type/Release/Set To Release")]
        public static void SetToRelease() => BuildVersionsManager.SetBuildType("Release");
        
        [MenuItem("Carter Games/Build Versions/Date/Set Date To Today")]
        public static void SetBuildDate() => BuildVersionsManager.SetDate();
        
        [MenuItem("Carter Games/Build Versions/Build Number/Increment Build Number")]
        public static void IncrementBuild() => BuildVersionsManager.SetBuildNumber();
        
        [MenuItem("Carter Games/Build Versions/Build Number/Reset Build Number")]
        public static void ResetBuild() => BuildVersionsManager.SetBuildNumber(1);

        [MenuItem("Carter Games/Build Versions/Version Number/Increment Major")]
        public static void IncrementMajor() => SystematicVersionUpdater.CallUpdateVersionNumber(EditorUserBuildSettings.activeBuildTarget, 0);
        
        [MenuItem("Carter Games/Build Versions/Version Number/Increment Minor")]
        public static void IncrementMinor() => SystematicVersionUpdater.CallUpdateVersionNumber(EditorUserBuildSettings.activeBuildTarget, 1);
        
        [MenuItem("Carter Games/Build Versions/Version Number/Increment Build")]
        public static void IncrementVersionBuild() => SystematicVersionUpdater.CallUpdateVersionNumber(EditorUserBuildSettings.activeBuildTarget);
        
        [MenuItem("Carter Games/Build Versions/Sync/Sync Application Version Number")]
        public static void SyncVersionNumber() => BuildVersionsManager.CallSyncInterfaces();

        [MenuItem("Carter Games/Build Versions/Sync/Update Cached Version Number")]
        public static void UpdateCachedVersionNumber() => BuildVersionsManager.UpdateCachedVersionNumber();
    }
}
