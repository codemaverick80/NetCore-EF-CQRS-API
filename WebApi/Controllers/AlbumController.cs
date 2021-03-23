namespace WebApi.Controllers
{
    using Application.CQRS.Albums.Commands.Create;
    using Application.CQRS.Albums.Commands.Patch;
    using Application.CQRS.Albums.Commands.Update;
    using Application.CQRS.Albums.Queries;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
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

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateAlbum request)
        {
            request.Id = id;
            await Mediator.Send(request);
            return NoContent();
        }


        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Patch(string id, JsonPatchDocument<PatchAlbum> request)
        {
            var albumDetail = await Mediator.Send(new AlbumDetailQuery() { Id = id });
            PatchAlbum albumToPatch = new PatchAlbum()
            {
                Id = albumDetail.Id.ToString(),
                Name = albumDetail.AlbumName,
                GenreId = albumDetail.GenreId.ToString(),
                ArtistId = albumDetail.ArtistId.ToString(),
                AlbumUrl = albumDetail.AlbumUrl,
                Label = albumDetail.Label,
                Rating = albumDetail.Rating,
                Year = albumDetail.Year
            };

            request.ApplyTo(albumToPatch, ModelState);
            if (!TryValidateModel(albumToPatch))
            {
                return ValidationProblem(ModelState);
            }
            await Mediator.Send(albumToPatch);
            return NoContent();

        }



    }
}
