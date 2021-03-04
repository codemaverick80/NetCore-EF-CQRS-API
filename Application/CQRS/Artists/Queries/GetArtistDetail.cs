using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Artists.Queries
{
    public class GetArtistDetail: IRequest<ArtistDetailResponse>
    {
        public string Id { get; set; }

    }

    public class GetArtistDetailHandler : IRequestHandler<GetArtistDetail, ArtistDetailResponse>
    {
        private readonly IApplicationDbContext dbContext;

        private readonly IMapper mapper;
        public GetArtistDetailHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ArtistDetailResponse> Handle(GetArtistDetail request, CancellationToken cancellationToken)
        {
            bool isValidGuid = Guid.TryParse(request.Id, out _);
            if (!isValidGuid)
            {
                throw new InvalidGuidException(nameof(Artist),request.Id);
            }
            var entity =await dbContext.Artist.FindAsync(Guid.Parse(request.Id));
            if (entity == null)
            {
                throw new NotFoundException(nameof(Artist), request.Id);
            }
            return mapper.Map<ArtistDetailResponse>(entity);
        }
    }



    public class ArtistDetailResponse : IMapFrom<Artist>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string YearActive { get; set; }
        public string Biography { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Artist, ArtistDetailResponse>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.ArtistName));
        }
    }
}
