/*
 * Copyright (c) 2018-Present Carter Games
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
        |   Fields
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        private Color defaultTextColor;
        private Color defaultBackgroundColor;
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Unity Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

        public override void OnInspectorGUI()
        {
            defaultTextColor = GUI.color;
            defaultBackgroundColor = GUI.backgroundColor;
            
            ShowLogo();

            GUILayout.Space(4.5f);
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            GUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);

            GUILayout.Space(2.5f);
            ShowValues();
            GUILayout.Space(5f);
            
            DrawLastVersionNumber();
            DrawAndroidBundleCodeSection();
            
            GUILayout.Space(5f);
            
            EditButton();
            
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

        /// <summary>
        /// Shows the logo for the asset if possible...
        /// </summary>
        private static void ShowLogo()
        {
            if (UtilEditor.Logo == null) return;
            
            GUILayout.Space(5f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // Shows either the Carter Games Logo or an alternative for if the icon is deleted/not included when you import the package
            if (GUILayout.Button(UtilEditor.Logo, GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
                GUI.FocusControl(null);
                
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        
        /// <summary>
        /// Shows the values for the asset options...
        /// </summary>
        private void ShowValues()
        {
            GUI.enabled = false;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("assetStatus"));

            EditorGUI.BeginChangeCheck();
            
            var oldSetting = serializedObject.FindProperty("buildUpdateTime").intValue;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("buildUpdateTime"));
            
            if (EditorGUI.EndChangeCheck() && serializedObject.FindProperty("buildUpdateTime").intValue != oldSetting)
            {
                switch (oldSetting)
                {
                    case 0 when serializedObject.FindProperty("buildUpdateTime").intValue.Equals(1):
                        UtilEditor.BuildInformation.BuildNumber++;
                        break;
                    case 1 when serializedObject.FindProperty("buildUpdateTime").intValue.Equals(0):
                        UtilEditor.BuildInformation.BuildNumber--;
                        break;
                }
            }
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("updateSemantic"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("showLogs"));
            GUI.enabled = true;
        }
        
        
        /// <summary>
        /// Draws the last version number section...
        /// </summary>
        private void DrawLastVersionNumber()
        {
            EditorGUILayout.LabelField("Cached Values", EditorStyles.boldLabel);

            GUI.enabled = false;

            if (serializedObject.FindProperty("lastSemanticNumber").stringValue.Length <= 0)
                serializedObject.FindProperty("lastSemanticNumber").stringValue = PlayerSettings.bundleVersion;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("lastSemanticNumber"));

            GUI.enabled = true;
            GUILayout.Space(2.5f);
        }
        
        
        /// <summary>
        /// Draws the android bundle code section...
        /// </summary>
        private void DrawAndroidBundleCodeSection()
        {
#if UNITY_ANDROID
            EditorGUILayout.LabelField("Android", EditorStyles.boldLabel);

            GUI.enabled = false;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("androidUpdateBundleCode"), new GUIContent("Bundle Code Update"));

            GUI.enabled = true;


            GUILayout.Space(2.5f);
#endif
        }
        
        
        /// <summary>
        /// Draws a button to direct the user to the edit settings section of the asset...
        /// </summary>
        private void EditButton()
        {
            if (GUILayout.Button("Edit Settings", GUILayout.Height(22.5f)))
            {
                SettingsService.OpenProjectSettings("Project/Carter Games/Build Versions");
            }
        }
    }
}