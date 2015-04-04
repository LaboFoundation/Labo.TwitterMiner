namespace Labo.TwitterMiner.Model
{
    using System.Collections.Generic;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterSearchResult
    {
        public int ApiRateLimitRemainingCount { get; set; }

        public long? SinceID { get; set; }

        public long? MaxID { get; set; }

        private List<TwitterTweet> m_Tweets;
        public List<TwitterTweet> Tweets
        {
            get { return m_Tweets ?? (m_Tweets = new List<TwitterTweet>()); }
            set { m_Tweets = value; }
        }
    }
}
