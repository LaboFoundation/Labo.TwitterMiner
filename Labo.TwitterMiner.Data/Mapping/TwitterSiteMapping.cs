namespace Labo.TwitterMiner.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterSiteMapping : EntityTypeConfiguration<TwitterSite>
    {
        public TwitterSiteMapping()
        {
            HasKey(t => t.ID);
            ToTable("TwitterSite");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.SiteUrl).HasColumnName("SiteUrl").IsRequired().IsUnicode(false).HasMaxLength(2048);
        }
    }
}
