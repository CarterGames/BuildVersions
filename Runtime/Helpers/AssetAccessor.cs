using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    public static class AssetAccessor
    {
        private static BuildVersionsAsset[] assets;


        private static IEnumerable<BuildVersionsAsset> Assets
        {
            get
            {
                if (assets != null) return assets;
                assets = Resources.LoadAll("Build Versions", typeof(BuildVersionsAsset)).Cast<BuildVersionsAsset>().ToArray();
                return assets;
            }
        }


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