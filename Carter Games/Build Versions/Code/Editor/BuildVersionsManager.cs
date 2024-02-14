/*
 * Copyright (c) 2024 Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Handles the updating of the build numbers for the asset..
    /// </summary>
    public class BuildVersionsManager : UnityEditor.Editor, IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Build Information Setters
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

        /// <summary>
        /// Updates the cached version number to the latest number...
        /// </summary>
        public static void UpdateCachedVersionNumber()
        {
            var options = UtilEditor.BuildOptions;
            
            var grabbed = SemanticVersionUpdater.GetVersionNumber(EditorUserBuildSettings.activeBuildTarget, false);

            if (!SemanticVersionUpdater.IsInSemanticFormat(grabbed))
            {
                BvLog.Normal("Unable to cache latest as it doesn't match the semantic formatting of \"x.y.z\"");
                return;
            }
            
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
            var info = UtilEditor.BuildInformation;
            
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
            var info = UtilEditor.BuildInformation;
            
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
            var info = UtilEditor.BuildInformation;

            if (value.Equals(-1))
                info.IncrementBuildNumber();
            else
                info.BuildNumber = value;
            
            EditorUtility.SetDirty(info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Build Process Implementations
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

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
            if (!ProjectBuildTypeValidator.CanUpdateVersions()) return;
            
            // Sets the build date to today xD
            SetDate();

            var settings = UtilEditor.BuildOptions;
            var info = UtilEditor.BuildInformation;
            
            // Stops if the asset is disabled from updating the build number...
            if (settings.Status == AssetUsageType.Disabled)
            {
                BvLog.Warning("Asset is disabled, will not update any build numbers this build.");
                return;
            }

            // Prompts the user 
            if (settings.Status == AssetUsageType.PromptMe)
            {
                if (!EditorUtility.DisplayDialog("Build Versions | Use Asset",
                        $"Do you want to use the asset to update build numbers for this build?\n\nThis will update the unique build number from {info.BuildNumber} -> {info.BuildNumber + 1} in the build information and allow the rest of the asset to function.\n\nYou can disable this prompt in the asset settings.",
                        "Yes (Enables Asset For Build)", "No (Disabled Asset For Build)")) return;
            }
            
            BuildHandler.Clear();

            // Calls any dialogue boxes needed for.
            CallRelevantDialogueBoxes(report.summary.platform);
            
            // Stops if the update time is not any...
            if (settings.UpdateTime != BuildIncrementTime.AnyBuild) return;

            // Runs the updaters...
            CallUpdaterInterfaces(report.summary.platform);
        }


        /// <summary>
        /// Runs when a build is made...
        /// </summary>
        /// <param name="report">The build report provided automatically</param>
        public void OnPostprocessBuild(BuildReport report)
        {
            if (!ProjectBuildTypeValidator.CanUpdateVersions()) return;
            
            var settings = UtilEditor.BuildOptions;
            
            // Stops if the asset is disabled from updating the build number...
            if (settings.Status == AssetUsageType.Disabled) return;
            
            // Stops if the update time is not Only Successful Builds...
            if (settings.UpdateTime != BuildIncrementTime.OnlySuccessfulBuilds) return;
            
            // Runs the updaters...
            CallUpdaterInterfaces(report.summary.platform);
        }
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Interface Broadcasters
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        /// <summary>
        /// Calls to run all updater interface listeners...
        /// </summary>
        /// <param name="target">The build target</param>
        private static void CallUpdaterInterfaces(BuildTarget target)
        {
            var listeners = UtilEditor.GetAllInterfacesOfType<IBuildUpdater>();

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
        /// Calls the sync all the version numbers that use the semantic number...
        /// </summary>
        public static void CallSyncInterfaces()
        {
            // Stops the sync if the user is not using semantic updating...
            if (UtilEditor.BuildOptions.SemanticUsageType == AssetUsageType.Disabled)
            {
                BvLog.Warning("Update semantic is disabled! Sync will not run while this setting is disabled.");
                return;
            }
            
            var listeners = UtilEditor.GetAllInterfacesOfType<ISyncable>();

            if (listeners.Length <= 0)
            {
                BvLog.Normal("No sync implementations found, so nothing will happen!");
                return;
            }

            var lastSaved = UtilEditor.BuildOptions.LastSemanticVersionNumberSaved;

            foreach (var t in listeners)
                t.OnVersionSync(lastSaved.Length > 0 ? lastSaved : Application.version);
        }


        /// <summary>
        /// Calls all build updaters that use dialogue to update 
        /// </summary>
        private static void CallRelevantDialogueBoxes(BuildTarget target)
        {
            var listeners = UtilEditor.GetAllInterfacesOfType<IPreBuildDialogue>();
            
            if (listeners.Length <= 0)
            {
                BvLog.Normal("No dialogue implementations found, ignoring.");
                return;
            }

            foreach (var t in listeners)
            {
                t.OnPreBuildDialogue(target);
            }
        }
    }
}