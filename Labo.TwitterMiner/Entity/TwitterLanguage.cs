namespace Labo.TwitterMiner.Entity
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TwitterLanguage
    {
        public TwitterLanguage()
        {
            TwitterUsers = new HashSet<TwitterUser>();
        }
    
        public int ID { get; set; }

        public string Code { get; set; }
    
        public ICollection<TwitterUser> TwitterUsers { get; set; }
    }
}
