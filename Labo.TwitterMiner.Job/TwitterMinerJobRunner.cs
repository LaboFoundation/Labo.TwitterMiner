namespace Labo.TwitterMiner.Job
{
    using Labo.Common.Data.EntityFramework.Session;
    using Labo.Common.Data.Session;
    using Labo.Common.Data.SqlServer;
    using Labo.Common.Ioc;
    using Labo.TwitterMiner.Data;
    using Labo.TwitterMiner.Services;
    using Labo.TwitterMiner.Video;
    using Labo.Video;
    using Labo.Youtube;

    using IocContainer = Labo.Common.Ioc.Container.IocContainer;

    public sealed class TwitterMinerJobRunner
    {
        public void Run()
        {
            ITwitterCrawler twitterCrawler = InitTwitterCrawler();
            twitterCrawler.Crawl("#video");
        }

        private static ITwitterCrawler InitTwitterCrawler()
        {
            IIocContainer iocContainer = new IocContainer();

            BaseEntityFrameworkSessionFactoryProvider sessionFactoryProvider = GetSessionFactoryProvider(iocContainer);

            ILaboVideoModule laboVideoModule = new LaboVideoModule();
            return new TwitterCrawlerBuilder(iocContainer)
                        .RegisterModule(new TwitterMinerDataModule(sessionFactoryProvider))
                        .RegisterModule(new TwitterMinerVideoModule(sessionFactoryProvider, laboVideoModule))
                        .RegisterModule(new LaboYoutubeVideoModule(laboVideoModule))
                        .Build();
        }

        private static BaseEntityFrameworkSessionFactoryProvider GetSessionFactoryProvider(IIocContainerRegistrar iocContainer)
        {
            SqlServerEntityFrameworkSessionFactoryProvider sessionFactoryProvider = new SqlServerEntityFrameworkSessionFactoryProvider();
            iocContainer.RegisterSingleInstance<ISessionFactoryProvider>(x => sessionFactoryProvider);
            iocContainer.RegisterSingleInstance<ISessionScopeProvider>(x => new SessionScopeProvider(x.GetInstance<ISessionFactoryProvider>()));
            return sessionFactoryProvider;
        }
    }
}
