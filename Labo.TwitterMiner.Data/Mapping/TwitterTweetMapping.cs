namespace Labo.TwitterMiner.Data.Mapping
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterTweetMapping : EntityTypeConfiguration<TwitterTweet>
    {
        public TwitterTweetMapping()
        {
            HasKey(t => t.ID);
            ToTable("TwitterTweet");
            Property(t => t.ID).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.OwnerQuery).HasMaxLength(300);
            Property(t => t.UserID).HasColumnName("UserID");
            Property(t => t.InReplyToScreenName).HasColumnName("InReplyToScreenName").IsUnicode(false).HasMaxLength(50);
            Property(t => t.InReplyToStatusId).HasColumnName("InReplyToStatusId");
            Property(t => t.InReplyToUserId).HasColumnName("InReplyToUserId");
            Property(t => t.Latitude).HasColumnName("Latitude");
            Property(t => t.Longitude).HasColumnName("Longitude");
            Property(t => t.PlaceID).HasColumnName("PlaceID").IsUnicode(false).HasMaxLength(20);
            Property(t => t.RetweetCount).HasColumnName("RetweetCount");
            Property(t => t.Source).HasColumnName("Source").IsUnicode(false).HasMaxLength(3000);
            Property(t => t.Text).HasColumnName("Text").IsUnicode(false).HasMaxLength(3000);
            Property(t => t.TextAsHtml).HasColumnName("TextAsHtml").IsUnicode(false).HasMaxLength(3000);
            Property(t => t.TextDecoded).HasColumnName("TextDecoded").IsUnicode(false).HasMaxLength(3000);
            Property(t => t.CreateDate).HasColumnName("CreateDate");
            Property(t => t.TextClear).HasColumnName("TextClear").IsUnicode(false).HasMaxLength(3000);
            HasOptional(t => t.TwitterPlace).WithMany(t => t.TwitterTweets).HasForeignKey(d => d.PlaceID);
            HasRequired(t => t.TwitterUser).WithMany(t => t.TwitterTweets).HasForeignKey(d => d.UserID);
            HasMany(t => t.TwitterUrls).WithMany(t => t.TwitterTweets).Map(
                m =>
                    {
                        m.ToTable("TwitterTweetUrl");
                        m.MapLeftKey("TweetID");
                        m.MapRightKey("UrlID");
                    });
        }
    }
}
