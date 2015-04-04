namespace Labo.TwitterMiner.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterUrlMapping : EntityTypeConfiguration<TwitterUrl>
    {
        public TwitterUrlMapping()
        {
            HasKey(t => t.ID);
            ToTable("TwitterUrl");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Value).HasColumnName("Value").IsUnicode(false).HasMaxLength(2048);
            Property(t => t.ExpandedValue).HasColumnName("ExpandedValue").IsUnicode(false).HasMaxLength(2048);
            Property(t => t.OriginalValue).HasColumnName("OriginalValue").IsUnicode(false).HasMaxLength(2048);
            Property(t => t.SiteID).HasColumnName("SiteID");
            HasOptional(t => t.TwitterSite).WithMany(t => t.TwitterUrls).HasForeignKey(d => d.SiteID);
        }
    }
}
