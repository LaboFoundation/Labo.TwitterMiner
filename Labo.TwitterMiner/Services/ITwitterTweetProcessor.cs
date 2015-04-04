namespace Labo.TwitterMiner.Services
{
    using Labo.TwitterMiner.Entity;

    public interface ITwitterTweetProcessor
    {
        void Process(TwitterTweet tweet);
    }
}