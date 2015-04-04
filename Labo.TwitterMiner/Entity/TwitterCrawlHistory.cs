namespace Labo.TwitterMiner.Entity
{
    using System;

    [Serializable]
    public class TwitterCrawlHistory
    {
        public long ID { get; set; }

        public long StartTweetID { get; set; }

        public long EndTweetID { get; set; }

        public DateTime Date { get; set; }

        public string Query { get; set; }
    }
}