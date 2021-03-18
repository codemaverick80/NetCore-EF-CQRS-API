namespace Application.CQRS.SeedDatabase.Commands
{
    using Application.Common.Interfaces;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    public class SeedTestDataCommand : IRequest
    {

    }

    public class SeedTestDataCommandHandler : IRequestHandler<SeedTestDataCommand>
    {

        private readonly IApplicationDbContext _context;

        public SeedTestDataCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SeedTestDataCommand request, CancellationToken cancellationToken)
        {
            var seeder = new TestDataSeeder(_context);
            await seeder.SeedAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
