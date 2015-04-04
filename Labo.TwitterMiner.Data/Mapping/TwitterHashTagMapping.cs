namespace Labo.TwitterMiner.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterHashTagMapping : EntityTypeConfiguration<TwitterHashTag>
    {
        public TwitterHashTagMapping()
        {
            HasKey(t => t.ID);
            ToTable("TwitterHashTag");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Name).HasColumnName("Name").IsRequired().IsUnicode(false).HasMaxLength(300);
            HasMany(t => t.TwitterTweets).WithMany(t => t.TwitterHashTags).Map(
                m =>
                    {
                        m.ToTable("TwitterTweetHashTag");
                        m.MapLeftKey("HashTagID");
                        m.MapRightKey("TweetID");
                    });
        }
    }
}
