namespace Infrastructure.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class TrackConfiguration : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Duration).HasMaxLength(20);
            builder.Property(e => e.TrackName).IsRequired().HasMaxLength(100);

            builder.HasOne(track => track.Album)
                .WithMany(album => album.Tracks)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Track_ToAlbum");
        }
    }


}
