using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    /// <summary>
    /// Stores the asset options for the build version asset...
    /// </summary>
    [CreateAssetMenu(fileName = "Build Version Options", menuName = "Carter Games/Build Versions | CG/New Options", order = 0)]
    public class BuildVersionOptions : BuildVersionsAsset
    {
        //
        //
        //  Fields
        //
        //
        
        // main settings...
        public AssetUsageType assetStatus = AssetUsageType.Enabled;
        public BuildIncrementTime buildUpdateTime = BuildIncrementTime.OnlySuccessfulBuilds;
        public AssetUsageType updateSemantic = AssetUsageType.PromptMe;
        
        // cached values...
        public string lastSemanticNumber;
        
        // Only applicable if using the android build platform
        public AssetUsageType androidUpdateBundleCode = AssetUsageType.Disabled;
        
        
        //
        //
        //  Properties
        //
        //
        
        /// <summary>
        /// Gets/Sets the last version number the asset has saved
        /// </summary>
        public string LastSemanticVersionNumberSaved
        {
            get => lastSemanticNumber;
            set => lastSemanticNumber = value;
        }
    }
}