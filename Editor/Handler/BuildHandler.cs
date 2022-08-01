using System.Collections.Generic;
using System.Linq;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Handles the build dialogue results...
    /// </summary>
    public static class BuildHandler
    {
        /// <summary>
        /// Stores the handler data for the updaters to use...
        /// </summary>
        private static List<HandlerDialogueData> Data = new List<HandlerDialogueData>();


        /// <summary>
        /// Registers a class into the handler system when a build is made...
        /// </summary>
        /// <param name="data">The data to add</param>
        public static void Register(HandlerDialogueData data) => Data.Add(data);


        /// <summary>
        /// Gets a handler data to use...
        /// </summary>
        /// <typeparam name="T">The class</typeparam>
        /// <returns>The result of the get</returns>
        public static HandlerDialogueData Get<T>()
        {
            return Data.FirstOrDefault(t => t.Id.Equals(BuildVersionsEditorUtil.GetClassName<T>()));
        }

        
        /// <summary>
        /// Clears the data for a new build...
        /// </summary>
        public static void Clear() => Data.Clear();
    }
}