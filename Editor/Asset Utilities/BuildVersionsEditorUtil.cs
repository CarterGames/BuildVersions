using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions.Editor
{
    public static class BuildVersionsEditorUtil
    {
        private static readonly string DefaultBuildInfoPath = "Assets/Resources/Build Versions/Build Information.asset";
        private static readonly string DefaultBuildOptionsPath = "Assets/Resources/Build Versions/Build Options.asset";
        private static readonly string BuildInfoObjectFilter = "t:buildinformation";
        private static readonly string OptionsTypeFilter = "t:buildversionoptions";
        
        public static readonly Color TitleColour = new Color32(104, 206, 94, 255);

        private static Texture2D cachedLogoImg;
        private static Texture2D cachedManagerHeaderImg;
        private static Texture2D cachedCarterGamesBannerImg;
        private static BuildInformation cachedBuildInformation;
        private static BuildVersionOptions cachedBuildOptions;

        public static Texture2D Logo
        {
            get
            {
                if (cachedLogoImg != null) return cachedLogoImg;
                cachedLogoImg = (Texture2D) GetFile<Texture2D>("BuildVersionsIcon");
                return cachedLogoImg;
            }
        }
        
        public static Texture2D ManagerHeader
        {
            get
            {
                if (cachedManagerHeaderImg != null) return cachedManagerHeaderImg;
                cachedManagerHeaderImg = (Texture2D) GetFile<Texture2D>("BuildVersionsEditorHeader");
                return cachedManagerHeaderImg;
            }
        }
        
        public static Texture2D CarterGamesBanner
        {
            get
            {
                if (cachedCarterGamesBannerImg != null) return cachedCarterGamesBannerImg;
                cachedCarterGamesBannerImg = (Texture2D) GetFile<Texture2D>("CarterGamesBanner");
                return cachedCarterGamesBannerImg;
            }
        }
        
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
        /// Checks to see whether or not a file of said type exists already (Editor Only)
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static bool HasFile(string filter)
        {
            return AssetDatabase.FindAssets(filter, null).Length > 0;
        }

        
        /// <summary>
        /// Creates a file of the type requested...
        /// </summary>
        /// <param name="path">The path to create the file in (Editor Only)</param>
        /// <typeparam name="T">The type the file is</typeparam>
        private static void CreateFile<T>(string path)
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
        /// Gets all the IBuildVersionUpdating implementations and returns the result (Editor Only)
        /// </summary>
        /// <returns>A Array of IBuildVersionUpdates</returns>
        public static T[] GetAllInterfacesOfType<T>()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && typeof(T).IsAssignableFrom(x));

            return types.Select(type => (T)Activator.CreateInstance(type)).ToArray();
        }
    }
}