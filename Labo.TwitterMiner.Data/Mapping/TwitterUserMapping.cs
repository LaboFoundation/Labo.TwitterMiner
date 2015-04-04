namespace Labo.TwitterMiner.Data.Mapping
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterUserMapping : EntityTypeConfiguration<TwitterUser>
    {
        public TwitterUserMapping()
        {
            HasKey(t => t.ID);
            ToTable("TwitterUser");
            Property(t => t.ID).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.ScreenName).HasColumnName("ScreenName").IsUnicode(false).HasMaxLength(50);
            Property(t => t.Name).HasColumnName("Name").IsUnicode(false).HasMaxLength(150);
            Property(t => t.Description).HasColumnName("Description").IsUnicode(false).HasMaxLength(300);
            Property(t => t.LanguageID).HasColumnName("LanguageID");
            Property(t => t.Location).HasColumnName("Location").IsUnicode(false).HasMaxLength(300);
            Property(t => t.ProfileImageUrl).HasColumnName("ProfileImageUrl").IsUnicode(false).HasMaxLength(2048);
            Property(t => t.ProfileImageUrlHttps).HasColumnName("ProfileImageUrlHttps").IsUnicode(false).HasMaxLength(2048);
            Property(t => t.Url).HasColumnName("Url").IsUnicode(false).HasMaxLength(2048);
            Property(t => t.CreateDate).HasColumnName("CreateDate");
            HasOptional(t => t.TwitterLanguage).WithMany(t => t.TwitterUsers).HasForeignKey(d => d.LanguageID);
        }
    }
}

