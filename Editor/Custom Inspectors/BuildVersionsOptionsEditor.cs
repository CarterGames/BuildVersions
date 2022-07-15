using UnityEditor;
using UnityEngine;


namespace CarterGames.Assets.BuildVersions.Editor
{
    [CustomEditor(typeof(BuildVersionOptions))]
    public class BuildVersionsOptionsEditor : UnityEditor.Editor
    {
        private BuildInformation info;
        
        private SerializedProperty assetStatus;
        private SerializedProperty shouldUpdate;
        private SerializedProperty buildUpdateTime;
        private SerializedProperty updatePlayerSettingsVersion;

        private Color defaultTextColor;
        private Color defaultBackgroundColor;
        
        private void OnEnable()
        {
            assetStatus = serializedObject.FindProperty("assetStatus");
            shouldUpdate = serializedObject.FindProperty("shouldUpdate");
            buildUpdateTime = serializedObject.FindProperty("buildUpdateTime");
            updatePlayerSettingsVersion = serializedObject.FindProperty("updateSystematic");

            info = BuildVersionsEditorUtil.BuildInformation;
            
            defaultTextColor = GUI.color;
            defaultBackgroundColor = GUI.backgroundColor;
        }

        public override void OnInspectorGUI()
        {
            ShowLogo();

            GUILayout.Space(4.5f);
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            GUILayout.Space(1.5f);

            GUI.color = BuildVersionsEditorUtil.TitleColour;
            EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);
            GUI.color = defaultTextColor;

            GUILayout.Space(2.5f);
            GUI.enabled = false;
            ShowValues();
            GUI.enabled = true;
            GUILayout.Space(2.5f);
            
            EditorGUILayout.EndVertical();

            GUI.backgroundColor = BuildVersionsEditorUtil.TitleColour;
            if (GUILayout.Button("Edit Settings", GUILayout.Height(22.5f)))
                SettingsService.OpenProjectSettings("Project/Carter Games/Build Versions");
            GUI.backgroundColor = defaultBackgroundColor;
            
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
        }


        private void ShowValues()
        {
            EditorGUILayout.PropertyField(assetStatus);
            EditorGUILayout.PropertyField(shouldUpdate);

            EditorGUI.BeginChangeCheck();
            
            var _oldSetting = buildUpdateTime.intValue;
            EditorGUILayout.PropertyField(buildUpdateTime);
            
            if (EditorGUI.EndChangeCheck() && buildUpdateTime.intValue != _oldSetting)
            {
                switch (_oldSetting)
                {
                    case 0 when buildUpdateTime.intValue.Equals(1):
                        info.BuildNumber++;
                        break;
                    case 1 when buildUpdateTime.intValue.Equals(0):
                        info.BuildNumber--;
                        break;
                }
            }

            EditorGUILayout.PropertyField(updatePlayerSettingsVersion);
        }
    }
}