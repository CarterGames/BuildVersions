using UnityEditor;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions.Editor
{
    [CustomEditor(typeof(BuildVersionSettings))]
    public class BuildVersionSettingsEditor : UnityEditor.Editor
    {
        private SerializedProperty buildUpdateTime;
        private SerializedProperty updatePlayerSettingsVersion;
        
        private void OnEnable()
        {
            buildUpdateTime = serializedObject.FindProperty("buildUpdateTime");
            updatePlayerSettingsVersion = serializedObject.FindProperty("updatePlayerSettingsVersion");
        }

        public override void OnInspectorGUI()
        {
            ShowLogo();
            GUILayout.Space(10f);
            ShowValues();
        }


        private void ShowLogo()
        {
            GUILayout.Space(5f);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // Shows either the Carter Games Logo or an alternative for if the icon is deleted/not included when you import the package
            // Note: if you are using an older version of the asset, the directory/name of the logo may not match this and therefore will display the text title only
            if (Resources.Load<Texture2D>("BVLogo"))
            {
                if (GUILayout.Button(Resources.Load<Texture2D>("BVLogo"), GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
                {
                    GUI.FocusControl(null);
                }
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(5f);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Build Versions Settings", EditorStyles.boldLabel, GUILayout.Width(TextWidth(" Build Versions Settings ")));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }


        private void ShowValues()
        {
            EditorGUILayout.PropertyField(buildUpdateTime);
            EditorGUILayout.PropertyField(updatePlayerSettingsVersion);
        }
        
        
        private float TextWidth(string text)
        {
            return GUI.skin.label.CalcSize(new GUIContent(text)).x;
        }
    }
}