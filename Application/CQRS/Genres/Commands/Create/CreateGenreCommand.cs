namespace Application.CQRS.Genres.Commands.Create
{
    using MediatR;
    public class CreateGenreCommand : IRequest<string>
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
