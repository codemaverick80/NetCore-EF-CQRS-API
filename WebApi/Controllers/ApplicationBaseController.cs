namespace WebApi.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    [Route("api/[controller]")]
   // [ApplicationExceptionFilter] 
    [ApiController]
    public abstract class ApplicationBaseController : ControllerBase
    {
        private IMediator mediator;
        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    }

}
