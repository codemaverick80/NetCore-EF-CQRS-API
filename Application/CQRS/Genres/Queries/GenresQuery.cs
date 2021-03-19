namespace Application.CQRS.Genres.Queries
{
    using Application.Common;
    using Application.Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    #region "Query Request"
    public class GenresQuery : IRequest<PagedList<Genre>>
    {
        public GenreResourceParameters GenreResourceParameters { get; set; }

    }

    #endregion

    #region "Query Request Handler"
    public class GetGenresQueryHandler : IRequestHandler<GenresQuery, PagedList<Genre>>
    {
        private readonly IApplicationDbContext context;
      
        public GetGenresQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
         
        }

        public async Task<PagedList<Genre>> Handle(GenresQuery request, CancellationToken cancellationToken)
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

   

}
