using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Handles the setup of the asset index for runtime references to scriptable objects used for the asset.
    /// </summary>
    public sealed class AssetIndexHandler : IPreprocessBuildWithReport
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private const string AssetFilter = "t:buildversionsasset";

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IPreprocessBuildWithReport Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The order this script is processed in, in this case its the default.
        /// </summary>
        public int callbackOrder => 0;


        /// <summary>
        /// Runs before a build is executed.
        /// </summary>
        /// <param name="report">The report about the build (I don't need it, but its a param for the method).</param>
        public void OnPreprocessBuild(BuildReport report)
        {
            UpdateIndex();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Initializes the event subscription needed for this to work in editor.
        /// </summary>
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            EditorApplication.update -= OnEditorUpdate;
            EditorApplication.update += OnEditorUpdate;
        }


        /// <summary>
        /// Runs when the editor has updated.
        /// </summary>
        private static void OnEditorUpdate()
        {
            // If the user is about to enter play-mode, update the index, otherwise leave it be. 
            if (!EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isPlaying) return;
            UpdateIndex();
        }


        /// <summary>
        /// Updates the index with all the save manager asset scriptable objects in the project.
        /// </summary>
        public static void UpdateIndex()
        {
            var foundAssets = new List<BuildVersionsAsset>();
            var asset = AssetDatabase.FindAssets(AssetFilter, null);

            if (asset == null || asset.Length <= 0) return;

            foreach (var assetInstance in asset)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetInstance);
                var assetObj =
                    (BuildVersionsAsset)AssetDatabase.LoadAssetAtPath(assetPath, typeof(BuildVersionsAsset));

                // Doesn't include editor only or the index itself.
                if (assetObj == null) continue;
                if (assetObj.GetType() == typeof(BuildVersionsAssetIndex)) continue;
                foundAssets.Add((BuildVersionsAsset)AssetDatabase.LoadAssetAtPath(assetPath, typeof(BuildVersionsAsset)));
            }
            
            UtilEditor.AssetIndex.SetLookup(foundAssets);
            EditorUtility.SetDirty(UtilEditor.AssetIndex);
            AssetDatabase.SaveAssets();
        }
    }
}