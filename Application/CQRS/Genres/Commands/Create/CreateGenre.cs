namespace Application.CQRS.Genres.Commands.Create
{
    using MediatR;
    public class CreateGenre : IRequest<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}

namespace Application.CQRS.Genres.Commands.Create
{
    using Application.Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenre, string>
    {
        private readonly IApplicationDbContext context;

        public CreateGenreCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Handle(CreateGenre request, CancellationToken cancellationToken)
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
