namespace WebApi.Controllers
{
    using Application.CQRS.Albums.Commands.Create;
    using Application.CQRS.Albums.Queries;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApi.Common.Helpers;
    using WebApi.Dtos;

    public class AlbumController : ApplicationBaseController
    {

        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AlbumController(IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }


        [HttpGet(Name = "GetAlbums")]
        public async Task<ActionResult<IEnumerable<AlbumResponse>>> Get([FromQuery] AlbumResourceParameters resourceParameters)
        {
            var result = await Mediator.Send(new AlbumsQuery() { ResourceParameters = resourceParameters });

            PaginationMetaData.CreatePaginationMetaData(result, resourceParameters, "GetAlbums", Url, httpContextAccessor);

            return Ok(mapper.Map<IEnumerable<AlbumResponse>>(result));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AlbumDetailResponse>> Get(string id)
        {
            var result = await Mediator.Send(new AlbumDetailQuery() { Id = id });
            //// TODO: 
            //result.SmallThumbnail = $"https://musicapi.com/image/album/s/{result.SmallThumbnail}";
            //result.MediumThumbnail = $"https://musicapi.com/image/album/m/{result.MediumThumbnail}";
            //result.LargeThumbnail = $"https://musicapi.com/image/album/l/{result.LargeThumbnail}";
            return Ok(mapper.Map<AlbumDetailResponse>(result));

        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Create([FromBody] CreateAlbum request)
        {
            var artistid = await Mediator.Send(request);
            return Ok(artistid);
        }




    }
}
