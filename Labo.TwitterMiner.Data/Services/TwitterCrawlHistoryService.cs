namespace Labo.TwitterMiner.Data.Services
{
    using System;
    using System.Linq;

    using Labo.Common.Data.Session;
    using Labo.TwitterMiner.Entity;
    using Labo.TwitterMiner.Services;

    public sealed class TwitterCrawlHistoryService : ITwitterCrawlHistoryService
    {
        private readonly ISessionScopeProvider m_SessionScopeProvider;

        public TwitterCrawlHistoryService(ISessionScopeProvider sessionScopeProvider)
        {
            m_SessionScopeProvider = sessionScopeProvider;
        }

        public TwitterCrawlHistory GetLastCrawlStatistics(string query)
        {
            using (ISessionScope sessionScope = m_SessionScopeProvider.CreateSessionScope())
            {
                return
                    sessionScope.GetRepository<TwitterCrawlHistory>()
                        .Query()
                        .Where(x => x.Query == query)
                        .OrderByDescending(x => x.Date)
                        .Take(1)
                        .SingleOrDefault();
            }
           
        }

        public void InsertIntoCrawlHistory(long startTweetID, long endTweetID, string hashTag)
        {
            using (ISessionScope sessionScope = m_SessionScopeProvider.CreateSessionScope())
            {
                TwitterCrawlHistory history = new TwitterCrawlHistory
                {
                    StartTweetID = startTweetID,
                    EndTweetID = endTweetID,
                    Query = hashTag,
                    Date = DateTime.Now,
                };
                sessionScope.GetRepository<TwitterCrawlHistory>().Insert(history);
                sessionScope.Complete();
            }
        }
    }
}
