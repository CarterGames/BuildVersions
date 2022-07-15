using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;



namespace CarterGames.Assets.BuildVersions.Editor
{
    public class BuildVersionsManager : UnityEditor.Editor, IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        /// <summary>
        /// Gets the information asset to edit...
        /// </summary>
        /// <returns>Build Info</returns>
        private static BuildInformation GetInformationAsset()
        {
            var info = BuildVersionsEditorUtil.BuildInformation;

            if (info != null) return info;
            BvLog.Error("Unable to update data as it was not found in the project!");
            return null;
        }
        
        
        /// <summary>
        /// Sets the build type string to the entered value...
        /// </summary>
        /// <param name="value">The value to set</param>
        public static void SetBuildType(string value)
        {
            var info = GetInformationAsset();
            
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
            var info = GetInformationAsset();
            
            info.SetBuildDate();
            EditorUtility.SetDirty(info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        

        public static void SetBuildNumber(int value = -1)
        {
            var info = GetInformationAsset();

            if (value.Equals(-1))
                info.IncrementBuildNumber();
            else
                info.BuildNumber = value;
            
            EditorUtility.SetDirty(info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        
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
            
            // Stops if the asset is disabled from updating the build number...
            if (settings.assetStatus == AssetUsageType.Disabled)
            {
                BvLog.Warning("Asset is disabled, will not update any build numbers this build.");
                return;
            }
            
            // Prompts the user 
            if (settings.assetStatus == AssetUsageType.PromptMe)
            {
                if (!EditorUtility.DisplayDialog("Build Versions",
                        "Do you want the build numbers to update for this build?\nYou can disable this prompt in the asset settings.",
                        "Yes", "No")) return;
            }
            
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
        
        
        /// <summary>
        /// Calls to run all updater interface listeners...
        /// </summary>
        /// <param name="target">The build target</param>
        private static void CallUpdaterInterfaces(BuildTarget target)
        {
            var listeners = BuildVersionsEditorUtil.GetAllInterfacesOfType<IBuildVersionUpdate>();

            if (listeners.Length <= 0)
            {
                BvLog.Error("No updaters found, so nothing will happen!");
                return;
            }

            foreach (var t in listeners)
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

            foreach (var t in listeners)
                t.OnVersionSync(Application.version);
        }
    }
}