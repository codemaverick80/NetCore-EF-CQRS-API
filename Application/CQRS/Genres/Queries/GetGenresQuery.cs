using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Genres.Queries
{

    #region "Query Request"
    public class GetGenresQuery : IRequest<PagedList<Genre>>
    {
        public GenreResourceParameters GenreResourceParameters { get; set; }

    }

    #endregion

    #region "Query Request Handler"
    public class GetGenresListQueryHandler : IRequestHandler<GetGenresQuery, PagedList<Genre>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetGenresListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PagedList<Genre>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        { 
            var query = context.Genre as IQueryable<Genre>;
            //TODO : Build search query
            //if (!string.IsNullOrWhiteSpace(request.GenreResourceParameters.SearchQuery))
            //{
            //    var searchQuery = request.GenreResourceParameters.SearchQuery.Trim();
            //    query = query.Where(g => g.GenreName.Contains(searchQuery));
            //}

            // TODO: Build select query. We just need Id and GenereName column from database (not all the columns)  
            query = query.TagWith("GetAllGenres").Select(e => new Genre { Id = e.Id, GenreName = e.GenreName });
            var result = await PagedList<Genre>.CreateAsync(query, request.GenreResourceParameters.PageNumber, request.GenreResourceParameters.PageSize);         
            return result;
        }
    }

    #endregion

    #region "Response Dto"
    public class GetGenresResponse : IMapFrom<Genre>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Genre, GetGenresResponse>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.GenreName));
        }

    }

    #endregion

}
