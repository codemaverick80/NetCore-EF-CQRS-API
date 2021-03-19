namespace Application.CQRS.Artists.Queries
{
    using Application.Common;
    using Application.Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    #region "Query Request"

    public class ArtistsQuery : IRequest<PagedList<Artist>>
    {
        public ArtistResourceParameters ResourceParameters { get; set; }

    }

    #endregion

    #region "Query Request Handler"

    public class ArtistsQueryHandler : IRequestHandler<ArtistsQuery, PagedList<Artist>>
    {
        private readonly IApplicationDbContext context;
       
        public ArtistsQueryHandler(IApplicationDbContext context)
        {
            this.context = context;           
        }
        public async Task<PagedList<Artist>> Handle(ArtistsQuery request, CancellationToken cancellationToken)
        {
            var query = context.Artist as IQueryable<Artist>;
            query = query.Select(a => new Artist
            {
                Id = a.Id,
                ArtistName = a.ArtistName,               
                SmallThumbnail = a.SmallThumbnail
                
            });
            var result = await PagedList<Artist>.CreateAsync(query, request.ResourceParameters.PageNumber, request.ResourceParameters.PageSize);
            return result;
        }
    }

    #endregion
    
}
