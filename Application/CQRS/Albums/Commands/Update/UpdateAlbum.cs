namespace Application.CQRS.Albums.Commands.Update
{
    using Application.Common.Exceptions;
    using Application.Common.Interfaces;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    public class UpdateAlbum : IRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ArtistId { get; set; }
        public string GenreId { get; set; }
        public int Rating { get; set; }
        public int Year { get; set; }
        public string Label { get; set; }
        public string AlbumUrl { get; set; }

    }

    public class UpdateAlbumHandler : IRequestHandler<UpdateAlbum>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAlbumHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateAlbum request, CancellationToken cancellationToken)
        {
            Guid albumId;
            bool isValidGuid = Guid.TryParse(request.Id, out albumId);
            if (!isValidGuid)
            {
                throw new InvalidGuidException(nameof(Albums), request.Id);
            }

            var entity = await _context.Album.FindAsync(Guid.Parse(request.Id));
            if (entity == null)
            {
                throw new NotFoundException(nameof(Albums), request.Id);
            }

            if (await ValidateArtistAndGenre(request))
            {
                Guid artistId = Guid.Parse(request.ArtistId);
                Guid genreId = Guid.Parse(request.GenreId);

                entity.Id = albumId;
                entity.GenreId = genreId;
                entity.ArtistId = artistId;
                entity.AlbumName = request.Name;
                entity.AlbumUrl = request.AlbumUrl;
                entity.Rating = request.Rating;
                entity.Label = request.Label;
                entity.Year = request.Year;
                await _context.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;

        }



        private async Task<bool> ValidateArtistAndGenre(UpdateAlbum request)
        {
            //bool isValidArtistAndGenre = false;

            bool isValidArtistId = Guid.TryParse(request.ArtistId, out _);
            if (!isValidArtistId)
            {
                throw new InvalidGuidException(nameof(request.ArtistId), request.ArtistId);
            }

            bool isValidGenretId = Guid.TryParse(request.GenreId, out _);
            if (!isValidGenretId)
            {
                throw new InvalidGuidException(nameof(request.GenreId), request.GenreId);
            }

            var artistExists = await _context.Artist.FindAsync(Guid.Parse(request.ArtistId));
            if (artistExists == null)
            {
                throw new NotFoundException(nameof(request.ArtistId), request.ArtistId);
            }

            var genreExists = await _context.Genre.FindAsync(Guid.Parse(request.GenreId));
            if (genreExists == null)
            {
                throw new NotFoundException(nameof(request.GenreId), request.ArtistId);
            }

            //isValidArtistAndGenre = true;
            //return isValidArtistAndGenre;
            return true;


        }
    }
}
