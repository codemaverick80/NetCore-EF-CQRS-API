namespace Application
{
    using Application.Common.PipelineBehaviours;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region"AutoMapper"
            /* AutoMapper mapping using reflection. We need this configuration here ONLY if we have DTOs objects in this project.
             * Its good item to put all the DTOs object in API project and mapp there.
             */
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());//AutoMapper
            #endregion


            #region "MediatR"
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
            #endregion

            return services;
        }


    }
}
