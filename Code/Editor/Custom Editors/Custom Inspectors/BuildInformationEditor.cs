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
    /// An editor override for the build information asset...
    /// </summary>
    [CustomEditor(typeof(BuildInformation))]
    public class BuildInformationEditor : UnityEditor.Editor
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Fields
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        private SerializedProperty buildType;
        private SerializedProperty buildDate;
        private SerializedProperty buildNumber;
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Unity Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

        private void OnEnable()
        {
            buildType = serializedObject.FindProperty("buildType");
            buildDate = serializedObject.FindProperty("buildDate");
            buildNumber = serializedObject.FindProperty("buildNumber");
        }

        
        public override void OnInspectorGUI()
        {
            ShowLogo();

            GUILayout.Space(2.5f);
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Information", EditorStyles.boldLabel);
            ShowValues();
            
            EditorGUILayout.EndVertical();
            
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        /// <summary>
        /// Draws the logo for the asset...
        /// </summary>
        private static void ShowLogo()
        {
            // Shows either the Logo if found, if not nothing will show here...
            if (UtilEditor.Logo != null)
            {
                GUILayout.Space(5f);

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                // Shows either the Logo if found, if not nothing will show here...
                if (GUILayout.Button(UtilEditor.Logo, GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
                    GUI.FocusControl(null);
                
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(5f);
        }


        /// <summary>
        /// Draws the property fields for the build info asset...
        /// </summary>
        private void ShowValues()
        {
            EditorGUILayout.PropertyField(buildType);
            
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(buildDate);
            EditorGUI.indentLevel--;
            
            EditorGUILayout.PropertyField(buildNumber);
        }
    }
}
