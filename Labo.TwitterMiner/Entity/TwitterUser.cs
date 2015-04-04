namespace Labo.TwitterMiner.Entity
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TwitterUser
    {
        public TwitterUser()
        {
            TwitterTweets = new HashSet<TwitterTweet>();
            TwitterTweetMentions = new HashSet<TwitterTweetMention>();
        }
    
        public long ID { get; set; }

        public string ScreenName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? LanguageID { get; set; }

        public string Location { get; set; }

        public string ProfileImageUrl { get; set; }

        public string ProfileImageUrlHttps { get; set; }

        public string Url { get; set; }

        public DateTime? CreateDate { get; set; }
    
        public TwitterLanguage TwitterLanguage { get; set; }

        public ICollection<TwitterTweet> TwitterTweets { get; set; }

        public ICollection<TwitterTweetMention> TwitterTweetMentions { get; set; }
    }
}
