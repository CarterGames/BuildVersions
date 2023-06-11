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

using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// A helper class to help get things for the editor to use...
    /// </summary>
    public static class UtilEditor
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Fields
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        // Paths
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static readonly string DefaultAssetIndexPath = "Assets/Resources/Carter Games/Build Versions/Asset Index.asset";
        private static readonly string DefaultBuildInfoPath = $"{AssetBasePath}/Carter Games/Build Versions/Data/Build Information.asset";
        private static readonly string DefaultBuildOptionsPath = $"{AssetBasePath}/Carter Games/Build Versions/Data/Build Options.asset";


        // Filter
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private const string BuildVersionsLogoFilter = "BuildVersionsIcon";
        private const string BuildVersionsHeaderFilter = "BuildVersionsEditorHeader";
        private const string CarterGamesBannerFilter = "CarterGamesBanner";
        private static readonly string AssetIndexFilter = "t:buildversionsassetindex";
        private static readonly string BuildInfoObjectFilter = "t:buildinformation";
        private static readonly string OptionsTypeFilter = "t:buildversionoptions";
        
        
        // Assets
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static BuildVersionsAssetIndex cachedAssetIndex;
        private static BuildInformation cachedBuildInformation;
        private static BuildVersionOptions cachedBuildOptions;
        
        
        // Texture Caches
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static Texture2D cachedLogoImg;
        private static Texture2D cachedBannerLogoImg;
        private static Texture2D cachedCarterGamesBannerImg;
        
        

        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Properties
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

        /// <summary>
        /// Gets the path where the asset code is located.
        /// </summary>
        private static string AssetBasePath
        {
            get
            {
                var script = AssetDatabase.FindAssets($"t:Script {nameof(UtilEditor)}")[0];
                var path = AssetDatabase.GUIDToAssetPath(script);

                path = path.Replace("/Carter Games/Build Versions/Code/Editor/Utility/UtilEditor.cs", "");
                return path;
            }
        }
        
        
        /// <summary>
        /// Gets the asset logo...
        /// </summary>
        public static Texture2D Logo => GetOrAssignCache(ref cachedLogoImg, BuildVersionsLogoFilter);


        /// <summary>
        /// Gets the asset logo
        /// </summary>
        public static Texture2D BannerLogo => GetOrAssignCache(ref cachedBannerLogoImg, BuildVersionsHeaderFilter);


        /// <summary>
        /// Gets the Carter Games Banner Logo...
        /// </summary>
        public static Texture2D CarterGamesBanner =>
            GetOrAssignCache(ref cachedCarterGamesBannerImg, CarterGamesBannerFilter);


        /// <summary>
        /// Gets the build information asset (or makes one if needed)...
        /// </summary>
        public static BuildVersionsAssetIndex AssetIndex =>
            CreateSoGetOrAssignCache(ref cachedAssetIndex, DefaultAssetIndexPath,
                AssetIndexFilter);
        
        
        /// <summary>
        /// Gets the build information asset (or makes one if needed)...
        /// </summary>
        public static BuildInformation BuildInformation =>
            CreateSoGetOrAssignCache(ref cachedBuildInformation, DefaultBuildInfoPath,
                BuildInfoObjectFilter);


        /// <summary>
        /// Gets the build version options asset (or makes one if needed)...
        /// </summary>
        public static BuildVersionOptions BuildOptions =>
            CreateSoGetOrAssignCache(ref cachedBuildOptions, DefaultBuildOptionsPath,
                OptionsTypeFilter);

        public static bool HasBuildInformationCached => cachedBuildInformation != null;
        public static bool HasBuildOptionsCached => cachedBuildOptions != null;


        /// <summary>
        /// Gets the name of the class entered...
        /// </summary>
        /// <typeparam name="T">The type to get..</typeparam>
        /// <returns>The name of said class</returns>
        public static string GetClassName<T>() => typeof(T).Name;
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

        /// <summary>
        /// Checks to see whether or not a file of said type exists already (Editor Only)
        /// </summary>
        /// <param name="filter">The filter to search for...</param>
        /// <returns>If the file was found.</returns>
        public static bool HasFile(string filter)
        {
            return AssetDatabase.FindAssets(filter, null).Length > 0;
        }


        /// <summary>
        /// Gets a file via filter.
        /// </summary>
        /// <param name="filter">The filter to search for.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The found file as an object if found successfully.</returns>
        private static object GetFileViaFilter<T>(string filter)
        {
            var asset = AssetDatabase.FindAssets(filter, null);
            if (asset == null || asset.Length <= 0) return null;
            var path = AssetDatabase.GUIDToAssetPath(asset[0]);
            return AssetDatabase.LoadAssetAtPath(path, typeof(T));
        }
        
        
        /// <summary>
        /// Gets or assigned the cached value of any type, just saving writing the same lines over and over xD
        /// </summary>
        /// <param name="cache">The cached value to assign or get.</param>
        /// <param name="filter">The filter to use.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The assigned cache.</returns>
        private static T GetOrAssignCache<T>(ref T cache, string filter)
        {
            if (cache != null) return cache;
            cache = (T)GetFileViaFilter<T>(filter);
            return cache;
        }
        
        
        /// <summary>
        /// Creates a scriptable object if it doesn't exist and then assigns it to its cache. 
        /// </summary>
        /// <param name="cache">The cached value to assign or get.</param>
        /// <param name="path">The path to create to if needed.</param>
        /// <param name="filter">The filter to use.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The assigned cache.</returns>
        private static T CreateSoGetOrAssignCache<T>(ref T cache, string path, string filter) where T : ScriptableObject
        {
            if (cache != null) return cache;
            cache = (T)GetFileViaFilter<T>(filter);

            if (cache == null)
            {
                cache = CreateScriptableObject<T>(path);
            }
            
            AssetIndexHandler.UpdateIndex();

            return cache;
        }
        
        
        /// <summary>
        /// Creates a scriptable object of the type entered when called.
        /// </summary>
        /// <param name="path">The path to create the new asset at.</param>
        /// <typeparam name="T">The type to make.</typeparam>
        /// <returns>The newly created asset.</returns>
        private static T CreateScriptableObject<T>(string path) where T : ScriptableObject
        {
            var instance = ScriptableObject.CreateInstance(typeof(T));

            CreateToDirectory(path);

            AssetDatabase.CreateAsset(instance, path);
            AssetDatabase.Refresh();

            return (T)instance;
        }
        
        
        /// <summary>
        /// Creates all the folders to a path if they don't exist already.
        /// </summary>
        /// <param name="path">The path to create to.</param>
        private static void CreateToDirectory(string path)
        {
            var currentPath = string.Empty;
            var split = path.Split('/');

            for (var i = 0; i < path.Split('/').Length; i++)
            {
                var element = path.Split('/')[i];
                currentPath += element + "/";

                if (i.Equals(split.Length - 1))
                {
                    continue;
                }

                if (Directory.Exists(currentPath))
                {
                    continue;
                }

                Directory.CreateDirectory(currentPath);
            }
        }


        /// <summary>
        /// Gets all the interface implementations and returns the result (Editor Only)
        /// </summary>
        /// <returns>An Array of the interface type</returns>
        public static T[] GetAllInterfacesOfType<T>()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && typeof(T).IsAssignableFrom(x));

            return types.Select(type => (T)Activator.CreateInstance(type)).ToArray();
        }


        /// <summary>
        /// Initializes the assets for use.
        /// </summary>
        public static void Initialize()
        {
            if (cachedAssetIndex == null)
            {
                cachedAssetIndex = AssetIndex;
            }
            
            AssetIndexHandler.UpdateIndex();
            EditorUtility.SetDirty(AssetIndex);
            
            cachedBuildInformation = BuildInformation;
            cachedBuildOptions = BuildOptions;
            
            EditorUtility.SetDirty(BuildInformation);
            EditorUtility.SetDirty(BuildOptions);
        }
    }
}