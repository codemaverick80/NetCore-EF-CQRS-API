using Infrastructure.Configuration;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace WebApi.Common.Extensions
{
    public static class ApplicationServiceExtensions
    {

        public static IServiceCollection AddDatabases(this IServiceCollection services, ApplicationConfiguration options)
        {
            var opt = OptionsBuilder(options.DbProvider, options.ApiDbConnectionString);
            services.AddDbContext<ApplicationDbContext>(opt);
            return services;
        }

        //// TODO : If we use Asp.Net core Identity system for Authentication
        //public static IServiceCollection AddAspNetIdentityDatabase(this IServiceCollection services, ApplicationConfiguration options)
        //{
        //    var optionsAction = OptionsBuilder(options.DbProvider, options.AspNetIdentityDbConnection);
        //    services.AddDbContext<IdentityDbContext>(optionsAction);
        //    return services;
        //}


        private static Action<DbContextOptionsBuilder> OptionsBuilder(string dbProvider, string dbConnectionString)
        {
            string migrationAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;
            switch (dbProvider)
            {
                case "SqlServer":
                    return builder => builder.UseSqlServer(dbConnectionString, options => options.MigrationsAssembly(migrationAssembly));
                case "Sqlite":
                    return builder => builder.UseSqlite(dbConnectionString, options => options.MigrationsAssembly(migrationAssembly));
                case "MySql":
                    return builder => builder.UseMySql(dbConnectionString, options => options.MigrationsAssembly(migrationAssembly));
                case "PostreSql":
                case "PostgreSql":
                    return builder => builder.UseNpgsql(dbConnectionString, options => options.MigrationsAssembly(migrationAssembly));
                default:
                    throw new NotImplementedException("Database Provider for " + dbProvider + " not supported.");
            }
        }


    }
}
