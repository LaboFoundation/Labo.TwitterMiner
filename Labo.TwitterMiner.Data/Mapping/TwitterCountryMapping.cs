namespace Labo.TwitterMiner.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterCountryMapping : EntityTypeConfiguration<TwitterCountry>
    {
        public TwitterCountryMapping()
        {
            HasKey(t => t.ID);
            ToTable("TwitterCountry");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Code).HasColumnName("Code").IsRequired().IsUnicode(false).HasMaxLength(10);
            Property(t => t.Name).HasColumnName("Name").IsUnicode(false).HasMaxLength(150);
        }
    }
}
