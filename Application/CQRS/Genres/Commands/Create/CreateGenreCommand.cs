using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Genres.Commands.Create
{
    public class CreateGenreCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}

namespace Application.CQRS.Genres.Commands.Create
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, string>
    {
        private readonly IApplicationDbContext context;

        public CreateGenreCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var entity = new Genre
            {
                Id = Guid.NewGuid(),
                GenreName = request.Name,
                Description = request.Description
            };
            context.Genre.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id.ToString();
        }
    }
}
