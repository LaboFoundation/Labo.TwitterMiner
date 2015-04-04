namespace Labo.TwitterMiner.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterTweetMentionMapping : EntityTypeConfiguration<TwitterTweetMention>
    {
        public TwitterTweetMentionMapping()
        {
            HasKey(t => t.ID);
            ToTable("TwitterTweetMention");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.TweetID).HasColumnName("TweetID");
            Property(t => t.UserID).HasColumnName("UserID");
            HasRequired(t => t.TwitterTweet).WithMany(t => t.TwitterMentions).HasForeignKey(d => d.TweetID);
            HasRequired(t => t.TwitterUser).WithMany(t => t.TwitterTweetMentions).HasForeignKey(d => d.UserID);
        }
    }
}
