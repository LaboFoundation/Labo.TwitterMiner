namespace Labo.TwitterMiner.Services
{
    using System.Collections.Generic;

    using Labo.TwitterMiner.Entity;

    internal interface ITwitterTweetProcessorService
    {
        void ProcessTweets(IList<TwitterTweet> tweets);

        void RegisterProcessor(ITwitterTweetProcessor twitterTweetProcessor);
    }
}