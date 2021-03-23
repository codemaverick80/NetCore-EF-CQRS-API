namespace Application.CQRS.Genres.Commands.Patch
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
    public class PatchGenre : IRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class PatchGenreCommandHandler : IRequestHandler<PatchGenre>
    {
        private readonly IApplicationDbContext _context;

        public PatchGenreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(PatchGenre request, CancellationToken cancellationToken)
        {
            ////Guid id = Guid.Empty;
            ////bool isValidGuid = Guid.TryParse(request.Id, out id);
            ////if (!isValidGuid)
            ////{
            ////    throw new InvalidGuidException(nameof(Genre), request.Id);
            ////}

            var entity = await _context.Genre.FindAsync(Guid.Parse(request.Id));
            if (entity == null)
            {
                throw new NotFoundException(nameof(Genre), request.Id);
            }

            entity.Id = Guid.Parse(request.Id);
            entity.GenreName = request.Name;
            entity.Description = request.Description;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
