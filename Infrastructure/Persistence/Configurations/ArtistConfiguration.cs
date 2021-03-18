namespace Infrastructure.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {


            builder.Property(e => e.Id).HasColumnName("Id");

            builder.Property(e => e.ArtistName).IsRequired().HasMaxLength(150);

            builder.Property(e => e.YearActive).HasMaxLength(50);

            builder.Property(e => e.Biography).HasMaxLength(4096);

            builder.Property(e => e.ThumbnailTag).HasMaxLength(50);

            builder.Property(e => e.LargeThumbnail).HasMaxLength(100);

            builder.Property(e => e.SmallThumbnail).HasMaxLength(100);




        }
    }
}
