using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Common.Filters
{
    public class ModelValidationActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes
            //ValidateRequestHeaders(context); 
        }

        private void ValidateRequestHeaders(ActionExecutingContext context)
        {
            string actionName = context.RouteData.Values["action"] as string;
            string controllerName = context.RouteData.Values["controller"] as string;
            string path = context.HttpContext.Request.Path.Value;

            var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var controller = actionDescriptor.ControllerName;
            var action = actionDescriptor.ActionName;
            //// ------------------------------------------------------------------------------------
            ApiHeaders requestHeaders = ApiHeaders.GetHeaders(context.HttpContext.Request.Headers);
            ValidationContext validateHeaderModel = new ValidationContext(requestHeaders);
            var validationResults = new List<ValidationResult>();
            bool isHeaderValid = Validator.TryValidateObject(requestHeaders, validateHeaderModel, validationResults, true);

            if (!isHeaderValid)
            {
                ApiErrorResponse er = new ApiErrorResponse();
                er.SystemError = new List<SystemError>();
                foreach (var item in validationResults)
                {
                    SystemError se = new SystemError()
                    {
                        CreatorApplicatioId = "Music-API", // Web Api Id (name)
                        Code = "invalid Headers",
                        Message = item.ErrorMessage
                    };
                    er.SystemError.Add(se);
                }
                context.Result = new BadRequestObjectResult(er);
                return;
            }

        }
    }
}
