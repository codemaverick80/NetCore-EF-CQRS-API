using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.AlbumName)
                    .IsRequired()
                    .HasMaxLength(200);

            builder.Property(e => e.AlbumUrl).HasMaxLength(500);

            builder.Property(e => e.Label).HasMaxLength(200);

            builder.Property(e => e.LargeThumbnail).HasMaxLength(100);

            builder.Property(e => e.MediumThumbnail).HasMaxLength(100);

            builder.Property(e => e.SmallThumbnail).HasMaxLength(100);

            builder.Property(e => e.ThumbnailTag).HasMaxLength(50);

            builder.HasOne(album => album.Artist)
                .WithMany(artist => artist.Albums)
                .HasForeignKey(album => album.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Album_ToArtist");

            builder.HasOne(album => album.Genre)
                .WithMany(genre => genre.Albums)
                .HasForeignKey(album => album.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Album_ToGenre");
        }
    }


}
