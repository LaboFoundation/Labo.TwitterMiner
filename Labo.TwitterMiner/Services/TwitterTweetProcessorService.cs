namespace Labo.TwitterMiner.Services
{
    using System.Collections.Generic;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterTweetProcessorService : ITwitterTweetProcessorService
    {
        private readonly IList<ITwitterTweetProcessor> m_TwitterTweetProcessors;

        public TwitterTweetProcessorService()
        {
            m_TwitterTweetProcessors = new List<ITwitterTweetProcessor>();
        }

        public void RegisterProcessor(ITwitterTweetProcessor twitterTweetProcessor)
        {
            m_TwitterTweetProcessors.Add(twitterTweetProcessor);
        }

        public void ProcessTweets(IList<TwitterTweet> tweets)
        {
            for (int i = 0; i < tweets.Count; i++)
            {
                TwitterTweet twitterTweet = tweets[i];

                for (int j = 0; j < m_TwitterTweetProcessors.Count; j++)
                {
                    ITwitterTweetProcessor twitterTweetProcessor = m_TwitterTweetProcessors[j];
                    twitterTweetProcessor.Process(twitterTweet);
                }
            }
        }
    }
}