namespace Labo.TwitterMiner.Services
{
    using Labo.TwitterMiner.Entity;

    public interface ITwitterCrawlHistoryService
    {
        TwitterCrawlHistory GetLastCrawlStatistics(string query);

        void InsertIntoCrawlHistory(long startTweetID, long endTweetID, string hashTag);
    }
}