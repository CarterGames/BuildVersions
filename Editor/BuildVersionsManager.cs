using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Handles the updating of the build numbers for the asset...
    /// </summary>
    public class BuildVersionsManager : UnityEditor.Editor, IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        //
        //
        //  Build Information Setters
        //
        //
        
        /// <summary>
        /// Updates the cached version number to the latest number...
        /// </summary>
        public static void UpdateCachedVersionNumber()
        {
            var options = BuildVersionsEditorUtil.BuildOptions;
            
            options.LastSystematicVersionNumberSaved = SystematicVersionUpdater.GetVersionNumber(EditorUserBuildSettings.activeBuildTarget, false);
            EditorUtility.SetDirty(options);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        
        /// <summary>
        /// Sets the build type string to the entered value...
        /// </summary>
        /// <param name="value">The value to set</param>
        public static void SetBuildType(string value)
        {
            var info = BuildVersionsEditorUtil.BuildInformation;
            
            info.BuildType = value;
            EditorUtility.SetDirty(info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        /// <summary>
        /// Sets the date manually 
        /// </summary>
        public static void SetDate()
        {
            var info = BuildVersionsEditorUtil.BuildInformation;
            
            info.SetBuildDate();
            EditorUtility.SetDirty(info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        

        /// <summary>
        /// Sets the build number to a new value...
        /// </summary>
        /// <param name="value"></param>
        public static void SetBuildNumber(int value = -1)
        {
            var info = BuildVersionsEditorUtil.BuildInformation;

            if (value.Equals(-1))
                info.IncrementBuildNumber();
            else
                info.BuildNumber = value;
            
            EditorUtility.SetDirty(info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        
        //
        //
        //  Build Process Implementations
        //
        //
        
        
        /// <summary>
        /// Required for the report interfaces...
        /// </summary>
        public int callbackOrder => 1;


        /// <summary>
        /// Runs when a build is made...
        /// </summary>
        /// <param name="report">The build report provided automatically</param>
        public void OnPreprocessBuild(BuildReport report)
        {
            // Sets the build date to today xD
            SetDate();

            var settings = BuildVersionsEditorUtil.BuildOptions;
            var info = BuildVersionsEditorUtil.BuildInformation;
            
            // Stops if the asset is disabled from updating the build number...
            if (settings.assetStatus == AssetUsageType.Disabled)
            {
                BvLog.Warning("Asset is disabled, will not update any build numbers this build.");
                return;
            }

            // Prompts the user 
            if (settings.assetStatus == AssetUsageType.PromptMe)
            {
                if (!EditorUtility.DisplayDialog("Build Versions | Use Asset",
                        $"Do you want to use the asset to update build numbers for this build?\n\nThis will update the unique build number from {info.BuildNumber} -> {info.BuildNumber + 1} in the build information and allow the rest of the asset to function.\n\nYou can disable this prompt in the asset settings.",
                        "Yes (Enables Asset For Build)", "No (Disabled Asset For Build)")) return;
            }
            
            BuildHandler.Clear();

            // Calls any dialogue boxes needed for 
            CallRelevantDialogueBoxes(report.summary.platform);
            
            // Stops if the update time is not any...
            if (settings.buildUpdateTime != BuildIncrementTime.AnyBuild) return;

            // Runs the updaters...
            CallUpdaterInterfaces(report.summary.platform);
        }


        /// <summary>
        /// Runs when a build is made...
        /// </summary>
        /// <param name="report">The build report provided automatically</param>
        public void OnPostprocessBuild(BuildReport report)
        {
            var settings = BuildVersionsEditorUtil.BuildOptions;
            
            // Stops if the asset is disabled from updating the build number...
            if (settings.assetStatus == AssetUsageType.Disabled) return;
            
            // Stops if the update time is not Only Successful Builds...
            if (settings.buildUpdateTime != BuildIncrementTime.OnlySuccessfulBuilds) return;
            
            // Runs the updaters...
            CallUpdaterInterfaces(report.summary.platform);
        }
        
        
        //
        //
        //  Interface Broadcasters
        //
        //
        
        
        /// <summary>
        /// Calls to run all updater interface listeners...
        /// </summary>
        /// <param name="target">The build target</param>
        private static void CallUpdaterInterfaces(BuildTarget target)
        {
            var listeners = BuildVersionsEditorUtil.GetAllInterfacesOfType<IBuildUpdater>();

            if (listeners.Length <= 0)
            {
                BvLog.Error("No updaters found, so nothing will happen!");
                return;
            }

            var _orderedEnumerable = listeners.OrderBy(t => t.UpdateOrder);

            foreach (var t in _orderedEnumerable)
                t.OnBuildVersionIncremented(target);
        }


        /// <summary>
        /// Calls the sync all the version numbers that use the systematic number...
        /// </summary>
        public static void CallSyncInterfaces()
        {
            // Stops the sync if the user is not using systematic updating...
            if (BuildVersionsEditorUtil.BuildOptions.updateSystematic == AssetUsageType.Disabled)
            {
                BvLog.Warning("Update Systematic is disabled! Sync will not run while this setting is disabled.");
                return;
            }
            
            var listeners = BuildVersionsEditorUtil.GetAllInterfacesOfType<ISyncable>();

            if (listeners.Length <= 0)
            {
                BvLog.Normal("No sync implementations found, so nothing will happen!");
                return;
            }

            var lastSaved = BuildVersionsEditorUtil.BuildOptions.LastSystematicVersionNumberSaved;

            foreach (var t in listeners)
                t.OnVersionSync(lastSaved.Length > 0 ? lastSaved : Application.version);
        }


        /// <summary>
        /// Calls all build updaters that use dialogue to update 
        /// </summary>
        /// <param name="target"></param>
        private static void CallRelevantDialogueBoxes(BuildTarget target)
        {
            var listeners = BuildVersionsEditorUtil.GetAllInterfacesOfType<IPreBuildDialogue>();
            
            if (listeners.Length <= 0)
            {
                BvLog.Normal("No dialogue implementations found, ignoring.");
                return;
            }

            foreach (var t in listeners)
                t.OnPreBuildDialogue(target);
        }
    }
}