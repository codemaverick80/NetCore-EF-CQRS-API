namespace WebApi.Controllers
{
    using Application.CQRS.Tracks.Queries;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApi.Common.Helpers;
    using WebApi.Dtos;

    public class TrackController : ApplicationBaseController
    {

        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TrackController(IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }


        //[HttpGet(Name = "GetTracks")]
        //public async Task<ActionResult<IEnumerable<TrackResponse>>> Get([FromQuery] TrackResourceParameters resourceParameters)
        //{
        //    var result = await Mediator.Send(new TrackQuery() { ResourceParameters = resourceParameters });
        //    PaginationMetaData.CreatePaginationMetaData(result, resourceParameters, "GetTracks", Url, httpContextAccessor);
        //    return Ok(mapper.Map<IEnumerable<TrackResponse>>(result));
        //}



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<ActionResult<GenreDetailResponse>> Get(string id)
        {           
            var result = await Mediator.Send(new TrackDetailQuery { Id = id });
            return Ok(mapper.Map<TrackDetailResponse>(result));
        }



    }
}
