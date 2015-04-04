namespace Labo.TwitterMiner.Services
{
    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterTweetTextCleanerProcessor : ITwitterTweetProcessor
    {
        private readonly ITwitterTextCleaner m_TwitterTextCleaner;

        public TwitterTweetTextCleanerProcessor(ITwitterTextCleaner twitterTextCleaner)
        {
            m_TwitterTextCleaner = twitterTextCleaner;
        }

        public void Process(TwitterTweet tweet)
        {
            tweet.TextClear = m_TwitterTextCleaner.CleanText(tweet.Text);
        }
    }
}
