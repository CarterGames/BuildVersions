using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    [CreateAssetMenu(fileName = "Build Version Options", menuName = "Carter Games/Build Versions | CG/New Options", order = 0)]
    public class BuildVersionOptions : BuildVersionsAsset
    {
        public AssetUsageType assetStatus = AssetUsageType.Enabled;
        public BuildIncrementTime buildUpdateTime = BuildIncrementTime.OnlySuccessfulBuilds;
        public AssetUsageType updateSystematic = AssetUsageType.Disabled;
        
        // Only applicable if using the android build platform
        public AssetUsageType androidUpdateBundleCode = AssetUsageType.Disabled;
    }
}