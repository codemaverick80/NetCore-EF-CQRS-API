using Application.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Common.Middleware
{
    /*
     * Handling Exceptions using middleware
     */
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {            
            var code = HttpStatusCode.InternalServerError;
            var incidentId = Guid.NewGuid();
            string result;
            
            switch (exception)
            {
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    //result = badRequestException.Message;
                    break;
                case InvalidGuidException invalidGuidException:
                    code = HttpStatusCode.BadRequest;
                    //result = invalidGuidException.Message;
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    //result = notFoundException.Message;
                    break;
                case RequestValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    //result = validationException.Message;
                    break;
                case DeleteFailureException deleteFailureException:
                    code = HttpStatusCode.BadRequest;
                    //result = validationException.Message;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code; 

            if (code == HttpStatusCode.InternalServerError)
            {
                result = JsonConvert.SerializeObject(new { ErrorMessage = "Oops! Something went wrong at servie side", incidentId });
                //TODO : Log error in DB or centeralized error looging system for support.
            }
            else
            {
                List<string> errors = new List<string>();
                var appException = exception as ApplicationBaseException;
                if (appException.Errors.Count > 0)
                {
                    foreach (var item in appException.Errors)
                    {
                       // errors.Add($"{item.Key} {item.Value[0]}");
                        errors.Add($"{item.Value[0]}");
                    }
                }

                result = JsonConvert.SerializeObject(new { ErrorMessage = exception.Message,Errors=errors, incidentId});
                //TODO : Log error in DB or centeralized error looging system for support.
            }
            //context.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = exception.Reason;
            
            return context.Response.WriteAsync(result);

        }

    }

    // Register this middleware inside Configure method (startup.cs) -  app.UseCustomExceptionHandler();
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }

}
