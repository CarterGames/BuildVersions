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
    /// A class to draw all the menu options for the navigation menu options in this asset...
    /// </summary>
    public static class BuildVersionsMenuOptions
    {
        [MenuItem("Tools/Carter Games/Build Versions/Edit Info", priority = 0)]
        public static void EditInfo() => Selection.activeObject = UtilEditor.BuildInformation;
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Edit Settings", priority = 1)]
        public static void EditSettings() => SettingsService.OpenProjectSettings("Project/Carter Games/Build Versions");
        

        [MenuItem("Tools/Carter Games/Build Versions/Type/Set To Prototype")]
        public static void SetToPrototype() => BuildVersionsManager.SetBuildType("Prototype");
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Type/Set To Development")]
        public static void SetToDevelopment() => BuildVersionsManager.SetBuildType("Development");
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Type/Set To Test")]
        public static void SetToTest() => BuildVersionsManager.SetBuildType("Test");
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Type/Alpha/Set To Pre-Alpha")]
        public static void SetToPreAlpha() => BuildVersionsManager.SetBuildType("Pre-Alpha");
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Type/Alpha/Set To Closed Alpha")]
        public static void SetToClosedAlpha() => BuildVersionsManager.SetBuildType("Closed Alpha");
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Type/Alpha/Set To Alpha")]
        public static void SetToAlpha() => BuildVersionsManager.SetBuildType("Alpha");
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Type/Alpha/Set To Open Alpha")]
        public static void SetToOpenAlpha() => BuildVersionsManager.SetBuildType("Open Alpha");
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Type/Beta/Set To Closed Beta")]
        public static void SetToClosedBeta() => BuildVersionsManager.SetBuildType("Closed Beta");
        

        [MenuItem("Tools/Carter Games/Build Versions/Type/Beta/Set To Beta")]
        public static void SetToBeta() => BuildVersionsManager.SetBuildType("Beta");
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Type/Beta/Set To Open Beta")]
        public static void SetToOpenBeta() => BuildVersionsManager.SetBuildType("Open Beta");
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Type/Release/Set To Release Candidate")]
        public static void SetToReleaseCandidate() => BuildVersionsManager.SetBuildType("Release Candidate");
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Type/Release/Set To Release")]
        public static void SetToRelease() => BuildVersionsManager.SetBuildType("Release");
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Date/Set Date To Today")]
        public static void SetBuildDate() => BuildVersionsManager.SetDate();
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Build Number/Increment Build Number")]
        public static void IncrementBuild() => BuildVersionsManager.SetBuildNumber();
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Build Number/Reset Build Number")]
        public static void ResetBuild() => BuildVersionsManager.SetBuildNumber(1);
        

        [MenuItem("Tools/Carter Games/Build Versions/Version Number/Increment Major")]
        public static void IncrementMajor() => SemanticVersionUpdater.CallUpdateVersionNumber(EditorUserBuildSettings.activeBuildTarget, 0);
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Version Number/Increment Minor")]
        public static void IncrementMinor() => SemanticVersionUpdater.CallUpdateVersionNumber(EditorUserBuildSettings.activeBuildTarget, 1);
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Version Number/Increment Build")]
        public static void IncrementVersionBuild() => SemanticVersionUpdater.CallUpdateVersionNumber(EditorUserBuildSettings.activeBuildTarget);
        
        
        [MenuItem("Tools/Carter Games/Build Versions/Sync/Sync Application Version Number")]
        public static void SyncVersionNumber() => BuildVersionsManager.CallSyncInterfaces();
        

        [MenuItem("Tools/Carter Games/Build Versions/Sync/Update Cached Version Number")]
        public static void UpdateCachedVersionNumber() => BuildVersionsManager.UpdateCachedVersionNumber();
    }
}
