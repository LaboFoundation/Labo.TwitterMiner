namespace Labo.TwitterMiner.Entity
{
    using System;

    [Serializable]
    public class TwitterMediaSize
    {
        public long MediaID { get; set; }

        public int SizeID { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Resize { get; set; }
    
        public TwitterMedia TwitterMedia { get; set; }
    }
}
