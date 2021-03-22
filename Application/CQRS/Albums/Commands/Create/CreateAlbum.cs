namespace Application.CQRS.Albums.Commands.Create
{
    using Application.Common.Exceptions;
    using Application.Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateAlbum :IRequest<string>
    {

       // public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ArtistId { get; set; }
        public Guid GenreId { get; set; }
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
            var entity = new Album
            {
                Id = id,
                AlbumName = request.Name,
                ArtistId = request.ArtistId,
                GenreId = request.GenreId,
                Rating = request.Rating,
                Year = request.Year,
                Label = request.Label,
                ThumbnailTag = id.ToString(),
                SmallThumbnail = request.SmallThumbnail,
                MediumThumbnail = request.MediumThumbnail,
                LargeThumbnail = request.LargeThumbnail,
                AlbumUrl = request.AlbumUrl
            };

            if (await ValidateArtistAndGenre(request))
            { 
                _context.Album.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);               
            }
            return entity.Id.ToString();


        }

        private async Task<bool> ValidateArtistAndGenre(CreateAlbum request)
        {
            bool isValidArtistAndGenre = false;

            bool isValidArtistId = Guid.TryParse(request.ArtistId.ToString(), out _);
            if (!isValidArtistId)
            {
                throw new InvalidGuidException(nameof(request.ArtistId), request.ArtistId);
            }

            bool isValidGenretId = Guid.TryParse(request.GenreId.ToString(), out _);
            if (!isValidGenretId)
            {
                throw new InvalidGuidException(nameof(request.GenreId), request.GenreId);
            }

            var artistExists = await _context.Artist.FindAsync(Guid.Parse(request.ArtistId.ToString()));
            if (artistExists == null)
            {
                throw new NotFoundException(nameof(request.ArtistId), request.ArtistId.ToString());
            }

            var genreExists = await _context.Genre.FindAsync(Guid.Parse(request.GenreId.ToString()));
            if (genreExists == null)
            {
                throw new NotFoundException(nameof(request.GenreId), request.ArtistId.ToString());
            }

            isValidArtistAndGenre = true;
            return isValidArtistAndGenre;


        }



    }
}
