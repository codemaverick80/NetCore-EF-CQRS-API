using Application.Common;
using Application.CQRS.Genres;
using Application.CQRS.Genres.Commands.Create;
using Application.CQRS.Genres.Commands.Delete;
using Application.CQRS.Genres.Commands.Update;
using Application.CQRS.Genres.Queries;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Common.Helpers;

namespace WebApi.Controllers
{
    public class GenreController : BaseController
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


        [HttpGet(Name ="GetGenres")]
        public async Task<ActionResult<IEnumerable<GetGenresResponse>>> GetAll([FromQuery] GenreResourceParameters resourceParams)
        {
            _logger.LogInformation("GetGenres endpoing ...");
            var result= await Mediator.Send(new GetGenresQuery() { GenreResourceParameters = resourceParams });
            // CreatePaginationMetaData(result,resourceParams);
            //Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(PaginationMetaData.CreatePaginationMetaData(result, resourceParams, "GetGenres", Url,_context)));

            PaginationMetaData.CreatePaginationMetaData(result, resourceParams, "GetGenres", Url, _context);
            return Ok(mapper.Map<IEnumerable<GetGenresResponse>>(result));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenreDetailResponse>> Get(string id)
        {
            _logger.LogInformation("GetGenre by id endpoing ...");
            var vm = await Mediator.Send(new GetGenreDetailQuery { Id = id });
            return Ok(vm);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post([FromBody] CreateGenreCommand request)
        {
           var genreId= await Mediator.Send(request);
           return Ok(genreId);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update([FromBody]UpdateGenreCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }


        // TODO : DO NOT Expose delete enpoint but in this demo we will
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(string id)
        {
            await Mediator.Send(new DeleteGenreCommand { Id = id });
            return NoContent();
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


    }
}
