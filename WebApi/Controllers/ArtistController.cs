﻿namespace WebApi.Controllers
{
    using Application.CQRS.Artists;
    using Application.CQRS.Artists.Commands.Create;
    using Application.CQRS.Artists.Commands.Update;
    using Application.CQRS.Artists.Queries;
    using AutoMapper;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApi.Common.Helpers;

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

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Create([FromBody] CreateArtist request)
        {
            var artistid = await Mediator.Send(request);
            return Ok(artistid);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update([FromBody] UpdateArtist request)
        {
            await Mediator.Send(request);
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
