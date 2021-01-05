using Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Common.Filters
{
    /*
     * Handling Exceptions using Exception filter attribute.
     * To use this, just decorate BaseController.cs with [ApplicationExceptionFilter]
     */
    public class ApplicationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            switch (context.Exception)
            {
                case NotFoundException e:
                    context.Result = new NotFoundObjectResult(e.Message);
                    break;
                case BadRequestException e:
                    context.Result = new BadRequestObjectResult(e.Message);
                    break;
            }

        }
    }

}
