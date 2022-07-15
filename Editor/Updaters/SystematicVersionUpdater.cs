using System;
using UnityEditor;

namespace CarterGames.Assets.BuildVersions.Editor
{
    public class SystematicVersionUpdater : IBuildVersionUpdate
    {
        public void OnBuildVersionIncremented(BuildTarget buildTarget)
        {
            if (BuildVersionsEditorUtil.BuildOptions.updateSystematic == AssetUsageType.Disabled) return;

            if (BuildVersionsEditorUtil.BuildOptions.updateSystematic == AssetUsageType.PromptMe)
            {
                if (!EditorUtility.DisplayDialog("Build Versions",
                        "Do you want to increment the systematic version number for this build?\nYou can disable this prompt in the asset settings.",
                        "Yes", "No")) return;
            }
            
            CallUpdateVersionNumber(buildTarget);
        }


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
        

        public static string UpdateVersionNumber(string input, int valueToIncrement = 2)
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