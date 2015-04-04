namespace Labo.TwitterMiner.Entity
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TwitterHashTag
    {
        public TwitterHashTag()
        {
            TwitterTweets = new HashSet<TwitterTweet>();
        }
    
        public long ID { get; set; }

        public string Name { get; set; }
    
        public ICollection<TwitterTweet> TwitterTweets { get; set; }
    }
}
