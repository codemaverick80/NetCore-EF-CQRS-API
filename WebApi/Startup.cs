using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Common.Extensions;
using WebApi.Common.Middleware;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }


        public ApplicationConfiguration ValidateApplicationConfiguration(IServiceCollection services)
        {
            ApplicationConfiguration applicationConfiguration = new ApplicationConfiguration(Configuration);
            services.AddSingleton<IApplicationConfiguration>(applicationConfiguration);
            return applicationConfiguration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ApplicationConfiguration appConfiguration = this.ValidateApplicationConfiguration(services);

            services.AddApplication();
            services.AddInfrastructure();
            services.AddDatabases(appConfiguration);
            //services.AddAspNetIdentityDatabase(appConfiguration); // For Asp.Net core Identity system

            services.AddControllers();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();          

            #region "Add Swagger"
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Music API",
                        Description = "This is music api",
                        Version = "v1"
                    }
                    );
            });
            #endregion


        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Music API"));
            }

            app.UseCustomExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
