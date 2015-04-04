namespace Labo.TwitterMiner.Data.Mapping
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterMediaSizeMapping : EntityTypeConfiguration<TwitterMediaSize>
    {
        public TwitterMediaSizeMapping()
        {
            HasKey(t => new { t.MediaID, t.SizeID });
            ToTable("TwitterMediaSize");
            Property(t => t.MediaID).HasColumnName("MediaID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.SizeID).HasColumnName("SizeID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.Width).HasColumnName("Width");
            Property(t => t.Height).HasColumnName("Height");
            Property(t => t.Resize).HasColumnName("Resize").IsUnicode(false).HasMaxLength(2048);
            HasRequired(t => t.TwitterMedia).WithMany(t => t.TwitterMediaSizes).HasForeignKey(d => d.MediaID);
        }
    }
}
