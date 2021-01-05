using Application.CQRS.SeedDatabase.Commands;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi
{
    public class Program
    {
        //public static void Main(string[] args)
        public static async Task Main(string[] args)
        {
           var host= CreateHostBuilder(args).Build();


            #region "DO NOT use in PROD"
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    ////Automatic database migration, First we need to create database migration by command line the it will work 
                    var appDbContext = services.GetRequiredService<ApplicationDbContext>();
                    appDbContext.Database.Migrate();


                    ////var identityContext = services.GetRequiredService<IdentityDbContext>();
                    ////identityContext.Database.Migrate();


                    ////Database seeding
                    var mediator = services.GetRequiredService<IMediator>();
                    await mediator.Send(new SeedTestDataCommand(), CancellationToken.None);

                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during database migration or seeding the data");

                    throw;
                }

            }

            #endregion

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
