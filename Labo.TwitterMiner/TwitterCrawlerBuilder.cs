namespace Labo.TwitterMiner
{
    using Labo.Common.Ioc;
    using Labo.TwitterMiner.Services;

    public sealed class TwitterCrawlerBuilder
    {
        private readonly IIocContainer m_IocContainer;

        private ITwitterCrawlHistoryService m_TwitterCrawlHistoryService;

        private ITwitterTweetStorageService m_TwitterTweetStorageService;

        public TwitterCrawlerBuilder(IIocContainer iocContainer)
            : this(iocContainer, null, null)
        {
        }

        public TwitterCrawlerBuilder(IIocContainer iocContainer, ITwitterCrawlHistoryService twitterCrawlHistoryService, ITwitterTweetStorageService twitterTweetStorageService)
        {
            m_IocContainer = iocContainer;
            m_TwitterCrawlHistoryService = twitterCrawlHistoryService;
            m_TwitterTweetStorageService = twitterTweetStorageService;

            Init();
        }

        private void Init()
        {
            m_IocContainer.RegisterSingleInstance(x => m_IocContainer);
            m_IocContainer.RegisterModule(new TwitterMinerModule());
        }

        public TwitterCrawlerBuilder RegisterTweetProcessor(ITwitterTweetProcessor twitterTweetProcessor)
        {
            ITwitterTweetProcessorService twitterTweetProcessorService = m_IocContainer.GetInstance<ITwitterTweetProcessorService>();
            twitterTweetProcessorService.RegisterProcessor(twitterTweetProcessor);
            return this;
        }

        public TwitterCrawlerBuilder RegisterTweetProcessor(string name)
        {
            RegisterTweetProcessor(m_IocContainer.GetInstance<ITwitterTweetProcessor>(name));
            return this;
        }

        public ITwitterCrawler Build()
        {
            if (m_TwitterCrawlHistoryService != null)
            {
                m_IocContainer.RegisterSingleInstance(x => m_TwitterCrawlHistoryService);
            }

            if (m_TwitterTweetStorageService != null)
            {
                m_IocContainer.RegisterSingleInstance(x => m_TwitterTweetStorageService);
            }

            return m_IocContainer.GetInstance<ITwitterCrawler>();
        }

        public TwitterCrawlerBuilder RegisterModule(ITwitterCrawlerModule module)
        {
            RegisterModule(module);

            module.RegisterTweetProcessors(this);
            
            return this;
        }

        public TwitterCrawlerBuilder RegisterModule(IIocModule module)
        {
            m_IocContainer.RegisterModule(module);

            return this;
        }
    }
}
