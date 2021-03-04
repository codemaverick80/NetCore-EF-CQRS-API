using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Artists.Queries
{
    #region "Query Request"
    public class GetArtists: IRequest<PagedList<Artist>>
    {
        public ArtistResourceParameters ResourceParameters { get; set; }

    }

    #endregion

    #region "Query Request Handler"
    public class GetArtistHandler : IRequestHandler<GetArtists, PagedList<Artist>>
    {
        private readonly IApplicationDbContext context;
        public GetArtistHandler(IApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<PagedList<Artist>> Handle(GetArtists request, CancellationToken cancellationToken)
        {
            var query = context.Artist as IQueryable<Artist>;
            query = query.Select(a => new Artist { Id = a.Id, ArtistName = a.ArtistName, YearActive = a.YearActive, Biography = a.Biography });
            var result =await PagedList<Artist>.CreateAsync(query, request.ResourceParameters.PageNumber, request.ResourceParameters.PageSize);
            return result;
        }
    }

    #endregion

    #region "Response Dto"
    public class GetArtistsReponse : IMapFrom<Artist>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Artist, GetArtistsReponse>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.ArtistName));
        }
    }

    #endregion
}
