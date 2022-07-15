using UnityEditor;

namespace CarterGames.Assets.BuildVersions.Editor
{
    public interface IBuildVersionUpdate
    {
        void OnBuildVersionIncremented(BuildTarget buildTarget);
    }
}