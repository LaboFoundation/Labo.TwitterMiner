namespace Labo.TwitterMiner.Data.Mapping
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterMediaMapping : EntityTypeConfiguration<TwitterMedia>
    {
        public TwitterMediaMapping()
        {
            HasKey(t => t.ID);
            ToTable("TwitterMedia");
            Property(t => t.ID).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.DisplayUrl).HasColumnName("DisplayUrl").IsUnicode(false).HasMaxLength(2048);
            Property(t => t.ExpandedUrl).HasColumnName("ExpandedUrl").IsUnicode(false).HasMaxLength(2048);
            Property(t => t.TwitterMediaType).HasColumnName("TwitterMediaType").IsUnicode(false).HasMaxLength(100);
            Property(t => t.MediaUrl).HasColumnName("MediaUrl").IsUnicode(false).HasMaxLength(2048);
            Property(t => t.MediaUrlHttps).HasColumnName("MediaUrlHttps").IsUnicode(false).HasMaxLength(2048);
            Property(t => t.Url).HasColumnName("Url").IsUnicode(false).HasMaxLength(2048);
            HasMany(t => t.TwitterTweets).WithMany(t => t.TwitterMedias).Map(
                m =>
                    {
                        m.ToTable("TwitterTweetMedia");
                        m.MapLeftKey("MediaID");
                        m.MapRightKey("TweetID");
                    });
        }
    }
}
