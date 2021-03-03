using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApi.Models;
using System.Text.Json;

namespace WebApi.Common.Filters
{
    /*
     * Register this in  ConfigureServices  (startup.cs)     * 
     */
    public class ModelValidationActionFilter : IActionFilter
    {      

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes
            ValidateRequestHeaders(context); 
        }

        private void ValidateRequestHeaders(ActionExecutingContext context)
        {
            // TODO: Getting action and controller name from context (ASP.NET CORE)
            string actionName = context.RouteData.Values["action"] as string;
            string controllerName = context.RouteData.Values["controller"] as string;
            string path = context.HttpContext.Request.Path.Value;

            var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var controller = actionDescriptor.ControllerName;
            var action = actionDescriptor.ActionName;
            
           
            ApiHeaders requestHeaders = ApiHeaders.GetHeaders(context.HttpContext.Request.Headers);

            #region "Log Request Headers & Payload"

            string stream = string.Empty;
            try
            {
                if (context.ActionArguments.Count > 0)
                {
                    if (context.ActionArguments.ToArray()[0].Value != null)
                    {
                        stream = JsonSerializer.Serialize(context.ActionArguments.Select(item => item.Value));
                    }
                }
                // TODO: Log requestHeaders, stream, controller, action
                // TODO: Log in Database              

            }
            catch {}

            #endregion


            #region "Request Headers Validation"


            ValidationContext validateHeaderModel = new ValidationContext(requestHeaders);
            var validationResults = new List<ValidationResult>();
            bool isHeaderValid = Validator.TryValidateObject(requestHeaders, validateHeaderModel, validationResults, true);

            if (!isHeaderValid)
            {
                string IncidentId = Guid.NewGuid().ToString();
                ApiErrorResponse er = new ApiErrorResponse();
                er.SystemError = new List<SystemError>();
                foreach (var item in validationResults)
                {
                    SystemError se = new SystemError()
                    {
                        CreatorApplicatioId = "Music-API", 
                        Code = "invalid Headers",
                        Message = item.ErrorMessage
                    };
                    er.SystemError.Add(se);
                }
                //context.Result = new BadRequestObjectResult(er);
                context.Result =new BadRequestObjectResult(JsonSerializer.Serialize(new { IncidentId, ErrorMessage = "Request headers validation error", Errors = er }));
                return;
            }

            #endregion


        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
            ApiHeaders resposeHeaders = new ApiHeaders().ResponseHeaders(ApiHeaders.GetHeaders(context.HttpContext.Request.Headers));
            context.HttpContext.Response.Headers.Add("API-CreationTimeStamp", resposeHeaders.CreationTimeStamp == DateTime.MinValue ? null : resposeHeaders.CreationTimeStamp.ToString("yyyy-MM-ddTHH:mm:ss.fffk"));
            context.HttpContext.Response.Headers.Add("API-SenderMessageId", resposeHeaders.SenderMessageId);
            context.HttpContext.Response.Headers.Add("API-SenderApplicationId", resposeHeaders.SenderApplicationId);
            context.HttpContext.Response.Headers.Add("API-SenderHostName", resposeHeaders.SenderHostName);
            context.HttpContext.Response.Headers.Add("API-SenderMessageIdEcho", resposeHeaders.SenderMessageIdEcho);
            context.HttpContext.Response.Headers.Add("API-OriginationApplicationId", resposeHeaders.OriginationApplicationId);
            context.HttpContext.Response.Headers.Add("API-TransactionId", resposeHeaders.TransactionId);
            try
            {
                // TODO: Log responseHeaders, responseResult, controller, action for audit purpose
                var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
                var controller = actionDescriptor.ControllerName;
                var action = actionDescriptor.ActionName;
                var responseResult = JsonSerializer.Serialize(context.Result);
                // TODO: Log in Database
            }
            catch { }

        }
    }
}
