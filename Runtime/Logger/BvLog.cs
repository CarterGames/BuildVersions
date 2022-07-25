using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    /// <summary>
    /// Handles the logs thrown by the asset...
    /// </summary>
    public static class BvLog
    {
        private const string LogPrefix = "<color=#67C454><b>Build Versions</b></color> | ";
        private const string WarningPrefix = "<color=#D6BA64><b>Warning</b></color> | ";
        private const string ErrorPrefix = "<color=#E77A7A><b>Error</b></color> | ";

        public static void Normal(string message) => Debug.Log($"{LogPrefix}{message}");
        public static void Warning(string message) => Debug.LogWarning($"{LogPrefix}{WarningPrefix}{message}");
        public static void Error(string message) => Debug.LogError($"{LogPrefix}{ErrorPrefix}{message}");
    }
}