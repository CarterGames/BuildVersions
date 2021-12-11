using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 
 *  Build Versions
 *							  
 *	Build Versions Manager
 *      The main script of the build versions asset. 
 *
 *  Warning:
 *	    Please refrain from editing this script as it will cause issues to the assets...
 *			
 *  Written by:
 *      Jonathan Carter
 *
 *  Published By:
 *      Carter Games
 *      E: hello@carter.games
 *      W: https://www.carter.games
 *		
 *  Version: 1.0.0
 *	Last Updated: 09/10/2021 (d/m/y)							
 * 
 */

namespace CarterGames.Assets.BuildVersions.Editor
{
    public class BuildVersionsManager : UnityEditor.Editor, IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        private static readonly string OptionsTypeFilter = "t:buildversionoptions";
        private static readonly string BuildInfoObjectFilter = "t:buildinformation";
        
        
        [MenuItem("Tools/Build Versions | CG/Type/Set To Prototype", priority = 1)]
        public static void SetToPrototype() => SetBuildType("Prototype");
        
        [MenuItem("Tools/Build Versions | CG/Type/Set To Development", priority = 1)]
        public static void SetToDevelopment() => SetBuildType("Development");
        
        [MenuItem("Tools/Build Versions | CG/Type/Alpha/Set To Pre-Alpha", priority = 2)]
        public static void SetToPreAlpha() => SetBuildType("Pre-Alpha");
        
        [MenuItem("Tools/Build Versions | CG/Type/Alpha/Set To Closed Alpha", priority = 2)]
        public static void SetToClosedAlpha() => SetBuildType("Closed Alpha");
        
        [MenuItem("Tools/Build Versions | CG/Type/Alpha/Set To Alpha", priority = 2)]
        public static void SetToAlpha() => SetBuildType("Alpha");
        
        [MenuItem("Tools/Build Versions | CG/Type/Alpha/Set To Open Alpha", priority = 2)]
        public static void SetToOpenAlpha() => SetBuildType("Open Alpha");
        
        [MenuItem("Tools/Build Versions | CG/Type/Beta/Set To Closed Beta", priority = 3)]
        public static void SetToClosedBeta() => SetBuildType("Closed Beta");

        [MenuItem("Tools/Build Versions | CG/Type/Beta/Set To Beta", priority = 3)]
        public static void SetToBeta() => SetBuildType("Beta");
        
        [MenuItem("Tools/Build Versions | CG/Type/Beta/Set To Open Beta", priority = 3)]
        public static void SetToOpenBeta() => SetBuildType("Open Beta");
        
        [MenuItem("Tools/Build Versions | CG/Type/Release/Set To Release Candidate", priority = 4)]
        public static void SetToReleaseCandidate() => SetBuildType("Release Candidate");
        
        [MenuItem("Tools/Build Versions | CG/Type/Release/Set To Release", priority = 4)]
        public static void SetToRelease() => SetBuildType("Release");
        
        [MenuItem("Tools/Build Versions | CG/Date/Set Date To Today")]
        public static void SetBuildDate() => SetDate();
        
        [MenuItem("Tools/Build Versions | CG/Build Number/Increment Build Number")]
        public static void IncrementBuild() => SetBuildNumber();
        
        [MenuItem("Tools/Build Versions | CG/Build Number/Reset Build Number")]
        public static void ResetBuild() => SetBuildNumber(1);

        [MenuItem("Tools/Build Versions | CG/Version Number/Increment Major")]
        public static void IncrementMajor() => CallUpdateVersionNumber(EditorUserBuildSettings.activeBuildTarget, 0);
        
        [MenuItem("Tools/Build Versions | CG/Version Number/Increment Minor")]
        public static void IncrementMinor() => CallUpdateVersionNumber(EditorUserBuildSettings.activeBuildTarget, 1);
        
        [MenuItem("Tools/Build Versions | CG/Version Number/Increment Build")]
        public static void IncrementVersionBuild() => CallUpdateVersionNumber(EditorUserBuildSettings.activeBuildTarget);


