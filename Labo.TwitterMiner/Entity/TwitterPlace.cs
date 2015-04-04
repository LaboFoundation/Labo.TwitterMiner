namespace Labo.TwitterMiner.Entity
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TwitterPlace
    {
        public TwitterPlace()
        {
            TwitterTweets = new HashSet<TwitterTweet>();
        }
    
        public string ID { get; set; }

        public int? CountryID { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string PlaceType { get; set; }

        public string Url { get; set; }
    
        public TwitterCountry TwitterCountry { get; set; }

        public ICollection<TwitterTweet> TwitterTweets { get; set; }
    }
}
