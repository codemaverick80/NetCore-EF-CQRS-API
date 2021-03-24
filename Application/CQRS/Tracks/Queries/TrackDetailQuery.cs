using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
namespace Application.CQRS.Tracks.Queries
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    public class TrackDetailQuery : IRequest<Track>
    {
        public string Id { get; set; }
    }


    public class TrackDetailQueryHandler : IRequestHandler<TrackDetailQuery, Track>
    {
        private readonly IApplicationDbContext _context;

        public TrackDetailQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Track> Handle(TrackDetailQuery request, CancellationToken cancellationToken)
        {
            Guid trackId;
            if (!Guid.TryParse(request.Id, out trackId))
            {
                throw new InvalidGuidException(nameof(Track), request.Id);
            }

            var entity =await _context.Track
                .Where(t => t.Id == trackId)
                .Select(t => new Track
                {
                    Id = t.Id,
                    TrackName = t.TrackName,
                    Composer = t.Composer,
                    Duration = t.Duration,
                    Featuring = t.Featuring,
                    AlbumId=t.AlbumId
                }).FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException(nameof(Track), request.Id);
            }
            return entity;
        }
    }
}
