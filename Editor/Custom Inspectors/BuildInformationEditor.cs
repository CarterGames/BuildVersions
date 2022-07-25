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
        //
        //
        //  Fields
        //
        //
        
        private SerializedProperty buildType;
        private SerializedProperty buildDate;
        private SerializedProperty buildNumber;
        
        
        //
        //
        //  Unity Methods
        //
        //
        
        
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

        
        //
        //
        //  Methods
        //
        //


        /// <summary>
        /// Draws the logo for the asset...
        /// </summary>
        private static void ShowLogo()
        {
            // Shows either the Logo if found, if not nothing will show here...
            if (BuildVersionsEditorUtil.HasFile("BuildVersionsIcon"))
            {
                GUILayout.Space(5f);

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                // Shows either the Logo if found, if not nothing will show here...
                if (GUILayout.Button(BuildVersionsEditorUtil.Logo, GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
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
            EditorGUILayout.PropertyField(buildDate);
            EditorGUILayout.PropertyField(buildNumber);
        }
    }
}
