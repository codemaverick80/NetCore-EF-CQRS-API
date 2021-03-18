namespace WebApi.Common.Middleware
{
    using Application.Common.Exceptions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using WebApi.Models;
    /// <summary>
    /// Global exception handling using middleware
    /// </summary>
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
            var IncidentId = Guid.NewGuid();
            string result;
            ApiErrorResponse er = new ApiErrorResponse();

            switch (exception)
            {
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case InvalidGuidException invalidGuidException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case RequestValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case DeleteFailureException deleteFailureException:
                    code = HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (code == HttpStatusCode.InternalServerError)
            {
                //result = JsonConvert.SerializeObject(new { ErrorMessage = "Oops! Something went wrong at servie side", incidentId });
                // TODO : Log error in DB or centeralized error looging system for support.
                er.SystemError = new List<SystemError>();
                SystemError se = new SystemError()
                {
                    CreatorApplicatioId = "Music-API",
                    Code = "Internal Server Error",
                    Message = "Oops! Something went wrong at servie side"
                };
                er.SystemError.Add(se);
                result = JsonConvert.SerializeObject(new { IncidentId, ErrorMessage = "Oops! Something went wrong at servie side" });

            }
            else
            {
                var appException = exception as ApplicationBaseException;
                // TODO : Log error in DB or centeralized error looging system for support.
                er.SystemError = new List<SystemError>();
                if (appException.Errors.Count > 0)
                {
                    foreach (var item in appException.Errors)
                    {
                        SystemError se = new SystemError()
                        {
                            CreatorApplicatioId = "Music-API",
                            Code = appException.Reason,
                            Message = item.Value[0]
                        };
                        er.SystemError.Add(se);
                    }
                }
                else
                {
                    SystemError se = new SystemError()
                    {
                        CreatorApplicatioId = "Music-API",
                        Code = appException.Reason,
                        Message = appException.Message
                    };
                    er.SystemError.Add(se);
                }

                result = JsonConvert.SerializeObject(new { IncidentId, ErrorMessage = exception.Message, Errors = er });
            }
            //context.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = exception.Reason;            
            return context.Response.WriteAsync(result);

        }

        private string BadRequestMessage(Exception exception, string incidentId)
        {
            var appException = exception as ApplicationBaseException;
            ApiErrorResponse er = new ApiErrorResponse();
            er.SystemError = new List<SystemError>();
            if (appException.Errors.Count > 0)
            {
                foreach (var item in appException.Errors)
                {
                    SystemError se = new SystemError()
                    {
                        CreatorApplicatioId = "Music-API",
                        Code = appException.Reason,
                        Message = item.Value[0]
                    };
                    er.SystemError.Add(se);
                }
            }
            return JsonConvert.SerializeObject(new { IncidentId = incidentId, ErrorMessage = exception.Message, Errors = er });
        }


    }

    // Register this middleware inside Configure method (startup.cs) -  app.UseCustomExceptionHandler();   
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Global Exception Handling Middleware
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }

}
