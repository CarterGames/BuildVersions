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
        //
        //
        //  Fields
        //
        //
        
        private BuildInformation info;
        
        private SerializedProperty assetStatus;
        private SerializedProperty buildUpdateTime;
        private SerializedProperty updatePlayerSettingsVersion;
        private SerializedProperty lastBuildNumber;
        private SerializedProperty androidCodeSetting;

        private Color defaultTextColor;
        private Color defaultBackgroundColor;
        
        
        //
        //
        //  Unity Methods
        //
        //
        
        
        private void OnEnable()
        {
            info = BuildVersionsEditorUtil.BuildInformation;
            
            assetStatus = serializedObject.FindProperty("assetStatus");
            buildUpdateTime = serializedObject.FindProperty("buildUpdateTime");
            updatePlayerSettingsVersion = serializedObject.FindProperty("updateSystematic");
            lastBuildNumber = serializedObject.FindProperty("lastSystematicNumber");
            androidCodeSetting = serializedObject.FindProperty("androidUpdateBundleCode");

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


        //
        //
        //  Methods
        //
        //
        

        /// <summary>
        /// Shows the logo for the asset if possible...
        /// </summary>
        private void ShowLogo()
        {
            if (!BuildVersionsEditorUtil.HasFile("BuildVersionsIcon")) return;
            
            GUILayout.Space(5f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // Shows either the Carter Games Logo or an alternative for if the icon is deleted/not included when you import the package
            if (GUILayout.Button(BuildVersionsEditorUtil.Logo, GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
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
            EditorGUILayout.PropertyField(assetStatus);

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
            GUI.enabled = true;
        }
        
        
        /// <summary>
        /// Draws the last version number section...
        /// </summary>
        private void DrawLastVersionNumber()
        {
            GUI.color = BuildVersionsEditorUtil.TitleColour;
            EditorGUILayout.LabelField("Cached Values", EditorStyles.boldLabel);
            GUI.color = defaultTextColor;
            
            GUI.enabled = false;

            if (lastBuildNumber.stringValue.Length <= 0)
                lastBuildNumber.stringValue = PlayerSettings.bundleVersion;

            EditorGUILayout.PropertyField(lastBuildNumber);

            GUI.enabled = true;
            GUILayout.Space(2.5f);
        }
        
        
        /// <summary>
        /// Draws the android bundle code section...
        /// </summary>
        private void DrawAndroidBundleCodeSection()
        {
#if UNITY_ANDROID
            GUI.color = BuildVersionsEditorUtil.TitleColour;
            EditorGUILayout.LabelField("Android", EditorStyles.boldLabel);
            GUI.color = defaultTextColor;

            GUI.enabled = false;

            EditorGUILayout.PropertyField(androidCodeSetting, new GUIContent("Bundle Code Update"));

            GUI.enabled = true;


            GUILayout.Space(2.5f);
#endif
        }
        
        
        /// <summary>
        /// Draws a button to direct the user to the edit settings section of the asset...
        /// </summary>
        private void EditButton()
        {
            GUI.backgroundColor = BuildVersionsEditorUtil.TitleColour;
            
            if (GUILayout.Button("Edit Settings", GUILayout.Height(22.5f)))
                SettingsService.OpenProjectSettings("Project/Carter Games/Build Versions");
            
            GUI.backgroundColor = defaultBackgroundColor;
        }
    }
}