using System;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    [CreateAssetMenu(fileName = "Build Information", menuName = "Carter Games/Build Versions | CG/New Build Information", order = 0)]
    public class BuildInformation : ScriptableObject
    {
        public string buildNameType;
        public string buildDate;
        public int buildNumber;
        
        public void IncrementBuildNumber()
        {
            buildNumber++;
        }

        public void SetBuildDate()
        {
            buildDate = DateTime.Now.Date.ToString("d");
        }
    }
}