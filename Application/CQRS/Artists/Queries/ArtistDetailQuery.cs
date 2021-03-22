namespace Application.CQRS.Artists.Queries
{
    using Application.Common.Exceptions;
    using Application.Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    public class ArtistDetailQuery : IRequest<Artist>
    {
        public string Id { get; set; }

    }

    public class ArtistDetailQueryHandler : IRequestHandler<ArtistDetailQuery, Artist>
    {
        private readonly IApplicationDbContext _context;
        public ArtistDetailQueryHandler(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Artist> Handle(ArtistDetailQuery request, CancellationToken cancellationToken)
        {
            bool isValidGuid = Guid.TryParse(request.Id, out _);
            if (!isValidGuid)
            {
                throw new InvalidGuidException(nameof(Artist), request.Id);
            }           
            var entity = _context.Artist.Select(a => new Artist
            {
                Id = a.Id,
                ArtistName = a.ArtistName,
                Biography = a.Biography,
                SmallThumbnail = a.SmallThumbnail,
                LargeThumbnail = a.LargeThumbnail,
                ArtistBasicInfo = a.ArtistBasicInfo
            }
            ).Where(a => a.Id == Guid.Parse(request.Id)).FirstOrDefault();

            if (entity == null)
            {
                throw new NotFoundException(nameof(Artist), request.Id);
            }
            return entity;
        }
    }

}
