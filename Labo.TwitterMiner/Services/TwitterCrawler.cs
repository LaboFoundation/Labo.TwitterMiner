namespace Labo.TwitterMiner.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Labo.Common.Ioc;
    using Labo.TwitterMiner.Entity;
    using Labo.TwitterMiner.Model;
    using Labo.TwitterMiner.Services.Twitter.Exceptions;

    internal sealed class TwitterCrawler : ITwitterCrawler
    {
        private readonly ITwitterCrawlHistoryService m_TwitterCrawlHistoryService;
        private readonly ITwitterTweetStorageService m_TwitterTweetStorageService;
        private readonly ITwitterTweetProcessorService m_TwitterTweetProcessorService;

        public TwitterCrawler(ITwitterCrawlHistoryService twitterCrawlHistoryService, ITwitterTweetStorageService twitterTweetStorageService, ITwitterTweetProcessorService twitterTweetProcessorService, IIocContainer iocContainer)
        {
            m_TwitterCrawlHistoryService = twitterCrawlHistoryService;
            m_TwitterTweetStorageService = twitterTweetStorageService;
            m_TwitterTweetProcessorService = twitterTweetProcessorService;
        }

        public void Crawl(string query)
        {
            long? startTweetID = null;
            long? endTweetID = null;

             TwitterSearchOptions searchOptions = new TwitterSearchOptions
                {
                    Q = query
                };

            TwitterCrawlHistory lastCrawlHistory = m_TwitterCrawlHistoryService.GetLastCrawlStatistics(query);
            if (lastCrawlHistory != null)
            {
                startTweetID = lastCrawlHistory.StartTweetID;
                //searchOptions.MaxID = lastCrawlHistory.StartTweetID;
            }

            TwitterSearchResult searchResult;

            do
            {
                try
                {
                    TwitterClient client = new TwitterClient();
                    searchResult = client.Search(searchOptions);
                    if (!endTweetID.HasValue)
                    {
                        endTweetID = searchResult.MaxID;
                    }

                    startTweetID = searchResult.MaxID;
                   
                    searchOptions.MaxID = searchResult.MaxID;
                    searchOptions.SinceID = null;
                    searchOptions.MaxID = null;

                    long maxTweetID = m_TwitterTweetStorageService.GetMaxTweetID(query);
                    if (maxTweetID >= searchResult.Tweets.Max(x => x.ID))
                    {
                        break;
                    }

                    List<TwitterTweet> tweets = searchResult.Tweets;

                    m_TwitterTweetProcessorService.ProcessTweets(tweets);
                }
                catch (TwitterRateLimitReachedException)
                {
                    break;
                }
            } 
            while (searchResult.ApiRateLimitRemainingCount > 0);

            if (startTweetID.HasValue && endTweetID.HasValue)
            {
                m_TwitterCrawlHistoryService.InsertIntoCrawlHistory(startTweetID.Value, endTweetID.Value, query);
            }
        }

        public ITwitterCrawler RegisterTweetProcessor(ITwitterTweetProcessor processor)
        {
            m_TwitterTweetProcessorService.RegisterProcessor(processor);

            return this;
        }
    }
}