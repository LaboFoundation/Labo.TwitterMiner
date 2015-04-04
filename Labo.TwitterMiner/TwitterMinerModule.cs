namespace Labo.TwitterMiner
{
    using Labo.Common.Ioc;
    using Labo.TwitterMiner.Services;

    public sealed class TwitterMinerModule : IIocModule
    {
        public void Configure(IIocContainer registry)
        {                    
            registry.RegisterSingleInstance<ITwitterCrawler>(x => new TwitterCrawler(x.GetInstance<ITwitterCrawlHistoryService>(), x.GetInstance<ITwitterTweetStorageService>(), x.GetInstance<ITwitterTweetProcessorService>(), x.GetInstance<IIocContainer>()));
            registry.RegisterSingleInstance<ITwitterTextCleaner>(x => new TwitterTextCleaner());
            registry.RegisterSingleInstance<ITwitterSiteResolver>(x => new TwitterSiteResolver());
            registry.RegisterSingleInstance<IUrlExpander>(x => new UrlExpander());

            registry.RegisterSingleInstanceNamed<ITwitterTweetProcessor>(x => new TwitterTweetTextCleanerProcessor(x.GetInstance<ITwitterTextCleaner>()), "TweetTextCleaner");
            registry.RegisterSingleInstanceNamed<ITwitterTweetProcessor>(x => new TwitterTweetUrlExpanderProcessor(x.GetInstance<ITwitterSiteResolver>(), x.GetInstance<IUrlExpander>()), "UrlExpander");

            registry.RegisterSingleInstance<ITwitterTweetProcessorService>(
                x =>
                    {
                        TwitterTweetProcessorService twitterTweetProcessorService = new TwitterTweetProcessorService();
                        twitterTweetProcessorService.RegisterProcessor(registry.GetInstance<ITwitterTweetProcessor>("TweetTextCleaner"));
                        twitterTweetProcessorService.RegisterProcessor(registry.GetInstance<ITwitterTweetProcessor>("UrlExpander"));
                        return twitterTweetProcessorService;
                    });
        }
    }
}