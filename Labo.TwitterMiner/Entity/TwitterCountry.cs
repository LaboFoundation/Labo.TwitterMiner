namespace Labo.TwitterMiner.Entity
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TwitterCountry
    {
        public TwitterCountry()
        {
            TwitterPlaces = new HashSet<TwitterPlace>();
        }
    
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    
        public ICollection<TwitterPlace> TwitterPlaces { get; set; }
    }
}
