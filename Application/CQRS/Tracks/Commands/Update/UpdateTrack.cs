namespace Application.CQRS.Tracks.Commands.Update
{
    using Application.Common.Exceptions;
    using Application.Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    public class UpdateTrack : IRequest
    {
        public string Id { get; set; }
        public string AlbumId { get; set; }
        public string Name { get; set; }
        public string Composer { get; set; }
        public string Performer { get; set; }
        public string Featuring { get; set; }
        public string Duration { get; set; }

    }

    public class UpdateTrackHandler : IRequestHandler<UpdateTrack>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTrackHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Unit> Handle(UpdateTrack request, CancellationToken cancellationToken)
        {
            Guid trackId;
            if (!Guid.TryParse(request.Id, out trackId))
            {
                throw new InvalidGuidException(nameof(Track), request.Id);
            }
            var entity = await _context.Track.FindAsync(Guid.Parse(request.Id));
            if (entity == null)
            {
                throw new NotFoundException(nameof(Track), request.Id);
            }

            Guid albumId;
            if (!Guid.TryParse(request.AlbumId, out albumId))
            {
                throw new InvalidGuidException(nameof(Album), request.AlbumId);
            }
            var albumEntity = await _context.Album.FindAsync(Guid.Parse(request.AlbumId));
            if (albumEntity == null)
            {
                throw new NotFoundException(nameof(Album), request.AlbumId);
            }

            entity.Id = trackId;
            entity.AlbumId = albumId;
            entity.TrackName = request.Name;
            entity.Duration = request.Duration;
            entity.Composer = request.Composer;
            entity.Featuring = request.Featuring;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
