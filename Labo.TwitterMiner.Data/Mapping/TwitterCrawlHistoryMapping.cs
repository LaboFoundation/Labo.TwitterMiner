namespace Labo.TwitterMiner.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterCrawlHistoryMapping : EntityTypeConfiguration<TwitterCrawlHistory>
    {
        public TwitterCrawlHistoryMapping()
        {
            HasKey(t => t.ID);
            ToTable("TwitterCrawlHistory");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.StartTweetID).HasColumnName("StartTweetID");
            Property(t => t.EndTweetID).HasColumnName("EndTweetID");
            Property(t => t.Date).HasColumnName("Date");
            Property(t => t.Query).HasColumnName("HashTag").IsRequired().IsUnicode(false).HasMaxLength(300);
        }
    }
}
