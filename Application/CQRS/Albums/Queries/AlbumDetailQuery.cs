namespace Application.CQRS.Albums.Queries
{
    using Application.Common.Exceptions;
    using Application.Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class AlbumDetailQuery : IRequest<Album>
    {
        public string Id { get; set; }
    }

    public class AlbumDetailQueryHandler : IRequestHandler<AlbumDetailQuery, Album>
    {
        private readonly IApplicationDbContext _context;

        public AlbumDetailQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Album> Handle(AlbumDetailQuery request, CancellationToken cancellationToken)
        {
            bool isValidGuid = Guid.TryParse(request.Id, out _);
            if (!isValidGuid)
            {
                throw new InvalidGuidException(nameof(Album), request.Id);
            }
            var entity = _context.Album.Select(a => new Album
            {
                Id = a.Id,
                AlbumName = a.AlbumName,
                Rating = a.Rating,
                Year = a.Year,
                Label=a.Label,
                LargeThumbnail = a.LargeThumbnail,
                MediumThumbnail = a.MediumThumbnail,
                SmallThumbnail = a.SmallThumbnail,
                AlbumUrl = a.AlbumUrl
            }).Where(a => a.Id == Guid.Parse(request.Id)).FirstOrDefault();

            if (entity == null)
            {
                throw new NotFoundException(nameof(Album), request.Id);
            }
            return entity;
        }
    }

}
