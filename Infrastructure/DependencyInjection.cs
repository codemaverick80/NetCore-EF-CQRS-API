namespace Infrastructure
{
    using Application.Common.Interfaces;
    using Common;
    using Infrastructure.Persistence;
    using Infrastructure.Services;
    using Microsoft.Extensions.DependencyInjection;
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());



            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString("MusicDbConnectionString"),
            //    mig => mig.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            //});


            return services;
        }

    }
}
