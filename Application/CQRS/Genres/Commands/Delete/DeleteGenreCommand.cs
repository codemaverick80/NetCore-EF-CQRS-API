using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Genres.Commands.Delete
{
   public class DeleteGenreCommand : IRequest
   {
        public string  Id { get; set; }
   }


    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
    {

        private readonly IApplicationDbContext _context;

        public DeleteGenreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            bool isValidGuid = Guid.TryParse(request.Id, out _);
            if (!isValidGuid)
            {
                throw new InvalidGuidException(nameof(Genre), request.Id);
            }         

            var entity = await _context.Genre.FindAsync(Guid.Parse(request.Id));            
            if (entity == null)
            {
                throw new NotFoundException(nameof(Genre), request.Id);
            }

            var hasAlbums = _context.Album.TagWith("GenreHasAlbumQuery").Any(al => al.GenreId == Guid.Parse(request.Id));
            if (hasAlbums)
            {     
                // TODO: we need to put better logic when deleting
                throw new DeleteFailureException("There are existing albums associated with this genre.", nameof(Genre), request.Id);
            }

            _context.Genre.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;


        }
    }
}
