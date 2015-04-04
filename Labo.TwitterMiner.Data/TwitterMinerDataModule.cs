namespace Labo.TwitterMiner.Data
{
    using System.Data.Entity.Infrastructure;

    using Labo.Common.Data.EntityFramework.Session;
    using Labo.Common.Data.Session;
    using Labo.Common.Ioc;
    using Labo.TwitterMiner.Data.Services;
    using Labo.TwitterMiner.Services;

    public sealed class TwitterMinerDataModule : ITwitterCrawlerModule
    {
        private const string TWEET_PERSISTANCE_PROCESSOR_NAME = "TweetPersistanceProcessor";

        private readonly BaseEntityFrameworkSessionFactoryProvider m_EntityFrameworkSessionFactoryProvider;

        public TwitterMinerDataModule(BaseEntityFrameworkSessionFactoryProvider sessionFactoryProvider)
        {
            m_EntityFrameworkSessionFactoryProvider = sessionFactoryProvider;            
        }

        public void Configure(IIocContainer registry)
        {
            m_EntityFrameworkSessionFactoryProvider.ObjectContextManager.RegisterObjectContextCreator(() => ((IObjectContextAdapter)new TwitterMinerDbContext()).ObjectContext);

            registry.RegisterSingleInstance<ITwitterCrawlHistoryService>(x => new TwitterCrawlHistoryService(x.GetInstance<ISessionScopeProvider>()));
            registry.RegisterSingleInstance<ITwitterTweetStorageService>(x => new TwitterTweetStorageService(x.GetInstance<ISessionScopeProvider>()));
            registry.RegisterSingleInstanceNamed<ITwitterTweetProcessor>(x => new TwitterTweetPersistenceProcessor(x.GetInstance<ISessionScopeProvider>()), TWEET_PERSISTANCE_PROCESSOR_NAME);
        }

        public void RegisterTweetProcessors(TwitterCrawlerBuilder builder)
        {
            builder.RegisterTweetProcessor(TWEET_PERSISTANCE_PROCESSOR_NAME);
        }
    }
}