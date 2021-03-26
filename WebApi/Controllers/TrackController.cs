namespace WebApi.Controllers
{
    using Application.CQRS.Tracks.Commands.Create;
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


        [HttpGet("{id}", Name = "GetTrack")]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<ActionResult<GenreDetailResponse>> Get(string id)
        {           
            var result = await Mediator.Send(new TrackDetailQuery { Id = id });
            return Ok(mapper.Map<TrackDetailResponse>(result));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]       
        public async Task<ActionResult> Post([FromBody] CreateTrack request)
        {
            var id = await Mediator.Send(request);
            return CreatedAtRoute(
                "GetTrack",                         //RoutName
                new { id = id },                    // location header (https://localhost:5001/api/track/57d26b23-ea99-41fe-aee5-87529dc2ae23)
                new { id = id, data = request }     //response body
                );            
        }






    }
}
