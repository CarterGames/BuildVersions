using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Updates the systematic version number...
    /// </summary>
    public sealed class SystematicVersionUpdater : IBuildUpdater, IPreBuildDialogue
    {
        //
        //
        //  IPreBuildDialogue Implementation 
        //
        //
        
        
        public void OnPreBuildDialogue(BuildTarget target)
        {
            if (BuildVersionsEditorUtil.BuildOptions.updateSystematic != AssetUsageType.PromptMe)
            {
                BuildHandler.Register(new HandlerDialogueData(BuildVersionsEditorUtil.GetClassName<SystematicVersionUpdater>(), true));
                return;
            }

            var currentSystematic = GetVersionNumber(target, false);
            var updatesSystematic = GetVersionNumber(target, true);
            
            var choice = EditorUtility.DisplayDialog("Build Versions | Systematic Version Updater",
                $"Do you want to increment the systematic version number for this build?\n\nThe version will update from {currentSystematic} to {updatesSystematic}.\n\nYou can disable this prompt in the asset settings.",
                $"Yes (Set to {updatesSystematic})", $"No (Leave as {currentSystematic})");

            BuildHandler.Register(new HandlerDialogueData(BuildVersionsEditorUtil.GetClassName<SystematicVersionUpdater>(), choice));
        }
        
        
        //
        //
        //  IBuildUpdater Implementation 
        //
        //
        
        public int UpdateOrder => 0;

        public void OnBuildVersionIncremented(BuildTarget buildTarget)
        {
            if (BuildVersionsEditorUtil.BuildOptions.updateSystematic == AssetUsageType.Disabled) return;
            if (!BuildHandler.Get<SystematicVersionUpdater>().Choice) return;

            CallUpdateVersionNumber(buildTarget);
            BuildVersionsEditorUtil.BuildOptions.LastSystematicVersionNumberSaved = GetVersionNumber(buildTarget, false);
        }


        //
        //
        //  Helper Methods 
        //
        //
        
        
        /// <summary>
        /// Gets the version number per platform, with a +1 adjustment is needed...
        /// </summary>
        /// <param name="buildTarget">The build target</param>
        /// <param name="updatedNumber">Should the returned number be incremented?</param>
        /// <returns>The number in string form</returns>
        public static string GetVersionNumber(BuildTarget buildTarget, bool updatedNumber)
        {
            switch (buildTarget)
            {
                case BuildTarget.StandaloneOSX:
                    return updatedNumber 
                        ? UpdateVersionNumber(PlayerSettings.macOS.buildNumber) 
                        : PlayerSettings.macOS.buildNumber;
                case BuildTarget.StandaloneWindows:
                    return updatedNumber 
                        ? UpdateVersionNumber(PlayerSettings.bundleVersion) 
                        : PlayerSettings.bundleVersion;
                case BuildTarget.iOS:
                    return updatedNumber 
                        ? UpdateVersionNumber(PlayerSettings.iOS.buildNumber) 
                        : PlayerSettings.iOS.buildNumber;
                case BuildTarget.Android:
                    return updatedNumber 
                        ? UpdateVersionNumber(PlayerSettings.bundleVersion) 
                        : PlayerSettings.bundleVersion;
                case BuildTarget.StandaloneWindows64:
                    return updatedNumber 
                        ? UpdateVersionNumber(PlayerSettings.bundleVersion) 
                        : PlayerSettings.bundleVersion;
                case BuildTarget.WebGL:
                    return updatedNumber 
                        ? UpdateVersionNumber(PlayerSettings.bundleVersion) 
                        : PlayerSettings.bundleVersion;
                case BuildTarget.WSAPlayer:
                    return updatedNumber 
                        ? UpdateVersionNumber(PlayerSettings.WSA.packageVersion.ToString()) 
                        : PlayerSettings.WSA.packageVersion.ToString();
                case BuildTarget.StandaloneLinux64:
                    return updatedNumber 
                        ? UpdateVersionNumber(PlayerSettings.bundleVersion) 
                        : PlayerSettings.bundleVersion;
                case BuildTarget.PS4:
                    return updatedNumber 
                        ? UpdateVersionNumber(PlayerSettings.PS4.appVersion) 
                        : PlayerSettings.PS4.appVersion;
                case BuildTarget.XboxOne:
                    return updatedNumber 
                        ? UpdateVersionNumber(PlayerSettings.XboxOne.Version) 
                        : PlayerSettings.XboxOne.Version;
                case BuildTarget.tvOS:
                    return updatedNumber 
                        ? UpdateVersionNumber(PlayerSettings.tvOS.buildNumber) 
                        : PlayerSettings.tvOS.buildNumber;
                case BuildTarget.Switch:
                    return updatedNumber 
                        ? UpdateVersionNumber(PlayerSettings.Switch.releaseVersion) 
                        : PlayerSettings.Switch.releaseVersion;
                case BuildTarget.Lumin:
                    return updatedNumber 
                        ? UpdateVersionNumber(PlayerSettings.Lumin.versionName) 
                        : PlayerSettings.Lumin.versionName;
                case BuildTarget.NoTarget:
                    return updatedNumber 
                        ? UpdateVersionNumber(PlayerSettings.bundleVersion) 
                        : PlayerSettings.bundleVersion;
                default:
                    BvLog.Error("Unable to increment build number, platform not recognised!");
                    return string.Empty;
            }
        }
        
        
        /// <summary>
        /// Updates the version number by 1 for the number position entered...
        /// </summary>
        /// <param name="buildTarget">The build target</param>
        /// <param name="numberToUpdate">The number to update</param>
        public static void CallUpdateVersionNumber(BuildTarget buildTarget, int numberToUpdate = 2)
        {
            switch (buildTarget)
            {
                case BuildTarget.StandaloneOSX:
                    PlayerSettings.macOS.buildNumber = UpdateVersionNumber(PlayerSettings.macOS.buildNumber, numberToUpdate);
                    break;
                case BuildTarget.StandaloneWindows:
                    PlayerSettings.bundleVersion = UpdateVersionNumber(PlayerSettings.bundleVersion, numberToUpdate);
                    break;
                case BuildTarget.iOS:
                    PlayerSettings.iOS.buildNumber = UpdateVersionNumber(PlayerSettings.iOS.buildNumber, numberToUpdate);
                    break;
                case BuildTarget.Android:
                    PlayerSettings.bundleVersion = UpdateVersionNumber(PlayerSettings.bundleVersion, numberToUpdate);
                    break;
                case BuildTarget.StandaloneWindows64:
                    PlayerSettings.bundleVersion = UpdateVersionNumber(PlayerSettings.bundleVersion, numberToUpdate);
                    break;
                case BuildTarget.WebGL:
                    PlayerSettings.bundleVersion = UpdateVersionNumber(PlayerSettings.bundleVersion, numberToUpdate);
                    break;
                case BuildTarget.WSAPlayer:
                    PlayerSettings.WSA.packageVersion = new Version(UpdateVersionNumber(PlayerSettings.WSA.packageVersion.ToString(), numberToUpdate));
                    break;
                case BuildTarget.StandaloneLinux64:
                    PlayerSettings.bundleVersion = UpdateVersionNumber(PlayerSettings.bundleVersion, numberToUpdate);
                    break;
                case BuildTarget.PS4:
                    PlayerSettings.PS4.appVersion = UpdateVersionNumber(PlayerSettings.PS4.appVersion, numberToUpdate);
                    break;
                case BuildTarget.XboxOne:
                    PlayerSettings.XboxOne.Version = UpdateVersionNumber(PlayerSettings.XboxOne.Version, numberToUpdate);
                    break;
                case BuildTarget.tvOS:
                    PlayerSettings.tvOS.buildNumber = UpdateVersionNumber(PlayerSettings.tvOS.buildNumber, numberToUpdate);
                    break;
                case BuildTarget.Switch:
                    PlayerSettings.Switch.releaseVersion = UpdateVersionNumber(PlayerSettings.Switch.releaseVersion, numberToUpdate);
                    PlayerSettings.Switch.displayVersion = UpdateVersionNumber(PlayerSettings.Switch.displayVersion, numberToUpdate);
                    break;
                case BuildTarget.Lumin:
                    PlayerSettings.Lumin.versionName = UpdateVersionNumber(PlayerSettings.Lumin.versionName, numberToUpdate);
                    break;
                case BuildTarget.NoTarget:
                    PlayerSettings.bundleVersion = UpdateVersionNumber(PlayerSettings.bundleVersion, numberToUpdate);
                    break;
                default:
                    BvLog.Error("Unable to increment build number, platform not recognised!");
                    break;
            }
        }


        /// <summary>
        /// Updates the systematic version number...
        /// </summary>
        /// <param name="input">The number string to edit</param>
        /// <param name="valueToIncrement">The index number to edit (0 = major, 1 = minor, 2 = patch (default))</param>
        /// <returns>The modified string</returns>
        private static string UpdateVersionNumber(string input, int valueToIncrement = 2)
        {
            // to update major or minor version, manually set it in Edit>Project Settings>Player>Other Settings>Version
            var versionParts = input.Split('.');
            
            // If the build version format is incorrect... complain about it and don't increment the number...
            if (versionParts.Length != 3 || !int.TryParse(versionParts[valueToIncrement], out var buildNumber))
            {
                BvLog.Error("Unable to update player settings build version, please make sure you are using a major, minor, patch (x.x.x) style format in your player settings");
                return input;
            }
            
            // Increments the build number by 1 on the build/patch number (major and minor can be edited with the tools menu)
            versionParts[valueToIncrement] = (buildNumber + 1).ToString();

            // Resets the values to 0 where applicable....
            switch (valueToIncrement)
            {
                case 0:
                    versionParts[1] = 0.ToString();
                    versionParts[2] = 0.ToString();
                    break;
                case 1:
                    versionParts[2] = 0.ToString();
                    break;
            }

            // returns the result...
            return $"{versionParts[0]}.{versionParts[1]}.{versionParts[2]}";
        }
    }
}