namespace Labo.TwitterMiner.Entity
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TwitterUrl
    {
        public TwitterUrl()
        {
            TwitterTweets = new HashSet<TwitterTweet>();
        }
    
        public long ID { get; set; }

        public string Value { get; set; }

        public string ExpandedValue { get; set; }

        public string OriginalValue { get; set; }

        public int? SiteID { get; set; }
    
        public ICollection<TwitterTweet> TwitterTweets { get; set; }

        public TwitterSite TwitterSite { get; set; }
    }
}
