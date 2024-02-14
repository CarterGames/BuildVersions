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

using UnityEditor;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Updates the scriptable object build number...
    /// </summary>
    public sealed class ScriptableObjectUpdater : IBuildUpdater
    {
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
            var info = UtilEditor.BuildInformation;

            if (info == null)
            {
                BvLog.Error("Unable to update data as it was not found in the project!");
                return;
            }

            info.IncrementBuildNumber();
            info.SetBuildDate();
            EditorUtility.SetDirty(info);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}