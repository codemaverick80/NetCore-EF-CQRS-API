namespace WebApi.Controllers
{
    using Application.CQRS.Artists;
    using Application.CQRS.Artists.Commands.Create;
    using Application.CQRS.Artists.Commands.Patch;
    using Application.CQRS.Artists.Commands.Update;
    using Application.CQRS.Artists.Queries;
    using AutoMapper;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApi.Common.Helpers;
    using WebApi.Dtos;

    public class ArtistController : ApplicationBaseController
    {
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment env;

        public ArtistController(IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment env)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.httpContextAccessor = httpContextAccessor;
            this.env = env;
        }

        [HttpGet(Name = "GetArtists")]
        public async Task<ActionResult<IEnumerable<ArtistResponse>>> GetAll([FromQuery] ArtistResourceParameters resourceParameters)
        {
            var result = await Mediator.Send(new ArtistsQuery() { ResourceParameters = resourceParameters });
            PaginationMetaData.CreatePaginationMetaData(result, resourceParameters, "GetArtists", Url, httpContextAccessor);
            return Ok(mapper.Map<IEnumerable<ArtistResponse>>(result));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ArtistDetailResponse>> Get(string id)
        {
            var result = await Mediator.Send(new ArtistDetailQuery() { Id = id });
            //// TODO: 
            //result.SmallThumbnail = $"https://musicapi.com/image/artist/s/{result.SmallThumbnail}";
            //result.LargeThumbnail = $"https://musicapi.com/image/artist/l/{result.LargeThumbnail}";
            return Ok(mapper.Map<ArtistDetailResponse>(result));
        }


        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Create([FromBody] CreateArtist request)
        {
            var artistid = await Mediator.Send(request);
            return Ok(artistid);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateArtist request)
        {
            request.Id = id;

            await Mediator.Send(request);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Patch(string id, JsonPatchDocument<PatchArtistCommand> request)
        {
            var artistDetail = await Mediator.Send(new ArtistDetailQuery() { Id = id });
            PatchArtistCommand artistToPatch = new PatchArtistCommand()
            {
                Id = artistDetail.Id.ToString(),
                Name = artistDetail.ArtistName,
                Biography = artistDetail.Biography,
                YearActive = artistDetail.YearActive,
                Born = artistDetail.ArtistBasicInfo.Born,
                AKA = artistDetail.ArtistBasicInfo.AlsoKnownAs,
                Died = artistDetail.ArtistBasicInfo.Died
            };

            request.ApplyTo(artistToPatch, ModelState);
            if(!TryValidateModel(artistToPatch))
            {
                return ValidationProblem(ModelState);
            }
            await Mediator.Send(artistToPatch);
            return NoContent();

        }




        #region "Upload and download Image"


        [Route("/api/image")]
        [HttpPost()]
        public async Task<IActionResult> UploadImage(IFormFile smallThumbnail)
        {
            var mimeType = smallThumbnail.FileName.Split('.').Last(); // abc.jpg => jpg
            var fileName = string.Concat(Path.GetRandomFileName(), ".", mimeType);

            //For local testing we are saving in wwwwroot folder but in prod we will use some cloud service
            var savePath = Path.Combine(env.WebRootPath, fileName);

            await using (var filestream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                await smallThumbnail.CopyToAsync(filestream);
            }

            return Ok(fileName);
        }


        [Route("/api/image/{image}")]
        [HttpGet]
        public IActionResult GetImage(string image)
        {
            var mimeType = image.Split('.').Last();
            var savePath = Path.Combine(env.WebRootPath, image);
            return new FileStreamResult(new FileStream(savePath, FileMode.Open, FileAccess.Read), "image/*");
            //return new FileStreamResult(filestrem, Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse(image));
        }

        #endregion


    }
}
