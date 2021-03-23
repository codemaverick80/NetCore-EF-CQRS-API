namespace Application.CQRS.Tracks.Queries
{
    using Application.Common;
    using Application.Common.Exceptions;
    using Application.Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    public class TrackQuery : IRequest<PagedList<Track>>
    {
        public TrackResourceParameters ResourceParameters { get; set; }

        public string AlbumId { get; set; }

    }

    public class TrackQueryHandler : IRequestHandler<TrackQuery, PagedList<Track>>
    {
        private readonly IApplicationDbContext _context;

        public TrackQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<PagedList<Track>> Handle(TrackQuery request, CancellationToken cancellationToken)
        {
            Guid albumId;
            bool isValidGuid = Guid.TryParse(request.AlbumId, out albumId);
            if (!isValidGuid)
            {
                throw new InvalidGuidException(nameof(Album), request.AlbumId);
            }
            var query = _context.Track as IQueryable<Track>;
            query = query
                .Where(t => t.AlbumId == albumId)
                .Select(a => new Track
                {
                    Id = a.Id,
                    TrackName = a.TrackName
                }); 
            var result = await PagedList<Track>.CreateAsync(query, request.ResourceParameters.PageNumber, request.ResourceParameters.PageSize);
            return result;
        }
    }
}
