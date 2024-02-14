/*
 * Copyright (c) 2024 Carter Games
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

using System.IO;
using UnityEditor;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Handles references to scriptable objects in the asset that need generating without user input etc.
    /// </summary>
    public static class ScriptableRef
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Asset Paths
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static readonly string AssetIndexPath = $"{AssetBasePath}/Carter Games/{AssetName}/Resources/Asset Index.asset";
        private static readonly string BuildInformationPath = $"{AssetBasePath}/Carter Games/{AssetName}/Data/Build Information.asset";
        private static readonly string BuildOptionsPath = $"{AssetBasePath}/Carter Games/{AssetName}/Data/Build Options.asset";
        

        // Asset Filters
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static readonly string AssetIndexFilter = $"t:{typeof(AssetIndex).FullName}";
        private static readonly string BuildInfoFilter = $"t:{typeof(BuildInformation).FullName}";
        private static readonly string OptionsFilter = $"t:{typeof(BuildVersionOptions).FullName}";


        // Asset Caches
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static AssetIndex assetIndexCache;
        private static BuildInformation buildInformationCache;
        private static BuildVersionOptions buildOptionsCache;
        
        
        // SerializedObject Caches
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static SerializedObject optionsAssetObjectCache;


        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // Helper Properties
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the path where the asset code is located.
        /// </summary>
        private static string AssetBasePath => FileEditorUtil.AssetBasePath;
        
        
        /// <summary>
        /// Gets the asset name stored in the file util editor class.
        /// </summary>
        private static string AssetName => FileEditorUtil.AssetName;

        
        // Asset Properties
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The asset index for the asset.
        /// </summary>
        public static AssetIndex AssetIndex =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref assetIndexCache, AssetIndexFilter, AssetIndexPath, AssetName, $"{AssetName}/Resources/Asset Index.asset");
        
        
        /// <summary>
        /// The editor settings for the asset.
        /// </summary>
        public static BuildInformation BuildInformation =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref buildInformationCache, BuildInfoFilter, BuildInformationPath, AssetName, $"{AssetName}/Data/Build Information.asset");
        
        
        /// <summary>
        /// The runtime settings for the asset.
        /// </summary>
        public static BuildVersionOptions BuildOptions =>
            FileEditorUtil.CreateSoGetOrAssignAssetCache(ref buildOptionsCache, OptionsFilter, BuildOptionsPath, AssetName, $"{AssetName}/Data/Build Options.asset");
        
        
        // Object Properties
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The editor SerializedObject for the asset.
        /// </summary>
        public static SerializedObject OptionsObject =>
            FileEditorUtil.CreateGetOrAssignSerializedObjectCache(ref optionsAssetObjectCache, BuildOptions);
        
        
        // Assets Initialized Check
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets if all the assets needed for the asset to function are in the project at the expected paths.
        /// </summary>
        public static bool HasAllAssets =>
            File.Exists(AssetIndexPath) && File.Exists(BuildInformationPath) &&
            File.Exists(BuildOptionsPath);
    }
}