namespace Labo.TwitterMiner.Entity
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TwitterTweet
    {
        public TwitterTweet()
        {
            TwitterHashTags = new HashSet<TwitterHashTag>();
            TwitterMedias = new HashSet<TwitterMedia>();
            TwitterUrls = new HashSet<TwitterUrl>();
            TwitterMentions = new HashSet<TwitterTweetMention>();
        }
    
        public long ID { get; set; }

        public long UserID { get; set; }

        public string OwnerQuery { get; set; }

        public string InReplyToScreenName { get; set; }

        public long? InReplyToStatusId { get; set; }

        public long? InReplyToUserId { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public string PlaceID { get; set; }

        public int RetweetCount { get; set; }

        public string Source { get; set; }

        public string Text { get; set; }

        public string TextAsHtml { get; set; }

        public string TextDecoded { get; set; }

        public DateTime? CreateDate { get; set; }

        public string TextClear { get; set; }
    
        public TwitterPlace TwitterPlace { get; set; }

        public TwitterUser TwitterUser { get; set; }

        public ICollection<TwitterHashTag> TwitterHashTags { get; set; }

        public ICollection<TwitterMedia> TwitterMedias { get; set; }

        public ICollection<TwitterUrl> TwitterUrls { get; set; }

        public ICollection<TwitterTweetMention> TwitterMentions { get; set; }
    }
}
