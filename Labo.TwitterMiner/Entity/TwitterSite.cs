namespace Labo.TwitterMiner.Entity
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TwitterSite
    {
        public TwitterSite()
        {
            TwitterUrls = new HashSet<TwitterUrl>();
        }
    
        public int ID { get; set; }

        public string SiteUrl { get; set; }
    
        public ICollection<TwitterUrl> TwitterUrls { get; set; }
    }
}
