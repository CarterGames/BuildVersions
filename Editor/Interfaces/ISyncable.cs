namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Implement to sync version numbers (systematic only)...
    /// </summary>
    public interface ISyncable
    {
        void OnVersionSync(string version);
    }
}