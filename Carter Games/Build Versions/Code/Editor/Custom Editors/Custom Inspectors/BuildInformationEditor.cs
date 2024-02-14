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
            buildType = serializedObject.Fp("buildType");
            buildDate = serializedObject.Fp("buildDate");
            buildNumber = serializedObject.Fp("buildNumber");
        }

        
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
                 
            GUILayout.Space(4.5f);
            
            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Information", EditorStyles.boldLabel);
            UtilEditor.DrawHorizontalGUILine();
            ShowValues();
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
            
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        /// <summary>
        /// Draws the property fields for the build info asset...
        /// </summary>
        private void ShowValues()
        {
            EditorGUILayout.PropertyField(buildType, EditorMetaData.BuildInformation.Type);
            
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(buildDate, EditorMetaData.BuildInformation.Date);
            EditorGUI.indentLevel--;
            
            EditorGUILayout.PropertyField(buildNumber, EditorMetaData.BuildInformation.Number);
        }
    }
}
