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

#if UNITY_EDITOR && UNITY_ANDROID

using UnityEditor;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Updates the android bundle code...
    /// </summary>
    public sealed class AndroidBundleCodeUpdater : IPreBuildDialogue, IBuildUpdater
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   IPreBuildDialogue Implementation 
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

        /// <summary>
        /// Shows a dialogue when a build is about to be made for updating the bundle code.
        /// </summary>
        /// <param name="target">The build platform target</param>
        public void OnPreBuildDialogue(BuildTarget target)
        {
            if (target != BuildTarget.Android) return;

            if (UtilEditor.BuildOptions.AndroidBundleCodeUsage != AssetUsageType.PromptMe)
            {
                BuildHandler.Register(new HandlerDialogueData(UtilEditor.GetClassName<AndroidBundleCodeUpdater>(), true));
                return;
            }

            var currentBundleCode = PlayerSettings.Android.bundleVersionCode;

            var choice = EditorUtility.DisplayDialog("Build Versions | Android Bundle Code Updater",
                $"Do you want to increment the bundle code for this build?\n\nThis will update the code from {currentBundleCode} to {currentBundleCode + 1}.\n\nYou can disable this prompt in the asset settings.",
                $"Yes (Set to {currentBundleCode + 1})", $"No (Leave as {currentBundleCode})");
            
            BuildHandler.Register(new HandlerDialogueData(UtilEditor.GetClassName<AndroidBundleCodeUpdater>(), choice));
        }
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   IBuildUpdater Implementation 
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

        /// <summary>
        /// The order the version updater should fire at.
        /// </summary>
        public int UpdateOrder => 0;
        
        
        /// <summary>
        /// Runs when a build is being made.
        /// </summary>
        /// <param name="buildTarget">The build platform target</param>
        public void OnBuildVersionIncremented(BuildTarget buildTarget)
        {
            if (buildTarget != BuildTarget.Android) return;
            if (UtilEditor.BuildOptions.AndroidBundleCodeUsage == AssetUsageType.Disabled) return;
            if (!BuildHandler.Get<AndroidBundleCodeUpdater>().Choice) return;

            PlayerSettings.Android.bundleVersionCode++;
        }
    }
}

#endif