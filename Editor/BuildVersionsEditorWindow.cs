using UnityEditor;
using UnityEngine;

/*
 * 
 *  Build Versions
 *							  
 *	Build Versions Editor Window
 *      The editor script for the build versions options
 *
 *  Warning:
 *	    Please refrain from editing this script as it will cause issues to the assets...
 *			
 *  Written by:
 *      Jonathan Carter
 *
 *  Published By:
 *      Carter Games
 *      E: hello@carter.games
 *      W: https://www.carter.games
 *		
 *  Version: 1.0.0
 *	Last Updated: 09/10/2021 (d/m/y)							
 * 
 */

namespace CarterGames.Assets.BuildVersions.Editor
{
    public class BuildVersionsEditorWindow : EditorWindow
    {
        private static readonly Color TitleColour = new Color32(104, 206, 94, 255);
        private Color defaultTextColour;
        
        private BuildVersionOptions options;
        private BuildInformation info;
        private SerializedObject obj;

        private SerializedProperty activeAsset;
        private SerializedProperty buildUpdateTime;
        private SerializedProperty updateSystematic;
        private SerializedProperty androidBundleCode;


        [MenuItem("Tools/Build Versions | CG/Settings", priority = -10)]
        private static void ShowWindow()
        {
            var window = GetWindow<BuildVersionsEditorWindow>();
            window.titleContent = new GUIContent("Build Versions Settings");
            window.Show();
        }


        private void OnEnable()
        {
            options = BuildVersionsManager.GetBuildVersionSettings(false);

            if (options == null)
                BuildVersionsManager.CheckOrSpawn();
            
            info = BuildVersionsManager.GetBuildInformation();
            options = BuildVersionsManager.GetBuildVersionSettings();
            if (options == null) return;
            obj = new SerializedObject(options);
            activeAsset = obj.FindProperty("assetActive");
            buildUpdateTime = obj.FindProperty("buildUpdateTime");
            updateSystematic = obj.FindProperty("updateSystematic");
            androidBundleCode = obj.FindProperty("androidUpdateBundleCode");
            defaultTextColour = GUI.color;
            
            EditorWindow editorWindow = this;
            editorWindow.minSize = new Vector2(450, 300f);
            editorWindow.maxSize = new Vector2(450f, 450f);
        }


        private void OnGUI()
        {
            ShowLogo();

            if (options == null) return;

            EditorGUILayout.HelpBox("Customise what happens when you make a build here.", MessageType.None);
            GUILayout.Space(7.5f);
            
            GUI.color = TitleColour;
            EditorGUILayout.LabelField("Enable Asset?", EditorStyles.boldLabel);
            GUI.color = defaultTextColour;
            EditorGUILayout.HelpBox(
                "Should the asset be active? Use this to disable the asset so it doesn't update the build information should you not want it to.",
                MessageType.None);
            EditorGUILayout.PropertyField(activeAsset, GUIContent.none);

            GUILayout.Space(5f);
            
            GUI.color = TitleColour;
            EditorGUILayout.LabelField("Build Number Update Time", EditorStyles.boldLabel);
            GUI.color = defaultTextColour;
            EditorGUILayout.HelpBox(
                "Defines when the build number get incremented. Either when the build is started or finished...",
                MessageType.None);
            
            EditorGUI.BeginChangeCheck();
            
            var _oldSetting = buildUpdateTime.intValue;
            EditorGUILayout.PropertyField(buildUpdateTime, GUIContent.none);
            
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
            
            GUILayout.Space(5f);

            GUI.color = TitleColour;
            EditorGUILayout.LabelField("Update Systematic Version Number?", EditorStyles.boldLabel);
            GUI.color = defaultTextColour;
            EditorGUILayout.HelpBox(
                "If true, the player settings version number will be updated, if not it will be left as is and only the scriptable object will be updated.",
                MessageType.None);
            EditorGUILayout.PropertyField(updateSystematic, GUIContent.none);

            if (EditorUserBuildSettings.activeBuildTarget.Equals(BuildTarget.Android))
            {
                if (updateSystematic.boolValue)
                {
                    GUI.color = TitleColour;
                    EditorGUILayout.LabelField("* ANDROID * | Update Bundle Code?", EditorStyles.boldLabel);
                    GUI.color = defaultTextColour;
                    EditorGUILayout.HelpBox("If true, the system will also update the android app bundle code.",
                        MessageType.None);
                    EditorGUILayout.PropertyField(androidBundleCode, GUIContent.none);
                }
                else
                {
                    GUI.color = TitleColour;
                    EditorGUILayout.LabelField("* ANDROID * | Update Bundle Code?", EditorStyles.boldLabel);
                    GUI.color = defaultTextColour;
                    EditorGUILayout.HelpBox(
                        "Enable systematic version updating to open the option to auto update the bundle code.",
                        MessageType.None);
                }
            }


            obj.ApplyModifiedProperties();
            obj.Update();
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
        
        
        private float TextWidth(string text)
        {
            return GUI.skin.label.CalcSize(new GUIContent(text)).x;
        }
    }
}