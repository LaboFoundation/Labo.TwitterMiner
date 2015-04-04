namespace Labo.TwitterMiner.Services
{
    public interface ITwitterCrawler
    {
        void Crawl(string query);

        ITwitterCrawler RegisterTweetProcessor(ITwitterTweetProcessor processor);
    }
}
