namespace Labo.TwitterMiner.Entity
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TwitterMedia
    {
        public TwitterMedia()
        {
            TwitterMediaSizes = new HashSet<TwitterMediaSize>();
            TwitterTweets = new HashSet<TwitterTweet>();
        }
    
        public long ID { get; set; }
        public string DisplayUrl { get; set; }
        public string ExpandedUrl { get; set; }
        public string TwitterMediaType { get; set; }
        public string MediaUrl { get; set; }
        public string MediaUrlHttps { get; set; }
        public string Url { get; set; }
    
        public ICollection<TwitterMediaSize> TwitterMediaSizes { get; set; }
        public ICollection<TwitterTweet> TwitterTweets { get; set; }
    }
}
