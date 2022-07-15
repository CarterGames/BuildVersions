using UnityEditor;

namespace CarterGames.Assets.BuildVersions.Editor
{
    public class ScriptableObjectUpdater : IBuildVersionUpdate
    {
        public void OnBuildVersionIncremented(BuildTarget buildTarget)
        {
            var info = BuildVersionsEditorUtil.BuildInformation;

            if (info == null)
            {
                BvLog.Error("Unable to update data as it was not found in the project!");
                return;
            }

            info.IncrementBuildNumber();
            info.SetBuildDate();
            EditorUtility.SetDirty(info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}