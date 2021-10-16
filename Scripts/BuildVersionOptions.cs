using UnityEngine;

/*
 * 
 *  Build Versions
 *							  
 *	Build Versions Options
 *      The scriptable object for the build versions options
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
    [CreateAssetMenu(fileName = "Build Version Options", menuName = "Carter Games/Build Versions | CG/New Options", order = 0)]
    public class BuildVersionOptions : ScriptableObject
    {
        public bool assetActive = true;
        public BuildIncrementTime buildUpdateTime = BuildIncrementTime.OnlySuccessfulBuilds;
        public bool updateSystematic;
        
        // Only applicable if using the android build platform
        public bool androidUpdateBundleCode;
    }
}