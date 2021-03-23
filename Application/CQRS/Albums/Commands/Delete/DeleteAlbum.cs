namespace Application.CQRS.Albums.Commands.Delete
{
    using Application.Common.Exceptions;
    using Application.Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    public class DeleteAlbum : IRequest
    {
        public string Id { get; set; }
    }


    public class DeleteAlbumHandler : IRequestHandler<DeleteAlbum>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAlbumHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteAlbum request, CancellationToken cancellationToken)
        {
            bool isValidGuid = Guid.TryParse(request.Id, out _);
            if (!isValidGuid)
            {
                throw new InvalidGuidException(nameof(Album), request.Id);
            }

            var entity = await _context.Album.FindAsync(Guid.Parse(request.Id));
            if (entity == null)
            {
                throw new NotFoundException(nameof(Album), request.Id);
            }

            //// TODO: check id album contains any tracks before deleting
            var hasTracks = _context.Track.TagWith("AlbumHasTrackQuery").Any(al => al.AlbumId == Guid.Parse(request.Id));
            if (hasTracks)
            {
                // TODO: we need to put better logic when deleting
                throw new DeleteFailureException("There are existing tracks associated with this album.", nameof(Album), request.Id);
            }

            _context.Album.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
