namespace Application.CQRS.Albums.Commands.Create
{
    using Application.Common.Exceptions;
    using Application.Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateAlbum :IRequest<string>
    {
       public string Name { get; set; }
        public string ArtistId { get; set; }
        public string GenreId { get; set; }
        public int Rating { get; set; }
        public int Year { get; set; }
        public string Label { get; set; }
        public string ThumbnailTag { get; set; }
        public string SmallThumbnail { get; set; }
        public string MediumThumbnail { get; set; }
        public string LargeThumbnail { get; set; }
        public string AlbumUrl { get; set; }

    }


    public class CreateAlbumHandler : IRequestHandler<CreateAlbum, string>
    {
        private readonly IApplicationDbContext _context;

        public CreateAlbumHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<string> Handle(CreateAlbum request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            if (await ValidateArtistAndGenre(request))
            {               
                Guid artistId = Guid.Parse(request.ArtistId);
                Guid genreId = Guid.Parse(request.GenreId);

                var entity = new Album
                {
                    Id = id,
                    AlbumName = request.Name,
                    ArtistId = artistId,
                    GenreId = genreId,
                    Rating = request.Rating,
                    Year = request.Year,
                    Label = request.Label,
                    ThumbnailTag = id.ToString(),               // Id will be the ThumbnailTag
                    SmallThumbnail = request.SmallThumbnail,    // Id_thumbnail_s.jpg
                    MediumThumbnail = request.MediumThumbnail,  // Id_thumbnail_m.jpg
                    LargeThumbnail = request.LargeThumbnail,    // Id_thumbnail_l.jpg
                    AlbumUrl = request.AlbumUrl
                };

                _context.Album.Add(entity);
                await _context.SaveChangesAsync(cancellationToken); 
                
            }
            return id.ToString();

        }

        private async Task<bool> ValidateArtistAndGenre(CreateAlbum request)
        {
            bool isValidArtistAndGenre = false;

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

            isValidArtistAndGenre = true;
            return isValidArtistAndGenre;
        }



    }
}
