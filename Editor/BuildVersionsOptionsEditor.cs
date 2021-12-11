using UnityEditor;
using UnityEngine;

/*
 * 
 *  Build Versions
 *							  
 *	Build Versions Options Editor
 *      The editor script for the build versions options scriptable object
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
    [CustomEditor(typeof(BuildVersionOptions))]
    public class BuildVersionsOptionsEditor : UnityEditor.Editor
    {
        private BuildInformation info;
        
        private SerializedProperty assetActive;
        private SerializedProperty buildUpdateTime;
        private SerializedProperty updatePlayerSettingsVersion;
        
        
        private void OnEnable()
        {
            assetActive = serializedObject.FindProperty("assetActive");
            buildUpdateTime = serializedObject.FindProperty("buildUpdateTime");
            updatePlayerSettingsVersion = serializedObject.FindProperty("updateSystematic");

            info = BuildVersionsManager.GetBuildInformation();
        }

        public override void OnInspectorGUI()
        {
            ShowLogo();
            GUILayout.Space(10f);
            ShowValues();
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
            EditorGUILayout.LabelField("Build Versions Options", EditorStyles.boldLabel, GUILayout.Width(TextWidth(" Build Versions Options ")));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }


        private void ShowValues()
        {
            EditorGUILayout.PropertyField(assetActive);

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
        
        
        private float TextWidth(string text)
        {
            return GUI.skin.label.CalcSize(new GUIContent(text)).x;
        }
    }
}