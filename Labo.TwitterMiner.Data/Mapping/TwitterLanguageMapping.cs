namespace Labo.TwitterMiner.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterLanguageMapping : EntityTypeConfiguration<TwitterLanguage>
    {
        public TwitterLanguageMapping()
        {
            HasKey(t => t.ID);
            ToTable("TwitterLanguage");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Code).HasColumnName("Code").IsUnicode(false).HasMaxLength(10);
        }
    }
}
