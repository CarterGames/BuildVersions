using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Handles the settings window for the asset...
    /// </summary>
    public class BuildVersionsSettings : MonoBehaviour
    {
        private static Color defaultTextColour;             // The default color of gui.color...
        private static SerializedObject options;            // The options asset to edit...
        

        /// <summary>
        /// Creates the settings window to show info...
        /// </summary>
        /// <returns></returns>
        [SettingsProvider]
        public static SettingsProvider BuildVersionsSettingsProvider()
        {
            var provider = new SettingsProvider("Project/Carter Games/Build Versions", SettingsScope.Project)
            {
                guiHandler = (searchContext) =>
                {
                    defaultTextColour = GUI.color;
                    DrawLogoGroup();

                     options = new SerializedObject(BuildVersionsEditorUtil.BuildOptions);
                    
                    DrawInfo();
                    GUILayout.Space(2.5f);
                    DrawOptions();
                    GUILayout.Space(2.5f);
                    DrawAndroidBundle();
                    DrawButton();
                    
                    if (options != null)
                    {
                        options.ApplyModifiedProperties();
                        options.Update();
                    }
                },
                
                keywords = new HashSet<string>(new[] { "Carter Games", "External Assets", "Tools", "Build", "Build Management", "Version", "Version Control" })
            };

            return provider;
        }


        /// <summary>
        /// Draws the logo of the asset if possible...
        /// </summary>
        private static void DrawLogoGroup()
        {
            if (!BuildVersionsEditorUtil.HasFile("BuildVersionsEditorHeader")) return;
            
            var managerHeader = BuildVersionsEditorUtil.BannerLogo;
            
            GUILayout.Space(5f);
                    
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
                    
            if (managerHeader != null)
            {
                if (GUILayout.Button(managerHeader, GUIStyle.none, GUILayout.MaxHeight(110)))
                {
                    GUI.FocusControl(null);
                }
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
                    
            GUILayout.Space(5f);
        }


        /// <summary>
        /// Draws the info of the asset...
        /// </summary>
        private static void DrawInfo()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
                    
            GUI.color = BuildVersionsEditorUtil.TitleColour;
            EditorGUILayout.LabelField("Info", EditorStyles.boldLabel);
            GUI.color = defaultTextColour;
         
            EditorGUILayout.LabelField(new GUIContent("Version"), new GUIContent(AssetVersionData.VersionNumber));
            EditorGUILayout.LabelField(new GUIContent("Release Date"), new GUIContent(AssetVersionData.ReleaseDate));

            GUILayout.Space(2.5f);
            EditorGUILayout.EndVertical();
        }
        

        /// <summary>
        /// Draws the options section of the asset...
        /// </summary>
        private static void DrawOptions()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
                    
            GUI.color = BuildVersionsEditorUtil.TitleColour;
            EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);
            GUI.color = defaultTextColour;
                    
            DrawEnableAsset();
            DrawUpdateBuildTime();
            DrawSystematic();
                    
            GUILayout.Space(2.5f);
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the enable asset option...
        /// </summary>
        private static void DrawEnableAsset()
        {
            EditorGUILayout.PropertyField(options.FindProperty("assetStatus"), new GUIContent("Asset Status"));
        }


        /// <summary>
        /// Draws the update time option...
        /// </summary>
        private static void DrawUpdateBuildTime()
        {
            EditorGUI.BeginChangeCheck();

            var info = BuildVersionsEditorUtil.BuildInformation;
            var prop = options.FindProperty("buildUpdateTime");
            var oldSetting = prop.intValue;

            EditorGUILayout.PropertyField(prop, new GUIContent("Build Update Time"));
            
            if (EditorGUI.EndChangeCheck() && prop.intValue != oldSetting)
            {
                switch (oldSetting)
                {
                    case 0 when prop.intValue.Equals(1):
                        info.BuildNumber++;
                        break;
                    case 1 when prop.intValue.Equals(0):
                        info.BuildNumber--;
                        break;
                }
            }
        }
        

        /// <summary>
        /// Draws the systematic option...
        /// </summary>
        private static void DrawSystematic()
        {
            EditorGUILayout.PropertyField(options.FindProperty("updateSystematic"), new GUIContent("Use Systematic Version"));

            GUI.enabled = false;
            EditorGUILayout.PropertyField(options.FindProperty("lastSystematicNumber"), new GUIContent("Cached Version Number"));
            GUI.enabled = true;
        }


        /// <summary>
        /// Draws the android bundle code option...
        /// </summary>
        private static void DrawAndroidBundle()
        {
            if (!EditorUserBuildSettings.activeBuildTarget.Equals(BuildTarget.Android)) return;
            
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);

            GUI.color = BuildVersionsEditorUtil.TitleColour;
            EditorGUILayout.LabelField("Android Settings", EditorStyles.boldLabel);
            GUI.color = defaultTextColour;

            EditorGUILayout.PropertyField(options.FindProperty("androidUpdateBundleCode"),
                new GUIContent("Update Bundle Code"));
            
            GUILayout.Space(2.5f);
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Draws the help buttons...
        /// </summary>
        private static void DrawButton()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Asset Store", GUILayout.Height(30), GUILayout.MinWidth(100)))
                Application.OpenURL("https://assetstore.unity.com/publishers/43356");

            if (GUILayout.Button("Documentation", GUILayout.Height(30), GUILayout.MinWidth(100)))
                Application.OpenURL("https://carter.games/buildversions");

            if (GUILayout.Button("Change Log", GUILayout.Height(30), GUILayout.MinWidth(100)))
                Application.OpenURL("https://carter.games/buildversions/changelog");

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Email", GUILayout.Height(30), GUILayout.MinWidth(100)))
                Application.OpenURL("mailto:support@carter.games?subject=I need help with the Build Versions asset.");

            if (GUILayout.Button("Discord", GUILayout.Height(30), GUILayout.MinWidth(100)))
                Application.OpenURL("https://carter.games/discord");

            if (GUILayout.Button("Report Issues", GUILayout.Height(30), GUILayout.MinWidth(100)))
                Application.OpenURL("https://carter.games/report");

            EditorGUILayout.EndHorizontal();

            if (BuildVersionsEditorUtil.HasFile("CarterGamesBanner"))
            {
                var carterGamesBanner = BuildVersionsEditorUtil.CarterGamesBanner;

                GUI.contentColor = new Color(1, 1, 1, .75f);

                if (GUILayout.Button(carterGamesBanner, GUILayout.MaxHeight(40)))
                    Application.OpenURL("https://carter.games");

                GUI.contentColor = defaultTextColour;
            }
            else
            {
                if (GUILayout.Button("Carter Games", GUILayout.MaxHeight(40)))
                    Application.OpenURL("https://carter.games");
            }
        }
    }
}