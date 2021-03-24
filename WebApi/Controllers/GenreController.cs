namespace WebApi.Controllers
{
    using Application.CQRS.Genres;
    using Application.CQRS.Genres.Commands.Create;
    using Application.CQRS.Genres.Commands.Delete;
    using Application.CQRS.Genres.Commands.Patch;
    using Application.CQRS.Genres.Commands.Update;
    using Application.CQRS.Genres.Queries;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebApi.Common.Helpers;
    using WebApi.Dtos;

    public class GenreController : ApplicationBaseController
    {
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _context;
        private readonly ILogger<GenreController> _logger;

        public GenreController(IMapper mapper, IHttpContextAccessor context, ILogger<GenreController> logger = null)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region 

        //private readonly IMediator mediator;
        //public GenreController(IMediator mediator)
        //{
        //    this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); ;
        //}
        //[HttpGet]
        //public async Task<ActionResult<GenresListVm>> GetAll()
        //{
        //    return Ok(await mediator.Send(new GetGenresListQuery()));           
        //}

        #endregion


        //[HttpGet]
        //public async Task<ActionResult<GenresListResponse>> GetAll()
        //{  
        //    return Ok(await Mediator.Send(new GetGenresListQuery()));
        //}


        [HttpGet(Name = "GetGenres")]
        public async Task<ActionResult<IEnumerable<GenreGetResponse>>> GetAll([FromQuery] GenreResourceParameters resourceParams)
        {
            _logger.LogInformation("GetGenres endpoing ...");
            var result = await Mediator.Send(new GenresQuery() { GenreResourceParameters = resourceParams });
            // CreatePaginationMetaData(result,resourceParams);
            //Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(PaginationMetaData.CreatePaginationMetaData(result, resourceParams, "GetGenres", Url,_context)));

            PaginationMetaData.CreatePaginationMetaData(result, resourceParams, "GetGenres", Url, _context);
            return Ok(mapper.Map<IEnumerable<GenreGetResponse>>(result));
        }


        [HttpGet("{id}", Name = "GetGenre")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenreDetailResponse>> Get(string id)
        {
            _logger.LogInformation("GetGenre by id endpoing ...");
            var result = await Mediator.Send(new GenreDetailQuery { Id = id });
            return Ok(mapper.Map<GenreDetailResponse>(result));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post([FromBody] CreateGenre request)
        {
            var id = await Mediator.Send(request);
            return CreatedAtRoute(
                "GetGenre",                         //RoutName
                new { id = id },                    // location header (https://localhost:5001/api/Genre/57d26b23-ea99-41fe-aee5-87529dc2ae23)
                new { id = id, data = request }     //response body
                );
            //return Ok(genreId);
        }


        // TODO: PUT will update full entity
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update([FromBody] UpdateGenre request)
        {
            await Mediator.Send(request);
            return NoContent();
        }


        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PartialGenreUpdate(string id, JsonPatchDocument<PatchGenre> request)
        {
            var genreFromDb = await Mediator.Send(new GenreDetailQuery { Id = id });
            PatchGenre genreToPatch = new PatchGenre()
            {
                Id = genreFromDb.Id.ToString(),
                Name = genreFromDb.GenreName,
                Description = genreFromDb.Description
            };

            // add validation
            request.ApplyTo(genreToPatch, ModelState);
            if (!TryValidateModel(genreToPatch))
            {
                return ValidationProblem(ModelState);
            }

            await Mediator.Send(genreToPatch);
            return NoContent();
        }

        //// TODO : DO NOT Expose delete enpoint but in this demo we will
        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    await Mediator.Send(new DeleteGenre { Id = id });
        //    return NoContent();
        //}   


    }

}


//public enum ResourceUriType
//{
//    PreviousPage,
//    NextPage
//}

//private void CreatePaginationMetaData<T>(PagedList<T> genresFromQuery, PagingParameters resourceParams)
//{
//    var previousPageLink = genresFromQuery.HasPrevious ? CreateGenresResourceUri(resourceParams, ResourceUriType.PreviousPage) : null;
//    var nextPageLink = genresFromQuery.HasNext ? CreateGenresResourceUri(resourceParams, ResourceUriType.NextPage) : null;
//    var paginationMetadata = new
//    {
//        TotalCount = genresFromQuery.TotalCount,
//        PageSize = genresFromQuery.PageSize,
//        CurrentPage = genresFromQuery.CurrentPage,
//        TotalPages = genresFromQuery.TotalPages,
//        PreviousPageLink = previousPageLink,
//        NextPageLink = nextPageLink
//    };
//    // TODO: Adding X-Pagination to resonse header
//    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
//}

//private string CreateGenresResourceUri(PagingParameters genresResourceParameters, ResourceUriType type)
//{
//    var a = Url;
//    switch (type)
//    {
//        case ResourceUriType.PreviousPage:
//            return Url.Link("GetGenres",
//              new
//              {
//                  pageNumber = genresResourceParameters.PageNumber - 1,
//                  pageSize = genresResourceParameters.PageSize                          
//              });
//        case ResourceUriType.NextPage:
//            return Url.Link("GetGenres",
//              new
//              {
//                  pageNumber = genresResourceParameters.PageNumber + 1,
//                  pageSize = genresResourceParameters.PageSize
//              });

//        default:
//            return Url.Link("GetGenres",
//            new
//            {
//                pageNumber = genresResourceParameters.PageNumber,
//                pageSize = genresResourceParameters.PageSize
//            });
//    }

//}
