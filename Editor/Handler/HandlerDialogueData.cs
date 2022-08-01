namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// The data for handling build dialogue results...
    /// </summary>
    public class HandlerDialogueData
    {
        /// <summary>
        /// The id of the data...
        /// </summary>
        public string Id;
        
        
        /// <summary>
        /// The choice the user made...
        /// </summary>
        public bool Choice;


        /// <summary>
        /// Constructor to setup the data class...
        /// </summary>
        /// <param name="id">The id to set...</param>
        /// <param name="choice">The choice to set...</param>
        public HandlerDialogueData(string id, bool choice)
        {
            Id = id;
            Choice = choice;
        }
    }
}