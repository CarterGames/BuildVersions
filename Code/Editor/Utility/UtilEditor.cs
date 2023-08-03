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

        // Filter
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private const string BuildVersionsLogoFilter = "BuildVersionsIcon";
        private const string BuildVersionsHeaderFilter = "BuildVersionsEditorHeader";
        private const string CarterGamesBannerFilter = "CarterGamesBanner";


        // Texture Caches
        /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        private static Texture2D cachedLogoImg;
        private static Texture2D cachedBannerLogoImg;
        private static Texture2D cachedCarterGamesBannerImg;
        
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Properties
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        /// <summary>
        /// Gets the asset logo...
        /// </summary>
        public static Texture2D Logo => FileEditorUtil.GetOrAssignCache(ref cachedLogoImg, BuildVersionsLogoFilter);


        /// <summary>
        /// Gets the asset logo
        /// </summary>
        public static Texture2D BannerLogo => FileEditorUtil.GetOrAssignCache(ref cachedBannerLogoImg, BuildVersionsHeaderFilter);


        /// <summary>
        /// Gets the Carter Games Banner Logo...
        /// </summary>
        public static Texture2D CarterGamesBanner =>
            FileEditorUtil.GetOrAssignCache(ref cachedCarterGamesBannerImg, CarterGamesBannerFilter);


        /// <summary>
        /// Gets the build information asset (or makes one if needed)...
        /// </summary>
        public static CarterGames.Assets.BuildVersions.AssetIndex AssetIndex => ScriptableRef.AssetIndex;


        /// <summary>
        /// Gets the build information asset (or makes one if needed)...
        /// </summary>
        public static CarterGames.Assets.BuildVersions.BuildInformation BuildInformation => ScriptableRef.BuildInformation;


        /// <summary>
        /// Gets the build version options asset (or makes one if needed)...
        /// </summary>
        public static CarterGames.Assets.BuildVersions.BuildVersionOptions BuildOptions => ScriptableRef.BuildOptions;

        
        /// <summary>
        /// Gets the build version options asset (or makes one if needed)...
        /// </summary>
        public static SerializedObject BuildOptionsObject => ScriptableRef.OptionsObject;


        /// <summary>
        /// Gets the name of the class entered...
        /// </summary>
        /// <typeparam name="T">The type to get..</typeparam>
        /// <returns>The name of said class</returns>
        public static string GetClassName<T>() => typeof(T).Name;
        
        
        /// <summary>
        /// Gets if there is a settings asset in the project.
        /// </summary>
        public static bool HasInitialized
        {
            get
            {
                AssetIndexHandler.UpdateIndex();
                return ScriptableRef.HasAllAssets;
            }
        }
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
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
            AssetDatabase.Refresh();
            
            // var index = AssetIndex;
            var info = BuildInformation;
            var settings = BuildOptions;
            
            // AssetIndexHandler.UpdateIndex();
            
            // EditorUtility.SetDirty(AssetIndex);
            // EditorUtility.SetDirty(BuildInformation);
            // EditorUtility.SetDirty(BuildOptions);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}