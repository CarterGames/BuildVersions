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

using System;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    /// <summary>
    /// Stores the build information for the asset...
    /// </summary>
    [CreateAssetMenu(fileName = "Build Information", menuName = "Carter Games/Build Versions | CG/New Build Information", order = 0)]
    public class BuildInformation : BuildVersionsAsset
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Fields
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        [SerializeField] private string buildType;
        [SerializeField] private SerializedDate buildDate;
        [SerializeField] private long buildTimestamp;
        [SerializeField] private int buildNumber;

        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Properties
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        /// <summary>
        /// Gets the current build type
        /// </summary>
        public string BuildType
        {
            get => buildType;
            set => buildType = value;
        }
        
        
        /// <summary>
        /// Get the current build date
        /// </summary>
        public SerializedDate BuildDate
        {
            get => buildDate;
            private set => buildDate = value;
        }
        
        
        /// <summary>
        /// Get the current build timestamp
        /// </summary>
        public long BuildTimestamp
        {
            get => buildTimestamp;
            private set => buildTimestamp = value;
        }


        /// <summary>
        /// Gets the current timestampt as a datetime.
        /// </summary>
        public DateTime TimestampAsDateTime => new DateTime(Epoch.Ticks).ToLocalTime().AddSeconds(buildTimestamp);
        

        /// <summary>
        /// Get the current build number
        /// </summary>
        public int BuildNumber
        {
            get => buildNumber;
            set => buildNumber = value;
        }

        
        /// <summary>
        /// Get the current version number
        /// </summary>
        public string SemanticVersionNumber => Application.version;
        
        
        /// <summary>
        /// Mainly as older 2020.3.x doesn't have the API for it. 
        /// </summary>
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Constructor
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */

        /// <summary>
        /// Creates a new build information asset when called.
        /// </summary>
        public BuildInformation()
        {
            buildDate = new SerializedDate(DateTime.Now);
            buildNumber = 1;
        }

        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
                
        /// <summary>
        /// Increments the build number...
        /// </summary>
        public void IncrementBuildNumber() => buildNumber++;


        /// <summary>
        /// Sets the build date to the current date on your system...
        /// </summary>
        public void SetBuildDate()
        {
            BuildDate = new SerializedDate(DateTime.Now);
            BuildTimestamp = (long)((DateTime.Now - Epoch).TotalSeconds);
        }
    }
}