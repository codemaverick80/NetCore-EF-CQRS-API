using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Common.Filters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
   // [ApplicationExceptionFilter] 
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private IMediator mediator;
        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    }

}
