using UnityEditor;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Implement to make a dialogue window appear before a build is made...
    /// </summary>
    public interface IPreBuildDialogue
    {
        void OnPreBuildDialogue(BuildTarget target);
    }
}