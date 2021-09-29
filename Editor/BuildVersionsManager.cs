using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions.Editor
{
    public class BuildVersionsManager : UnityEditor.Editor, IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        [MenuItem("Tools/Build Versions | CG/Type/Set To Prototype", priority = 1)]
        public static void SetToPrePrototype() => SetBuildType("Prototype");
        
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
        
        [MenuItem("Tools/Build Versions | CG/Type/Release/Set To Release", priority = 4)]
        public static void SetToRelease() => SetBuildType("Release");
        
        [MenuItem("Tools/Build Versions | CG/Type/Release/Set To Release Candidate", priority = 4)]
        public static void SetToReleaseCandidate() => SetBuildType("Release Candidate");

        [MenuItem("Tools/Build Versions | CG/Date/Set Date To Today")]
        public static void SetBuildDate() => SetDate();
        
        [MenuItem("Tools/Build Versions | CG/Build Number/Increment Build Number")]
        public static void IncrementBuild() => SetBuildNumber();
        
        [MenuItem("Tools/Build Versions | CG/Build Number/Reset Build Number")]
        public static void ResetBuild() => SetBuildNumber(0);
        
        

        private static void SetBuildType(string value)
        {
            var _info = GetBuildInformation();
            if (_info == null)
            {
                Debug.LogWarning("Build Incrementer: Unable to update data as it was not found in the project!");
                return;
            }
            _info.buildNameType = value;
            EditorUtility.SetDirty(_info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        private static void SetDate()
        {
            var _info = GetBuildInformation();
            if (_info == null)
            {
                Debug.LogError("Build Incrementer: Unable to update data as it was not found in the project!");
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
                Debug.LogError("Build Incrementer: Unable to update data as it was not found in the project!");
                return;
            }

            if (value.Equals(-1))
                _info.IncrementBuildNumber();
            else
                _info.buildNumber = value;
            
            EditorUtility.SetDirty(_info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        
        public int callbackOrder => 1;

        
        public void OnPreprocessBuild(BuildReport report)
        {
            var _settings = GetBuildVersionSettings();
            if (_settings.buildUpdateTime != BuildIncrementTime.AnyBuild) return;
            UpdateBuildNumber(_settings);
        }
        
        
        public void OnPostprocessBuild(BuildReport report)
        {
            var _settings = GetBuildVersionSettings();
            if (_settings.buildUpdateTime != BuildIncrementTime.OnlySuccessfulBuilds) return;
            UpdateBuildNumber(_settings);
        }


        private void UpdateBuildNumber(BuildVersionSettings settings)
        {
            if (settings.updatePlayerSettingsVersion)
                UpdatePlayerSettings();
            else
                UpdateBuildScriptableObject();
        }


        private void UpdatePlayerSettings()
        {
            
        }


        private void UpdateBuildScriptableObject()
        {
            var _info = GetBuildInformation();
            if (_info == null)
            {
                Debug.LogError("Build Incrementer: Unable to update data as it was not found in the project!");
                return;
            }
            _info.IncrementBuildNumber();
            _info.SetBuildDate();
            EditorUtility.SetDirty(_info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        

        private static BuildInformation GetBuildInformation()
        {
            var _asset = AssetDatabase.FindAssets("t:buildinformation", null);

            if (_asset.Length > 0)
            {
                var _path = AssetDatabase.GUIDToAssetPath(_asset[0]);
                var _loadedSettings =
                    (BuildInformation)AssetDatabase.LoadAssetAtPath(_path, typeof(BuildInformation));

                return _loadedSettings;
            }
            
            Debug.LogError("Build Incrementer: No Build Information Found In Project!");
            return null;
        }


        private static BuildVersionSettings GetBuildVersionSettings()
        {
            var _asset = AssetDatabase.FindAssets("t:buildversionsettings", null);

            if (_asset.Length > 0)
            {
                var _path = AssetDatabase.GUIDToAssetPath(_asset[0]);
                var _loadedSettings =
                    (BuildVersionSettings)AssetDatabase.LoadAssetAtPath(_path, typeof(BuildVersionSettings));

                return _loadedSettings;
            }
            
            Debug.LogError("Build Incrementer: No Build Version Settings Found In Project!");
            return null;
        }
    }
}