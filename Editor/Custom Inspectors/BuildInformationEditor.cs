using UnityEditor;
using UnityEngine;


namespace CarterGames.Assets.BuildVersions.Editor
{
    [CustomEditor(typeof(BuildInformation))]
    public class BuildInformationEditor : UnityEditor.Editor
    {
        private SerializedProperty buildType;
        private SerializedProperty buildDate;
        private SerializedProperty buildNumber;
        
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


        private void ShowLogo()
        {
            GUILayout.Space(5f);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // Shows either the Carter Games Logo or an alternative for if the icon is deleted/not included when you import the package
            // Note: if you are using an older version of the asset, the directory/name of the logo may not match this and therefore will display the text title only
            if (BuildVersionsEditorUtil.Logo)
            {
                if (GUILayout.Button(BuildVersionsEditorUtil.Logo, GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
                    GUI.FocusControl(null);
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(5f);

        }


        private void ShowValues()
        {
            EditorGUILayout.PropertyField(buildType);
            EditorGUILayout.PropertyField(buildDate);
            EditorGUILayout.PropertyField(buildNumber);
        }
        
        
        private float TextWidth(string text)
        {
            return GUI.skin.label.CalcSize(new GUIContent(text)).x;
        }
    }
}