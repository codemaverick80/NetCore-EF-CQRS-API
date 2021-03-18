namespace WebApi.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    [Route("api/[controller]")]
   // [ApplicationExceptionFilter] 
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private IMediator mediator;
        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    }

}
