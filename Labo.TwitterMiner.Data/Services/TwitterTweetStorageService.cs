namespace Labo.TwitterMiner.Data.Services
{
    using System.Linq;

    using Labo.Common.Data.Session;
    using Labo.TwitterMiner.Entity;
    using Labo.TwitterMiner.Services;

    internal sealed class TwitterTweetStorageService : ITwitterTweetStorageService
    {
        private readonly ISessionScopeProvider m_SessionScopeProvider;

        public TwitterTweetStorageService(ISessionScopeProvider sessionScopeProvider)
        {
            m_SessionScopeProvider = sessionScopeProvider;
        }

        public long GetMaxTweetID(string query)
        {
            using (ISessionScope sessionScope = m_SessionScopeProvider.CreateSessionScope())
            {
                return sessionScope.GetRepository<TwitterTweet>()
                        .Query()
                        .Where(x => x.OwnerQuery == query)
                        .Select(x => x.ID)
                        .DefaultIfEmpty()
                        .Max(x => x);
            }
        }
    }
}
