using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.GenreName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Description).HasMaxLength(4096);
        }
    }


}
