namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Implement to sync version numbers (semantic only)...
    /// </summary>
    public interface ISyncable
    {
        void OnVersionSync(string version);
    }
}