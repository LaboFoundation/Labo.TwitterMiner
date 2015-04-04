namespace Labo.TwitterMiner.Services
{
    using Labo.Common.Ioc;

    public interface ITwitterCrawlerModule : IIocModule
    {
        void RegisterTweetProcessors(TwitterCrawlerBuilder builder);
    }
}