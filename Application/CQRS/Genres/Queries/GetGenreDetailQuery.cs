namespace Application.CQRS.Genres.Queries
{
    using Application.Common.Exceptions;
    using Application.Common.Interfaces;
    using Application.Common.Mappings;
    using AutoMapper;
    using Domain.Entities;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    #region "Query Request"
    public class GetGenreDetailQuery:IRequest<GenreDetailResponse>
    {
        public string Id { get; set; }
    }

    #endregion

    #region "Query Request Handler"

    public class GetGenreDetailQueryHandler : IRequestHandler<GetGenreDetailQuery, GenreDetailResponse>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetGenreDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GenreDetailResponse> Handle(GetGenreDetailQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var newGuid))
            {
                throw new InvalidGuidException(nameof(Genre), request.Id);
            }

            var entity = await context.Genre.FindAsync(Guid.Parse(request.Id));

            if (entity == null)
            {
                throw new NotFoundException(nameof(Genre), request.Id);
            }
            var genreDetail = mapper.Map<GenreDetailDto>(entity);
            var vm = new GenreDetailResponse
            {
                GenreDetail = genreDetail
            };

            return vm;

        }      

    }

    #endregion

    #region "Dto"
    public class GenreDetailDto : IMapFrom<Genre>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Genre, GenreDetailDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.GenreName));
        }

    }
    #endregion

    #region "Query Response"

    public class GenreDetailResponse
    {
        public GenreDetailDto GenreDetail { get; set; }
    }

    #endregion

}
