using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
   public interface IApplicationDbContext
    {
        DbSet<Genre> Genre { get; set; }
        DbSet<Artist> Artist { get; set; }
        DbSet<Album> Album { get; set; }
        DbSet<Track> Track { get; set; }
        DbSet<ArtistBasicInfo> ArtistBasicInfo { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);


    }
}
