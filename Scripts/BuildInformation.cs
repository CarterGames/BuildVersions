using System;
using UnityEngine;

/*
 * 
 *  Build Versions
 *							  
 *	Build Information
 *      The scriptable object that contains the data about the current build version...
 *			
 *  Written by:
 *      Jonathan Carter
 *
 *  Published By:
 *      Carter Games
 *      E: hello@carter.games
 *      W: https://www.carter.games
 *		
 *  Version: 1.0.0
 *	Last Updated: 09/10/2021 (d/m/y)							
 * 
 */

namespace CarterGames.Assets.BuildVersions
{
    [CreateAssetMenu(fileName = "Build Information", menuName = "Carter Games/Build Versions | CG/New Build Information", order = 0)]
    public class BuildInformation : ScriptableObject
    {
        [SerializeField, HideInInspector] private string buildType;
        [SerializeField, HideInInspector] private string buildDate;
        [SerializeField, HideInInspector] private int buildNumber = 1;

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
        public string BuildDate
        {
            get => buildDate;
            set => buildDate = value;
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
        public string SystematicVersionNumber => Application.version;
        
        /// <summary>
        /// Increments the build number...
        /// </summary>
        public void IncrementBuildNumber()
        {
            buildNumber++;
        }

        /// <summary>
        /// Sets the build date to the current date on your system...
        /// </summary>
        public void SetBuildDate()
        {
            BuildDate = DateTime.Now.Date.ToString("d");
        }
    }
}