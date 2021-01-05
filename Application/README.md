#### Application Layer (Core)

 -  This layer contains all application logic. 
 -  It is dependent on the domain layer, but has no dependencies on any other layer or project.
 -  This layer defines interfaces that are implemented by outside layers. 
For example, if the application need to access a notification service, a new interface would be added to application and an implementa


#### CQRS using MediatR

###### Application Project
<em>Required Nuget Packages:</em>
> 1. <em>MediatR</em>
> 2. <em>MediatR.Extensions.Microsoft.DependencyInjection</em>

```csharp
/*
* This is Query
* IRequest<List<Genre>> : this is the return type of this query
*/
public class GetGenresQuery : IRequest<List<Genre>>
{    

}

/* This is Query Handler */
public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, List<Genre>>
{
    private readonly IApplicationDbContext context;  

    public GetGenresListQueryHandler(IApplicationDbContext context)
    {
        this.context = context;        
    }

    public async Task<List<Genre>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    { 
        var query = context.Genre as IQueryable<Genre>; 
        // TODO: Build select query. We just need Id and GenereName column from database (not all the columns)  
        query = query.Select(e => new Genre { Id = e.Id, GenreName = e.GenreName });
        var result = await query.ToListAsync();       
        return result;
    }
}

```








###### WebApi Project
<em>Required Nuget Packages:</em>
> 1. <em>MediatR</em>


```csharp
 public class GenreController : BaseController
 {
    private readonly IMediator mediator;
    public GenreController(IMediator mediator)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); ;
    }

    [HttpGet]
    public async Task<ActionResult<GenresListVm>> GetAll()
    {
        return Ok(await mediator.Send(new GetGenresQuery()));           
    }

}

```