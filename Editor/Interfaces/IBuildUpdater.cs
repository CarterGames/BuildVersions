using UnityEditor;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Implement to make a build updater that will increment build numbers on build...
    /// </summary>
    public interface IBuildUpdater
    {
        int UpdateOrder { get; }
        void OnBuildVersionIncremented(BuildTarget buildTarget);
    }
}