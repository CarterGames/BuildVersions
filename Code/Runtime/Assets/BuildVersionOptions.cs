/*
 * Copyright (c) 2018-Present Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    /// <summary>
    /// Stores the asset options for the build version asset...
    /// </summary>
    [CreateAssetMenu(fileName = "Build Version Options", menuName = "Carter Games/Build Versions | CG/New Options", order = 0)]
    public class BuildVersionOptions : BuildVersionsAsset
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Fields
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        // main settings...
        public AssetUsageType assetStatus = AssetUsageType.Enabled;
        public BuildIncrementTime buildUpdateTime = BuildIncrementTime.OnlySuccessfulBuilds;
        public AssetUsageType updateSemantic = AssetUsageType.PromptMe;
        
        // cached values...
        public string lastSemanticNumber;
        
        // Only applicable if using the android build platform
        public AssetUsageType androidUpdateBundleCode = AssetUsageType.Disabled;

        [SerializeField] private bool showLogs;
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Properties
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

        /// <summary>
        /// Gets/Sets the last version number the asset has saved
        /// </summary>
        public string LastSemanticVersionNumberSaved
        {
            get => lastSemanticNumber;
            set => lastSemanticNumber = value;
        }


        public bool ShowLogs => showLogs;
    }
}