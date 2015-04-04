namespace Labo.TwitterMiner.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterPlaceMapping : EntityTypeConfiguration<TwitterPlace>
    {
        public TwitterPlaceMapping()
        {
            HasKey(t => t.ID);
            ToTable("TwitterPlace");
            Property(t => t.ID).HasColumnName("ID").IsRequired().IsUnicode(false).HasMaxLength(20);
            Property(t => t.CountryID).HasColumnName("CountryID");
            Property(t => t.Name).HasColumnName("Name").IsUnicode(false).HasMaxLength(300);
            Property(t => t.FullName).HasColumnName("FullName").IsUnicode(false).HasMaxLength(500);
            Property(t => t.PlaceType).HasColumnName("PlaceType").IsUnicode(false).HasMaxLength(30);
            Property(t => t.Url).HasColumnName("Url").IsUnicode(false).HasMaxLength(2048);
            HasOptional(t => t.TwitterCountry).WithMany(t => t.TwitterPlaces).HasForeignKey(d => d.CountryID);
        }
    }
}
