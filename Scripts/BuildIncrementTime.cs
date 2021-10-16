/*
 * 
 *  Build Versions
 *							  
 *	Build Increment Time
 *      The options for when the build number should update
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
    public enum BuildIncrementTime
    {
        AnyBuild,
        OnlySuccessfulBuilds,
    }
}