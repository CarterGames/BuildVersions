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
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Provider
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

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
                    DrawLogoGroup();

                    DrawInfo();
                    GUILayout.Space(2.5f);
                    DrawOptions();
                    GUILayout.Space(2.5f);
                    DrawButton();
                    
                    if (UtilEditor.BuildOptionsObject != null)
                    {
                        UtilEditor.BuildOptionsObject.ApplyModifiedProperties();
                        UtilEditor.BuildOptionsObject.Update();
                    }
                },
                
                keywords = new HashSet<string>(new[] { "Carter Games", "External Assets", "Tools", "Build", "Build Management", "Version", "Version Control" })
            };

            return provider;
        }

        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Drawer Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        /// <summary>
        /// Draws the logo of the asset if possible...
        /// </summary>
        private static void DrawLogoGroup()
        {
            if (UtilEditor.BannerLogo == null) return;

            GUILayout.Space(5f);
                    
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(UtilEditor.BannerLogo, GUIStyle.none, GUILayout.MaxHeight(110)))
            {
                GUI.FocusControl(null);
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
            
            EditorGUILayout.LabelField("Info", EditorStyles.boldLabel);
            UtilEditor.DrawHorizontalGUILine();

            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField(EditorMetaData.AssetVersionInfo.Number, new GUIContent(AssetVersionData.VersionNumber));
            VersionEditorGUI.DrawCheckForUpdatesButton();
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.LabelField(EditorMetaData.AssetVersionInfo.Date, new GUIContent(AssetVersionData.ReleaseDate));

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
            
            EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);
            UtilEditor.DrawHorizontalGUILine();
            
            GUILayout.Space(1.5f);
            DrawEditorOptions();
            GUILayout.Space(2.5f);
            DrawAndroidBundle();
            GUILayout.Space(1.5f);
            
            GUILayout.Space(2.5f);
            EditorGUILayout.EndVertical();
        }


        private static void DrawEditorOptions()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            PerUserSettings.ShowEditorSettingsDropdown =
                EditorGUILayout.Foldout(PerUserSettings.ShowEditorSettingsDropdown, "Editor");

            if (!PerUserSettings.ShowEditorSettingsDropdown)
            {
                GUILayout.Space(1.5f);
                EditorGUILayout.EndVertical();
                return;
            }

            EditorGUILayout.BeginVertical("Box");
            
            PerUserSettings.VersionValidationAutoCheckOnLoad = EditorGUILayout.Toggle(EditorMetaData.PerUser.AutoVersionCheck, PerUserSettings.VersionValidationAutoCheckOnLoad);
            
            EditorGUILayout.PropertyField(UtilEditor.BuildOptionsObject.Fp("assetStatus"), EditorMetaData.Settings.AssetStatus);
            EditorGUILayout.PropertyField(UtilEditor.BuildOptionsObject.Fp("runInDevBuilds"), EditorMetaData.Settings.RunInDev);

            DrawUpdateBuildTime();
            DrawSemantic();

            PerUserSettingsRuntime.ShowDebugLogs = EditorGUILayout.Toggle(EditorMetaData.PerUser.RuntimeDebugLogs, PerUserSettingsRuntime.ShowDebugLogs);

            
            EditorGUILayout.EndVertical();
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
        


        /// <summary>
        /// Draws the update time option...
        /// </summary>
        private static void DrawUpdateBuildTime()
        {
            EditorGUI.BeginChangeCheck();
            
            var prop = UtilEditor.BuildOptionsObject.Fp("buildUpdateTime");
            var oldSetting = prop.intValue;

            EditorGUILayout.PropertyField(prop, EditorMetaData.Settings.BuildUpdateTime);
            
            if (EditorGUI.EndChangeCheck() && prop.intValue != oldSetting)
            {
                switch (oldSetting)
                {
                    case 0 when prop.intValue.Equals(1):
                        UtilEditor.BuildInformation.BuildNumber++;
                        break;
                    case 1 when prop.intValue.Equals(0):
                        UtilEditor.BuildInformation.BuildNumber--;
                        break;
                }
            }
        }
        

        /// <summary>
        /// Draws the semantic option...
        /// </summary>
        private static void DrawSemantic()
        {
            EditorGUILayout.PropertyField(UtilEditor.BuildOptionsObject.Fp("updateSemantic"), EditorMetaData.Settings.SemanticUpdate);

            GUI.enabled = false;
            EditorGUILayout.PropertyField(UtilEditor.BuildOptionsObject.Fp("lastSemanticNumber"), EditorMetaData.Settings.LastSemantic);
            GUI.enabled = true; 
        }


        /// <summary>
        /// Draws the android bundle code option...
        /// </summary>
        private static void DrawAndroidBundle()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            PerUserSettings.ShowAndroidSettingsDropdown =
                EditorGUILayout.Foldout(PerUserSettings.ShowAndroidSettingsDropdown, "Android");

            if (!PerUserSettings.ShowAndroidSettingsDropdown)
            {
                GUILayout.Space(1.5f);
                EditorGUILayout.EndVertical();
                return;
            }
            
            EditorGUI.BeginDisabledGroup(!EditorUserBuildSettings.activeBuildTarget.Equals(BuildTarget.Android));
            
            EditorGUILayout.BeginVertical("Box");
            GUILayout.Space(1.5f);
            
            if (!EditorUserBuildSettings.activeBuildTarget.Equals(BuildTarget.Android))
            {
                EditorGUILayout.HelpBox("Not targeting android build platform", MessageType.Warning);
                GUILayout.Space(1.5f);
            }
            
            EditorGUILayout.PropertyField(UtilEditor.BuildOptionsObject.Fp("androidUpdateBundleCode"),
                EditorMetaData.Settings.BundleCode);
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            
            
            EditorGUI.EndDisabledGroup();
        }


        /// <summary>
        /// Draws the help buttons...
        /// </summary>
        private static void DrawButton()
        {
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Buy Me A Coffee", GUILayout.Height(30), GUILayout.MinWidth(100)))
            {
                Application.OpenURL("https://www.buymeacoffee.com/cartergames");
            }
            
            if (GUILayout.Button("GitHub", GUILayout.Height(30), GUILayout.MinWidth(100)))
            {
                Application.OpenURL("https://github.com/CarterGames/BuildVersions");
            }

            if (GUILayout.Button("Documentation", GUILayout.Height(30), GUILayout.MinWidth(100)))
            {
                Application.OpenURL("https://carter.games/buildversions");
            }
            
            if (GUILayout.Button("Support", GUILayout.Height(30), GUILayout.MinWidth(100)))
            {
                Application.OpenURL("https://carter.games/contact");
            }

            EditorGUILayout.EndHorizontal();

            if (UtilEditor.CarterGamesBanner != null)
            {
                var carterGamesBanner = UtilEditor.CarterGamesBanner;

                GUI.contentColor = new Color(1, 1, 1, .75f);

                if (GUILayout.Button(carterGamesBanner, GUILayout.MaxHeight(40)))
                {
                    Application.OpenURL("https://carter.games");
                }

                GUI.contentColor = Color.white;
            }
            else
            {
                if (GUILayout.Button("Carter Games", GUILayout.MaxHeight(40)))
                {
                    Application.OpenURL("https://carter.games");
                }
            }
        }
    }
}