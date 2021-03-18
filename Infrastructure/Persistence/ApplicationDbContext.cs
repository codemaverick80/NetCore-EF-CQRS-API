namespace Infrastructure.Persistence
{
    using Application.Common.Interfaces;
    using Common;
    using Domain.Common;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IDateTime dateTime;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService, IDateTime dt)
            : base(options)
        {
            this.currentUserService = currentUserService;
            dateTime = dt;
        }


        public DbSet<Genre> Genre { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Track> Track { get; set; }
        public DbSet<ArtistBasicInfo> ArtistBasicInfo { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var entity in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.CreatedBy = Guid.Parse(currentUserService.UserId);
                        entity.Entity.DateCreated = dateTime.Now;
                        entity.Entity.IsDeleted = false;
                        break;
                    case EntityState.Modified:
                        entity.Entity.ModifiedBy = Guid.Parse(currentUserService.UserId);
                        entity.Entity.DateModified = dateTime.Now;
                        entity.Entity.IsDeleted = false;
                        break;
                    case EntityState.Deleted:
                        entity.State = EntityState.Modified;
                        entity.Entity.DateModified = dateTime.Now;
                        entity.Entity.ModifiedBy = Guid.Parse(currentUserService.UserId);
                        entity.Entity.IsDeleted = true;
                        break;
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            SetGlobalQueryFilters(modelBuilder);
        }


        #region "Soft Delete"
        private void SetGlobalQueryFilters(ModelBuilder modelBuilder)
        {
            //This feature was introduced in EF Core 2.0
            //https://docs.microsoft.com/en-us/ef/core/querying/filters
            modelBuilder.Entity<Album>().HasQueryFilter(q => !q.IsDeleted);
            modelBuilder.Entity<Genre>().HasQueryFilter(q => !q.IsDeleted);
            modelBuilder.Entity<Artist>().HasQueryFilter(q => !q.IsDeleted);
            modelBuilder.Entity<ArtistBasicInfo>().HasQueryFilter(q => !q.IsDeleted);
            modelBuilder.Entity<Track>().HasQueryFilter(q => !q.IsDeleted);

        }

        #endregion



    }
}
