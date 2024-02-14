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

using UnityEditor;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// An editor override for the build options asset...
    /// </summary>
    [CustomEditor(typeof(BuildVersionOptions))]
    public class BuildVersionsOptionsEditor : UnityEditor.Editor
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Unity Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

        public override void OnInspectorGUI()
        {
            GUILayout.Space(5f);
            
            // Draws the script field.
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            UtilEditor.DrawSoScriptSection(target);
            
            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space(3.5f);
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            GUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);
            UtilEditor.DrawHorizontalGUILine();

            GUILayout.Space(2.5f);
            ShowValues();
            GUILayout.Space(5f);
            
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space(3.5f);
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            DrawLastVersionNumber();
            
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space(3.5f);
            
            DrawAndroidBundleCodeSection();
            
            EditorGUILayout.Space(1.5f);
            
            EditButton();
            
            EditorGUILayout.Space(1.5f);

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        /// <summary>
        /// Shows the values for the asset options...
        /// </summary>
        private void ShowValues()
        {
            EditorGUI.BeginDisabledGroup(true);
            
            EditorGUILayout.Toggle(EditorMetaData.PerUser.AutoVersionCheck, PerUserSettings.VersionValidationAutoCheckOnLoad);
            EditorGUILayout.PropertyField(serializedObject.Fp("assetStatus"), EditorMetaData.Settings.AssetStatus);
            EditorGUILayout.PropertyField(UtilEditor.BuildOptionsObject.Fp("runInDevBuilds"), EditorMetaData.Settings.RunInDev);

            EditorGUI.BeginChangeCheck();
            
            var oldSetting = serializedObject.Fp("buildUpdateTime").intValue;
            EditorGUILayout.PropertyField(serializedObject.Fp("buildUpdateTime"), EditorMetaData.Settings.BuildUpdateTime);
            
            if (EditorGUI.EndChangeCheck() && serializedObject.Fp("buildUpdateTime").intValue != oldSetting)
            {
                switch (oldSetting)
                {
                    case 0 when serializedObject.Fp("buildUpdateTime").intValue.Equals(1):
                        UtilEditor.BuildInformation.BuildNumber++;
                        break;
                    case 1 when serializedObject.Fp("buildUpdateTime").intValue.Equals(0):
                        UtilEditor.BuildInformation.BuildNumber--;
                        break;
                }
            }
            
            EditorGUILayout.PropertyField(serializedObject.Fp("updateSemantic"), EditorMetaData.Settings.SemanticUpdate);
 
            EditorGUILayout.Toggle(EditorMetaData.PerUser.RuntimeDebugLogs, PerUserSettingsRuntime.ShowDebugLogs);
            
            EditorGUI.EndDisabledGroup();
        }
        
        
        /// <summary>
        /// Draws the last version number section...
        /// </summary>
        private void DrawLastVersionNumber()
        {
            EditorGUILayout.LabelField("Cached Values", EditorStyles.boldLabel);
            UtilEditor.DrawHorizontalGUILine();

            GUI.enabled = false;

            if (serializedObject.Fp("lastSemanticNumber").stringValue.Length <= 0)
            {
                serializedObject.Fp("lastSemanticNumber").stringValue = PlayerSettings.bundleVersion;
            }

            EditorGUILayout.PropertyField(serializedObject.Fp("lastSemanticNumber"), EditorMetaData.Settings.LastSemantic);

            GUI.enabled = true;
            GUILayout.Space(2.5f);
        }
        
        
        /// <summary>
        /// Draws the android bundle code section...
        /// </summary>
        private void DrawAndroidBundleCodeSection()
        {
            EditorGUI.BeginDisabledGroup(!EditorUserBuildSettings.activeBuildTarget.Equals(BuildTarget.Android));

            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Android", EditorStyles.boldLabel);
            UtilEditor.DrawHorizontalGUILine();

            GUI.enabled = false;

            EditorGUILayout.PropertyField(serializedObject.Fp("androidUpdateBundleCode"), EditorMetaData.Settings.BundleCode);

            GUI.enabled = true;

            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space(3.5f);
            
            EditorGUI.EndDisabledGroup();
        }
        
        
        /// <summary>
        /// Draws a button to direct the user to the edit settings section of the asset...
        /// </summary>
        private void EditButton()
        {
            if (GUILayout.Button("Edit Settings", GUILayout.Height(27.5f)))
            {
                SettingsService.OpenProjectSettings("Project/Carter Games/Build Versions");
            }
        }
    }
}