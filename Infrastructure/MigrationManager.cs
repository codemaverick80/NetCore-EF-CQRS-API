namespace Infrastructure
{
    using Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Linq;
    /*
     * Note : While this approach is productive for local development and testing of migrations, 
     * this approach is inappropriate for managing production databases. 
     * Please read https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli
     */
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    try
                    {
                        if (appContext.Database.GetPendingMigrations().Count() > 0)
                        {
                            appContext.Database.Migrate();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return host;
        }
    }
}
