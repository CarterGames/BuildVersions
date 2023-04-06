﻿using System;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    /// <summary>
    /// Stores the build information for the asset...
    /// </summary>
    [CreateAssetMenu(fileName = "Build Information", menuName = "Carter Games/Build Versions | CG/New Build Information", order = 0)]
    public class BuildInformation : BuildVersionsAsset
    {
        //
        //
        //  Fields
        //
        //
        
        [SerializeField, HideInInspector] private string buildType;
        [SerializeField] private SerializableDate buildDate;
        [SerializeField, HideInInspector] private int buildNumber = 1;


        //
        //
        //  Properties
        //
        //
        
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
        public SerializableDate BuildDate
        {
            get => buildDate;
            private set => buildDate = value;
        }
        

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
        /// Increments the build number...
        /// </summary>
        public void IncrementBuildNumber() => buildNumber++;


        /// <summary>
        /// Sets the build date to the current date on your system...
        /// </summary>
        public void SetBuildDate() => BuildDate = new SerializableDate(DateTime.Now);
    }
}