        private static void SetBuildType(string value)
        {
            var _info = GetBuildInformation();
            if (_info == null)
            {
                Debug.LogWarning("Build Incrementer: Unable to update data as it was not found in the project!");
                return;
            }
            _info.BuildType = value;
            EditorUtility.SetDirty(_info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        private static void SetDate()
        {
            var _info = GetBuildInformation();
            if (_info == null)
            {
                Debug.LogError("Build Versions | CG: Unable to update data as it was not found in the project!");
                return;
            }
            _info.SetBuildDate();
            EditorUtility.SetDirty(_info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        
        private static void SetBuildNumber(int value = -1)
        {
            var _info = GetBuildInformation();
            if (_info == null)
            {
                Debug.LogError("Build Versions | CG: Unable to update data as it was not found in the project!");
                return;
            }

            if (value.Equals(-1))
                _info.IncrementBuildNumber();
            else
                _info.BuildNumber = value;
            
            EditorUtility.SetDirty(_info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        
        public int callbackOrder => 1;

        
        public void OnPreprocessBuild(BuildReport report)
        {
            CheckOrSpawn();
            UpdateBuildDate();
            var _settings = GetBuildVersionSettings();
            if (!_settings.assetActive) return;
            if (_settings.buildUpdateTime != BuildIncrementTime.AnyBuild) return;
            UpdateBuildNumber(_settings, report.summary.platform);
        }
        
        
        public void OnPostprocessBuild(BuildReport report)
        {
            CheckOrSpawn();
            var _settings = GetBuildVersionSettings();
            if (!_settings.assetActive) return;
            if (_settings.buildUpdateTime != BuildIncrementTime.OnlySuccessfulBuilds) return;
            UpdateBuildNumber(_settings, report.summary.platform);
        }


        private void UpdateBuildNumber(BuildVersionOptions options, BuildTarget report)
        {
            if (options.updateSystematic)
                CallUpdateVersionNumber(report);
            
            UpdateBuildScriptableObject();
        }


        private static void CallUpdateVersionNumber(BuildTarget t, int valueToEdit = 2)
        {
            switch (t)
            {
                case BuildTarget.StandaloneOSX:
                    PlayerSettings.macOS.buildNumber = UpdateVersionNumber(PlayerSettings.macOS.buildNumber, valueToEdit);
                    break;
                case BuildTarget.StandaloneWindows:
                    PlayerSettings.bundleVersion = UpdateVersionNumber(PlayerSettings.bundleVersion, valueToEdit);
                    break;
                case BuildTarget.iOS:
                    PlayerSettings.iOS.buildNumber = UpdateVersionNumber(PlayerSettings.iOS.buildNumber, valueToEdit);
                    break;
                case BuildTarget.Android:
                    PlayerSettings.bundleVersion = UpdateVersionNumber(PlayerSettings.bundleVersion, valueToEdit);
                    if (GetBuildVersionSettings().androidUpdateBundleCode)
                        PlayerSettings.Android.bundleVersionCode++;
                    break;
                case BuildTarget.StandaloneWindows64:
                    PlayerSettings.bundleVersion = UpdateVersionNumber(PlayerSettings.bundleVersion, valueToEdit);
                    break;
                case BuildTarget.WebGL:
                    PlayerSettings.bundleVersion = UpdateVersionNumber(PlayerSettings.bundleVersion, valueToEdit);
                    break;
                case BuildTarget.WSAPlayer:
                    PlayerSettings.WSA.packageVersion = new Version(UpdateVersionNumber(PlayerSettings.WSA.packageVersion.ToString(), valueToEdit));
                    break;
                case BuildTarget.StandaloneLinux64:
                    PlayerSettings.bundleVersion = UpdateVersionNumber(PlayerSettings.bundleVersion, valueToEdit);
                    break;
                case BuildTarget.PS4:
                    PlayerSettings.PS4.appVersion = UpdateVersionNumber(PlayerSettings.PS4.appVersion, valueToEdit);
                    break;
                case BuildTarget.XboxOne:
                    PlayerSettings.XboxOne.Version = UpdateVersionNumber(PlayerSettings.XboxOne.Version, valueToEdit);
                    break;
                case BuildTarget.tvOS:
                    PlayerSettings.tvOS.buildNumber = UpdateVersionNumber(PlayerSettings.tvOS.buildNumber, valueToEdit);
                    break;
                case BuildTarget.Switch:
                    PlayerSettings.Switch.releaseVersion = UpdateVersionNumber(PlayerSettings.Switch.releaseVersion, valueToEdit);
                    PlayerSettings.Switch.displayVersion = UpdateVersionNumber(PlayerSettings.Switch.displayVersion, valueToEdit);
                    break;
                case BuildTarget.Lumin:
                    PlayerSettings.Lumin.versionName = UpdateVersionNumber(PlayerSettings.Lumin.versionName, valueToEdit);
                    break;
                case BuildTarget.NoTarget:
                    PlayerSettings.bundleVersion = UpdateVersionNumber(PlayerSettings.bundleVersion, valueToEdit);
                    break;
                default:
                    Debug.Log("Build Versions | CG: Unable to increment build number, platform not recognised!");
                    break;
            }
        }



        private static string UpdateVersionNumber(string input, int valueToIncrement = 2)
        {
            // to update major or minor version, manually set it in Edit>Project Settings>Player>Other Settings>Version
            var versionParts = input.Split('.');
            
            // If the build version format is incorrect... complain about it and don't increment the number...
            if (versionParts.Length != 3 || !int.TryParse(versionParts[valueToIncrement], out var buildNumber))
            {
                Debug.LogError(
                    "Build Versions | CG: Unable to update player settings build version, please make sure you are using a major, minor, patch (x.x.x) style format in your player settings");
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


        private void UpdateBuildScriptableObject()
        {
            var _info = GetBuildInformation();
            if (_info == null)
            {
                Debug.LogError("Build Versions | CG: Unable to update data as it was not found in the project!");
                return;
            }
            _info.IncrementBuildNumber();
            _info.SetBuildDate();
            EditorUtility.SetDirty(_info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        private void UpdateBuildDate()
        {
            var _info = GetBuildInformation();
            if (_info == null)
            {
                Debug.LogError("Build Versions | CG: Unable to update data as it was not found in the project!");
                return;
            }

            if (_info.BuildDate == DateTime.Now.Date.ToString("d")) return;
            _info.SetBuildDate();
            EditorUtility.SetDirty(_info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        

        public static BuildInformation GetBuildInformation()
        {
            var _asset = AssetDatabase.FindAssets(BuildInfoObjectFilter, null);

            if (_asset.Length > 0)
            {
                var _path = AssetDatabase.GUIDToAssetPath(_asset[0]);
                var _loadedSettings =
                    (BuildInformation)AssetDatabase.LoadAssetAtPath(_path, typeof(BuildInformation));

                return _loadedSettings;
            }
            else
            {
                AssetDatabase.CreateAsset(CreateInstance(typeof(BuildInformation)), $"Assets/Build Information.asset");
                AssetDatabase.Refresh();
                
                _asset = AssetDatabase.FindAssets(BuildInfoObjectFilter, null);

                if (_asset.Length > 0)
                {
                    var _path = AssetDatabase.GUIDToAssetPath(_asset[0]);
                    var _loadedSettings =
                        (BuildInformation)AssetDatabase.LoadAssetAtPath(_path, typeof(BuildInformation));

                    return _loadedSettings;
                }
                
                Debug.LogError("Build Versions | CG: No Build Information Found In Project!");
                return null;
            }
        }


        public static BuildVersionOptions GetBuildVersionSettings(bool showLog = true)
        {
            var _asset = AssetDatabase.FindAssets(OptionsTypeFilter, null);

            if (_asset.Length > 0)
            {
                var _path = AssetDatabase.GUIDToAssetPath(_asset[0]);
                var _loadedSettings =
                    (BuildVersionOptions)AssetDatabase.LoadAssetAtPath(_path, typeof(BuildVersionOptions));

                return _loadedSettings;
            }
            
            if (showLog)
                Debug.LogError("Build Versions | CG: No Build Version Options Found In Project!");
            
            return null;
        }


        public static void CheckOrSpawn()
        {
            var _options = AssetDatabase.FindAssets(OptionsTypeFilter, null);
            var _info = AssetDatabase.FindAssets(BuildInfoObjectFilter, null);

            if (_options.Length <= 0)
                CreateOptions();

            if (_info.Length <= 0)
                CreateInformation();
        }

        private static void CreateOptions()
        {
            AssetDatabase.CreateAsset(CreateInstance(typeof(BuildVersionOptions)), $"Assets/Build Version Options.asset");
            AssetDatabase.Refresh();
        }
        
        private static void CreateInformation()
        {
            AssetDatabase.CreateAsset(CreateInstance(typeof(BuildInformation)), $"Assets/Build Information.asset");
            AssetDatabase.Refresh();
        }
    }
}