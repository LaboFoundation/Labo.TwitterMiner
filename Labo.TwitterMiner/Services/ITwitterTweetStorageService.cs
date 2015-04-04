namespace Labo.TwitterMiner.Services
{
    public interface ITwitterTweetStorageService
    {
        long GetMaxTweetID(string query);
    }
}