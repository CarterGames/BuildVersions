using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    /// <summary>
    /// A helper class to access the build version assets at runtime...
    /// </summary>
    public static class AssetAccessor
    {
        //
        //
        //  Fields
        //
        //
        
        // a cache of all the assets found...
        private static BuildVersionsAsset[] assets;

        
        //
        //
        //  Properties
        //
        //
        
        
        /// <summary>
        /// Gets all the assets from the build versions asset...
        /// </summary>
        private static IEnumerable<BuildVersionsAsset> Assets
        {
            get
            {
                if (assets != null) return assets;
                assets = Resources.LoadAll("Build Versions", typeof(BuildVersionsAsset)).Cast<BuildVersionsAsset>().ToArray();
                return assets;
            }
        }

        
        //
        //
        //  Methods
        //
        //
        
        
        /// <summary>
        /// Gets the Build Versions Asset requested...
        /// </summary>
        /// <typeparam name="T">The build versions asset to get.</typeparam>
        /// <returns>The asset if it exists.</returns>
        public static T GetAsset<T>() where T : BuildVersionsAsset
        {
            return (T)Assets.FirstOrDefault(t => t.GetType() == typeof(T));
        }
    }
}