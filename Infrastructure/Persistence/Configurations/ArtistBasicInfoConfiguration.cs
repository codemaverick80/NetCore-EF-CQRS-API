namespace Infrastructure.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class ArtistBasicInfoConfiguration : IEntityTypeConfiguration<ArtistBasicInfo>

    {
        public void Configure(EntityTypeBuilder<ArtistBasicInfo> builder)
        {
            builder.HasKey(e => e.ArtistId);

            builder.Property(e => e.ArtistId).ValueGeneratedNever();

            builder.Property(e => e.AlsoKnownAs).HasMaxLength(500);

            builder.Property(e => e.Born).HasMaxLength(100);

            builder.Property(e => e.Died).HasMaxLength(100);

            builder.HasOne(d => d.Artist)
                .WithOne(p => p.ArtistBasicInfo)
                .HasForeignKey<ArtistBasicInfo>(d => d.ArtistId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ArtistBasicInfo_ToArtist");
        }
    }


}
