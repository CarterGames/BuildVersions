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
    public static class BuildVersionsEditorUtil
    {
        //
        //
        //  Fields
        //
        //
        
        // The default paths & filters to find the scriptable object files needed...
        private static readonly string DefaultBuildInfoPath = "Assets/Resources/Carter Games/Build Versions/Build Information.asset";
        private static readonly string DefaultBuildOptionsPath = "Assets/Resources/Carter Games/Build Versions/Build Options.asset";
        private static readonly string BuildInfoObjectFilter = "t:buildinformation";
        private static readonly string OptionsTypeFilter = "t:buildversionoptions";

        // The colour for the titles in the editor...
        public static readonly Color TitleColour = new Color32(104, 206, 94, 255);

        // The cached values of the property getters...
        private static Texture2D cachedLogoImg;
        private static Texture2D cachedBannerLogoImg;
        private static Texture2D cachedCarterGamesBannerImg;
        private static BuildInformation cachedBuildInformation;
        private static BuildVersionOptions cachedBuildOptions;

        //
        //
        //  Properties
        //
        //
        
        /// <summary>
        /// Gets the asset logo...
        /// </summary>
        public static Texture2D Logo
        {
            get
            {
                if (cachedLogoImg != null) return cachedLogoImg;
                cachedLogoImg = (Texture2D) GetFile<Texture2D>("BuildVersionsIcon");
                return cachedLogoImg;
            }
        }
        
        
        /// <summary>
        /// Gets the asset logo
        /// </summary>
        public static Texture2D BannerLogo
        {
            get
            {
                if (cachedBannerLogoImg != null) return cachedBannerLogoImg;
                cachedBannerLogoImg = (Texture2D) GetFile<Texture2D>("BuildVersionsEditorHeader");
                return cachedBannerLogoImg;
            }
        }



        /// <summary>
        /// Gets the Carter Games Banner Logo...
        /// </summary>
        public static Texture2D CarterGamesBanner
        {
            get
            {
                if (cachedCarterGamesBannerImg != null) return cachedCarterGamesBannerImg;
                cachedCarterGamesBannerImg = (Texture2D) GetFile<Texture2D>("CarterGamesBanner");
                return cachedCarterGamesBannerImg;
            }
        }


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
        

        /// <summary>
        /// Gets the name of the class entered...
        /// </summary>
        /// <typeparam name="T">The type to get..</typeparam>
        /// <returns>The name of said class</returns>
        public static string GetClassName<T>() => typeof(T).Name;

        
        //
        //
        //  Methods
        //
        //
        
        
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
        public static void CreateToDirectory(string path)
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
        /// Gets the first file of the type requested that isn't the class (Editor Only)
        /// </summary>
        /// <param name="filter">the search filter</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>object</returns>
        private static object GetFile<T>(string filter)
        {
            var asset = AssetDatabase.FindAssets(filter, null);
            var path = AssetDatabase.GUIDToAssetPath(asset[0]);
            return AssetDatabase.LoadAssetAtPath(path, typeof(T));
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
        /// Gets the width of the text entered...
        /// </summary>
        /// <param name="text">The text the gauge</param>
        /// <returns>The width of the text entered</returns>
        public static float TextWidth(string text)
        {
            return GUI.skin.label.CalcSize(new GUIContent(text)).x;
        }
    }
}