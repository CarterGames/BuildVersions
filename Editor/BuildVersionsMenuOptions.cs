using UnityEditor;

namespace CarterGames.Assets.BuildVersions.Editor
{
    public class BuildVersionsMenuOptions
    {
        [MenuItem("Tools/Build Versions | CG/Type/Set To Prototype", priority = 1)]
        public static void SetToPrototype() => BuildVersionsManager.SetBuildType("Prototype");
        
        [MenuItem("Tools/Build Versions | CG/Type/Set To Development", priority = 1)]
        public static void SetToDevelopment() => BuildVersionsManager.SetBuildType("Development");
        
        [MenuItem("Tools/Build Versions | CG/Type/Set To Test", priority = 1)]
        public static void SetToTest() => BuildVersionsManager.SetBuildType("Test");
        
        [MenuItem("Tools/Build Versions | CG/Type/Alpha/Set To Pre-Alpha", priority = 2)]
        public static void SetToPreAlpha() => BuildVersionsManager.SetBuildType("Pre-Alpha");
        
        [MenuItem("Tools/Build Versions | CG/Type/Alpha/Set To Closed Alpha", priority = 2)]
        public static void SetToClosedAlpha() => BuildVersionsManager.SetBuildType("Closed Alpha");
        
        [MenuItem("Tools/Build Versions | CG/Type/Alpha/Set To Alpha", priority = 2)]
        public static void SetToAlpha() => BuildVersionsManager.SetBuildType("Alpha");
        
        [MenuItem("Tools/Build Versions | CG/Type/Alpha/Set To Open Alpha", priority = 2)]
        public static void SetToOpenAlpha() => BuildVersionsManager.SetBuildType("Open Alpha");
        
        [MenuItem("Tools/Build Versions | CG/Type/Beta/Set To Closed Beta", priority = 3)]
        public static void SetToClosedBeta() => BuildVersionsManager.SetBuildType("Closed Beta");

        [MenuItem("Tools/Build Versions | CG/Type/Beta/Set To Beta", priority = 3)]
        public static void SetToBeta() => BuildVersionsManager.SetBuildType("Beta");
        
        [MenuItem("Tools/Build Versions | CG/Type/Beta/Set To Open Beta", priority = 3)]
        public static void SetToOpenBeta() => BuildVersionsManager.SetBuildType("Open Beta");
        
        [MenuItem("Tools/Build Versions | CG/Type/Release/Set To Release Candidate", priority = 4)]
        public static void SetToReleaseCandidate() => BuildVersionsManager.SetBuildType("Release Candidate");
        
        [MenuItem("Tools/Build Versions | CG/Type/Release/Set To Release", priority = 4)]
        public static void SetToRelease() => BuildVersionsManager.SetBuildType("Release");
        
        [MenuItem("Tools/Build Versions | CG/Date/Set Date To Today")]
        public static void SetBuildDate() => BuildVersionsManager.SetDate();
        
        [MenuItem("Tools/Build Versions | CG/Build Number/Increment Build Number")]
        public static void IncrementBuild() => BuildVersionsManager.SetBuildNumber();
        
        [MenuItem("Tools/Build Versions | CG/Build Number/Reset Build Number")]
        public static void ResetBuild() => BuildVersionsManager.SetBuildNumber(1);

        [MenuItem("Tools/Build Versions | CG/Version Number/Increment Major")]
        public static void IncrementMajor() => SystematicVersionUpdater.CallUpdateVersionNumber(EditorUserBuildSettings.activeBuildTarget, 0);
        
        [MenuItem("Tools/Build Versions | CG/Version Number/Increment Minor")]
        public static void IncrementMinor() => SystematicVersionUpdater.CallUpdateVersionNumber(EditorUserBuildSettings.activeBuildTarget, 1);
        
        [MenuItem("Tools/Build Versions | CG/Version Number/Increment Build")]
        public static void IncrementVersionBuild() => SystematicVersionUpdater.CallUpdateVersionNumber(EditorUserBuildSettings.activeBuildTarget);
        
        [MenuItem("Tools/Build Versions | CG/Sync/Sync Application Version Number")]
        public static void SyncVersionNumber() => BuildVersionsManager.CallSyncInterfaces();
    }
}