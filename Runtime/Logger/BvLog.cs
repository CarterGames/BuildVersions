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

        
        /// <summary>
        /// Displays a normal debug message for the build versions asset...
        /// </summary>
        /// <param name="message">The message to show...</param>
        public static void Normal(string message)
        {
#if Disable_BvLogs
            Debug.Log($"{LogPrefix}{message}");   
#endif
        }
        
        
        /// <summary>
        /// Displays a warning debug message for the build versions asset...
        /// </summary>
        /// <param name="message">The message to show...</param>
        public static void Warning(string message) 
        {
#if Disable_BvLogs
            Debug.LogWarning($"{LogPrefix}{WarningPrefix}{message}");
#endif
        }
        
        
        /// <summary>
        /// Displays a error debug message for the build versions asset...
        /// </summary>
        /// <param name="message">The message to show...</param>
        public static void Error(string message)
        {
#if Disable_BvLogs
            Debug.LogError($"{LogPrefix}{ErrorPrefix}{message}");
#endif
        }
    }
}