using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    [CreateAssetMenu(fileName = "Build Version Settings", menuName = "Carter Games/Build Versions | CG/New Settings", order = 0)]
    public class BuildVersionSettings : ScriptableObject
    {
        public BuildIncrementTime buildUpdateTime;
        public bool updatePlayerSettingsVersion;
    }
}