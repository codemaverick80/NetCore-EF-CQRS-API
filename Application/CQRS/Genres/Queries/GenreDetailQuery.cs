namespace Application.CQRS.Genres.Queries
{
    using Application.Common.Exceptions;
    using Application.Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    #region "Query Request"
    public class GenreDetailQuery:IRequest<Genre>
    {
        public string Id { get; set; }
    }

    #endregion

    #region "Query Request Handler"

    public class GetGenreDetailQueryHandler : IRequestHandler<GenreDetailQuery, Genre>
    {
        private readonly IApplicationDbContext context;       

        public GetGenreDetailQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
          
        }

        public async Task<Genre> Handle(GenreDetailQuery request, CancellationToken cancellationToken)
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
            return entity; 
        }    

    }

    #endregion
     

}
