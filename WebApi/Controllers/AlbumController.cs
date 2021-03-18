namespace WebApi.Controllers
{
    using Application.CQRS.Albums.Queries;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApi.Common.Helpers;

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

        public async Task<ActionResult<IEnumerable<GetAlbumsResponse>>> Get([FromQuery] AlbumResourceParameters resourceParameters)
        {
            var result = await Mediator.Send(new GetAlbumsQuery() { ResourceParameters = resourceParameters });

            PaginationMetaData.CreatePaginationMetaData(result, resourceParameters, "GetAlbums", Url, httpContextAccessor);

            return Ok(mapper.Map<IEnumerable<GetAlbumsResponse>>(result));

        }





    }
}
