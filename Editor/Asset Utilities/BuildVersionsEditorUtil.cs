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
        public static BuildInformation BuildInformation
        {
            get
            {
                if (cachedBuildInformation != null) return cachedBuildInformation;

                if (HasFile(BuildInfoObjectFilter))
                {
                    cachedBuildInformation = (BuildInformation)GetFile<BuildInformation>(BuildInfoObjectFilter);
                    return cachedBuildInformation;
                }

                CreateFile<BuildInformation>(DefaultBuildInfoPath);
                cachedBuildInformation = (BuildInformation)GetFile<BuildInformation>(BuildInfoObjectFilter);
                cachedBuildInformation.SetBuildDate();
                return cachedBuildInformation;
            }
        }
        
        
        /// <summary>
        /// Gets the build version options asset (or makes one if needed)...
        /// </summary>
        public static BuildVersionOptions BuildOptions
        {
            get
            {
                if (cachedBuildOptions != null) return cachedBuildOptions;

                if (HasFile(OptionsTypeFilter))
                {
                    cachedBuildOptions = (BuildVersionOptions)GetFile<BuildVersionOptions>(OptionsTypeFilter);
                    return cachedBuildOptions;
                }

                CreateFile<BuildVersionOptions>(DefaultBuildOptionsPath);
                cachedBuildOptions = (BuildVersionOptions)GetFile<BuildVersionOptions>(OptionsTypeFilter);
                return cachedBuildOptions;
            }
        }
        

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
        /// Creates a file of the type requested...
        /// </summary>
        /// <param name="path">The path to create the file in (Editor Only)</param>
        /// <typeparam name="T">The type the file is</typeparam>
        public static void CreateFile<T>(string path)
        {
            var instance = ScriptableObject.CreateInstance(typeof(T));

            var currentPath = string.Empty;
            
            foreach (var element in path.Split('/'))
            {
                if (!element.Equals("Assets"))
                    currentPath += "/" + element;
                else
                    currentPath = element;
                
                if (Directory.Exists(element)) continue;
                Directory.CreateDirectory(currentPath);
            }
            
            AssetDatabase.CreateAsset(instance, path);
            AssetDatabase.Refresh();
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