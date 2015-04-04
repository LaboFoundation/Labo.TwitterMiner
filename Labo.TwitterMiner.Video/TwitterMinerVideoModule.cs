namespace Labo.TwitterMiner.Video
{
    using System;
    using System.Data.Entity.Infrastructure;

    using Labo.Common.Data.EntityFramework.Session;
    using Labo.Common.Data.Session;
    using Labo.Common.Ioc;
    using Labo.TwitterMiner.Services;
    using Labo.TwitterMiner.Video.Data;
    using Labo.TwitterMiner.Video.Services;
    using Labo.Video;
    using Labo.Video.Services;

    public sealed class TwitterMinerVideoModule : ITwitterCrawlerModule
    {
        private const string TWITTER_VIDEO_PERSISTENCE_PROCESSOR_NAME = "TwitterVideoPersistence";

        private readonly BaseEntityFrameworkSessionFactoryProvider m_EntityFrameworkSessionFactoryProvider;

        private readonly ILaboVideoModule m_LaboVideoModule;

        public TwitterMinerVideoModule(BaseEntityFrameworkSessionFactoryProvider sessionFactoryProvider, ILaboVideoModule laboVideoModule)
        {
            m_EntityFrameworkSessionFactoryProvider = sessionFactoryProvider;  
          
            m_LaboVideoModule = laboVideoModule;
        }

        public void Configure(IIocContainer registry)
        {
            m_EntityFrameworkSessionFactoryProvider.ObjectContextManager.RegisterObjectContextCreator(() => ((IObjectContextAdapter)new HobbiesEntities()).ObjectContext);

            registry.RegisterSingleInstanceNamed<ITwitterTweetProcessor>(x => new TwitterVideoPersistenceProcessor(x.GetInstance<ISessionScopeProvider>(), x.GetInstance<IVideoRetrieveService>()), TWITTER_VIDEO_PERSISTENCE_PROCESSOR_NAME);
        }

        public ITwitterCrawlerModule RegisterVideoInfoRetriever(Func<IIocContainerResolver, IVideoInfoRetriever> creator, string name)
        {
            m_LaboVideoModule.RegisterVideoInfoRetriever(creator, name);

            return this;
        }

        public void RegisterTweetProcessors(TwitterCrawlerBuilder builder)
        {
            builder.RegisterTweetProcessor(TWITTER_VIDEO_PERSISTENCE_PROCESSOR_NAME);
        }
    }
}