namespace Labo.TwitterMiner.Entity
{
    using System;

    [Serializable]
    public class TwitterTweetMention
    {
        public long ID { get; set; }

        public long TweetID { get; set; }

        public long UserID { get; set; }
    
        public TwitterTweet TwitterTweet { get; set; }

        public TwitterUser TwitterUser { get; set; }
    }
}
