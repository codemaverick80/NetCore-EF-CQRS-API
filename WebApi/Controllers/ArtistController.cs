namespace WebApi.Controllers
{
    using Application.CQRS.Artists;
    using Application.CQRS.Artists.Commands.Create;
    using Application.CQRS.Artists.Commands.Update;
    using Application.CQRS.Artists.Queries;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebApi.Common.Helpers;

    public class ArtistController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ArtistController(IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = "GetArtists")]
        public async Task<ActionResult<IEnumerable<GetArtistsReponse>>> GetAll([FromQuery] ArtistResourceParameters resourceParameters)
        {
            var result = await Mediator.Send(new GetArtists() { ResourceParameters = resourceParameters });

            PaginationMetaData.CreatePaginationMetaData(result, resourceParameters, "GetArtists", Url, httpContextAccessor);

            return Ok(mapper.Map<IEnumerable<GetArtistsReponse>>(result));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ArtistDetailResponse>> Get(string id)
        {
            return Ok(await Mediator.Send(new GetArtistDetail() { Id = id }));
        }              

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Create([FromBody] CreateArtist request)
        {
            var artistid =await Mediator.Send(request);
            return Ok(artistid);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update([FromBody] UpdateArtist request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

    }
}
