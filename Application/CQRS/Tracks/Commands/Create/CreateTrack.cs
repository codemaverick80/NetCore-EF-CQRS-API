namespace Application.CQRS.Tracks.Commands.Create
{
    using Application.Common.Exceptions;
    using Application.Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateTrack : IRequest<string>
    {
        public string Name { get; set; }
        public string AlbumId { get; set; }
        public string Composer { get; set; }
        public string Performer { get; set; }
        public string Featuring { get; set; }
        public string Duration { get; set; }

    }

    public class CreateTrackHandler : IRequestHandler<CreateTrack, string>
    {
        private readonly IApplicationDbContext _context;

        public CreateTrackHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateTrack request, CancellationToken cancellationToken)
        {
            Guid albumId;
            bool isValidAlbumId = Guid.TryParse(request.AlbumId, out albumId);
            if (!isValidAlbumId)
            {
                throw new InvalidGuidException(nameof(request.AlbumId), request.AlbumId);
            }

            var albumExists = await _context.Album.FindAsync(Guid.Parse(request.AlbumId));
            if (albumExists == null)
            {
                throw new NotFoundException(nameof(request.AlbumId), request.AlbumId);
            }

            var entity = new Track
            {
                Id = Guid.NewGuid(),
                TrackName = request.Name,
                AlbumId = albumId,
                Composer = string.IsNullOrWhiteSpace(request.Composer) ? null : request.Composer,
                Performer = string.IsNullOrWhiteSpace(request.Performer) ? null : request.Performer,
                Featuring = string.IsNullOrWhiteSpace(request.Featuring) ? null : request.Featuring,
                Duration = string.IsNullOrWhiteSpace(request.Duration) ? null : request.Duration

            };
            _context.Track.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id.ToString();
        }


    }

}
