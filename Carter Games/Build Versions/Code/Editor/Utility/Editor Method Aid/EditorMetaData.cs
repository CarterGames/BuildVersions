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
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Handles all meta data for editor properties etc.
    /// </summary>
    public static class EditorMetaData
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Asset Info
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        public struct AssetVersionInfo
        {
            public static readonly GUIContent Number =
                new GUIContent(
                    "Version",
                    "The version number of the asset you currently have imported.");
        
        
            public static readonly GUIContent Date =
                new GUIContent(
                    "Release date",
                    "The date the version of the asset you are using was released on.");
        }
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Per User Settings
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        public struct PerUser
        {
            public static readonly GUIContent AutoVersionCheck =
                new GUIContent(
                    "Update check on load",
                    "Checks for any updates to the asset from the GitHub page when you load the project.");
        
        
            public static readonly GUIContent RuntimeDebugLogs =
                new GUIContent(
                    "Show logs?",
                    "See debug.log messages from the asset in editor or runtime?");
        }
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Build Information
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

        public struct BuildInformation
        {
            public static readonly GUIContent Type =
                new GUIContent(
                    "Build type",
                    "The \"type\" the build should show as.");


            public static readonly GUIContent Date =
                new GUIContent(
                    "Build date",
                    "The date the build was made on.");


            public static readonly GUIContent Number =
                new GUIContent(
                    "Build number",
                    "The raw iteration number of the build.");
        }

        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Build Options
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        public struct Settings
        {
            public static readonly GUIContent AssetStatus = 
                new GUIContent(
                    "Asset status", 
                    "Defines if the asset if enabled, partially enabled or disabled.");
            
            
            public static readonly GUIContent RunInDev = 
                new GUIContent(
                    "Use in dev build?", 
                    "Defines if the asset runs when making development builds or not.");
            
            
            public static readonly GUIContent BuildUpdateTime = 
                new GUIContent(
                    "Build update time", 
                    "Defines when the build version number is updated in the build process.");
            
            
            public static readonly GUIContent SemanticUpdate = 
                new GUIContent(
                    "Update semantic", 
                    "Defines how the semantic version number is updated when a build is made.");
            
            
            public static readonly GUIContent LastSemantic = 
                new GUIContent(
                    "Last cached semantic", 
                    "The currently cached semantic version number");
            
            
            public static readonly GUIContent BundleCode = 
                new GUIContent(
                    "Bundle code update", 
                    "Defines hwo the android bundle code is updated when a build is made.");
        }
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Serializable Date
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

        public struct SerializableDate
        {
            public static readonly GUIContent Day = 
                new GUIContent(
                    "Day", 
                    "The day the build was made on.");

            public static readonly GUIContent Month =
                new GUIContent(
                    "Month", 
                    "The month the build was made on.");

            public static readonly GUIContent Year =
                new GUIContent(
                    "Year", 
                    "The year the build was made on.");
        }
    }
}